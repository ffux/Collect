using System;
using Parse;

namespace Collect
{
    public class CollectionItems
    {
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public ParseFile Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ParseUser Owner { get; set; }

        public CollectionItems()
        {


        }
    }
}