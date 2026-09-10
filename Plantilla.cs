using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using static System.Console;

namespace  Negocio
{
    internal interface IProducto<T>
    {
        int Precio {get;set;}
        int Id {get;set;}
        int Cantidad {get;set;} 
        string Nombre {get;set;}


    }

    internal interface IMenu<T>
    {
        List<Producto<T>> Productos {get;set;}

    }

    internal interface ICliente<T>
    {
        string nombre {get;}
        List<Pedido<T>> Pedidos {get;set;}


    }

    internal interface IPedido<T>
    {
        List<Producto<T>> Ordenes {get;set;} //lista de productos ordenados

    }


    public class PlatillosJson
    {
        [JsonPropertyName("precio")]
        public int Precio { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";
    }

}