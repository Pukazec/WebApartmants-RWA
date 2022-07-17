using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class City
    {
        public City()
        {

        }

        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name.ToString();
    }
}
