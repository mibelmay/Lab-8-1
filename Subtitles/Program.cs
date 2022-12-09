using Subtitles;
using System;

namespace Lab8
{
    public static class Program
    {
       static void Main()
        {
            string path = Directory.GetCurrentDirectory() + $"\\subs.txt";
            Subtitle[] subtitles = SubtitlesLoader.LoadSubtitles(path);

            DisplaySubtitles.DrawBorder();
            DisplaySubtitles display = new DisplaySubtitles(subtitles);
            display.Start();

            Console.ReadLine();
        }
    }
}