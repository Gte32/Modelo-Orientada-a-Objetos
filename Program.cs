using static System.Console;
using System.Text.Json;

namespace Negocio

{

    class Program
    {
        //debe ser static para no crear otro objeto
        public static string protocoloInicio()
        {
            WriteLine("Bienvenido a Rappi 2, porfavor ingrese su usuario o cree uno nuevo ");
            WriteLine("1 - Crear usuario");
            WriteLine("2 - Iniciar sesion");
            WriteLine("Cualquier otra tecla para salir");

            string opcion = ReadLine();
            if (opcion == "1")
            {
                WriteLine("Ingrese su nombre de usuario a crear");
                string nombre = ReadLine();
                string contraseña = ReadLine();
                return nombre;
            }
            else if (opcion == "2")
            {
                WriteLine("Ingrese su nombre de usuario para iniciar sesion");
                string nombre = ReadLine();
                return nombre;
            }
            else
            {
                WriteLine("Saliendo del programa");
                return null;
            }

        }

        static void mostrarJsonMenu(List<PlatillosJson> menu)
        {
            WriteLine("Menu de platillos disponibles:");
            for(int i = 0; i < menu.Count; i++)
            {
                WriteLine(" {0} - {1} - ${2}", menu[i].Id, menu[i].Nombre, menu[i].Precio);
            }
        }

        public static Cliente<string> crearCliente(string nombre)
        {
            Cliente<string> cliente = new Cliente<string>(nombre);
            return cliente;
        }

        public static Menu<T> convertirMenu<T>(List<PlatillosJson> jsonMenu)
        {
            Menu<T> menu = new Menu<T>();

            for(int i = 0; i < jsonMenu.Count; i++)
            {
                Producto<T> producto = new Producto<T>
                {
                    Precio = jsonMenu[i].Precio,
                    Id = jsonMenu[i].Id,
                    Cantidad = jsonMenu[i].Cantidad,
                    Nombre = jsonMenu[i].Nombre


                };

                menu.productos.Add(producto);

            }

            return menu;

        }
        static void printMenu()
        {
            //for i 
        }


        static void Main(string[] args)
        {
            //crea o pide el usuario al cual acceder
            string nombreUsuario = protocoloInicio();

            //luego se crea el cliente si es que fue creado
            Cliente<string> cliente = crearCliente(nombreUsuario);

            //se lee el json con los platillos y se deserializa a una lista de objetos PlatillosJson
            string json = File.ReadAllText("platillos.json");
            List<PlatillosJson> jsonMenu = JsonSerializer.Deserialize<List<PlatillosJson>>(json);
            Menu<string> menu = convertirMenu<string>(jsonMenu);



            //platillosObjetos()
            //mostrarMenu(menu);











            //WriteLine("Cliente creado: {0}", cliente.nombre);






        }   
    }

}