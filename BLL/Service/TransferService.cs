﻿using BLL.Interface;
using DAL.Factory;
using DAL.Implementation.Memory;
using DAL.Interface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class TransferService : ITransferService
    {
        
        private readonly ITransferRepository transferRepository; 

        public TransferService()
        {
            transferRepository = FactoryRepository.TransferRepository;
        }

        public void RegistrarCuenta(Cuenta cuenta)
        {
            if (cuenta == null) throw new ArgumentNullException(nameof(cuenta));
            transferRepository.AgregarCuenta(cuenta);
        }

        public void Transferir(Cuenta cuentaOrigen, Cuenta cuentaDestino, decimal monto)
        {
            if (cuentaOrigen == null) throw new ArgumentNullException(nameof(cuentaOrigen));
            if (cuentaDestino == null) throw new ArgumentNullException(nameof(cuentaDestino));
            if (monto <= 0) throw new ArgumentException("El monto debe ser mayor a cero.", nameof(monto));
            if (cuentaOrigen.Saldo < monto) throw new InvalidOperationException("Saldo insuficiente en la cuenta origen.");

            bool mismoTitular = cuentaOrigen.Cliente.CUIT == cuentaDestino.Cliente.CUIT;
            bool esConversion = cuentaOrigen.GetType() != cuentaDestino.GetType();

            if (esConversion && !mismoTitular)
            {
                throw new InvalidOperationException("Conversiones sólo permitidas entre cuentas del mismo titular.");
            }

            // Retirar saldo primero para evitar problemas
            cuentaOrigen.Retirar(monto);

            // Procesar la transferencia
            var operacion = ProcesarTransferencia((dynamic)cuentaOrigen, (dynamic)cuentaDestino, monto);

            // Imprimimos la operacion gracias el metodo ToString() de la clase Operacion
            Console.WriteLine(operacion);

            //Ir al repositorio
            //1) Actualizar la cuenta origen
            //2) Actualizar la cuenta destino
            //3) Insertar la operacion
            //commit tran
            // Guardar operación, persistencia, etc.

            transferRepository.ActualizarOrigen(cuentaOrigen);
            transferRepository.ActualizarDestino(cuentaDestino);
            transferRepository.Insertar(operacion);


        }

        public Operacion ProcesarTransferencia(CajaAhorro cuentaOrigen, CajaAhorro cuentaDestino, decimal monto)
        {
            cuentaDestino.Depositar(monto);
            return new Operacion(cuentaOrigen, cuentaDestino, DateTime.Now, monto, TipoOperacion.TransferenciaATerceros);
        }
        public Operacion ProcesarTransferencia(MonederoBTC cuentaOrigen, MonederoBTC cuentaDestino, decimal monto)
        {
            cuentaDestino.Depositar(monto);
            return new Operacion(cuentaOrigen, cuentaDestino, DateTime.Now, monto, TipoOperacion.TransferenciaATerceros);
        }
        public Operacion ProcesarTransferencia(CajaAhorro cuentaOrigen, MonederoBTC cuentaDestino, decimal monto)
        {
            decimal montoConvertido = ConvertirPesosABTC(monto);
            cuentaDestino.Depositar(montoConvertido);
            return new Operacion(cuentaOrigen, cuentaDestino, DateTime.Now, montoConvertido, TipoOperacion.Conversion);
        }
        public Operacion ProcesarTransferencia(MonederoBTC cuentaOrigen, CajaAhorro cuentaDestino, decimal monto)
        {
            decimal montoConvertido = ConvertirBTCAPesos(monto);
            cuentaDestino.Depositar(montoConvertido);
            return new Operacion(cuentaOrigen, cuentaDestino, DateTime.Now, montoConvertido, TipoOperacion.Conversion);
        }
        public Operacion ProcesarTransferencia(Cuenta cuentaOrigen, Cuenta cuentaDestino, decimal monto)
        {
            throw new NotImplementedException($"No se soporta transferencia de {cuentaOrigen.GetType().Name} a {cuentaDestino.GetType().Name}");
        }

        public decimal ConvertirPesosABTC(decimal montoPesos)
        {
            const decimal tasaPesosABtc = 0.000000008338m; // tasa fija ejemplo
            return montoPesos * tasaPesosABtc;
        }

        public decimal ConvertirBTCAPesos(decimal montoBtc)
        {
            const decimal tasaBtcAPesos = 119928832; // tasa fija ejemplo
            return montoBtc * tasaBtcAPesos;
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            return transferRepository.ObtenerCuentas();
        }

        public IEnumerable<Operacion> ObtenerOperaciones()
        {
            return transferRepository.ObtenerOperaciones();
        }
    }
}
