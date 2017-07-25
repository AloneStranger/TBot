using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary
{
    public static class DBWork
    {
        public static bool Register(int UId)
        {
            return true;
        }

        public static bool CheckRight(int UId, string right)
        {
            return true;
        }

        public static bool SetTimer(int UId, int Duration, string Description)
        {
            return true;
        }

        public static string GetTimers(int UId)
        {
            return "<b>Таймеры:</b>";
        }
    }

}