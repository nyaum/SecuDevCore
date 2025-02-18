namespace SecuDevCore.Models
{
    public class Schedule
    {
        public int id { get; set; }
        public int _ScheduleID;
        public int ScheduleID
        {
            get => _ScheduleID;
            set
            {
                _ScheduleID = value;
                id = value;
            }
        }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; } = true;

        private string _ScheduleName;
        public string ScheduleName {
            get => _ScheduleName;
            set
            {
                _ScheduleName = value;
                title = value;
            }
        }
        private string _StartDate;
        public string StartDate {
            get => _StartDate;
            set
            {
                _StartDate = value;
                start = value;
            }
        }
        private string _EndDate;
        public string EndDate {
            get => _EndDate; 
            set
            {
                _EndDate = value;
                end = value;
            }
        }
    }
}
