using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Negocio
{
    
    internal class Producto<T> : IProducto<T>
    {
        public int Precio {get;set;}
        public int Cantidad {get;set;}
        public int Id {get;set;}
        public string Nombre {get;set;}
    }

    internal struct Menu<T> : IMenu<T>
    {
        public List<Producto<T>> productos => productos;
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
        public Producto<T> ordenes => ordenes; //lista de productosW ordenados
    }


}