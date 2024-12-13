using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingApp.Core
{
    public class Rule
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public int ProductRuleId { get; set; } // 外键，关联到ProductRule


        public RuleType Type { get; set; }

        public string Content { get; set; }// 固定文本的值或日期格式

        public int SerialLength { get; set; }// 流水号位数


        /// <summary>
        /// 流水号累加规则
        /// "Daily", "Monthly", "Global"
        /// </summary>
        public string IncrementType { get; set; }// "Daily", "Monthly", "Global"
    }

    /// <summary>
    /// 流水号增长类型
    /// </summary>
    public class SerialIncrementType
    {
        public const string Daily = "Daily";

        public const string Monthly = "Monthly";

        public const string Global = "Global";
    }
}
