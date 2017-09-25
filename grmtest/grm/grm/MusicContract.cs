using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grm
{
    public class MusicContract
    {
        private String artist;
        private String title;
        private String usage;
        private DateTime startDate;
        private DateTime endDate;

        public string Artist { get => artist; set => artist = value; }
        public string Title { get => title; set => title = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string Usage { get => usage; set => usage = value; }

        public MusicContract()
        {

        }

        public MusicContract(string artist, string title, string usage, DateTime startDate, DateTime endDate)
        {
            this.artist = artist;
            this.title = title;
            this.usage = usage;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public MusicContract(string artist, string title,  string usage, DateTime startDate)
        {
            this.artist = artist;
            this.title = title;
            this.usage = usage;
            this.startDate = startDate;
        }
    }
}
