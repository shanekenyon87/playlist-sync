using System;
using Xunit;
using Library;
using System.Collections.Generic;


namespace tests
{
    public class PlayListTests
    {
        [Fact]
        public void ReadPlayListTest()
        {
          String sourcePath = Utility.GetAppRoot() + "/source";
          PlayList pl = new PlayList();
          pl.Read(sourcePath + "/Adirondac.m3u");
          Assert.NotEmpty(pl.MusicFiles);
          MusicFile m = pl.MusicFiles[0];
          Assert.Equal("Ends Of The Earth - Lord Huron", m.Trackname);
          Assert.Equal("/Lord Huron/Lonesome Dreams/01 Ends Of The Earth.m4a", m.Filename);
          Assert.Equal("m4a", m.Type);
        }
    }
}
