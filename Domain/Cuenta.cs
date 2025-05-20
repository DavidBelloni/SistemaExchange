using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Cuenta
    {
        public decimal Saldo { get; protected set; }

        public Cliente Cliente { get; set; }

        public void Depositar(decimal monto)
        {
            if (monto < 0)
                throw new ArgumentException("El monto a depositar debe ser positivo.");
            Saldo += monto;
        }

        public void Retirar(decimal monto)
        {
            if (monto < 0)
                throw new ArgumentException("El monto a retirar debe ser positivo.");
            if (Saldo < monto)
                throw new InvalidOperationException("Saldo insuficiente.");
            Saldo -= monto;
        }

        // Metodo para actualizar cuenta luego de una transferencia
        public void ActualizarCuenta(Cuenta cuentaOrigen, Cuenta cuentaDestino)
        {
            // Actualizar la cuenta origen
            cuentaOrigen.Retirar(cuentaOrigen.Saldo);
            // Actualizar la cuenta destino
            cuentaDestino.Depositar(cuentaDestino.Saldo);

        }


        //public abstract void Accept(ICuentaVisitor visitor);
        //public abstract void AcceptOrigen(ITransferenciaVisitor visitor);
    }
}
