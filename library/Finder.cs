using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    public class Finder
    {
        private String SourcePath;
        private String FileType;
        public List<MusicFile> MusicFiles {
            get { return this.MusicFiles; }
            private set { this.MusicFiles = value; }
            }

        public Finder(String SourcePath, String FileType) {
            this.SourcePath = SourcePath;
            this.FileType = FileType;
        }

        public List<MusicFile> GetMusicFilesByPlayList(String PlayList) {
            MusicFiles = new List<MusicFile>();
            return MusicFiles;
        }
        public List<MusicFile> GetMusicFiles() {
            string[] files = GetFiles();
            MusicFiles = new List<MusicFile>();
            foreach(string name in files) {
                if (name.EndsWith(FileType)) {
                    FileInfo f = new FileInfo(name);
                    MusicFile m = new MusicFile();
                    m.SourcePath = f.DirectoryName;
                    m.Filename = f.Name;
                    m.Type = FileType;
                    MusicFiles.Add(m);
                }
            }
            return MusicFiles;
        }

        public string[] GetFiles() {
            string[] files = new string[1];
            if (!CanDo()) return files;
            files = Directory.GetFiles(SourcePath, "*." + FileType, SearchOption.AllDirectories);
            return files;
        }

        public Boolean CanDo() {
            if (Directory.Exists(SourcePath)) return true;
            Console.WriteLine("Cannot finde directory = " + SourcePath);
            return false;
        }
    }
}
