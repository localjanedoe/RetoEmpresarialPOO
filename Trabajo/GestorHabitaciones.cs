using System;
using System.Collections.Generic;

namespace Trabajo
{
    // Esqueleto. La implementación completa de búsqueda con cruce de fechas
    // corresponde al Programador 1 (Emmanuel).
    internal class GestorHabitaciones
    {
        private List<Habitacion> habitaciones = new List<Habitacion>();

        public void Agregar(Habitacion h)
        {
            habitaciones.Add(h);
        }

        public List<Habitacion> ListarTodas()
        {
            return habitaciones;
        }

        public List<Habitacion> ListarDisponiblesPorTipo(int tipo)
        {
            List<Habitacion> resultado = new List<Habitacion>();
            foreach (var h in habitaciones)
            {
                if (h.tipo == tipo && h.EstaDisponible()) resultado.Add(h);
            }
            return resultado;
        }

        public Habitacion BuscarPorNumero(string numero)
        {
            foreach (var h in habitaciones)
            {
                if (h.numero == numero) return h;
            }
            return null;
        }

        public int CambiarPrecioPorTipo(int tipo, double nuevoPrecio)
        {
            int afectadas = 0;
            foreach (var h in habitaciones)
            {
                if (h.tipo == tipo)
                {
                    h.ActualizarPrecio(nuevoPrecio);
                    afectadas++;
                }
            }
            return afectadas;
        }
    }
}
