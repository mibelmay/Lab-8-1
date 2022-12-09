using System;
using System.Timers;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Timer = System.Threading.Timer;

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

        public override string ToString()
        {
            return $"{StartTime}, {EndTime}, {Text}, {Position}, {Color}";
        }

    }

    internal static class SubtitlesLoader
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

        private Subtitle[] subtitles;
        public DisplaySubtitles(Subtitle[] subtitles)
        {
            this.subtitles = subtitles;
        }

        static int borderLength = 20;
        static int borderWidth = 70;
        private static int timerInterval = 1000;
        private int currentTime = 0;



        public void Start()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += Tick;
            timer.AutoReset = true;
            timer.Enabled = true;

        }
        private void Tick(Object obj, ElapsedEventArgs e)
        {
            foreach (var subtitle in subtitles)
            {
                if (subtitle.StartTime == currentTime) WriteSubtitle(subtitle);
                else if (subtitle.EndTime == currentTime) DeleteSubtitle(subtitle);
            }
            currentTime++;
        }


        public static void WriteSubtitle(Subtitle subtitle)
        {
            SetPosition(subtitle);
            SetColor(subtitle);
            Console.Write(subtitle.Text);
        }

        public static void DeleteSubtitle(Subtitle subtitle)
        {
            SetPosition(subtitle);
            for (int i = 0; i < subtitle.Text.Length; i++)
            {
                Console.Write(" ");
            }
        }
        public static void SetColor(Subtitle subtitle)
        {
            switch (subtitle.Color)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "DarkMagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
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

        public static void SetPosition(Subtitle subtitle)
        {
            switch (subtitle.Position)
            {
                case "Top":
                    Console.SetCursorPosition((borderWidth - subtitle.Text.Length) / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((borderWidth - subtitle.Text.Length) / 2, borderLength);
                    break;
                case "Right":
                    Console.SetCursorPosition(borderWidth - 1 - subtitle.Text.Length, (borderLength / 2) + 1);
                    break;
                case "Left":
                    Console.SetCursorPosition(3, (borderLength / 2) + 1);
                    break;
                default:
                    break;
            }
        }

        public static void DrawBorder() 
        {
            for (int a = borderWidth; a > 0; a--) 
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorLeft = a;
                Console.Write("-");
            }
            for (int b = 0; b < borderLength; b++)
            {
                Console.SetCursorPosition(1, b + 1);
                Console.WriteLine("|");
                Console.SetCursorPosition(borderWidth, b + 1);
                Console.WriteLine("|");
            }
            for (int c = borderWidth; c > 0; c--)
            {
                Console.SetCursorPosition(1, borderLength + 1);
                Console.CursorLeft = c;
                Console.Write("-");
            }

        }
    }

}
