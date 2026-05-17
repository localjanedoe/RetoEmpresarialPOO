using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Servicio
    {
        public string nombre;
        public double costo;
        public string descripcion;

        public Servicio(string nombre, double costo, string descripcion)
        {
            this.nombre = nombre;
            this.costo = costo;
            this.descripcion = descripcion;
        }

        public static List<Servicio> Catalogo()
        {
            return new List<Servicio>
            {
                new Servicio("Servicio a la habitacion", 15, "Comida a la habitación"),
                new Servicio("Limpieza adicional", 10, "Servicio de limpieza extra"),
                new Servicio("Transporte", 25, "Servicio de traslado desde o hacia el aeropuerto"),
                new Servicio("Otros", 5, "Servicios adicionales")
            };
        }

        public override string ToString()
        {
            return $"{nombre} (${costo}) - {descripcion}";
        }
    }
}
