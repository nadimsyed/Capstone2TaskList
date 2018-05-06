using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone2
{
    class Task
    {
        private string teamMemberName;
        private string description;
        private DateTime date;
        private bool taskCompletion;

        public string TeamMemberName { get => teamMemberName; set => teamMemberName = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool TaskCompletion { get => taskCompletion; set => taskCompletion = value; }

        public Task(string name, string task, string date)
        {
            TeamMemberName = name;
            Description = task;
            //Date = Date.AddDays(days);
            IFormatProvider enUsDateFormat = new CultureInfo("en-US").DateTimeFormat;
            Date = DateTime.Parse(date, enUsDateFormat);
            //figure out how to convert to date,month,year
            //Date = DateTime.Parse(days, "dd/MM/yyyy HH:mm:ss.fff");
            TaskCompletion = false;
        }

        //public static string 
    }
}
