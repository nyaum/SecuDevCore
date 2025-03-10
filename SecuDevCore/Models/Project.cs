﻿using SecuDev.Helper;

namespace SecuDev.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int SoftwareID { get; set; }
        public string SoftwareName { get; set; }
        public int InstallTypeID { get; set; }
        public string InstallTypeName { get; set; }
        public string UID { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public string UUID { get; set; }
        public string FileName { get; set; }
        public string _InstallDate { get; set; }
        public string InstallDate 
        {
            get => _InstallDate;
            set => _InstallDate = string.IsNullOrWhiteSpace(value) ? value
                : Utility.DateTimeFormat(value, 1);
        }
    }
}
