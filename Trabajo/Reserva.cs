using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Reserva
    {
        public int IDReserva;
        public DateTime FechaEntrada;
        public DateTime FechaSalida;
        public int NumNoches;
        public bool Estado;
        public string origen;
        public Cliente cliente;

        public List<Servicio> Servicios = new List<Servicio>();
        public Habitacion Habitacion;
        public Promocion promocionAplicada;  // null si no se aplica ninguna

        public double CalcularCosto()
        {
            if (Habitacion == null) return 0;

            double costoBase = Habitacion.precioPorNoche * NumNoches;
            double costoServicios = 0;
            foreach (var servicio in Servicios)
            {
                costoServicios += servicio.costo;
            }
            double subtotal = costoBase + costoServicios;

            if (promocionAplicada != null)
            {
                subtotal = promocionAplicada.AplicarA(subtotal);
            }
            return subtotal;
        }

        public void Cancelar()
        {
            Estado = false;
            if (Habitacion != null)
            {
                Habitacion.ActualizarEstado(true);
            }
            Console.WriteLine($"Reserva {IDReserva} cancelada.");
        }

        public void Modificar(DateTime nuevaFecha, int nuevasNoches)
        {
            FechaEntrada = nuevaFecha;
            FechaSalida = nuevaFecha.AddDays(nuevasNoches);
            NumNoches = nuevasNoches;
            Console.WriteLine("Reserva modificada.");
        }
    }
}
