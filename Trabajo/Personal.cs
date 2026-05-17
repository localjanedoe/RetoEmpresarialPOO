using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Personal : Usuario
    {
        public Personal(string nombre, int id, Hotel hotel) : base(nombre, id, hotel) { }

        // En el pdf dice que son funciones del programador 2
        public override void Menu()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine($"--- Menú Personal ({nombre}) - {hotel.nombre} ---");
                Console.WriteLine("1. Verificar Disponibilidad");
                Console.WriteLine("2. Registrar Reserva Presencial");
                Console.WriteLine("3. Modificar Habitación");
                Console.WriteLine("4. Cancelar Reserva");
                Console.WriteLine("0. Cerrar sesión");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        Console.WriteLine("Todo esto esta pendiente muchachos");
                        Console.ReadKey();
                        break;
                    case 0: LogOut(); break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 0);
        }
    }
}
