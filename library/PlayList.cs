using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
  public class PlayList
  {
    public PlayList(string SourcePath, string DestinationPath, string MusicSourcePath)
    {
      this.SourcePath = SourcePath;
      this.DestinationPath = DestinationPath;
      this.MusicSourcePath = MusicSourcePath;
    }
    private static string HEADER = "#EXTM3U";
    private static string EXTINF = "#EXTINF:";
    private String SourcePath;
    private String DestinationPath;
    private String MusicSourcePath;
    public String PlaylistFilename;
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
      int added = 0;
      int skipped = 0;
      PlaylistFilename = Filename;
      Filename = SourcePath + "/" + Filename;
      Console.WriteLine("Reading source playlist file = " + Filename);
      if (!CanDo(Filename)) return;
      StreamReader sr = File.OpenText(Filename);
      String s = "";
      MusicFile m = new MusicFile();
      while ((s = sr.ReadLine()) != null)
      {
        if (s.StartsWith(EXTINF))
        {
          m = new MusicFile();
          m.Trackname = s.Split(',')[1];
          m.Playtime = Int16.Parse((s.Split(',')[0].Replace(EXTINF, "")));
        }
        else if (s.StartsWith("/"))
        {
          
          if (!File.Exists(MusicSourcePath + CleanFilename(s)))
          {
            skipped++;
            continue;
          }
          m.Filename = CleanFilename(s);
          m.SubFolder = Name;
          m.Type = s.Split('.')[1];
          MusicFiles.Add(m);
          added++;
        }
      }
      Console.WriteLine("Files added = " + added + " skipped =  " + skipped);
    }

    public void Write(String Filename)
    {
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
      if (DestinationPath == "" || DestinationPath == "/") return;
      if (Directory.Exists(DestinationPath + "/" + Name))
      {
        Directory.Delete(DestinationPath + "/" + Name, true);
      }
    }
    public void CopyTracks()
    {
      // TODO: This must put the files in a specific Playlist directory
      // TODO: This must purge the existing files so no garbo stays around
      int copied = 0;
      int failed = 0;
      if (Directory.Exists(MusicSourcePath))
      {
        String sourceFile;
        
        Console.WriteLine("Starting copy of " + MusicFiles.Count + " files for playlist " + PlaylistFilename);
        foreach (MusicFile m in MusicFiles)
        {
          sourceFile = MusicSourcePath + "/" + CleanFilename(m.Filename);
          
          if (File.Exists(sourceFile)) {
            Directory.CreateDirectory(
              Path.GetDirectoryName(DestinationPath + "/" + Name + "/" + CleanFilename(m.Filename)
              ));
            File.Copy(
              sourceFile,
              DestinationPath + "/" + Name + "/" + CleanFilename(m.Filename)
            );
            copied++;
          } else {
            failed++;
          }
        }
      } else {
        throw new Exception("Cannot find directory " + MusicSourcePath);
      }
      Console.WriteLine("Files copied = " + copied + " failed = " + failed);
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

    private String CleanFilename(String filename) {
      return filename.Replace(MusicSourcePath, "");
    }
  }
}