using System;
using Xunit;
using Library;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
namespace tests
{
    public class CollectionTests
    {
        [Fact]
        public void CollectionInitializationTest()
        {
          String sourcePath = Utility.GetAppRoot() + "source";
          Collection c = new Collection(sourcePath, "");
          c.GetPlayLists();
          Assert.NotEqual(0, c.PlayLists.Count);
          foreach (PlayList p in c.PlayLists) {
            Debug.WriteLine(p.PlaylistFilename + " " + p.MusicFiles.Count);
          }
        }
    }
}
