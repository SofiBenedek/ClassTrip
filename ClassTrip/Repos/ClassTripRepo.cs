using ClassTrip.Models.DbMysqlModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTrip.Repos
{
    public class ClassTripRepo
    {
        private readonly TripContext _context = new TripContext();

        public ObservableCollection<TripJavitva> GetAll()
        {
            return new ObservableCollection<TripJavitva>(_context.TripJavitvas.ToList());
            
        }

        public int GetSumDeposit()
        {
            return _context.TripJavitvas.Sum(x => x.PaidAmount);
        }

        public double GetAVGDeposit()
        {
            double avgdeposit = _context.TripJavitvas.Average(x => x.PaidAmount);

            return Math.Round(avgdeposit, 1);
        }

        public int GetMaxDeposit()
        {
            return _context.TripJavitvas.Max(x => x.PaidAmount);
        }

        public int GetNoPaidStudentsCount()
        {
            return _context.TripJavitvas.Where(x => x.PaidAmount == 0).Count();
        }

        public void Update(TripJavitva changeData, int changePaidAmount)
        {
            changeData.PaidAmount = changePaidAmount;
            _context.TripJavitvas.Update(changeData);
            _context.SaveChanges();
        }
        public void Create(TripJavitva newData, string newName, string newClass, string newDestination, int newPaidAmount)
        {
            newData.Id = _context.TripJavitvas.Max(x => x.Id) + 1;
            newData.Name = newName;
            newData.Class = newClass;
            newData.Destination = newDestination;
            newData.PaidAmount = newPaidAmount;

            _context.TripJavitvas.Add(newData);
            _context.SaveChanges();

        }

        public void Delete(TripJavitva deleteData)
        {
            _context.TripJavitvas.Remove(deleteData);
            _context.SaveChanges();
        }

    }
}
