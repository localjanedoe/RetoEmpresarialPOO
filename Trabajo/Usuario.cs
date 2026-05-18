using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Usuario
    {
        protected string nombre;
        protected int id;
        protected Hotel hotel;

        public string GetNombre() => nombre;
        public int GetId() => id;

        public Usuario() { }

        public Usuario(string nombre, int id, Hotel hotel)
        {
            this.nombre = nombre;
            this.id = id;
            this.hotel = hotel;
        }

        public virtual void Menu()
        {
            Console.WriteLine("Menú base de Usuario (Nota: Esta pendiente por implementar).");
        }

        public static Usuario LogIn(Hotel hotel)
        {
            int rol;
            int ID;

            do
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido al sistema {hotel.nombre}.");
                Console.WriteLine("Seleccione rol:");
                Console.WriteLine("1. Cliente");
                Console.WriteLine("2. Personal (Recepcionista)");
                Console.WriteLine("3. Administración");
                Console.WriteLine("0. Salir");

                if (!int.TryParse(Console.ReadLine(), out rol))
                {
                    Console.WriteLine("Error. Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                switch (rol)
                {
                    case 0:
                        Console.WriteLine("Saliendo del sistema...");
                        return null;

                    case 1:
                        Console.Write("Ingrese su documento: ");
                        if (!int.TryParse(Console.ReadLine(), out int docInt))
                        {
                            Console.WriteLine("Documento inválido. Presione una tecla...");
                            Console.ReadKey();
                            continue;
                        }

                        Cliente existente = hotel.Clientes.BuscarPorDocumento(docInt);
                        if (existente != null) return existente;

                        Console.Write("Ingrese su nombre: ");
                        string nombreCliente = Console.ReadLine();
                        Cliente nuevo = new Cliente(nombreCliente, docInt, hotel);
                        hotel.Clientes.Registrar(nuevo);
                        return nuevo;

                    case 2:
                        Console.Write("Ingrese el código del personal: ");
                        if (!int.TryParse(Console.ReadLine(), out ID))
                        {
                            Console.WriteLine("Error. Presione una tecla...");
                            Console.ReadKey();
                            continue;
                        }
                        if (ID == 2)
                        {
                            return new Personal("Recepcionista", ID, hotel);
                        }
                        Console.WriteLine("Código incorrecto.");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Write("Ingrese el código del administrador: ");
                        if (!int.TryParse(Console.ReadLine(), out ID))
                        {
                            Console.WriteLine("Error. Presione una tecla...");
                            Console.ReadKey();
                            continue;
                        }
                        if (ID == 1)
                        {
                            return new Administrador("Administrador", ID, hotel);
                        }
                        Console.WriteLine("Código incorrecto.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            } while (rol != 0);

            return null;
        }

        public virtual void LogOut()
        {
            Console.WriteLine($"Sesión cerrada para {nombre}.");
        }
    }
}
