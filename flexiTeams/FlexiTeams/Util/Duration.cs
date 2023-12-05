using System.Data.Common;

namespace FlexiTeams.Util
{
    public struct Duration
    {
        private int _seconds;

        public Duration(int hours, int minutes, int seconds)
        {
             _seconds = hours*3600 + minutes*60 + seconds;
        }

        public Duration(int hours, int minutes)
        {
            int Hours = hours;
            int Minutes = minutes;

            _seconds = Hours * 3600 + Minutes * 60;
        }

        public readonly int TotalHours() { return _seconds/3600; }

        public readonly int TotalMinutes()
        {
            return _seconds / 60;
        }

        public readonly int TotalSeconds()
        {
            return _seconds;
        }

        override
        public string ToString()
        {
            string result = "";

            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            while(_seconds/3600 > 0)
            {
                hours++;
                _seconds -= 3600;
            }
            while (_seconds / 60 > 0)
            {
                minutes++;
                _seconds -= 60;
            }

            if (hours > 0 || minutes > 0 || seconds > 0)
            {
                if (hours > 0)
                {
                    result += hours;
                }

                if (minutes > 0)
                {
                    result += ":" + minutes;
                }

                if (seconds > 0)
                {
                    result += ":" + seconds;
                }
            }
            return result;
        }

        public static Duration operator+(Duration d1, Duration d2)
        {
            return new Duration(0, 0, d1.TotalSeconds() + d2.TotalSeconds());
        }
        public static bool operator ==(Duration d1, Duration d2)
        {
            return (d1.TotalSeconds == d2.TotalSeconds);
        }
        public static bool operator !=(Duration d1, Duration d2)
        {
            return (! (d1 == d2));
        }
        public static bool operator <(Duration d1, Duration d2)
        {
            return (d1.TotalSeconds() < d2.TotalSeconds());
        }
        public static bool operator >(Duration d1, Duration d2)
        {
            return d1.TotalSeconds() > d2.TotalSeconds();
        }
        public static bool operator <=(Duration d1, Duration d2)
        {
            return d1.TotalSeconds() <= d2.TotalSeconds();
        }
        public static bool operator >=(Duration d1, Duration d2)
        {
            return d1.TotalSeconds() >= d2.TotalSeconds();
        }
    }
}