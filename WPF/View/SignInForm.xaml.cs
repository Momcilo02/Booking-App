using BookingApp.View.Guest;

using BookingApp.View.Owner;
using BookingApp.View.Guide;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.View.Tourist;
using BookingApp.Repository;
using BookingApp.Domain.Models;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {
        public User User { get; set; }
        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
            User = new User();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);

            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    OpenAppropriateMainView(user);
                }
                else
                {
                    ShowMessage("Wrong password!");
                }
            }
            else
            {
                ShowMessage("Wrong username!");
            }
        }

        private void OpenAppropriateMainView(User user)
        {
            switch (user.Type)
            {
                case Enumeration.UserType.Guest:
                    OpenGuestMainView(user);
                    break;
                case Enumeration.UserType.Owner :
                    OpenOwnerMainView(user);
                    break;
                case Enumeration.UserType.SuperOwner:
                    OpenOwnerMainView(user);
                    break;
                case Enumeration.UserType.Guide:
                    OpenGuideMainView(user);
                    break;
                case Enumeration.UserType.Tourist:
                    OpenTouristMainView(user);
                    break;
                default:
                    ShowMessage("Unknown user type!");
                    break;
            }
        }

        private void OpenGuestMainView(User user)
        {
            var curr = (App)System.Windows.Application.Current;
            curr.User = user;
            GuestMainView guestMainView = new GuestMainView(user);
            guestMainView.ShowDialog();
        }

        private void OpenOwnerMainView(User user)
        {
            OwnerMainView ownerMainView = new OwnerMainView(user);
            ownerMainView.ShowDialog();
        }

        private void OpenGuideMainView(User user)
        {
            GuideMainView guideMainView = new GuideMainView(user);
            guideMainView.ShowDialog();
        }

        private void OpenTouristMainView(User user)
        {
            WelcomeTouristWindow welcomeTouristView = new WelcomeTouristWindow(user);
            welcomeTouristView.ShowDialog();
            Close();
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
    
