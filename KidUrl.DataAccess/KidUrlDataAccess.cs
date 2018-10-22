using KidUrl.DataAccess.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KidUrl.DataAccess
{
    public class KidUrlDataAccess : IKidUrlDataAccess
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn;

        public KidUrlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("MyConn"));
        }

        public string GetLongUrl(string shortUrl)
        {
            string result = "";
            try
            {
                conn.Open();
                var sql = $"Select LongUrl from Url WHERE ShortCode='{shortUrl}'";
                var cmd = new SqlCommand(sql, conn);
                result = (string)cmd.ExecuteScalar();
            }
            catch(Exception ex)
            {
                throw new Exception("Exception while retrieving long url! Message: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
            return result;
        }

        public string GetShortUrl(string longUrl)
        {
            string result = "";
            try
            {
                conn.Open();
                var sql = $"Select ShortCode from Url WHERE LongUrl='{longUrl}'";
                var cmd = new SqlCommand(sql, conn);
                result = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while retrieving short url! Message: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            if (result == null)
            {
                var shortGuid = Guid.NewGuid();
                if(SaveLongUrl(longUrl, shortGuid))
                {
                    result = shortGuid.ToString();
                }
            }

            return result;
        }

        private bool SaveLongUrl(string longUrl, Guid shortUrlGuid)
        {
            var result = 0;
            try {
                string sql = "INSERT INTO Url (LongUrl, ShortCode, CreatedDate) VALUES(@param1,@param2,@param3)";
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@param1", longUrl);
                cmd.Parameters.AddWithValue("@param2", shortUrlGuid);
                cmd.Parameters.AddWithValue("@param3", DateTime.Now);
                cmd.CommandType = CommandType.Text;
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while saving long url! Message: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result > 0 ? true : false;
        }
    }
}
