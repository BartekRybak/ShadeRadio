using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using NAudio;
using NAudio.Wave;

namespace ShadeRadio
{
    class Program
    {
        private static Player player;
        private static Station[] stations;
        private static int currentStationIndex = 0;
        static void Main(string[] args)
        {
            stations = LoadStationsFromFile("stations.txt");
            Console.Title = stations[currentStationIndex].name;
            if(stations.Length == 0)
            {
                Console.WriteLine("Add some stations to file stations.txt");
                Console.WriteLine("Example: Coding Radio|https://coderadio-relay-blr.freecodecamp.org/radio/8010/radio.mp3");
                Console.ReadKey();
                Environment.Exit(1);
            }

            player = new Player(stations[currentStationIndex].url);
            player.Play();
          
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Up/Down - volume");
                Console.WriteLine("Left/Right - change station");
                Console.WriteLine("Space - pause/play");
                Console.WriteLine("Edit stations.txt to add more stations");
                Console.WriteLine();
                Console.WriteLine("Station[{0}/{1}] : {2}", currentStationIndex + 1, stations.Length,stations[currentStationIndex].name);
                Console.WriteLine("Volume : {0}", player.GetVolume());

                ConsoleKey key = Console.ReadKey().Key;
                switch(key)
                {
                    case ConsoleKey.Spacebar:
                        if(player != null)
                        {
                            if(player.GetPlaybackState() == PlaybackState.Playing)
                            {
                                player.Pause();
                            }
                            else
                            {
                                player.Play();
                            }
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if(player.GetVolume() < 100)
                        {
                            if(player.GetVolume() + 10 > 100)
                            { 
                                player.SetVolume(100); 
                            }
                            else
                            {
                                player.SetVolume(player.GetVolume() + 10);
                            }
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (player.GetVolume() > 0)
                        {
                            if(player.GetVolume() - 10 < 0)
                            {
                                player.SetVolume(0);
                            }
                            else
                            {
                                player.SetVolume(player.GetVolume() - 10);
                            }  
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if(currentStationIndex < stations.Length - 1)
                        {
                            currentStationIndex++;
                            player.Pause();
                            player = new Player(stations[currentStationIndex].url);
                            player.Play();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if(currentStationIndex > 0)
                        {
                            currentStationIndex--;
                            player.Pause();
                            player = new Player(stations[currentStationIndex].url);
                            player.Play();
                        }
                        break;
                }
            }
        }

        private static Station[] LoadStationsFromFile(string file)
        {
            List<Station> stations = new List<Station>();
            if(File.Exists(file))
            {
                foreach(string line in File.ReadAllLines(file))
                {
                    string[] splitted = line.Split('|');

                    if(splitted.Length < 2)
                    {
                        stations.Add(new Station("Station Error",string.Empty));
                    }
                    else
                    {
                        stations.Add(new Station(splitted[0], splitted[1]));
                    }
                }
            }
            return stations.ToArray();
        }
    }
}
