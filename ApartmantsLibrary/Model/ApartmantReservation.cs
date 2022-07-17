using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class ApartmantReservation
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment{ get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Guid Guid{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public string Details{ get; set; }
        public string UserName{ get; set; }
        public string UserEmail{ get; set; }
        public string UserPhone{ get; set; }
        public string UserAddress{ get; set; }
    }
}
