using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public  interface ITransferRepository
    {
        void ActualizarOrigen(Cuenta cuentaOrigen);
        void ActualizarDestino(Cuenta cuentaDestino);
        void Insertar(Operacion operacion);
        void AgregarCuenta(Cuenta cuenta);
        IEnumerable<Cuenta> ObtenerCuentas();
        IEnumerable<Operacion> ObtenerOperaciones();

    }

}
