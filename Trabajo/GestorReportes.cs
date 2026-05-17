using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class GestorReportes
    {
        private Hotel hotel;

        public GestorReportes(Hotel hotel)
        {
            this.hotel = hotel;
        }

        // Reporte de ocupación por tipo de habitación.
        public void OcupacionPorTipo()
        {
            int[] total = new int[4];
            int[] ocupadas = new int[4];

            foreach (var h in hotel.Habitaciones.ListarTodas())
            {
                if (h.tipo < 1 || h.tipo > 3) continue;
                total[h.tipo]++;
                if (!h.EstaDisponible()) ocupadas[h.tipo]++;
            }

            Console.WriteLine("\n--- Reporte de Ocupación por Tipo ---");
            string[] nombres = { "", "Simple", "Doble", "Matrimonial" };
            for (int t = 1; t <= 3; t++)
            {
                double pct = total[t] == 0 ? 0 : (ocupadas[t] * 100.0 / total[t]);
                Console.WriteLine($"{nombres[t],-12}: {ocupadas[t]}/{total[t]} ocupadas ({pct:F1}%)");
            }
        }

        public void WebVsPresencial()
        {
            int web = 0, presencial = 0;
            foreach (var r in hotel.Reservas.Listar())
            {
                if (r.origen == "Web") web++;
                else if (r.origen == "Presencial") presencial++;
            }
            Console.WriteLine("\n--- Reporte Web vs Presencial ---");
            Console.WriteLine($"Reservas Web        : {web}");
            Console.WriteLine($"Reservas Presencial : {presencial}");
            Console.WriteLine($"Total               : {web + presencial}");
        }

        public double IngresosPorPeriodo(DateTime ini, DateTime fin)
        {
            double total = 0;
            foreach (var r in hotel.Reservas.Listar())
            {
                if (!r.Estado) continue;
                if (r.FechaEntrada >= ini && r.FechaEntrada <= fin)
                    total += r.CalcularCosto();
            }
            return total;
        }

        // Mes de 30 días asumiendo 100% de ocupación (convención del enunciado).
        public double IngresosProyectados()
        {
            double total = 0;
            foreach (var h in hotel.Habitaciones.ListarTodas())
            {
                total += h.precioPorNoche * 30;
            }
            return total;
        }
    }
}
