using System.Collections.Generic;

namespace Core.Areas.Dashboard.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Admin> Admins{ get; set; }
    }
}