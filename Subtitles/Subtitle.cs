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

    }
}
