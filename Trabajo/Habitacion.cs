using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo
{
    internal class Habitacion
    {
        public string numero;
        public int tipo;
        public double precioPorNoche;
        public bool disponible;


        
        public Habitacion(string Numero, int Tipo, double precio, bool Disponible)
        {
            Numero = numero;
            Tipo = tipo;
            precio = precioPorNoche;
            Disponible = disponible;
        }

        public static void EstaDisponible(bool Disponible)
        {
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
