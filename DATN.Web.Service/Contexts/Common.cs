using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DATN.Web.Service.Contexts
{
    public static class Common
    {
        public static string GetUrlImage(string host, string image)
        {
            if (!string.IsNullOrEmpty(image))
            {
                return "https://" + host + "/Upload/" + image;
            } else
            {

                return image;
            }
        }

        public static string SaveImage(string host, string urlImage)
        {
            if (!string.IsNullOrEmpty(urlImage))
            {
                return urlImage.Split(host + "/Upload/")[1]; ;
            }
            else
            {
                return null;
            }
        }
    }
}
