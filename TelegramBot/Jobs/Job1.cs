using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Jobs
{
    public static class Job1
    {
        public static void Test()
        {
            Console.WriteLine($"[{DateTime.UtcNow}]: test!");
        }
    }
}
