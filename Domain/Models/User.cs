using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Models
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Counter { get; set; }
        public Enumeration.UserType Type { get; set; }
        public User() { }

        public User(string username, string password, Enumeration.UserType type)
        {
            Username = username;
            Password = password;
            Type = type;
        }
        public User(string username, string password, Enumeration.UserType type,int counter)
        {
            Username = username;
            Password = password;
            Type = type;
            Counter = counter;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, ((int)Type).ToString(),Counter.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Type = (Enumeration.UserType)Convert.ToInt32(values[3]);
            Counter = Convert.ToInt32(values[4]);
        }
    }
}
