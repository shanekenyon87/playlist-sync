using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Library
{
  public class PlayList
  {
    public PlayList(string SourcePath, string DestinationPath)
    {
      this.SourcePath = SourcePath;
      this.DestinationPath = DestinationPath;
    }
    private static string HEADER = "#EXTM3U";
    private static string EXTINF = "#EXTINF:";
    private String SourcePath;
    private String DestinationPath;
    private String PlaylistFilename;
    private string Name
    {
      get
      {
        return PlaylistFilename.Replace(".m3u", "");
      }
    }
    public List<MusicFile> MusicFiles = new List<MusicFile>();
    public void Read(String Filename)
    {
      PlaylistFilename = Filename;
      Filename = SourcePath + "/" + Filename;
      if (!CanDo(Filename)) return;
      StreamReader sr = File.OpenText(Filename);
      String s = "";
      MusicFile m = new MusicFile();
      while ((s = sr.ReadLine()) != null)
      {
        if (s.StartsWith(EXTINF))
        {
          Debug.WriteLine(s);
          m = new MusicFile();
          m.Trackname = s.Split(',')[1];
          m.Playtime = Int16.Parse((s.Split(',')[0].Replace(EXTINF, "")));
        }
        else if (s.StartsWith("/"))
        {
          m.Filename = s;
          m.SubFolder = Name;
          m.Type = s.Split('.')[1];
          MusicFiles.Add(m);
        }
      }
    }

    public void Write(String Filename)
    {
      // TODO: This must have an associated directory and the filenames must include that,
      // each playlist will have its own collection of songs
      Filename = DestinationPath + "/" + Filename;
      if (File.Exists(Filename))
      {
        File.Delete(Filename);
      }
      using (StreamWriter sr = new StreamWriter(Filename))
      {
        sr.WriteLine(HEADER);
        foreach (MusicFile m in MusicFiles)
        {
          sr.WriteLine(EXTINF +
            m.Playtime +
            "," + m.Trackname);
          sr.WriteLine(m.DosFilename);
        }
      }
    }

    public void PurgeTracks()
    {
      List<String> files = GetAllFiles(DestinationPath);
      foreach (string f in files)
      {
        if (f.Replace(DestinationPath, "").StartsWith("/.")) continue;
        File.Delete(f);
      }
    }

    private List<String> GetAllFiles(string sDir)
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

    public void CopyTracks(string RelativePath)
    {
      string Temp = SourcePath;
      SourcePath = RelativePath;
      CopyTracks();
      SourcePath = Temp;
    }
    public void CopyTracks()
    {
      // TODO: This must put the files in a specific Playlist directory
      // TODO: This must purge the existing files so no garbo stays around
      if (MusicFiles.Count != 0)
      {
        foreach (MusicFile m in MusicFiles)
        {
          Directory.CreateDirectory(Path.GetDirectoryName(DestinationPath + "/" + Name + "/" + m.Filename));
          File.Copy(
            SourcePath + "/" + m.Filename,
            DestinationPath + "/" + Name + "/" + m.Filename
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