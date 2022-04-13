using System;
using Microsoft.VisualBasic;

namespace Domain
{
    public class ClientBuysBook
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public Book Book { get; set; }
        public DateTime Date { get; set; }
        public int QuantityBuys { get; set; }
        
    }
}