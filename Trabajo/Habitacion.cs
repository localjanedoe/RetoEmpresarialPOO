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

        public static Dictionary<string, int> HabitacionSimple()
        {
            return new Dictionary<string, int>()
            {
               {"Simple", 1}
            };
        }


        public static Dictionary<string, int> HabitacionDoble()
        {
            return new Dictionary<string, int>()
            {
               {"Doble", 2}
            };
        }

        public static Dictionary<string, int> HabitacionMatrimonial()
        {
            return new Dictionary<string, int>()
            {
               {"atrimonial", 3}
            };
        }



        public static void EstaDisponible()
        {
            
        }

        public static void ActualizarEstado()
        {

        }

        public static void CalcularCosto(double precio)
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
