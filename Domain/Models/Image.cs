using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int EntityId { get; set; }

        public Enumeration.EntityType Type { get; set; }

        public Image()
        {

        }
        public Image(int id, string path, int entityId, Enumeration.EntityType type)
        {
            Id = id;
            Path = path;
            EntityId = entityId;
            Type = type;
        }
        public Image(string path, int entityId, Enumeration.EntityType type)
        {
            Path = path;
            EntityId = entityId;
            Type = type;
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), Path, EntityId.ToString(), Type.ToString() };
            return values;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Path = values[1];
            EntityId = Convert.ToInt32(values[2]);
            Enum.TryParse(values[3], out Enumeration.EntityType Type);
        }
    }
}
