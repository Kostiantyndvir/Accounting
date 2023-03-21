using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    public abstract class Category 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CostCategory : Category
    {
    }

    public class ProfitCategory : Category
    {
    }
    public abstract class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sum { get; set; }
        public DateOnly Data { get; set; }
    }

    public class Profit : Item
    {
        public ProfitCategory category { get; set; }
    }
    public class Cost : Item
    {
        public CostCategory category { get; set; }
    }

    // for diagrams
    public class Column
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class ViewModel
    {
        public List<Column> Data { get; set; }
        public ViewModel()
        {
            Data = new List<Column>();
        }
    }
}
