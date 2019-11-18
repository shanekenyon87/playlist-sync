using System;

namespace Library
{
    public class MusicFile
    {
      public string SourcePath;
      public string DestinationPath;
      public string PlayList;
      public string Filename;
      public string Trackname;
      public string Type;
      public int Playtime;
      public string SubFolder;

      public string DosFilename {
        get {
          string ret = Filename;
          if (ret.StartsWith("/")) ret = ret.Substring(1);
          if (!String.IsNullOrEmpty(SubFolder)) {
            ret = SubFolder + "\\" + ret;
          }
          return ret.Replace("/","\\");
        }
      }
    }
}