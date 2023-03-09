using System.Collections.Generic;

namespace DaringAPI.Entities
{
    public class Facebook
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string [] Posts { get; set; }
        public ICollection<string> user { get; set; }
        
    }
}