using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CajaAhorro : Cuenta
    {
        public string CBU { get; }

        public CajaAhorro(string cbu)
        {
            CBU = cbu;
            Saldo = 0m;
        }





        //public override void Accept(ICuentaVisitor visitor)
        //{
        //    visitor.VisitCajaDeAhorro(this);
        //}

        //public override void AcceptOrigen(ITransferenciaVisitor visitor)
        //{
        //    visitor.VisitDesdeCajaDeAhorro(this);
        //}
    }
}
