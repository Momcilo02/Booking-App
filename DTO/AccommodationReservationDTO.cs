using BookingApp.Domain.Models;
using BookingApp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.DTO
{
    public class AccommodationReservationDTO:ValidationBase
    {
        public int Id {  get; set; }
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; } 
            set {
                if(startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                if(endDate != value)
                {
                    endDate = value; 
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        public int UserId {  get; set; }
        public Accommodation Accommodation { get; set; }
        public string? MainImagePath { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public int MinResDays { get; set; }
        private string numberOfPeople;
        public string NumberOfPeople
        {
            get { return numberOfPeople; }
            set
            {
                if(numberOfPeople != value)
                {
                    numberOfPeople = value;
                    OnPropertyChanged(nameof(NumberOfPeople));
                }
            }
        }
        private string lengthOfStay;
        public string LengthOfStay
        {
            get { return lengthOfStay; }
            set
            {
                if(lengthOfStay != value)
                {
                    lengthOfStay = value;
                    OnPropertyChanged(nameof(LengthOfStay));
                }
            }
        }
        public bool CheckNumber {  get; set; }

        public AccommodationReservationDTO(int id, DateOnly start, DateOnly end, int userId, Accommodation accommodation, string peoplenumber)
        {
            Id = id;
            StartDate = start.ToDateTime(TimeOnly.Parse("10:00PM"));
            EndDate = end.ToDateTime(TimeOnly.Parse("10:00PM"));
            UserId = userId;
            Accommodation = accommodation;
            StartDateString = start.ToString();
            EndDateString = end.ToString();
            if(accommodation.Images != null && accommodation.Images.Count > 0)
            {
                MainImagePath = accommodation.Images[0].Path;
            }
            else
            {
                MainImagePath = null;
            }
            NumberOfPeople = peoplenumber;
            CheckNumber = false;
        }
        public AccommodationReservationDTO(AccommodationReservation accommodationReservation) 
        {
            Id = accommodationReservation.Id;
            StartDate = accommodationReservation.StartDate.ToDateTime(TimeOnly.Parse("10:00PM"));
            EndDate = accommodationReservation.EndDate.ToDateTime(TimeOnly.Parse("10:00PM"));
            UserId = accommodationReservation.User.Id;
            Accommodation = accommodationReservation.Accommodation;
            if(accommodationReservation.Accommodation.Images != null && accommodationReservation.Accommodation.Images.Count > 0)
            {
                MainImagePath = accommodationReservation.Accommodation.Images[0].Path;
            }
            else
            {
                MainImagePath = null;
            }

            StartDateString = accommodationReservation.StartDate.ToString();
            EndDateString = accommodationReservation.EndDate.ToString();
            NumberOfPeople = accommodationReservation.NumberOfPeople.ToString();
            CheckNumber = false;
        }
        public AccommodationReservationDTO()
        {
            CheckNumber = false;
        }
        public AccommodationReservation ToAccommodationReservation()
        {
            DateOnly start = DateOnly.FromDateTime((DateTime)startDate);
            DateOnly end = DateOnly.FromDateTime((DateTime)endDate);
            return new AccommodationReservation(Id, Accommodation, new User() { Id = UserId }, start, end,Convert.ToInt32( NumberOfPeople));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrEmpty(this.startDate.ToString())) {
                this.ValidationErrors["StartDate"] = "First Date is Required";
            }
            if (string.IsNullOrEmpty(this.endDate.ToString()))
            {
                this.ValidationErrors["EndDate"] = "Last Date is Required";
            }
            if (this.startDate > this.endDate)
            {
                this.ValidationErrors["EndDate"] = "Last Date Must be Later";
            }
            int x;
            Int32.TryParse(numberOfPeople, out x);
            if (string.IsNullOrEmpty(this.numberOfPeople) && this.CheckNumber ==true)
            {
                this.ValidationErrors["NumberOfPeople"] = "Required";
            }else if (!Int32.TryParse(numberOfPeople, out x) && this.CheckNumber == true)
            {
                this.ValidationErrors["NumberOfPeople"] = "Must be a number";
            }else if(this.CheckNumber ==true && x > this.Accommodation.MaxGuests)
            {
                this.ValidationErrors["NumberOfPeople"] = "Can't be more then " + this.Accommodation.MaxGuests.ToString();
            }
            
            if (string.IsNullOrEmpty(this.lengthOfStay) ){
                this.ValidationErrors["LengthOfStay"] = "Must enter length of stay";
            }
            Int32.TryParse(this.lengthOfStay, out x);
            if (!Int32.TryParse(this.lengthOfStay, out x))
            {
                this.ValidationErrors["LengthOfStay"] = "Must be a number";
            }else if (x < this.MinResDays)
            {
                MessageBox.Show(x.ToString());
                this.ValidationErrors["LengthOfStay"] = "Length of stay must be atleast " + MinResDays.ToString();
            }


        }

        public override void Demo()
        {
            throw new NotImplementedException();
        }
    }
}
