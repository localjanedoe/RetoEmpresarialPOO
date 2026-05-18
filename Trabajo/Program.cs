using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hotel hotel = new Hotel("Velisse Hotel");
            SembrarDatos(hotel);

            while (true)
            {
                Usuario usuario = Usuario.LogIn(hotel);
                if (usuario == null) break;
                usuario.Menu();   // Para tener algunos polimorfismos
            }

            Console.WriteLine("Hasta pronto.");
        }

        // Datos random xD
        private static void SembrarDatos(Hotel hotel)
        {
            hotel.Habitaciones.Agregar(new Habitacion("101", 1, 80,  true));
            hotel.Habitaciones.Agregar(new Habitacion("102", 1, 80,  true));
            hotel.Habitaciones.Agregar(new Habitacion("103", 1, 80,  true));
            hotel.Habitaciones.Agregar(new Habitacion("201", 2, 120, true));
            hotel.Habitaciones.Agregar(new Habitacion("202", 2, 120, true));
            hotel.Habitaciones.Agregar(new Habitacion("203", 2, 120, true));
            hotel.Habitaciones.Agregar(new Habitacion("301", 3, 160, true));
            hotel.Habitaciones.Agregar(new Habitacion("312", 3, 160, true)); // T_T
            hotel.Habitaciones.Agregar(new Habitacion("302", 3, 160, true));
            hotel.Habitaciones.Agregar(new Habitacion("303", 3, 160, true));

            hotel.Promociones.Add(new Promocion(
                "Temporada baja",
                15,
                new DateTime(2026, 5, 1),
                new DateTime(2026, 6, 30),
                0));
        }
    }
}
