using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class Owner
    {
        public Owner()
        {

        }

        public Owner(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}
