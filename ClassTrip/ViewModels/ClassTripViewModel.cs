using ClassTrip.Models.DbMysqlModels;
using ClassTrip.Repos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassTrip.ViewModels
{
    public partial class ClassTripViewModel : ObservableObject
    {
        private readonly ClassTripRepo _repo = new ClassTripRepo();

        [ObservableProperty]
        private ObservableCollection<TripJavitva> datas;

        [ObservableProperty]
        private int sumDeposit;

        [ObservableProperty]
        private double avgDeposit;

        [ObservableProperty]
        private int noPaidAmountCount;

        [ObservableProperty]
        private int changePaidAmount;

        [ObservableProperty]
        private string newName;

        [ObservableProperty]
        private string newClass;

        [ObservableProperty]
        private string newDestination;

        [ObservableProperty]
        private int newPaidAmount;

        [ObservableProperty]
        private TripJavitva delete;

        [ObservableProperty]
        private TripJavitva selectedItem;

        [ObservableProperty]
        private int maxDeposit;

        public ClassTripViewModel()
        {
            LoadData();
        }
        private void LoadData()
        {
            Datas = _repo.GetAll();

            SumDeposit = _repo.GetSumDeposit();
            AvgDeposit = _repo.GetAVGDeposit();
            MaxDeposit = _repo.GetMaxDeposit();
            NoPaidAmountCount = _repo.GetNoPaidStudentsCount();
        }

        partial void OnSelectedItemChanged(TripJavitva value)
        {
            if (value != null)
            {
                ChangePaidAmount = value.PaidAmount;
                Delete = value;

            }
        }

        [RelayCommand]
        private void UpdateData()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Elöször válasz ki egy adatot");
                return;
            }
            if (ChangePaidAmount <= 0)
            {
                MessageBox.Show("Az Összegnek nullánál nagyobbnak kell lennie");
                return;
            }
            _repo.Update(SelectedItem, ChangePaidAmount);
            MessageBox.Show("Sikeres Módosítás");
            ChangePaidAmount = 0;
            SelectedItem = null;
            LoadData();
        }
        [RelayCommand]
        private void CreateData()
        {
            if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewClass) || string.IsNullOrWhiteSpace(NewDestination))
            {
                MessageBox.Show("Minden mező kitöltése kötelező");
                return;
            }
            if (NewPaidAmount <= 0)
            {
                MessageBox.Show("Az Összegnek nullánál nagyobbnak kell lennie");
                return;
            }
            var newData = new TripJavitva();
            _repo.Create(newData, NewName, NewClass, NewDestination, NewPaidAmount);
            MessageBox.Show("Sikeres létrehozás");
            NewName = "";
            NewClass = "";
            NewDestination = "";
            NewPaidAmount = 0;
            LoadData();
        }
        [RelayCommand]
        private void DeleteData()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Elöször válasz ki egy adatot");
                return;
            }
            _repo.Delete(Delete);
            MessageBox.Show("Sikeres Törlés");
            Delete = null;
            SelectedItem = null;
            LoadData();
        }


    }
}
