using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    /// <summary>
    /// A playlist is considered a repeating playlist if any of the songs contain a reference to a previous song in the playlist. 
    /// Otherwise, the playlist will end with the last song which points to null.
    /// 
    /// Implement a function IsRepeatingPlaylist that, efficiently with respect to time used, returns true if a playlist is repeating or false if it is not. 
    /// Only edit the Song class.
    /// 
    /// ***********
    /// For example, the following code prints "True" as both songs point to each other.
    /// Song first = new Song("Hello");
    /// Song second = new Song("Eye of the tiger");
    /// 
    /// first.NextSong = second;
    /// second.NextSong = first;
    /// 
    /// Console.WriteLine(first.IsRepeatingPlaylist());
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Performing Test 1: ");
            Console.WriteLine(Test1() ? "Success" : "Fail");

            Console.Write("Performing Test 2: ");
            Console.WriteLine(Test2() ? "Success" : "Fail");

            Console.Write("Performing Test 3: ");
            Console.WriteLine(!Test3() ? "Success" : "Fail");

            Console.Write("Performing Test 4: ");
            Console.WriteLine(!Test4() ? "Success" : "Fail");

            Console.Write("Performing Test 5: ");
            Console.WriteLine(Test5() ? "Success" : "Fail");


            Console.ReadLine();
        }

        /// <summary>
        /// Test if playlist is a simple closed loop.
        /// </summary>
        /// <returns></returns>
        static bool Test1()
        {
            var playlist = new List<Song>
            {
                new Song("Song 0"),
                new Song("Song 1"),
                new Song("Song 2"),
                new Song("Song 3")
            };

            playlist[0].NextSong = playlist[1];
            playlist[1].NextSong = playlist[2];
            playlist[2].NextSong = playlist[3];
            playlist[3].NextSong = playlist[0];

            return playlist[0].IsInRepeatingPlaylist();
        }

        /// <summary>
        /// Test if playlist repeats, starting songs can be skipped.
        /// </summary>
        /// <returns></returns>
        static bool Test2()
        {
            var playlist = new List<Song>
            {
                new Song("Song 0"),
                new Song("Song 1"),
                new Song("Song 2"),
                new Song("Song 3")
            };

            playlist[0].NextSong = playlist[1];
            playlist[1].NextSong = playlist[2];
            playlist[2].NextSong = playlist[3];
            playlist[3].NextSong = playlist[0];

            return playlist[2].IsInRepeatingPlaylist();
        }

        /// <summary>
        /// Test if playlist is not a loop.
        /// </summary>
        /// <returns></returns>
        static bool Test3()
        {
            var playlist = new List<Song>
            {
                new Song("Song 0"),
                new Song("Song 1"),
                new Song("Song 2"),
                new Song("Song 3")
            };

            playlist[0].NextSong = playlist[1];
            playlist[1].NextSong = playlist[2];
            playlist[2].NextSong = playlist[3];

            return playlist[2].IsInRepeatingPlaylist();
        }

        /// <summary>
        /// Test if playlist repeats, last song is followed by first song.
        /// </summary>
        /// <returns></returns>
        static bool Test4()
        {
            var playlist = new List<Song>
            {
                new Song("Song 0"),
                new Song("Song 1"),
                new Song("Song 2"),
                new Song("Song 3")
            };

            playlist[0].NextSong = playlist[1];
            playlist[1].NextSong = playlist[2];
            playlist[2].NextSong = playlist[3];
            playlist[3].NextSong = playlist[2];

            return playlist[0].IsInRepeatingPlaylist();
        }

        /// <summary>
        /// Test if solution time acceptable when playlist contains 5 million songs.
        /// </summary>
        /// <returns></returns>
        static bool Test5()
        {
            var playlist = new List<Song>() { new Song("Song 0") };
            var now = DateTime.Now;

            for (int i = 1; i < 5000000; i++)
            {
                var song = new Song("Song " + i.ToString());
                playlist[i - 1].NextSong = song;

                playlist.Add(song);
            }

            playlist[playlist.Count - 1].NextSong = playlist[0];

            var timeSpentInitialing = DateTime.Now.Subtract(now);

            now = DateTime.Now;

            if (!playlist[0].IsInRepeatingPlaylist()) return false;

            var timeSpentInAlgorithm = DateTime.Now.Subtract(now);

            return timeSpentInitialing.Seconds / 4 > timeSpentInAlgorithm.Seconds;
        }

    }

    public class Song
    {
        public string Name { get; set; }

        public Song NextSong { get; set; }

        public Song(string name)
        {
            Name = name;
        }

        public bool IsInRepeatingPlaylist()
        {
            var checkedsongs = new HashSet<Song>();  //this list will work with solution 1
            var currentsong = this;
            var nextsong = currentsong.NextSong;
            var afternextsong =nextsong.NextSong; // this variable for solution 2 only 
            // solution 1
            //var checkedsongs = new HashSet<Song>();
            //while (nextsong != null)
            //{
            //    if (currentsong.Equals(nextsong)){
            //        return true;
            //    }
            //    else
            //    {
            //        if (checkedsongs.Contains(nextsong))
            //        {
            //            return false;
            //        }
            //        checkedsongs.Add(nextsong);
            //        nextsong = nextsong.NextSong;

            //    }
            //}
            //return false;


            // solution 2

            while (nextsong != null)
            {
                if (nextsong == currentsong)
                {
                    return true;
                }
                else
                {
                    if (nextsong.NextSong != null)
                    {
                        nextsong = nextsong.NextSong;
                        // check if there is infinty loop
                        afternextsong = nextsong.NextSong != null ? nextsong.NextSong : nextsong; // get after next song
                        if (nextsong == afternextsong.NextSong){break; } //if there is infinty loop then break
                    }
                    else { break; }

                }
                
            }
            return false;

        }
    }
}
