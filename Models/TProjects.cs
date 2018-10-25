using System;
using System.Collections.Generic;

namespace taskiiApp.Models
{
    public partial class TProjects
    {
        public long Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public double? TotalHours { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
