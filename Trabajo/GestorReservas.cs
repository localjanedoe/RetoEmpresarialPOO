using System;
using System.Collections.Generic;

namespace Trabajo
{
    // Esqueleto. La creación con validación de cruce de fechas y el cambio
    // de habitación con historial corresponden al Programador 2.
    internal class GestorReservas
    {
        private List<Reserva> reservas = new List<Reserva>();
        private int siguienteId = 1;

        public int SiguienteId()
        {
            return siguienteId++;
        }

        public void Agregar(Reserva r)
        {
            reservas.Add(r);
        }

        public Reserva BuscarPorId(int id)
        {
            foreach (var r in reservas)
            {
                if (r.IDReserva == id) return r;
            }
            return null;
        }

        public List<Reserva> BuscarPorCliente(int documento)
        {
            List<Reserva> resultado = new List<Reserva>();
            foreach (var r in reservas)
            {
                if (r.cliente != null && r.cliente.GetId() == documento)
                    resultado.Add(r);
            }
            return resultado;
        }

        public List<Reserva> Listar()
        {
            return reservas;
        }
    }
}
