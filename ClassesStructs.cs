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
        public string Nombre { get; set; } = "";

        public void Mostrar()
        {
            WriteLine(
                "{0} - {1} | Cantidad: {2} | Precio: ${3}",
                Id,
                Nombre,
                Cantidad,
                Precio
            );
        }

        
    }

    internal struct Menu<T> : IMenu<T>
    {
        public List<Producto<T>> Productos{get;set;}

        public Menu()
        {
            Productos = new List<Producto<T>>();
        }

         public void Mostrar()
        {
            WriteLine("MENU:");

            foreach (Producto<T> producto in Productos)
            {
                producto.Mostrar();
            }
        }
    }

    internal struct Cliente<T> : ICliente<T>
    {
        public string nombre {get;set;}
        public List<Pedido<T>> Pedidos {get;set;} 

        public Cliente(string nombre)
        {
            this.nombre = nombre;
            Pedidos = new List<Pedido<T>>();
        }

        public void Mostrar()
        {
            WriteLine("CLIENTE: " + nombre);

            for (int i = 0; i < Pedidos.Count; i++)
            {
                WriteLine("\nPedido #{0}", i + 1);
                Pedidos[i].Mostrar();
            }
        }


    }

    internal struct Pedido<T> : IPedido<T>
    {
        public List<Producto<T>> Ordenes {get;set;} //

        public Pedido()
        {
            Ordenes = new List<Producto<T>>();
        } 
        public void Mostrar()
        {
            WriteLine("PEDIDO:");

            foreach (Producto<T> producto in Ordenes)
            {
                producto.Mostrar();
            }
        }
    }


}