using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using PropertyManager.Domain.Models;
using System;
using System.Collections.Generic;

namespace PropertyManager.Infrastructure
{
    public class DataBase
    {
        public List<Property> Properties { get; set; }
        public List<Manager> Managers { get; set; }
        public List<Client> Clients { get; set; }
        public List<Arrangement> Arrangements { get; set; }
        public List<PaySchedule> PaySchedules { get; set; }
        public List<Payment> Payments { get; set; }        

        public DataBase()
        {
            PopulateSampleData();
        }

        private void PopulateSampleData()
        {
            Properties = new List<Property>();
            Properties.Add(new Property { Id = 1, Address = "1 Hill Street VIC 3001" });
            Properties.Add(new Property { Id = 2, Address = "2 Park Street VIC 3002" });
            Properties.Add(new Property { Id = 3, Address = "3 Beach Street VIC 3003" });

            Managers = new List<Manager>();
            Managers.Add(new Manager { Id = 1, Name = "Nash Manager" });

            Clients = new List<Client>();
            Clients.Add(new Client { Id = 1, Name = "Adam Client" });
            Clients.Add(new Client { Id = 2, Name = "Dave Client" });
            Clients.Add(new Client { Id = 3, Name = "Steve Client" });

            Arrangements = new List<Arrangement>();
            Arrangements.Add(new Arrangement { Id = 1, PropertyId = 1, ManagerId = 1, ClientId = 1, RentPerWeek = 400, 
                Start = DateTime.Parse("2020-07-03"), End = DateTime.Parse("2020-12-31") });
            Arrangements.Add(new Arrangement { Id = 2, PropertyId = 2, ManagerId = 1, ClientId = 2, RentPerWeek = 450,
                Start = DateTime.Parse("2020-07-02"), End = DateTime.Parse("2021-04-30") });
            Arrangements.Add(new Arrangement { Id = 3, PropertyId = 3, ManagerId = 1, ClientId = 3, RentPerWeek = 500,
                Start = DateTime.Parse("2020-07-10"), End = DateTime.Parse("2021-06-30") });

            PaySchedules = new List<PaySchedule>();
            PaySchedules.Add(new PaySchedule { Id = 1, ArrangementId = 1, DueDate = DateTime.Parse("2020-07-10"), Balance = 400 });
            PaySchedules.Add(new PaySchedule { Id = 2, ArrangementId = 2, DueDate = DateTime.Parse("2020-07-09"), Balance = 450 });
            PaySchedules.Add(new PaySchedule { Id = 3, ArrangementId = 3, DueDate = DateTime.Parse("2020-07-17"), Balance = 500 });

            Payments = new List<Payment>();
        }
    }
}
