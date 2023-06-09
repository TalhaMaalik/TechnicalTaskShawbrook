﻿using DataAccessLayer.DataModel;

namespace DataAccessLayer.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext _Context;

        public CustomerRepository(AppDbContext context)
        {
            _Context = context;
        }

        public void CreateCustomer(CustomerModel customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            _Context.Customers.Add(customer);
        }

        public bool DoesCustomerHaveMembership(Guid customerID, Guid membershipId)
        {
            var membershipRecord = _Context.CustomerMemberships
                .FirstOrDefault(i => i.CustomerID == customerID && i.MembershipID == membershipId);
            if(membershipRecord == null)
                return false;
            else if(membershipRecord.EndDate < DateTime.Now)
                return false;
            return true;
        }

        public CustomerModel GetCustomerById(Guid customerID)
        {
            var customer = _Context.Customers.FirstOrDefault(c => c.Id == customerID);
            if (customer == null)
                throw new ArgumentException(nameof(customer));
            return customer;
        }
        public void AddNewMembership(CustomerMembershipModel customerMembership)
        {
            if (customerMembership == null)
                throw new ArgumentException(nameof(customerMembership));
            _Context.CustomerMemberships.Add(customerMembership);
        }
        public bool CustomerExists(Guid customerID)
        {
            return _Context.Customers.Any(c => c.Id == customerID);
        }
        public bool CustomerExistsByEmail(string email)
        {
            return _Context.Customers.Any(c => c.Email == email);
        }
        public bool SaveChanges()
        {
            return (_Context.SaveChanges() >=0);
        }


    }
}
