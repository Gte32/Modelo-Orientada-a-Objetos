using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace  Negocio
{
    internal interface IPlatillo<T>
    {
        int precio {get;}
        int id {get;}
        int cantidad {get;} 
        string nombre {get;}


    }

    internal interface IMenu<T>
    {
        Platillo<T> platillos {get;}

    }

    internal interface ICliente<T>
    {
        string nombre {get;}
        List<Pedido<T>> pedidos {get;}


    }

    internal interface IPedido<T>
    {
        Platillo<T> ordenes {get;} //lista de platillos ordenados

    }

}