using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo
{
    internal class Reserva
    {
        public int idReserva;
        public string fechaEntrada;
        public int numNoches;
        public bool estado;
        public List<Servicios> Servicios = new List <Servicios> ();
        public Habitacion habitacion;
        public Cliente cliente;
        
        
        public double CalcularCosto()
        {
            if (habitacion == null) return 0;
            double costoBase = habitacion.precioPorNoche * numNoches;
        }

        public void Cancelar()
        {

        }

        public void Modificar()
        {

        }
    }
}
