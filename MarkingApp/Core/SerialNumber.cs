using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingApp.Core
{
    [SugarTable("tbSerialNumber")]
    public class SerialNumber
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        public string Key { get; set; }  // Key = ProductName + IncrementType + 日期

        public int CurrentValue { get; set; }
    }
}
