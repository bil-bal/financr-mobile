using System;
using System.Collections.Generic;
using System.Text;

namespace financr.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
