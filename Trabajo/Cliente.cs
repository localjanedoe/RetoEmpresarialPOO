using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo
{
    internal class Cliente
    {
        public string nombre;
        public string documento;
        public List<Reserva> historial;
        public Cliente(string nombre, string documento)

        {
            historial = new List<Reserva>();
        }
        

        public static void ConsultarDisponibilidad()
        {

        }

        public static void ConsultarPrecios()
        {

        }

        public static void RealizarReservaWeb()
        {
            
            Console.WriteLine("En el hotel tenemos tres tipos de habitación");
        }

        public static void ConsultarReserva()
        {

        }

        public static void SolicitarServicio()
        {

        }

    }
}
