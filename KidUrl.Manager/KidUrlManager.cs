using KidUrl.DataAccess.Interface;
using KidUrl.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KidUrl.Manager
{
    public class KidUrlManager : IKidUrlManager
    {
        private readonly IKidUrlDataAccess _kidUrlDataAccess;

        public KidUrlManager(IKidUrlDataAccess kidUrlDataAccess)
        {
            _kidUrlDataAccess = kidUrlDataAccess;
        }

        public string ConvertUrl(string url)
        {
            
            if (IsUrlLongUrl(url))
            {
                if (IsUrlValid(url))
                {
                    var shortUrlCode = _kidUrlDataAccess.GetShortUrl(url);
                    return @"kidurl.my/" + shortUrlCode;
                }
            }
            else
            {
                var shortCode = "";

                if (url.Substring(0, 10) == "kidurl.my/")
                    shortCode = url.Substring(10);
                else if (url.Substring(0, 17) == "http://kidurl.my/")
                    shortCode = url.Substring(17);
                else if (url.Substring(0, 18) == "https://kidurl.my/")
                    shortCode = url.Substring(18);

                if (IsShortUrlValid(shortCode))
                    return _kidUrlDataAccess.GetLongUrl(shortCode);
            }

            return "Invalid URL provided!";
        }

        private bool IsUrlLongUrl(string url)
        {
            if (url.Contains("kidurl.my/"))
            {
                if (DoesUrlContainsKidUrl(url))
                    return false;
            }
            return true;
        }

        private static bool DoesUrlContainsKidUrl(string url)
        {
            return url.Substring(0, 10) == "kidurl.my/" || url.Substring(0, 17) == "http://kidurl.my/" || url.Substring(0, 18) == "https://kidurl.my/";
        }

        private bool IsUrlValid(string url)
        {
            var pattern = @"^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var result = reg.IsMatch(url);
            return result;
        }

        private bool IsShortUrlValid(string url)
        {
            var pattern = @"^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var result = reg.IsMatch(url);
            return result;
        }
    }
}
