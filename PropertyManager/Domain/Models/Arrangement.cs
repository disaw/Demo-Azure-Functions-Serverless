using System;

namespace PropertyManager.Domain.Models
{
    public class Arrangement
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int ManagerId { get; set; }

        public int ClientId { get; set; }

        public double RentPerWeek { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
