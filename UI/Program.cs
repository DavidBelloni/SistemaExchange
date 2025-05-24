using BLL.Service;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            Operacion operacion = null;
            TransferService service = new TransferService();

            Cliente cliente = new Cliente("20354746418", "David");
            CajaAhorro cajaAhorro = new CajaAhorro("1111111111111111111111");
            cajaAhorro.Depositar(1000000);
            cliente.AgregarCuenta(cajaAhorro);

            MonederoBTC mon = new MonederoBTC("YYYY-YYYY");
            cliente.AgregarCuenta(mon);

            // TRANSFERENCIA DE CA A BTC
            // (ANTES)
            Console.WriteLine("Antes de convertir de $ a BTC");
            Console.WriteLine($"Caja Ahorro $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Monedero BTC: {mon.Saldo} \n");

            // PROCESO + REGISTRO DE OPERACION
            service.Transferir(cajaAhorro, mon, 10000);

            // (DESPUES)
            Console.WriteLine("Luego de hacer la conversion");
            Console.WriteLine($"Caja Ahorro $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Monedero BTC: {mon.Saldo} \n");

            // TRANSFERENCIA DE BTA A CA

            // PROCESO + REGISTRO DE OPERACION
            service.Transferir(mon, cajaAhorro, (decimal)0.00008338);

            // (DESPUES)
            Console.WriteLine("Luego de hacer la conversión a saldo inicial");
            Console.WriteLine($"Caja Ahorro $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Monedero BTC: {mon.Saldo} \n");

            // CREACION DE CLIENTE Y CUENTA
            Cliente cliente2 = new Cliente("20392120187", "María");
            CajaAhorro cajaAhorro2 = new CajaAhorro("2222222222222222222222");
            cliente2.AgregarCuenta(cajaAhorro2);

            // TRANSFERENCIA DE CA A CA
            // ANTES
            Console.WriteLine("Antes de transferir de $ a $");
            Console.WriteLine($"Caja Ahorro 1 $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Caja Ahorro 2 $: {cajaAhorro2.Saldo} \n");

            // PROCESO + REGISTRO DE OPERACION
            service.Transferir(cajaAhorro, cajaAhorro2, 50000);

            // DESPUES
            Console.WriteLine("Después de transferir de $ a $");
            Console.WriteLine($"Caja Ahorro 1 $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Caja Ahorro 2 $: {cajaAhorro2.Saldo} \n");


            // CREACION DE CLIENTE Y CUENTA
            Cliente cliente3 = new Cliente("402003007", "Lucas");
            MonederoBTC mon3 = new MonederoBTC("ZZZZ-ZZZZ");
            MonederoBTC mon4 = new MonederoBTC("RRRR-RRRR");
            mon3.Depositar(1);
            mon4.Depositar(0);
            cliente3.AgregarCuenta(mon3);
            cliente3.AgregarCuenta(mon4);

            // TRANSFERENCIA DE BTC A BTC
            // (ANTES)
            Console.WriteLine("Antes de transferir de BTC a BTC");
            Console.WriteLine($"Monedero BTC 1: {mon3.Saldo}");
            Console.WriteLine($"Monedero BTC 2: {mon4.Saldo} \n");

            // PROCESO + REGISTRO DE OPERACION
            service.Transferir(mon3, mon4, (decimal)0.5);

            // (DESPUES)
            Console.WriteLine("Despues de transferir BTC a BTC");
            Console.WriteLine($"Monedero BTC 1: {mon3.Saldo}");
            Console.WriteLine($"Monedero BTC 2: {mon4.Saldo} \n");

        }
    }
}
