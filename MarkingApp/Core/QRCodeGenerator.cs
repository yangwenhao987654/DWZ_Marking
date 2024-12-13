using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ApplicationServices;
using MarkingApp.DAL.Services;

namespace MarkingApp.Core
{
    public class QRCodeGenerator
    {
        private readonly RuleService ruleService;

        private readonly SerialService serialService;

        public QRCodeGenerator(RuleService ruleService)
        {
            this.ruleService = ruleService;
        }

        public QRCodeGenerator()
        {
            ruleService = new RuleService();

            serialService = new SerialService();
        }

        public string GenerateQRCode(int productId)
        {
            var rules = ruleService.GetRulesByProductId(productId);
            var qrCode = new StringBuilder();
            foreach (var rule in rules)
            {
                switch (rule.Type)
                {
                    case RuleType.FixedText:
                        qrCode.Append(rule.Content);
                        break;
                    case RuleType.Date:
                        qrCode.Append(DateTime.Now.ToString(rule.Content));
                        break;
                    case RuleType.SerialNumber:
                        qrCode.Append(GenerateSerialNumber(rule));
                        break;
                }
            }

            return qrCode.ToString();
        }

        private string GenerateSerialNumber(Rule rule)
        {
            string key;
            switch (rule.IncrementType)
            {
                case SerialIncrementType.Daily:
                    key = $"{rule.ProductRuleId}_{rule.IncrementType}_{DateTime.Now:yyyyMMdd}";
                    break;
                case SerialIncrementType.Monthly:
                    key = $"{rule.ProductRuleId}_{rule.IncrementType}_{DateTime.Now:yyyyMM}";
                    break;
                case SerialIncrementType.Global:
                    key = $"{rule.ProductRuleId}_{rule.IncrementType}";
                    break;
                default:
                    throw new InvalidOperationException("Unsupported IncrementType");
            }

            var serial = serialService.GetSerialNumber(key);
            if (serial == null)
            {
                serial = new SerialNumber { Key = key, CurrentValue = 1 };
                serialService.SaveSerialNumber(serial);
            }
            else
            {
                serial.CurrentValue++;
                serialService.UpdateSerialNumber(serial);
            }

            return serial.CurrentValue.ToString().PadLeft(rule.SerialLength, '0');
        }

    }
}
