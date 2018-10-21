using KidUrl.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace KidUrl.DataAccess
{
    public class KidUrlDataAcess : IKidUrlDataAccess
    {
        private string con = @"Data Source=.\SQLEXPRESS; Database=KidUrl; Integrated Security = True;";
        private SqlConnection conn;

        public KidUrlDataAcess()
        {
            conn = new SqlConnection(con);
        }

        public string GetLongUrl(string shortUrl)
        {
            string result = "";
            try
            {
                conn.Open();
                var st = $"Select LongUrl from Url WHERE ShortCode='{shortUrl}'";
                var cmd = new SqlCommand(st, conn);
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
                var st = $"Select ShortCode from Url WHERE LongUrl='{longUrl}'";
                var cmd = new SqlCommand(st, conn);
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
