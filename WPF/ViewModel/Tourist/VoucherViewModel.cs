using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Models;
using BookingApp.DTO;
using BookingApp.WPF.View.Tourist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class VoucherViewModel
    {
        public User User { get; set; }
        public Frame fr { get; set; }
        public Frame fr2 { get; set; }
        public VoucherDTO SelectedVoucher { get; set; }
        public ObservableCollection<VoucherDTO> Vouchers { get; set; }
        private VoucherService VoucherService { get; set; }
        public ICommand MenuClick { get; }

        public VoucherViewModel(User user, Frame frame, Frame frame2)
        {
            User = user;
            fr = frame;
            VoucherService = new VoucherService();
            SelectedVoucher = new VoucherDTO();
            Vouchers = new ObservableCollection<VoucherDTO>();
            MenuClick = new RelayCommand(param => Menu(param));
            UpdateVouchers();
        }
        public void UpdateVouchers()
        {
            foreach (Voucher voucher in VoucherService.GetAll())
            {
                if (IsVoucherValid(voucher))
                {
                    Vouchers.Add(new VoucherDTO(voucher));
                }
            }
        }
        private void Menu(object parameter)
        {
            fr.Navigate(new TouristHomeView(User, fr, fr2));

        }
        private bool IsVoucherValid(Voucher voucher)
        {
            bool isValid = voucher.UserId == User.Id && voucher.ExpiryDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) >= 0;
            if (!isValid)
            {
                DeleteExpiredVoucher(voucher);
            }
            return isValid;
        }

        private void DeleteExpiredVoucher(Voucher voucher)
        {
            if (voucher.UserId == User.Id && voucher.ExpiryDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0)
            {
                VoucherService.Delete(voucher);
            }
        }
    }
}
