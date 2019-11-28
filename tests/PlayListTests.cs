using System;
using Xunit;
using Library;
using System.Collections.Generic;
using System.IO;
namespace tests
{
    public class PlayListTests
    {
        [Fact]
        public void ReadPlayListTest()
        {
          String sourcePath = Utility.GetAppRoot() + "source";
          PlayList pl = new PlayList(sourcePath, "");
          pl.Read("Adirondac.m3u");
          Assert.NotEmpty(pl.MusicFiles);
          MusicFile m = pl.MusicFiles[0];
          Assert.Equal("Ends Of The Earth - Lord Huron", m.Trackname);
          Assert.Equal("/Lord Huron/Lonesome Dreams/01 Ends Of The Earth.m4a", m.Filename);
          Assert.Equal("m4a", m.Type);
        }

        [Fact]
        public void WritePlayListTest()
        {
          String sourcePath = Utility.GetAppRoot() + "source";
          String destPath = Utility.GetAppRoot() + "output";
          PlayList pl = new PlayList(sourcePath, destPath);
          pl.Read("Adirondac.m3u");
          pl.Write("Adirondac.m3u");
          Assert.True(File.Exists(destPath + "/Adirondac.m3u"));
        }

        [Fact]
        public void CopyTracksTest()
        {
          String sourcePath = Utility.GetAppRoot() + "source";
          String destPath = Utility.GetAppRoot() + "output";
          destPath = "/Volumes/Music";
          PlayList pl = new PlayList(sourcePath, destPath);
          pl.PurgeTracks();
          pl.Read("Adirondac.m3u");
          pl.Write("Adirondac.m3u");
          pl.CopyTracks("/Users/shanekenyon/Music/iTunes/iTunes Media/Music");
          Assert.True(File.Exists(destPath + "/Adirondac.m3u"));
        }
    }
}
