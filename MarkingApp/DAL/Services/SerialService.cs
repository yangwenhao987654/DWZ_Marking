using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkingApp.Core;
using ScanApp.DAL.DBContext;
using SqlSugar;

namespace MarkingApp.DAL.Services
{
    public class SerialService
    {
        private readonly SqlSugarClient _db;
        public SerialService()
        {
            _db = MyDbContext.Instance;
        }

        public SerialNumber GetSerialNumber(string key)
        {
            SerialNumber serialNumber = _db.Queryable<SerialNumber>().First(r => r.Key == key);
            return serialNumber;
        }

        public void SaveSerialNumber(SerialNumber serialNumber)
        {
            _db.Insertable<SerialNumber>(serialNumber).ExecuteCommand();
        }

        public void UpdateSerialNumber(SerialNumber serialNumber)
        {
            _db.Updateable(serialNumber).Where(r => r.ID == serialNumber.ID).ExecuteCommand();

        }
    }
}
