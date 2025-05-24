using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public class FactoryRepository
    {
        private static int backendType = 0;

        static FactoryRepository()
        {

            backendType = int.Parse(ConfigurationManager.AppSettings["BackendType"]);
        }

        public static ITransferRepository TransferRepository
        {
            get
            {
                if (backendType == (int)BackendType.Memory)
                    return DAL.Implementation.Memory.TransferRepository.Current;

                else
                     throw new Exception("BackendType no disponible");
            }
        }

    }



    internal enum BackendType
    {
        Memory,
        SqlServer
    }
}
