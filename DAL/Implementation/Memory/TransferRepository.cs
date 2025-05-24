using DAL.Interface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation.Memory
{
    public class TransferRepository : ITransferRepository
    {
        #region singleton
        private readonly static TransferRepository _instance = new TransferRepository();
        public static TransferRepository Current
        {
            get
            {
                return _instance;
            }
        }
        private TransferRepository()
        {

        }
        #endregion

        private List<Cuenta> cuentas = new List<Cuenta>();
        private List<Operacion> operaciones = new List<Operacion>();


        public void AgregarCuenta(Cuenta cuenta)
        {
            bool existe = cuentas.Any(c =>
                c.Cliente.CUIT == cuenta.Cliente.CUIT &&
                c.GetType() == cuenta.GetType() &&
                IdentificadorCuenta(c) == IdentificadorCuenta(cuenta)
            );

            if (!existe)
                cuentas.Add(cuenta);
            else
                throw new Exception("La cuenta ya existe");
        }

        public void Insertar(Operacion operacion)
        {
            if (operacion == null) throw new ArgumentNullException(nameof(operacion));
            operaciones.Add(operacion);
        }

        public void ActualizarOrigen(Cuenta cuentaOrigen)
        {
            var cuenta = cuentas.FirstOrDefault(c =>
                c.Cliente.CUIT == cuentaOrigen.Cliente.CUIT &&
                c.GetType() == cuentaOrigen.GetType() &&
                IdentificadorCuenta(c) == IdentificadorCuenta(cuentaOrigen)
            );
            if (cuenta != null)
            {
                cuenta.ActualizarSaldo(cuentaOrigen.Saldo);
            }
            else
            {
                throw new Exception("Cuenta no encontrada");
            }
        }

        public void ActualizarDestino(Cuenta cuentaDestino)
        {
            var cuenta = cuentas.FirstOrDefault(c =>
                c.Cliente.CUIT == cuentaDestino.Cliente.CUIT &&
                c.GetType() == cuentaDestino.GetType() &&
                IdentificadorCuenta(c) == IdentificadorCuenta(cuentaDestino)
            );
            if (cuenta != null)
            {
                cuenta.ActualizarSaldo(cuentaDestino.Saldo);
            }
            else
            {
                throw new Exception("Cuenta no encontrada");
            }
        }

        public string IdentificadorCuenta(Cuenta cuenta)
        {
            if (cuenta is CajaAhorro ca)
                return ca.CBU;
            if (cuenta is MonederoBTC btc)
                return btc.Direccion;

            // Agrega otros tipos si es necesario
            return null;
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return cuentas;
        }

        public IEnumerable<Operacion> ObtenerOperaciones()
        {
            return operaciones;
        }
    }
}
