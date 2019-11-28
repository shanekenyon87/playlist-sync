using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Library
{
  public class Collection
  {
    public Collection(string SourcePath, string DestinationPath)
    {
      this.SourcePath = SourcePath;
      this.DestinationPath = DestinationPath;
    }
    private String SourcePath;
    private String DestinationPath;
    public List<PlayList> PlayLists = new List<PlayList>();
    public void GetPlayLists() {
      List<String> files = Utility.GetAllFiles(SourcePath);
      foreach (String f in files)
      {
        if (f.EndsWith("m3u"))
        {
          PlayList p = new PlayList(SourcePath, DestinationPath);
          p.Read(f);
          PlayLists.Add(p);
        }
      }
    }
  }
}