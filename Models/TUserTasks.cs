using System;
using System.Collections.Generic;

namespace taskiiApp.Models
{
    public partial class TUserTasks
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        public string Hours { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public long UserId { get; set; }
    }
}
