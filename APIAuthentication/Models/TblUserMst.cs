using System;
using System.Collections.Generic;

namespace APIAuthentication.Models
{
    public partial class TblUserMst
    {
        public string UserName { get; set; } = null!;
        public string? Password { get; set; }
        public string? Salt { get; set; }
    }
}
