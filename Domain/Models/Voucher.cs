using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int GuideId { get; set; }
        public string Reason { get; set; }
        public Voucher() { }
        public Voucher(int id, DateOnly expiryDate, string name, int userId, int guideId, string reason)
        {
            Id = id;
            ExpiryDate = expiryDate;
            Name = name;
            UserId = userId;
            GuideId = guideId;
            Reason = reason;
        }
        public Voucher(DateOnly expiryDate, string name, int userId, int guideId, string reason)
        {
            ExpiryDate = expiryDate;
            Name = name;
            UserId = userId;
            GuideId = guideId;
            Reason = reason;
        }
        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Name, ExpiryDate.ToString(), UserId.ToString(), GuideId.ToString(), Reason };
            return values;
        }


        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            ExpiryDate = DateOnly.Parse(values[2]);
            UserId = int.Parse(values[3]);
            GuideId = int.Parse(values[4]);
            Reason = values[5];
        }

    }
}
