using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo
{
    internal class Usuario
    {
        

        public static void LogIn()
        {
            int ID;
            int rol;

            do
            {
                // El personal y el admin tienen un código distintivo para poder acceder a sus funciones
                Console.WriteLine("Bienvenido al sistema.");
                Console.WriteLine("Seleccione rol: \n1. Cliente \n2. Personal \n3. Administración");
                if (!int.TryParse(Console.ReadLine(), out rol))
                {
                    Console.WriteLine("Error. Presione cualquier tecla para salir . . .");
                    Console.ReadKey();
                    continue;
                }

                switch (rol)
                {
                    case 1:
                        Program.InterfazClientes();
                        break;


                    case 2:
                        Console.WriteLine("Ingrese el código respectivo para el personal del hotel");
                        if (!int.TryParse(Console.ReadLine(), out ID))
                        {
                            Console.WriteLine("Error. Presione cualquier tecla para salir . . .");
                            Console.ReadKey();
                            continue;
                        }

                        if (ID == 0002)
                        {
                            Program.InterfazPersonal();
                        }

                        else
                        {
                            Console.WriteLine("Código Incorrecto. Intente de nuevo");
                            Console.ReadKey();
                        }
                        break;
                        

                    case 3:
                        Console.WriteLine("Ingrese el código respectivo para el personal del hotel");
                        if (!int.TryParse(Console.ReadLine(), out ID))
                        {
                            Console.WriteLine("Error. Presione cualquier tecla para salir . . .");
                            Console.ReadKey();
                            continue;
                        }

                        if (ID == 0001)
                        {
                            Program.InterfazAdmin();
                        }

                        else
                        {
                            Console.WriteLine("Código Incorrecto. Intente de nuevo");
                            Console.ReadKey();
                        }
                        break;


                    default:
                         Console.WriteLine("Error, ingrese un carácter válido");
                        Console.ReadKey();
                        break;
                }


            } while (rol >= 1 || rol <= 3);
        }

        public static void LogOut()
        {

        }
    }
}
