using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoninDigital.Models
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
        }

        public GoninDigitalDBContext Db { get; private set; }

        private DataProvider()
        {
            Db = new GoninDigitalDBContext();
        }
    }
}
