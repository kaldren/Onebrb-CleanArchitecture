using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Client.Settings
{
    public class AppSettings
    {
        public MainSettings MainSettings { get; set; }
    }

    public class MainSettings
    {
        public string AppName { get; set; }
        public string WelcomeSlogan { get; set; }
        public string BaseUrl { get; set; }
    }
}
