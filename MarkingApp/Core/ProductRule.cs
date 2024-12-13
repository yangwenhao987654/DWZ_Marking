using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingApp.Core
{
    public class ProductRule
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

     
        public string ProductName { get; set; }

        public List<Rule> Rules { get; set; }=new List<Rule>();
    }
}
