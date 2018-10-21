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
            if (IsUrlValid(url))
            {
                if (IsUrlLongUrl(url))
                {
                    var shortUrlCode = _kidUrlDataAccess.GetShortUrl(url);
                    return @"kidurl.my/" + shortUrlCode;
                }
                else
                {
                    return _kidUrlDataAccess.GetLongUrl(url);
                }
            }

            return "Invalid URL provided!";
        }

        private bool IsUrlLongUrl(string url)
        {
            if (url.Contains("kidurl.my/"))
            {
                return false; // Need to improve it
            }
            return true;
        }

        private bool IsUrlValid(string url)
        {
            //return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
            var pattern = @"^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var result = reg.IsMatch(url);
            return result;
        }
    }
}
