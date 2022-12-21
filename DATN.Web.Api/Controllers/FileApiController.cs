using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using DATN.Web.Service.Model;
using DATN.Web.Service.Properties;
using DATN.Web.Service.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DATN.Web.Api.Controllers
{
    /// <summary>
    /// Controller File
    /// </summary>
    [Route("api/[controller]")]
    public class FileApiController : ControllerBase
    {
        private IWebHostEnvironment _webHostEnvironment;
        public FileApiController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// upload file
        /// </summary>
        [HttpPost("Upload/{fileName}")]
        public async Task<IActionResult> Upload(string fileName)
        {
            string linkImage = "";
            try
            {
                var uploadFiles = Request.Form.Files;
                foreach (var source in uploadFiles)
                {

                    fileName = fileName + System.IO.Path.GetExtension(source.FileName) ?? "";
                    string filePath = GetFilePath(fileName);

                    if (Directory.Exists(filePath))
                    {
                        linkImage = GetImage(fileName);
                    }
                    else
                    {

                        using (var stream = new FileStream(Path.Combine(GetFilePath(source.FileName), filePath), FileMode.Create))
                        {
                            await source.CopyToAsync(stream);
                            linkImage = GetImage(fileName);
                        }

                    }
                }

                var dic = new Dictionary<string, string>();
                dic["data"] = fileName;
                dic["url"] = linkImage;

                var actionResult = new DAResult(200, Resources.getDataSuccess, "", dic);
                return Ok(actionResult);
            }
            catch (Exception exception)
            {
                var actionResult = new DAResult(500, Resources.error, exception.Message, "");
                return Ok(actionResult);
            }
        }

        private string GetFilePath(string fileName)
        {
            return this._webHostEnvironment.WebRootPath + "\\Upload\\" + fileName;
        }

        private string GetImage(string fileName)
        {
            string imgUrl = string.Empty;
            string hostUrl = Request.Host.ToString();
            string filePath = GetFilePath(fileName);

            if (System.IO.File.Exists(filePath))
            {
                imgUrl = "https://" + hostUrl + "/Upload/" + fileName;
            }
            return imgUrl;
        }

    }
}
