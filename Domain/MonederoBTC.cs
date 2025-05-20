using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MonederoBTC : Cuenta
    {
        public string Direccion { get; }

        public MonederoBTC(string direccion)
        {
            Direccion = direccion;
            Saldo = 0m;
        }



        //public override void Accept(ICuentaVisitor visitor)
        //{
        //    visitor.VisitMonederoBTC(this);
        //}

        //public override void AcceptOrigen(ITransferenciaVisitor visitor)
        //{
        //    visitor.VisitDesdeMonederoBTC(this);
        //}
    }
}
