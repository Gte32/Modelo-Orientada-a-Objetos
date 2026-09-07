using static System.Console;
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

        public static Cliente<string> crearCliente(string nombre)
        {
            Client<string> nombre = new Cliente<string>(nombre);
           return new Cliente<string>(nombre);
        }

        static void Main(string[] args)
        {
            string nombreUsuario = protocoloInicio();
            Cliente<string> cliente = crearCliente(nombreUsuario);
            





        }   
    }

}