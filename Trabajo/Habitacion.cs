using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo
{
    internal class Habitacion
    {
        public string numero;
        public int tipo;             // 1=Simple, 2=Doble, 3=Matrimonial
        public double precioPorNoche;
        public bool disponible;

        public Habitacion(string numero, int tipo, double precioPorNoche, bool disponible)
        {
            this.numero = numero;
            this.tipo = tipo;
            this.precioPorNoche = precioPorNoche;
            this.disponible = disponible;
        }

        public bool EstaDisponible()
        {
            return disponible;
        }

        public void ActualizarEstado(bool nuevoEstado)
        {
            disponible = nuevoEstado;
        }

        public double CalcularCostoNoches(int noches)
        {
            return precioPorNoche * noches;
        }

        public string VerDescripcion()
        {
            string nombreTipo = tipo switch
            {
                1 => "Simple",
                2 => "Doble",
                3 => "Matrimonial",
                _ => "Desconocido"
            };
            return $"Habitación {numero} - Tipo: {nombreTipo} - Precio/noche: ${precioPorNoche} - " +
                   (disponible ? "Disponible" : "Ocupada");
        }

        public void ActualizarPrecio(double nuevoPrecio)
        {
            precioPorNoche = nuevoPrecio;
        }
    }
}
