using System;

namespace PropertyManager.Domain.Models
{
    public class PaySchedule
    {
        public int Id { get; set; }

        public int ArrangementId { get; set; }

        public DateTime DueDate { get; set; }

        public double Balance { get; set; }
    }
}
