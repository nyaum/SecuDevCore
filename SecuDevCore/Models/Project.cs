using SecuDev.Helper;

namespace SecuDev.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string LocationName { get; set; }
        public string SoftwareName { get; set; }
        public string InstallTypeName { get; set; }
        public string UserName { get; set; }
        public string _InstallDate { get; set; }
        public string InstallDate 
        {
            get => _InstallDate;
            set => _InstallDate = string.IsNullOrWhiteSpace(value) ? value
                : Utility.DateTimeFormat(value, 1);
        }
    }
}
