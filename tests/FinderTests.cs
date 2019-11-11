using System;
using Xunit;
using Library;
using System.Collections.Generic;


namespace tests
{
    public class FinderTests
    {
        [Fact]
        public void GetFilesTest()
        {
            string path = @"/Users/shanekenyon/Music/iTunes/";
            string[] files =
                new Finder(path, "foo").GetFiles();
            Console.Write("Files = " + files.Length);
            Assert.NotEmpty(files);
        }

        [Fact]
        public void GetMusicFilesTest()
        {
            string path = @"/Users/shanekenyon/Music/iTunes/";
            List<MusicFile> files =
                new Finder(path, "mp3").GetMusicFiles();
            Console.Write("Files = " + files.Count);
            Assert.NotEmpty(files);
        }

        [Fact]
        public void GetMusicFilePropertiesTest()
        {
            string path = @"/Users/shanekenyon/Music/iTunes/";
            List<MusicFile> files =
                new Finder(path, "mp3").GetMusicFiles();
            var f = files[0];
            Assert.Contains(path, f.SourcePath);
            Assert.Equal("mp3", f.Type);
            Assert.NotNull(f.Filename);
        }
    }
}
