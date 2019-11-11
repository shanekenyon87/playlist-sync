using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    public class Utility
    {
      public static String GetAppRoot() {
            var appRoot = AppContext.BaseDirectory.Substring(0,AppContext.BaseDirectory.LastIndexOf("/bin"));
            return appRoot.Substring(0,appRoot.LastIndexOf("/")+1);
        }
    }
}
