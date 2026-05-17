using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Hotel
    {
        public string nombre;
        public GestorClientes Clientes = new GestorClientes();
        public GestorReservas Reservas = new GestorReservas();
        public GestorHabitaciones Habitaciones = new GestorHabitaciones();
        public GestorReportes Reportes;
        public List<Promocion> Promociones = new List<Promocion>();

        public Hotel(string nombre)
        {
            this.nombre = nombre;
            this.Reportes = new GestorReportes(this);
        }

        // Devuelve la primera promoción aplicable a un tipo de habitación y fecha dados,
        // o null si no hay ninguna.
        public Promocion BuscarPromocionAplicable(int tipoHabitacion, DateTime fecha)
        {
            foreach (var p in Promociones)
            {
                if (p.EsAplicable(tipoHabitacion, fecha)) return p;
            }
            return null;
        }
    }
}
