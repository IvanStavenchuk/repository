using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Objects
{
    public sealed class User : BaseObject
    {
        public string FirstName { get; set; }
        public string MiddletName { get; set; }
        public string LastName { get; set; }
        public long DeptId { get; set; }
    }
}
