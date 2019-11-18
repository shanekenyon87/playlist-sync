using System;
using Xunit;
using Library;
using System.Collections.Generic;
using System.IO;


namespace tests
{
    public class MusicFileTests
    {
        [Fact]
        public void DosFilenameTest()
        {
          MusicFile m = new MusicFile();
          m.Filename = "/foo/bar.mp3";
          Assert.Equal("foo\\bar.mp3", m.DosFilename);
          m.SubFolder = "kilroy";
          Assert.Equal("kilroy\\foo\\bar.mp3", m.DosFilename);
        }
    }
}