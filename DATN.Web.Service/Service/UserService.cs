﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DATN.Web.Service.Constants;
using DATN.Web.Service.Contexts;
using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Exceptions;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DATN.Web.Service.Service
{
    public class UserService : BaseService, IUserService
    {
        IUserRepo _userRepo;

        public UserService(IUserRepo userRepo) : base(userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Dictionary<string, object>> Login(LoginModel model)
        {
            var user = await _userRepo.Login(model);
            var context = new ContextData();
            if (user != null)
            {
                context.UserId = user.user_id;
                context.Email = user.email;
                context.FirstName = user.first_name;
                context.LastName = user.last_name;
                context.Avatar = user.avatar;
            }

            var jwtTokenConfig =
                JsonConvert.DeserializeObject<JwtTokenConfig>(_userRepo.GetConfiguration()
                    .GetConnectionString("JwtTokenConfig"));

            var data = await this.GetContextReturn(context, jwtTokenConfig);
            return data;
        }

        /// <summary>
        /// Lấy thông tin trả về client
        /// </summary>
        /// <param name="user"></param>
        /// <param name="expiredSeconds">Thời gian hết hạn</param>
        private async Task<Dictionary<string, object>> GetContextReturn(ContextData context,
            JwtTokenConfig jwtTokenConfig)
        {
            var token = this.CreateAuthenToken(context, jwtTokenConfig);
            var result = new Dictionary<string, object>
            {
                { "Token", token },
                { "TokenTimeout", jwtTokenConfig.ExpiredSeconds },
                {
                    "Context", new
                    {
                        UserId = context.UserId,
                        Email = context.Email,
                        FirstName = context.FirstName,
                        LastName = context.LastName,
                        Avatar = context.Avatar
                    }
                }
            };
            return result;
        }

        /// <summary>
        /// Tạo token Authen
        /// </summary>
        private string CreateAuthenToken(ContextData context, JwtTokenConfig jwtTokenConfig)
        {
            var claimIdentity = new ClaimsIdentity();
            claimIdentity.AddClaim(new Claim(TokenKeys.UserId, context.UserId.ToString()));
            claimIdentity.AddClaim(new Claim(TokenKeys.Email, context.Email));
            claimIdentity.AddClaim(new Claim(TokenKeys.FirstName, context.FirstName));
            claimIdentity.AddClaim(new Claim(TokenKeys.LastName, context.LastName));

            var expire = DateTime.Now.AddSeconds(jwtTokenConfig.ExpiredSeconds);
            var key = Encoding.ASCII.GetBytes(jwtTokenConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expire,
                Subject = claimIdentity,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken smeToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(smeToken);
            return token;
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        public async Task<UserEntity> ResetPassword(ResetPassword resetPassword)
        {
            if (resetPassword.confirm_password != resetPassword.new_password)
            {
                throw new ValidateException("Confirm Password doesn't match", "");
            }

            string field;
            object value;

            if (resetPassword.email != null)
            {
                field = "email";
                value = resetPassword.email;
            }
            else if (resetPassword.phone != null)
            {
                field = "phone";
                value = resetPassword.phone;
            }
            else
            {
                field = "user_id";
                value = resetPassword.user_id;
            }

            var existedUser = await _userRepo.GetAsync<UserEntity>(field, value);
            var res = existedUser.FirstOrDefault();

            if (res == null)
            {
                throw new ValidateException("User doesn't exist", "");
            }

            var encodedData = BCrypt.Net.BCrypt.HashPassword(resetPassword.new_password);
            res.password = encodedData;

            await _userRepo.UpdateAsync<UserEntity>(res, "password");

            return res;
        }

        public async Task<DAResult> Signup(SignupModel model)
        {
            var newUser = new UserEntity();
            newUser.user_id = Guid.NewGuid();
            newUser.email = model.email;
            newUser.phone = model.phone;
            newUser.password = model.password;
            newUser.first_name = model.first_name;
            newUser.last_name = model.last_name;
            newUser.avatar = model.avatar;
            newUser.is_block = false;
            newUser.role = Role.Customer;
            // Check tồn tại Email
            var existUserEmail = (await _userRepo.GetAsync<UserEntity>("email", newUser.email))?.FirstOrDefault();
            if (existUserEmail != null)
            {
                return new DAResult(int.Parse(ResultCode.ExistEmail), Resources.msgExistEmail, "", newUser);
            }
            // Check tồn tại Số điện thoại
            var existUserPhone = (await _userRepo.GetAsync<UserEntity>("phone", newUser.phone))?.FirstOrDefault();
            if (existUserPhone != null)
            {
                return new DAResult(int.Parse(ResultCode.ExistPhone), Resources.msgExistPhone, "", newUser);
            }
            var user = await _userRepo.InsertAsync<UserEntity>(newUser);

            var newAddress = new AddressEntity();
            newAddress.address_id = Guid.NewGuid();
            newAddress.user_id = user.user_id;
            newAddress.district_code = model.district_code;
            newAddress.district = model.district;
            newAddress.province_code = model.province_code;
            newAddress.province = model.province;
            newAddress.commune_code = model.commune_code;
            newAddress.commune = model.commune;
            newAddress.address_detail = model.address_detail;
            newAddress.is_default = true;

            var address = await _userRepo.InsertAsync<AddressEntity>(newAddress);

            var context = new ContextData();
            if (user != null)
            {
                context.UserId = user.user_id;
                context.Email = user.email;
                context.FirstName = user.first_name;
                context.LastName = user.last_name;
                context.Avatar = user.avatar;
            }

            var jwtTokenConfig =
                JsonConvert.DeserializeObject<JwtTokenConfig>(_userRepo.GetConfiguration()
                    .GetConnectionString("JwtTokenConfig"));

            var data = await this.GetContextReturn(context, jwtTokenConfig);
            return new DAResult(200, Resources.signupSuccess, "", data);
        }
    }
}