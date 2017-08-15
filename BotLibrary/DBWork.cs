using System;
using System.Data.SqlClient;

namespace BotLibrary
{
    public static class DBWork
    {
        private static SqlConnection _cn = new SqlConnection(String.Format(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {0}TBot.mdf; Integrated Security = True; Connect Timeout = 30", AppDomain.CurrentDomain.BaseDirectory));

        private static SqlConnection Con{ get { try { _cn.Open(); } catch { } return _cn; } }

        private static Int32 GetInt(object data) { return Int32.Parse(data.ToString()); }


        public static string Register(int UId, string UName)
        {
            try
            {
                using (var cmd = Con.CreateCommand())
                {
                    cmd.CommandText = String.Format("SELECT COUNT(1) FROM dbo.Users WHERE UId = {0}", UId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (GetInt(reader.GetValue(0)) > 0)
                            return "Уже зарегистрирован";

                        cmd.CommandText = String.Format("INSERT INTO dbo.Users(UId,UName) VALUES ({0}, '{1}')", UId, UName);
                        cmd.ExecuteNonQuery();
                        return "";
                    }
                }
            }
            catch (Exception e) { return e.Message; }
        }

        public static bool CheckRight(int UId, string right)
        {
            using (var cmd = Con.CreateCommand())
            {
                cmd.CommandText = String.Format("SELECT COUNT(1) FROM dbo.Rights WHERE UId = {0} AND URight = '{1}'", UId, right.ToLower());
                try
                {
                    using (var reader = cmd.ExecuteReader())
                        return GetInt(reader.GetValue(0)) > 0;
                }
                catch { }
            }
            return false;
        }

        public static bool SetTimer(int UId, double Duration, string Description)
        {
            return true;
        }

        public static string GetTimers(int UId)
        {
            return "<b>Таймеры:</b>";
        }
    }

}