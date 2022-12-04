using Subtitles;
using System;

namespace Lab8
{
    public static class Program
    {
       static void Main()
        {
            string path = "D:\\прога\\8 лаба\\Subtitles\\Subtitles\\bin\\debug\\net6.0\\subs.txt";
            Subtitle[] subtitles = SubtitlesLoader.LoadSubtitles(path);

            DisplaySubtitles.DrawBorder();
        }
    }
}