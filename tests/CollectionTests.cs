using System;
using Xunit;
using Xunit.Abstractions;
using Library;
using System.Collections.Generic;
namespace tests
{
    public class CollectionTests
    {
        private readonly ITestOutputHelper output;

        public CollectionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CollectionInitializationTest()
        {
          String sourcePath = Utility.GetAppRoot() + "source";
          String destPath = Utility.GetAppRoot() + "output";
          Collection c = new Collection(sourcePath, destPath, "/Users/shanekenyon/Music/iTunes/iTunes Media/Music");
          c.GetPlayLists();
          c.WritePlayLists();
          Assert.NotEqual(0, c.PlayLists.Count);
          foreach (PlayList p in c.PlayLists) {
            output.WriteLine(p.PlaylistFilename + " " + p.MusicFiles.Count);
          }
        }
    }
}
