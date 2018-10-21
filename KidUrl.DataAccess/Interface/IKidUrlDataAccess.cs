using System;
using System.Collections.Generic;
using System.Text;

namespace KidUrl.DataAccess.Interface
{
    public interface IKidUrlDataAccess
    {
        string SaveLongUrl(string longUrl);
        string GetShortUrl(string longUrl);
        string GetLongUrl(string shortUrl);
    }
}
