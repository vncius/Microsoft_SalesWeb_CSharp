using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do {0} deve ser entre {2} e {1}.")]
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller) 
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final) 
        {
            return Sellers.Sum(s => s.TotalSales(initial, final));
        }
    }
}
