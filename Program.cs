using static System.Console;
using System.Text.Json;

namespace Negocio

{

    class Program
    {

    static Cliente<string>? seleccionarUsuario()
    {
        if (!File.Exists("usuarios.json"))
        {
            WriteLine("No hay usuarios disponibles.");
            return null;
        }

        string json = File.ReadAllText("usuarios.json");

        List<Cliente<string>> usuarios =
            JsonSerializer.Deserialize<List<Cliente<string>>>(json)
            ?? new List<Cliente<string>>();

        if (usuarios.Count == 0)
        {
            WriteLine("No hay usuarios disponibles.");
            return null;
        }

        WriteLine("USUARIOS GUARDADOS:\n");

        for (int i = 0; i < usuarios.Count; i++)
        {
            WriteLine("{0}. {1}", i + 1, usuarios[i].nombre);
        }

        WriteLine("\nSelecciona un usuario:");

        if (!int.TryParse(ReadLine(), out int seleccion))
        {
            return null;
        }

        if (seleccion < 1 || seleccion > usuarios.Count)
        {
            return null;
        }

        return usuarios[seleccion - 1];
    }  

        //debe ser static para no crear otro objeto
    static Cliente<string> protocoloInicio()
    {
        Console.Clear();

        WriteLine("Bienvenido a Rappi 2, porfavor ingrese su usuario o cree uno nuevo");
        WriteLine("1 - Crear usuario");
        WriteLine("2 - Iniciar sesion");
        WriteLine("Cualquier otra tecla para salir");

        string opcion = ReadLine();

        if (opcion == "1")
        {
            Console.Clear();

            WriteLine("Ingrese su nombre de usuario a crear");
            string nombre = ReadLine();

            return crearCliente(nombre);
        }
        else if (opcion == "2")
        {
            Console.Clear();

            Cliente<string>? cliente = seleccionarUsuario();

            if (cliente == null)
            {
                WriteLine("\nNo hay usuarios disponibles.");
                ReadKey();
                Environment.Exit(0);
            }

            return cliente.Value;
        }
        else
        {
            Console.Clear();
            WriteLine("Saliendo del programa");
            Environment.Exit(0);

            return new Cliente<string>();
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
        static void printMenu<T>(Menu<T> menu, Cliente<T> cliente)
        {
            int i = 0;
            bool done = true;
            
            Pedido<T> orden = new Pedido<T>();

                while (done)
                {
                    Console.Clear();
                    WriteLine("Usuario: " + cliente.nombre);
                    WriteLine("Seleccione el Producto para agregarlo a su orden:\n");
                    
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

                        WriteLine("=>Si no escribe un número válido, nada sera eliminado<=");

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
                       Console.Clear();
                       WriteLine("Pedido completado, saliendo del menu.....");
                       cliente.Pedidos.Add(orden);
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

        static void mostrarPedidos(Cliente<string> cliente) 
        { 
            WriteLine("Usuario: " + cliente.nombre);
            WriteLine("\nPEDIDOS DEL CLIENTE:");
                for (int i = 0; i < cliente.Pedidos.Count; i++) 
                {  
                    WriteLine($"\nPedido #{i + 1}"); 
                    foreach (Producto<string> producto in cliente.Pedidos[i].Ordenes) 
                    { 
                        WriteLine( "{0} - {1} | Cantidad: {2} | Precio: ${3}",
                        producto.Id, producto.Nombre, producto.Cantidad, producto.Precio ); 
                    } 
                } 
        }

        static void guardarCliente(Cliente<string> cliente)
        {
            JsonSerializerOptions opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            List<Cliente<string>> usuarios = new List<Cliente<string>>();

            usuarios.Add(cliente);

            string json = JsonSerializer.Serialize(usuarios, opciones);

            File.WriteAllText("usuarios.json", json);
        }


        static void Main(string[] args)
        {
            //crea o pide el usuario al cual acceder
            Cliente<string> cliente = protocoloInicio();

            //se lee el json con los platillos y se deserializa a una lista de objetos PlatillosJson
            string json = File.ReadAllText("platillos.json");
            List<PlatillosJson> jsonMenu = JsonSerializer.Deserialize<List<PlatillosJson>>(json);
            Menu<string> menu = convertirMenu<string>(jsonMenu);

            //hacer menu interactuable
            printMenu(menu,cliente);
            mostrarPedidos(cliente);
            guardarCliente(cliente);

            //platillosObjetos()
            //mostrarMenu(menu);











            //WriteLine("Cliente creado: {0}", cliente.nombre);






        }   
    }

}