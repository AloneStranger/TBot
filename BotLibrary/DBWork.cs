using System;
using System.Data.SqlClient;

namespace BotLibrary
{
    public static class DBWork
    {
        private static SqlConnection _cn = new SqlConnection(String.Format(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {0}TBot.mdf; Integrated Security = True; Connect Timeout = 30", AppDomain.CurrentDomain.BaseDirectory));

        private static SqlConnection con{ get { try { _cn.Open(); } catch { } return _cn; } }

        public static string Register(int UId)
        {
            try
            {
                using (var cmd = new SqlCommand("", con))
                {
                    cmd.CommandText = @"Insert into ";
                    return "";
                }
            }
            catch (Exception e) { return e.Message; }
        }

        public static bool CheckRight(int UId, string right)
        {
            return true;
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