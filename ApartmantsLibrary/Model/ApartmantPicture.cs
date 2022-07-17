using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class ApartmantPicture
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Bytes { get; set; }
        public string Name { get; set; }
        public bool IsRepresentative { get; set; }        
    }
}
