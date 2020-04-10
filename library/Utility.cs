using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Library
{
  public class Utility
  {
    public static String GetAppRoot()
    {
      var appRoot = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.LastIndexOf("/bin"));
      return appRoot.Substring(0, appRoot.LastIndexOf("/") + 1);
    }

    public static String GetCurrentUserRoot() {
      var userRoot = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
      return userRoot;
    }

    public static List<String> GetAllFiles(string sDir)
    {
      List<String> files = new List<String>();
      try
      {
        foreach (string f in Directory.GetFiles(sDir))
        {
          files.Add(f);
        }
        foreach (string d in Directory.GetDirectories(sDir))
        {
          files.AddRange(GetAllFiles(d));
        }
      }
      catch (System.Exception ex)
      {
        Debug.WriteLine(ex.Message);
      }
      return files;
    }
  }
}
