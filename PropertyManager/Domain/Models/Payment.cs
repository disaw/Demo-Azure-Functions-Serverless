using System;

namespace PropertyManager.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int ArrangementId { get; set; }

        public double Ammount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
