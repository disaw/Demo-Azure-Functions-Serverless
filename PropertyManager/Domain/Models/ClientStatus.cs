namespace PropertyManager.Domain.Models
{
    public class ClientStatus
    {
        public string Status { get; set; }

        public Client Client { get; set; }

        public Property Property { get; set; }
    }
}
