using PropertyManager.Domain.Models;
using PropertyManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyManager.Functions
{
    public static class ArrangementService
    {
        private static DataBase _dataBase = new DataBase();

        public static bool IsValidManagerId(int managerId)
        {
            return _dataBase.Managers.Select(m => m.Id == managerId).FirstOrDefault();
        }

        public static List<ClientStatus> GetArrearsClients(int managerId)
        {
            return GetArrearsClientStatuses(managerId);
        }       

        public static List<ClientStatus> GetClientsStatus(int managerId)
        {
            var statuses = new List<ClientStatus>();

            statuses.AddRange(GetPaidClientStatuses(managerId));

            statuses.AddRange(GetArrearsClientStatuses(managerId));

            return statuses;
        }

        public static bool IsValidArrangementId(int arrangementId)
        {
            return _dataBase.Arrangements.Where(a => a.Id == arrangementId).Any();
        }

        public static void MakePayment(int arrangementId, double ammount)
        {
            var id = _dataBase.Payments.Select(p => p.Id).FirstOrDefault() + 1;

            _dataBase.Payments.Add(new Payment { Id = id, ArrangementId = arrangementId, Ammount = ammount, DateTime = DateTime.Now });
        }
        
        public static DataBase PeekDataBase()
        {
            return _dataBase;
        }

        private static List<ClientStatus> GetArrearsClientStatuses(int managerId)
        {
            var statuses = new List<ClientStatus>();

            var arrangements = _dataBase.Arrangements.Where(a => a.ManagerId == managerId);

            foreach (var arrangement in arrangements)
            {
                double payments = _dataBase.Payments.Where(p => p.ArrangementId == arrangement.Id).Sum(p => p.Ammount);
                
                if(payments < arrangement.RentPerWeek)
                {
                    statuses.Add(new ClientStatus
                    {
                        Status = "ARREARS",
                        Client = _dataBase.Clients.Where(c => c.Id == arrangement.ClientId).FirstOrDefault(),
                        Property = _dataBase.Properties.Where(p => p.Id == arrangement.PropertyId).FirstOrDefault()
                    });
                }
            }

            return statuses;
        }

        private static List<ClientStatus> GetPaidClientStatuses(int managerId)
        {
            var statuses = new List<ClientStatus>();

            var arrangements = _dataBase.Arrangements.Where(a => a.ManagerId == managerId);

            foreach (var arrangement in arrangements)
            {
                double payments = _dataBase.Payments.Where(p => p.ArrangementId == arrangement.Id).Sum(p => p.Ammount);

                if (payments >= arrangement.RentPerWeek)
                {
                    statuses.Add(new ClientStatus
                    {
                        Status = "PAID",
                        Client = _dataBase.Clients.Where(c => c.Id == arrangement.ClientId).FirstOrDefault(),
                        Property = _dataBase.Properties.Where(p => p.Id == arrangement.PropertyId).FirstOrDefault()
                    });
                }
            }

            return statuses;
        }
    }
}
