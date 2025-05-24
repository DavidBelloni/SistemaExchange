using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ITransferService
    {
        void Transferir(Cuenta origen, Cuenta destino, decimal monto);
        Operacion ProcesarTransferencia(Cuenta origen, Cuenta destino, decimal monto);
        decimal ConvertirPesosABTC(decimal monto);
        decimal ConvertirBTCAPesos(decimal monto);


    }
}
