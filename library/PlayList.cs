using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    public class PlayList
    {
        public String Name;
        public List<MusicFile> MusicFiles = new List<MusicFile>();
        public void Read(String Filename) {
            this.Name = Filename;
            if (!CanDo()) return;
            StreamReader sr = File.OpenText(Name);
            String s = "";
            MusicFile m = new MusicFile();
            while ((s = sr.ReadLine()) != null) {
                if (s.StartsWith("#EXTINF:")) {
                    Console.WriteLine(s);
                    m = new MusicFile();
                    m.Trackname = s.Split(',')[1];
                } else if (s.StartsWith("/")) {
                    m.Filename = s;
                    m.Type = s.Split('.')[1];
                    MusicFiles.Add(m);
                }
            }
        }

        public void Write(String Filename) {

        }

        public Boolean CanDo() {
            if (File.Exists(Name)) {
                if (Name.EndsWith("m3u")) return true;
                Console.WriteLine("Only M3U playlists are supported.");
                return false;
            }
            Console.WriteLine("Cannot finde file = " + Name);
            return false;
        }
    }
}