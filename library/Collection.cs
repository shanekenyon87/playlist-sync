using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Library
{
  public class Collection
  {
    public Collection(string SourcePath, string DestinationPath, string MusicSourcePath)
    {
      this.SourcePath = SourcePath;
      this.DestinationPath = DestinationPath;
      this.MusicSourcePath = MusicSourcePath;
    }
    private String SourcePath;
    private String DestinationPath;
    private String MusicSourcePath;
    public List<PlayList> PlayLists = new List<PlayList>();
    public void GetPlayLists() {
      List<String> files = Utility.GetAllFiles(SourcePath);
      foreach (String f in files)
      {
        if (f.EndsWith("m3u"))
        {
          PlayList p = new PlayList(SourcePath, DestinationPath, MusicSourcePath);
          p.Read(f.Substring(f.LastIndexOf("/") + 1));
          PlayLists.Add(p);
        }
      }
    }
    public void WritePlayLists() {
      foreach (PlayList pl in PlayLists)
      {
        pl.PurgeTracks();
        pl.Write(pl.PlaylistFilename);
        pl.CopyTracks();
      }
    }
  }
}