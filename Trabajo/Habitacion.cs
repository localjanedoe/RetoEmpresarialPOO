using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo
{
    internal class Habitacion
    {
        public string numero;
        public string tipo;
        public double precioPorNoche;
        public bool Disponible;

        public Habitacion(string numero, string tipo, double precioPorNoche, bool Disponible)
        {
            this.numero = numero; 
            this.tipo = tipo;
            this.precioPorNoche = precioPorNoche;
            this.Disponible = Disponible;
            
        }

        public static void EstaDisponible(bool Disponible)
        {
            Console.WriteLine();

            if (Disponible == true)
            {
                Cliente.RealizarReservaWeb();
            }
            else
            {
                Console.WriteLine("La habitación se encuentra ocupada.");
                Console.ReadKey();
            }
        }

        public static void ActualizarEstado()
        {

        }

        public void CalcularCosto(double precio)
        {
            
        }

        public static void VerDescripcion(string descripcion)
        {

        }

        public static void ActualizarPrecio()
        {

        }
    }
}
