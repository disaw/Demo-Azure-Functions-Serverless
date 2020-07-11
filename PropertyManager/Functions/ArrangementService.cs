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
            int paymentId = _dataBase.Payments.Any() ? _dataBase.Payments.Max(p => p.Id) + 1 : 1;
            var arrangement = _dataBase.Arrangements.Where(a => a.Id == arrangementId).FirstOrDefault();

            _dataBase.Payments.Add(new Payment { Id = paymentId, ArrangementId = arrangementId, Ammount = ammount, DateTime = DateTime.Now });

            var paySchedule = _dataBase.PaySchedules.Where(s => s.ArrangementId == arrangementId).LastOrDefault();
            double balance = paySchedule.Balance = paySchedule.Balance - ammount;            

            while (balance < 1) //Setting future pay schedules
            {                
                int payScheduleId = _dataBase.PaySchedules.Any() ? _dataBase.PaySchedules.Max(p => p.Id) + 1 : 1;
                var lastPaySchedule = _dataBase.PaySchedules.Where(s => s.ArrangementId == arrangementId).LastOrDefault();

                if (arrangement.End > lastPaySchedule.DueDate)
                {
                    _dataBase.PaySchedules.Add(new PaySchedule
                    {
                        Id = payScheduleId,
                        ArrangementId = arrangementId,
                        DueDate = lastPaySchedule.DueDate.AddDays(7),
                        Balance = arrangement.RentPerWeek + lastPaySchedule.Balance
                    });

                    balance = arrangement.RentPerWeek + lastPaySchedule.Balance;
                }
                else
                {
                    break;
                }                
            }
        }
        
        public static DataBase PeekDataBase(bool reset = false)
        {
            if(reset)
            {
                _dataBase = new DataBase();
            }

            return _dataBase;
        }

        private static List<ClientStatus> GetArrearsClientStatuses(int managerId)
        {
            var statuses = new List<ClientStatus>();

            var arrangements = _dataBase.Arrangements.Where(a => a.ManagerId == managerId);

            foreach (var arrangement in arrangements)
            {
                var paySchedule = _dataBase.PaySchedules.Where(s => s.ArrangementId == arrangement.Id).LastOrDefault();

                if (paySchedule.DueDate < DateTime.Now)
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
                var paySchedule = _dataBase.PaySchedules.Where(s => s.ArrangementId == arrangement.Id).LastOrDefault();

                if (paySchedule.DueDate > DateTime.Now)
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
