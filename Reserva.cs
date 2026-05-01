using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Reserva
    {
        public int IdReserva;
        public string FechaEntrada;
        public int NumNoches;
        public bool Estado;

        public List<Servicios> Servicios = new List<Servicios>();
        public Habitacion Habitacion;
        public Cliente Cliente;

        public double CalcularCosto()
        {
            if (Habitacion == null) return 0;

            double costoBase = Habitacion.precioPorNoche * NumNoches;

            double costoServicios = 0;
            foreach (var servicio in Servicios)
            {
                costoServicios += servicio.Precio
            }

            return costoBase + costoServicios;
        }

        public void Cancelar()
        {
            Estado = false;
            Console.WriteLine("Reserva cancelada");
        }

        public void Modificar(string nuevaFecha, int nuevasNoches)
        {
            FechaEntrada = nuevaFecha;
            NumNoches = nuevasNoches;
            Console.WriteLine("Reserva modificada");
        }
    }
}