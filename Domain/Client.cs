namespace Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public ClientType ClientType { get; set; }
        public Address Address { get; set; }
        
    }
}