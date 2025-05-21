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

            Console.WriteLine("Antes de convertir de $ a BTC");
            Console.WriteLine($"Caja Ahorro $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Monedero BTC: {mon.Saldo} \n");

            service.Transferir(cajaAhorro, mon, 10000);
            service.Operacion(cajaAhorro, mon, 10000, TipoOperacion.Conversion);

            Console.WriteLine("Luego de hacer la conversion");
            Console.WriteLine($"Caja Ahorro $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Monedero BTC: {mon.Saldo} \n");

            service.Transferir(mon, cajaAhorro, (decimal)0.00008338);
            service.Operacion(mon, cajaAhorro, (decimal)0.00008338, TipoOperacion.Conversion);

            Console.WriteLine("Luego de hacer la conversión a saldo inicial");
            Console.WriteLine($"Caja Ahorro $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Monedero BTC: {mon.Saldo} \n");

            Cliente cliente2 = new Cliente("20392120187", "María");
            CajaAhorro cajaAhorro2 = new CajaAhorro("2222222222222222222222");
            cliente2.AgregarCuenta(cajaAhorro2);

            Console.WriteLine("Antes de transferir de $ a $");
            Console.WriteLine($"Caja Ahorro 1 $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Caja Ahorro 2 $: {cajaAhorro2.Saldo} \n");

            service.Transferir(cajaAhorro, cajaAhorro2, 50000);
            service.Operacion(cajaAhorro, cajaAhorro2, 50000, TipoOperacion.TransferenciaATerceros);

            Console.WriteLine("Después de transferir de $ a $");
            Console.WriteLine($"Caja Ahorro 1 $: {cajaAhorro.Saldo}");
            Console.WriteLine($"Caja Ahorro 2 $: {cajaAhorro2.Saldo} \n");

            // Imprimir por pantalla la lista de operaciones
            Console.WriteLine("Resumen de movimientos de cuentas: \n");
            foreach (var op in TransferService.Operaciones)
            {
                op.MostrarInformacion();
                Console.WriteLine();
            }

        }
    }
}
