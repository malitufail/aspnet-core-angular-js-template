using System;
using System.Collections.Generic;

namespace taskiiApp.Models
{
    public partial class TUsers
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessLevel { get; set; }
        public string IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
