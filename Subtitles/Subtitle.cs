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
        public static void SetColor(Subtitle subtitle)
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

        private static void SetPosition(Subtitle subtitle)
        {
            switch (subtitle.Position)
            {
                case "Top":
                    Console.SetCursorPosition((72 - subtitle.Text.Length) / 2, 3);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((72 - subtitle.Text.Length) / 2, 23);
                    break;
                case "Right":
                    Console.SetCursorPosition(70 - 1 - subtitle.Text.Length, 26 / 2);
                    break;
                case "Left":
                    Console.SetCursorPosition(4, 26 / 2);
                    break;
                default:
                    break;
            }
        }

        public static void DrawBorder() // высота от 3 до 23 ширина от 4 до 70
        {
            for (int a = 70; a > 1; a--) 
            {
                Console.SetCursorPosition(1, 1);
                Console.CursorLeft = a;
                Console.Write("-");
            }
            for (int b = 0; b < 20; b++)
            {
                Console.SetCursorPosition(2, b + 2);
                Console.WriteLine("|");
                Console.SetCursorPosition(70, b + 2);
                Console.WriteLine("|");
            }
            for (int c = 70; c > 1; c--)
            {
                Console.SetCursorPosition(1, 22);
                Console.CursorLeft = c;
                Console.Write("-");
            }
            Console.Read();
        }
    }

}
