using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Promocion
    {
        public string nombre;
        public double descuento;          // porcentaje 0-100
        public DateTime fechaInicio;
        public DateTime fechaFin;
        public int tipoHabitacionAplica;  // 0 = todas, 1=Simple, 2=Doble, 3=Matrimonial

        public Promocion(string nombre, double descuento, DateTime inicio, DateTime fin, int tipoAplica)
        {
            this.nombre = nombre;
            this.descuento = descuento;
            this.fechaInicio = inicio;
            this.fechaFin = fin;
            this.tipoHabitacionAplica = tipoAplica;
        }

        public bool EsAplicable(int tipoHabitacion, DateTime fecha)
        {
            if (fecha < fechaInicio || fecha > fechaFin) return false;
            if (tipoHabitacionAplica != 0 && tipoHabitacionAplica != tipoHabitacion) return false;
            return true;
        }

        public double AplicarA(double monto)
        {
            return monto - (monto * descuento / 100.0);
        }

        public override string ToString()
        {
            string tipoStr = tipoHabitacionAplica switch
            {
                0 => "Todas",
                1 => "Simple",
                2 => "Doble",
                3 => "Matrimonial",
                _ => "?"
            };
            return $"{nombre} | {descuento}% off | {fechaInicio:yyyy-MM-dd} a {fechaFin:yyyy-MM-dd} | Aplica: {tipoStr}";
        }
    }
}
