using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    public class Finder
    {
        private String Path;
        private String PlayList;
        public List<MusicFile> MusicFiles;
        public Finder(String path, String playlist) {
            this.Path = path;
            this.PlayList = playlist;
        }
        public List<MusicFile> GetMusicFiles() {
            string[] files = GetFiles();
            MusicFiles = new List<MusicFile>();
            foreach(string name in files) {
                FileInfo f = new FileInfo(name);
                MusicFile m = new MusicFile();
                m.SourcePath = f.DirectoryName;
                m.Name = f.Name;
                m.Type = "mp3";
                m.PlayList = PlayList;
                MusicFiles.Add(m);
            }
            return MusicFiles;
        }
        public string[] GetFiles() {
            string[] files = new string[1];
            if (!CanDo()) return files;
            files = Directory.GetFiles(Path, "*.mp3", SearchOption.AllDirectories);
            return files;
        }
        public Boolean CanDo() {
            if (Directory.Exists(Path)) return true;
            Console.WriteLine("Cannot finde directory = " + Path);
            return false;
        }
        public static String GetAppRoot() {
            var appRoot = AppContext.BaseDirectory.Substring(0,AppContext.BaseDirectory.LastIndexOf("/bin"));
            return appRoot.Substring(0,appRoot.LastIndexOf("/")+1);
        }
    }
}
