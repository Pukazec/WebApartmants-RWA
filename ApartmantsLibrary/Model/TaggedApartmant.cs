using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class TaggedApartmant
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int ApartmantId { get; set; }
        public Apartment Apartment { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
