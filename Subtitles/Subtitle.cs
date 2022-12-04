using System;
using System.Timers;
using System.Collections.Generic;
using System.IO;

namespace Subtitles
{
    internal class Subtitle
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

        public override string ToString() // временно для проверки
        {
            return $"{StartTime}, {EndTime}, {Text}, {Position}, {Color}";
        }

    }

    internal class SubtitlesLoader
    {
        public static void LoadSubtitles()
        {
            string path = "D:\\прога\\8 лаба\\Subtitles\\Subtitles\\bin\\debug\\net6.0\\subs.txt";
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
        }

    }
}
