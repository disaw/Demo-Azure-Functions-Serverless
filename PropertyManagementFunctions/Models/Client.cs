using System.ComponentModel;

namespace PropertyManagementFunctions.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PropertyId { get; set; }

        public int ArrangementId { get; set; }
    }
}
