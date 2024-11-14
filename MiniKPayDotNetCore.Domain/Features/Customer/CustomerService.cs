using Microsoft.EntityFrameworkCore;
using MiniKPayDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.Domain.Features.Customer
{
    public class CustomerService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public List<TblCustomer> GetCustomers()
        {
            var model = _db.TblCustomers.AsNoTracking().ToList();
            return model;
        }

        public TblCustomer GetCustomer(int id) 
        {
            var item = _db.TblCustomers.FirstOrDefault(x => x.CustomerId == id);
            if (item is null) {
                return null;
            }
            return item;
        }

        public TblCustomer CreateCustomer( TblCustomer customer)
        {
            _db.TblCustomers.Add(customer);
            _db.SaveChanges();
            return customer;
        }

        public TblCustomer UpdateCustomer(int id, TblCustomer customer) 
        { 
            var item = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerId == id);
            if (item is null) {
                return null;
            }

            item.CustomerFullName = customer.CustomerFullName;
            item.MobileNo = customer.MobileNo;
            item.CustomerPin = customer.CustomerPin;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
           
        }

        public TblCustomer PatchCustomer(int id, TblCustomer customer)
        {
            var item = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(customer.CustomerFullName)) { 
                item.CustomerFullName = customer.CustomerFullName;
            }
            if (!string.IsNullOrEmpty(customer.MobileNo))
            {
                item.MobileNo = customer.MobileNo;
            }
            if (!string.IsNullOrEmpty(customer.CustomerPin))
            {
                item.CustomerPin = customer.CustomerPin;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public bool DeleteCustomer(int id)
        {
            var item = _db.TblCustomers.AsNoTracking().FirstOrDefault();
            if(item is null)
            {
                return false;
            }
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return true;

        }
    }   
}
