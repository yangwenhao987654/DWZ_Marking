using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogTool;
using MarkingApp.Core;
using ScanApp.DAL.DBContext;
using SqlSugar;

namespace MarkingApp.DAL.Services
{
    public class RuleService
    {

        private readonly SqlSugarClient _db;

        public RuleService()
        {
            _db =MyDbContext.Instance;
        }

        public void SaveProductRule(ProductRule productRule, List<Rule> rules)
        {
            _db.BeginTran();
            try
            {
                var productRuleId = _db.Insertable(productRule).ExecuteReturnIdentity();
                productRule.Id = productRuleId; // 手动将返回的 ID 赋值到对象
                foreach (var rule in rules)
                {
                    rule.ProductRuleId = productRuleId;
                    _db.Insertable(rule).ExecuteCommand();
                }
                _db.CommitTran();
            }
            catch (Exception ex)
            {
                _db.RollbackTran(); 
                LogMgr.Instance.Error($"SaveProductRule()->保存失败：{ex.Message}");
            }
        }

        public List<ProductRule> GetProductRules()
        {
            return _db.Queryable<ProductRule>().ToList();
        }

        public List<Rule> GetRulesByProductId(int productRuleId)
        {
            return _db.Queryable<Rule>().Where(r => r.ProductRuleId == productRuleId).ToList();
        }


    }
}
