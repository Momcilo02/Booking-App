using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string FirstComment { get; set; }
        public User Creator { get; set; }
        public bool IsClosed { get; set; }

        public Forum()
        {
            
        }
        public Forum(Location location, string firstComment, User creator)
        {
            Location = location;
            FirstComment = firstComment;
            Creator = creator;
            IsClosed = false;
        }

        public string[] ToCSV()
        {
            string[] values = {Id.ToString(), Location.Id.ToString(),  FirstComment, Creator.Id.ToString(), IsClosed.ToString()};
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = new Location() { Id = Convert.ToInt32(values[1]) };
            FirstComment = Convert.ToString(values[2]);
            Creator = new User() { Id = Convert.ToInt32(values[3]) };
            IsClosed = Convert.ToBoolean(values[4]);
        }
    }
}
