using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Negocio
{
    
    internal struct Platillo<T> : IPlatillo<T>
    {
        public int precio => precio;
        public int cantidad => cantidad;
        public int id => id;
        public string nombre => nombre;

    }

    internal struct Menu<T> : IMenu<T>
    {
        public Platillo<T> platillos => platillos;
    }

    internal struct Cliente<T> : ICliente<T>
    {
        public string nombre {get;set;}
        public List<Pedido<T>> pedidos {get;set;} 

        public Cliente(string nombre)
        {
            this.nombre = nombre;
            pedidos = new List<Pedido<T>>();
        }
    }

    internal struct Pedido<T> : IPedido<T>
    {
        public Platillo<T> ordenes => ordenes; //lista de platillos ordenados
    }


}