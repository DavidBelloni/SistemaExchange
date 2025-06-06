﻿using System;
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
        public void ActualizarSaldo(decimal nuevoSaldo)
        {
            Saldo = nuevoSaldo;
        }


        //public abstract void Accept(ICuentaVisitor visitor);
        //public abstract void AcceptOrigen(ITransferenciaVisitor visitor);
    }
}
