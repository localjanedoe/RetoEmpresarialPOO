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
        
        public static void MenuClientes()
        {
           
            Console.Clear();
            Console.WriteLine("Bienvenido a Velisse Hotel");
            Console.WriteLine("¿Qué desea hacer? \n1. Realizar Reserva Online \n2. Consultar Disponibilidad \n3. Consultar Precios \n4. Consultar Reserva \n5. Cancelar Reserva");
            int opcion = Int32.Parse(Console.ReadLine());
            
            switch(opcion)
            {
                case 1:
                    RealizarReservaWeb();
                    break;


                case 2:
                    ConsultarDisponibilidad();
                    break;

                case 3:
                    ConsultarPrecios();
                    break;

                case 4:
                    ConsultarReserva();
                    break;

                case 5:
                    Reserva.Cancelar();
                    break;
                    


            }


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
