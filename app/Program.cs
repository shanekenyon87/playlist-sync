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
            String userRoot = Utility.GetCurrentUserRoot();
            String iTunesRelativePath = "Music/iTunes/iTunes Media/Music";
            String iTunesPath = userRoot + "/" + iTunesRelativePath;

            Collection c = new Collection(sourcePath, destPath, iTunesPath);
            c.GetPlayLists();
            c.WritePlayLists();
        }
    }
}
