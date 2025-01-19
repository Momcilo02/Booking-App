using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Models
{
    public class TouristNotification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Description {  get; set; }
        public TouristNotification() { }
        public TouristNotification(int id, string title, List<string> description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
        public TouristNotification(string title, List<string> description)
        {
            Title = title;
            Description = description;
        }
        public string[] ToCSV()
        {
            string descrpitons = string.Join(",", Description);
            string[] values = { Id.ToString(),Title.ToString(),descrpitons };
            return values;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Title = Convert.ToString(values[1]);
            Description = values[2].Split(',').ToList();

        }
    }
}
