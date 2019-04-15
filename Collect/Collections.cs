using System;
using Parse;

namespace Collect
{
    public class Collection
    {

        public string ObjectId { get; set; }
        public string Name { get; set; }
        public ParseFile Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ParseUser Owner { get; set; }

        public Collection()
        {


        }
    }
}