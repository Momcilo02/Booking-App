using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public User Creator { get; set; }
        public int ForumId { get; set; }
        public int ForumOrder {  get; set; }
        public ForumComment()
        {
            
        }
        public ForumComment(string comment, User user, int forumId, int forumOrder)
        {
            Comment = comment;
            Creator = user;
            ForumId = forumId;
            ForumOrder = forumOrder;
        }

        public string[] ToCSV()
        {
            string[] values = {Id.ToString(), Comment, Creator.Id.ToString(), ForumId.ToString(), ForumOrder.ToString()};
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Comment = Convert.ToString(values[1]);
            Creator = new User() { Id = Convert.ToInt32(values[2]) };
            ForumId = Convert.ToInt32(values[3]);
            ForumOrder = Convert.ToInt32(values[4]);
        }
    }
}
