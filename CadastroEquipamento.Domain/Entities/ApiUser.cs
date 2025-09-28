using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroEquipamento.Domain.Entities
{
    public class ApiUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Address Address { get; set; } = new Address();
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public Company Company { get; set; } = new Company();
    }

    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string Suite { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
    }

    public class Company
    {
        public string Name { get; set; } = string.Empty;
    }
}
