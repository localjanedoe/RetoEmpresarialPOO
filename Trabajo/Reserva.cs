using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Reserva
    {
        public int IdReserva;
        public DateTime FechaEntrada;
        public DateTime FechaSalida;
        public int NumNoches;
        public bool Estado;

        public List<Servicios> Servicios = new List<Servicios>();
        public Habitacion Habitacion;
        

        public double CalcularCosto()
        {
            if (Habitacion == null) return 0;

            double costoBase = Habitacion.precioPorNoche * NumNoches;

            double costoServicios = 0;
            foreach (var servicio in Servicios)
            {
                costoServicios += servicio.costo;
            }

            return costoBase + costoServicios;
        }

        public void Cancelar()
        {
            Estado = false;
            Console.WriteLine("Reserva cancelada");
        }

        public void Modificar(DateTime nuevaFecha, int nuevasNoches)
        {
            FechaEntrada = nuevaFecha;
            NumNoches = nuevasNoches;
            Console.WriteLine("Reserva modificada");
        }
    }
}