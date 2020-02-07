using System;
using Library;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting playlist copy");
            String sourcePath = Utility.GetAppRoot() + "source";
            String destPath = Utility.GetAppRoot() + "output";
            //destPath = "/Volumes/Music";
            PlayList pl = new PlayList(sourcePath, destPath, "/Users/shanekenyon/Music/iTunes/iTunes Media/Music");
            pl.Read("Just Like Heaven.m3u");
            pl.PurgeTracks();
            pl.Write("Just Like Heaven.m3u");
            pl.CopyTracks();
        }
    }
}
