using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class Apartment : IComparable<Apartment>
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
        public Owner Owner { get; set; }
        public string OwnerName { get; set; }
        public int OwnerId { get; set; }
        public Status Status { get; set; }
        public string StatusName { get; set; }
        public string StatusNameEng { get; set; }
        public int StatusId { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int BeachDistance { get; set; }
        public int TotalRooms { get; set; }
        public decimal Price { get; set; }
        public int NumOfPictures{ get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stars { get; set; }
        public string ImgUrl { get; set; }

        public int CompareTo(Apartment other)
        {
            return this.Price.CompareTo(other.Price);
        }
    }
}
