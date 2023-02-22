using DATN.Web.Service.Constants;
using DATN.Web.Service.Contexts;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Middlewares
{
    /// <summary>
    /// Middleware thiết lập thông tin đầy đủ cho một request
    /// </summary>
    public class AuthenContextHandleMiddleware
    {
        private readonly ILogger _log;
        private readonly RequestDelegate _next;
        public AuthenContextHandleMiddleware(
            ILogger<AuthenContextHandleMiddleware> log,
            RequestDelegate next)
        {
            _log = log;
            _next = next;
        }

        public async Task Invoke(HttpContext context, IContextService contextService)
        {
            var logProperties = new Dictionary<string, string>();
            try
            {
                if (NoRequestAuthentication(context.Request.Path))
                {
                    await _next(context);
                    return;
                }
                var (isAuthenticated, authenProperties) = await this.ProcessAuthenToken(context, contextService);
                await HandleContext(context, isAuthenticated);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                await HandleContext(context, false);
            }
        }

        /// <summary>
        /// Xử lý Context
        /// </summary>
        private async Task HandleContext(HttpContext context, bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                await _next(context);
            }
            else
            {
                // Đăng nhập thất bại
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                var result = new DAResult(401, "Không thể truy cập", "", null);
                var jsonResult = JsonConvert.SerializeObject(result);
                byte[] content = Encoding.UTF8.GetBytes(jsonResult);
                await context.Response.Body.WriteAsync(content, 0, content.Length);
            }
        }

        /// <summary>
        /// Đọc thông tin authen token của request
        /// </summary>
        private async Task<(bool, Dictionary<string, string>)> ProcessAuthenToken(HttpContext context, IContextService contextService)
        {
            var isAuthenticated = false;
            var authenProperties = new Dictionary<string, string>();
            if (context.Request == null || context.Request.Headers == null || !context.Request.Headers.ContainsKey(HeaderKeys.Authorization))
            {
                isAuthenticated = false;
            }
            else
            {
                string authHeader = context.Request.Headers[HeaderKeys.Authorization];
                if (!string.IsNullOrEmpty(authHeader))
                {
                    //string token = authHeader.Split(new char[] { ' ' })[1];
                    string token = authHeader;
                    var handled = new JwtSecurityTokenHandler();
                    var jsonToken = handled.ReadJwtToken(token);
                    var payload = jsonToken.Payload;

                    var contextData = new ContextData();
                    contextData.UserId = Guid.Parse(payload[TokenKeys.UserId].ToString());
                    contextData.CartId = Guid.Parse(payload[TokenKeys.CartId].ToString());
                    contextData.Email = payload[TokenKeys.Email].ToString();
                    contextData.FirstName = payload[TokenKeys.FirstName].ToString();
                    contextData.LastName = payload[TokenKeys.LastName].ToString();
                    contextData.TokenExpired = DateTime.Parse(payload[TokenKeys.TokenExpired].ToString());
                    
                    contextService.Set(contextData);
                    authenProperties.Add(nameof(ContextData.Email), $"{contextData.Email}");
                    isAuthenticated = true;
                    if (contextData.TokenExpired < DateTime.Now)
                    {
                        isAuthenticated = false;
                    }
                }
            }
            return (isAuthenticated, authenProperties);
        }

        /// <summary>
        /// Các đầu Api không yêu cầu login
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool NoRequestAuthentication(string path)
        {
            var lstPath = new List<string> { 
                "/api/Products/info", 
                "/api/Users/login", 
                "/api/Users/signup",
                "/api/Products/homepage",
                "/api/Categorys/getCategory",
                "/api/Products/relation",
                "/api/Products/rateOption",
                "/api/Products/commentProduct",
                "/Upload/",
                "/listProductCompare/"
            };
            if (lstPath.Any(x => path.Contains(x)))
            {
                return true;
            }
            return false;
        }
    }
    

    public static class SetAuthenContextHandlerExtensions
    {
        public static IApplicationBuilder UseSetAuthContextHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenContextHandleMiddleware>();
        }
    }
}
