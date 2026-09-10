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

                menu.Productos.Add(producto);

            }

            return menu;

        }
        static void printMenu<T>(Menu<T> menu)//, Cliente<T> cliente)
        {
            int i = 0;
            bool done = true;
            
            Pedido<T> orden = new Pedido<T>();

                while (done)
                {
                    Console.Clear();

                    WriteLine(
                        "{0} - {1} - ${2}",
                        menu.Productos[i].Id,
                        menu.Productos[i].Nombre,
                        menu.Productos[i].Precio
                    );

                    WriteLine();
                    WriteLine("← → para moverte | Enter para seleccionar | Esc para salir | O para ver su orden | E para eliminar de tu orden");

                    ConsoleKey tecla = ReadKey(true).Key;

                    if (tecla == ConsoleKey.RightArrow)
                    {
                        i++;

                        if (i >= menu.Productos.Count)
                            i = 0;
                    }
                    else if (tecla == ConsoleKey.LeftArrow)
                    {
                        i--;

                        if (i < 0)
                            i = menu.Productos.Count - 1;
                    }
                    else if (tecla == ConsoleKey.Enter)
                    {
                        addOrder(menu.Productos[i], orden);
                        Console.Clear();
                        
                    }
                    else if (tecla == ConsoleKey.O)
                    {
                        WriteLine("\nTu Orden:");

                        for (int z = 0; z < orden.Ordenes.Count; z++)
                         {
                             WriteLine(
                                 "{0} - {1} | Cantidad: {2} | Precio: ${3}",
                                    orden.Ordenes[z].Id,
                                    orden.Ordenes[z].Nombre,
                                    orden.Ordenes[z].Cantidad,
                                    orden.Ordenes[z].Precio
                             );
                             
                        }
                        ReadKey();
                    }
                    else if (tecla == ConsoleKey.E)
                    {
                        WriteLine("");
                        if (orden.Ordenes.Count == 0)
                        {
                            WriteLine("Tu orden está vacía.");
                            ReadKey();
                            continue;
                        }

                        // mostrar orden
                        for (int z = 0; z < orden.Ordenes.Count; z++)
                        {
                            WriteLine(
                                "{0} - {1} | Cantidad: {2} | Precio: ${3}",
                                z,
                                orden.Ordenes[z].Nombre,
                                orden.Ordenes[z].Cantidad,
                                orden.Ordenes[z].Precio
                            );
                        }

                        WriteLine("\nEscribe el número del producto que deseas quitar:");

                        if (!int.TryParse(ReadLine(), out int eliminar))
                            continue;

                        if (eliminar >= 0 && eliminar < orden.Ordenes.Count)
                        {
                            orden.Ordenes.RemoveAt(eliminar);
                        }
                    }
                    else if (tecla == ConsoleKey.Escape)
                    {
                       done = false;
                       WriteLine("Pedido completado, saliendo del menu.....");
                       ReadKey();
                       Console.Clear();
                    }
                }


        }

        static void addOrder<T>(Producto<T> producto, Pedido<T> orden)
        {
            Console.Clear();
             Producto<T> copia = new Producto<T>
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio
            };

            WriteLine("Ha seleccionado el siguiente Producto:\n");

            WriteLine(
                "{0} - {1} | Cantidad: {2} | Precio: ${3}",
                copia.Id,
                copia.Nombre,
                copia.Cantidad,
                copia.Precio
            );

            WriteLine("¿Cuantos deseas llevar?\n");

            int cantidad;

            while (!int.TryParse(ReadLine(), out cantidad) || cantidad <= 0)
            {
                WriteLine("Ingresa una cantidad válida:");
            }

            copia.Cantidad = cantidad;
            copia.Precio = cantidad*copia.Precio;
            Console.Clear();
            WriteLine("Se ha agregado a su orden el siguiente:\n");
            WriteLine("Cantidad: " + copia.Cantidad);
            WriteLine("Precio: $" + copia.Precio);

            orden.Ordenes.Add(copia);
            ReadKey();
            Console.Clear();
        }


        static void Main(string[] args)
        {
            //crea o pide el usuario al cual acceder
            //string nombreUsuario = protocoloInicio();
            //luego se crea el cliente si es que fue creado
            //Cliente<string> cliente = crearCliente(nombreUsuario);

            //se lee el json con los platillos y se deserializa a una lista de objetos PlatillosJson
            string json = File.ReadAllText("platillos.json");
            List<PlatillosJson> jsonMenu = JsonSerializer.Deserialize<List<PlatillosJson>>(json);
            Menu<string> menu = convertirMenu<string>(jsonMenu);
            printMenu(menu);


            //platillosObjetos()
            //mostrarMenu(menu);











            //WriteLine("Cliente creado: {0}", cliente.nombre);






        }   
    }

}