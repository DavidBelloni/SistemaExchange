using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ITransferService
    {
        void Transferir(Domain.Cuenta cuentaOrigen, Domain.Cuenta cuentaDestino, decimal monto);
        void Operacion(Domain.Cuenta cuentaOrigen, Domain.Cuenta cuentaDestino, decimal monto, Domain.TipoOperacion tipoOperacion);
        decimal ConvertirPesosABTC(decimal monto);
        decimal ConvertirBTCAPesos(decimal monto);

    }
}
