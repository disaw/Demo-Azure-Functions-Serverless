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

            var paidArrangements = _dataBase.Arrangements.Where(a =>
                a.ManagerId == managerId
                && _dataBase.Payments.Any(p => p.ArrangementId == a.Id)
                && _dataBase.Payments.Sum(p => p.Ammount) >= a.RentPerWeek);

            foreach (var paidArrangement in paidArrangements)
            {
                statuses.Add(new ClientStatus { Status = "PAID", 
                    Client = _dataBase.Clients.Where(c => c.Id == paidArrangement.ClientId).FirstOrDefault(),
                    Property = _dataBase.Properties.Where(p => p.Id == paidArrangement.PropertyId).FirstOrDefault()
                });
            }

            statuses.AddRange(GetArrearsClientStatuses(managerId));

            return statuses;
        }

        public static bool IsValidArrangementId(int arrangementId)
        {
            return _dataBase.Arrangements.Select(a => a.Id == arrangementId).FirstOrDefault();
        }

        public static void MakePayment(int arrangementId, double ammount)
        {
            var id = _dataBase.Payments.Select(p => p.Id).FirstOrDefault() + 1;

            _dataBase.Payments.Add(new Payment { Id = id, ArrangementId = arrangementId, Ammount = ammount, DateTime = DateTime.Now });
        }

        private static List<ClientStatus> GetArrearsClientStatuses(int managerId)
        {
            var statuses = new List<ClientStatus>();

            var unpaidArrangements = _dataBase.Arrangements.Where(a =>
                a.ManagerId == managerId
                && !_dataBase.Payments.Any(p => p.ArrangementId == a.Id)
                && _dataBase.Payments.Sum(p => p.Ammount) < a.RentPerWeek);

            foreach (var unpaidArrangement in unpaidArrangements)
            {
                statuses.Add(new ClientStatus
                {
                    Status = "ARREARS",
                    Client = _dataBase.Clients.Where(c => c.Id == unpaidArrangement.ClientId).FirstOrDefault(),
                    Property = _dataBase.Properties.Where(p => p.Id == unpaidArrangement.PropertyId).FirstOrDefault()
                });
            }

            return statuses;
        }        
    }
}
