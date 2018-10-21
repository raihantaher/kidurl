using KidUrl.DataAccess.Interface;
using KidUrl.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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
            return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
        }
    }
}
