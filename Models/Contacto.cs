using System;

namespace contactos.Models
{
    public class Contacto
    {
        public long? id {get; set;}
        public string nombre {get; set;}
        public string email {get; set;}
        public DateTime? nace {get; set;}
        public string mensaje {get; set;}
    }
}