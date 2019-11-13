using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
  public class PlayList
  {
    public PlayList(string SourcePath, string DestinationPath)
    {
      this.SourcePath = SourcePath;
      this.DestinationPath = DestinationPath;
    }
    public static string HEADER = "#EXTM3U";
    public static string EXTINF = "#EXTINF:";
    private String SourcePath;
    private String DestinationPath;
    public List<MusicFile> MusicFiles = new List<MusicFile>();
    public void Read(String Filename)
    {
      Filename = SourcePath + "/" + Filename;
      if (!CanDo(Filename)) return;
      StreamReader sr = File.OpenText(Filename);
      String s = "";
      MusicFile m = new MusicFile();
      while ((s = sr.ReadLine()) != null)
      {
        if (s.StartsWith(EXTINF))
        {
          Console.WriteLine(s);
          m = new MusicFile();
          m.Trackname = s.Split(',')[1];
          m.Playtime = Int16.Parse((s.Split(',')[0].Replace(EXTINF, "")));
        }
        else if (s.StartsWith("/"))
        {
          m.Filename = s;
          m.Type = s.Split('.')[1];
          MusicFiles.Add(m);
        }
      }
    }

    public void Write(String Filename)
    {
      Filename = DestinationPath + "/" + Filename;
      if (File.Exists(Filename)) {
        File.Delete(Filename);
      }
      using (StreamWriter sr = new StreamWriter(Filename)) {
        sr.WriteLine(HEADER);
        foreach (MusicFile m in MusicFiles)
        {
          sr.WriteLine(EXTINF +
            m.Playtime +
            "," + m.Trackname);
          sr.WriteLine(m.Filename);
        }
      }
    }

    public void CopyTracks(string RelativePath) {
      string Temp = SourcePath;
      SourcePath = RelativePath;
      CopyTracks();
      SourcePath = Temp;
    }
    public void CopyTracks() {
      if (MusicFiles.Count != 0) {
        foreach (MusicFile m in MusicFiles)
        {
          Directory.CreateDirectory(Path.GetDirectoryName(DestinationPath + "/" + m.Filename));
          File.Copy(
            SourcePath + "/" + m.Filename,
            DestinationPath + "/" + m.Filename
          );
        }
      }
    }

    public Boolean CanDo(string Filename)
    {
      if (File.Exists(Filename))
      {
        if (Filename.EndsWith("m3u")) return true;
        Console.WriteLine("Only M3U playlists are supported.");
        return false;
      }
      Console.WriteLine("Cannot finde file = " + Filename);
      return false;
    }
  }
}