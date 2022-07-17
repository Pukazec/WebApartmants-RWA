using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class TagType
    {   public TagType() 
        {
        }

        public TagType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public TagType(int id, string name, string nameEng, Guid guid) : this(id, name)
        {
            NameEng = nameEng;
            Guid = guid;
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NamePrint 
        { 
            get
            {
                return $"{Name} / {NameEng}";
            }
            set
            {
                NameEng = value;
            }
        }
    }
}
