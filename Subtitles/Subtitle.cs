using System;
using System.Timers;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Subtitles
{
    class Subtitle
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Text { get; set; }
        public string Position { get; set; }
        public string Color { get; set; }

        public Subtitle(int startTime, int endTime, string text, string position = "Bottom", string color = "White")
        {
            StartTime = startTime;
            EndTime = endTime;
            Text = text;
            Position = position;
            Color = color;
        }

        public override string ToString()
        {
            return $"{StartTime}, {EndTime}, {Text}, {Position}, {Color}";
        }

    }

    internal class SubtitlesLoader
    {
        public static Subtitle[] LoadSubtitles(string path)
        {
            
            List<Subtitle> subtitles = new List<Subtitle>();
            string[] text = File.ReadAllLines(path);

            for (int i = 0; i < text.Length; i++)
            {
                string[] line = text[i].Split(' ');
                int startTime = int.Parse(line[0].Split(':')[0]) * 60 + int.Parse(line[0].Split(':')[1]);
                int endTime = int.Parse(line[2].Split(':')[0]) * 60 + int.Parse(line[2].Split(':')[1]);

                string position;
                string color;
                string subText = "";

                if (line[3][0] == '[')
                {
                    position = line[3].Replace("[", "").Replace(",", "");
                    color = line[4].Replace("]", "");
                    for (int j = 5; j < line.Length; j++)
                    {
                        subText += $"{line[j]} ";
                    }
                    subText = line[5];
                    subtitles.Add(new Subtitle(startTime, endTime, subText, position, color));
                    continue;
                }
                for (int j = 3; j < line.Length; j++)
                {
                    subText += $"{line[j]} ";
                }
                subtitles.Add(new Subtitle(startTime, endTime, subText));

            }
            return subtitles.ToArray();
        }

    }

    internal class DisplaySubtitles
    {

        private static void SetColor(Subtitle subtitle)
        {
            switch (subtitle.Color)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "White":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
        }
    }

}
