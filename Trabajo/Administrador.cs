using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Administrador : Usuario
    {
        public Administrador(string nombre, int id, Hotel hotel) : base(nombre, id, hotel) { }

        public override void Menu()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine($"--- Menú Administrador ({nombre}) - {hotel.nombre} ---");
                Console.WriteLine("1. Modificar costo de habitaciones");
                Console.WriteLine("2. Reporte de ocupación por tipo");
                Console.WriteLine("3. Reporte Web vs Presencial");
                Console.WriteLine("4. Reporte de ingresos por periodo");
                Console.WriteLine("5. Calcular ingresos proyectados (mes 30 días)");
                Console.WriteLine("6. Gestionar promociones");
                Console.WriteLine("7. Listar clientes registrados");
                Console.WriteLine("8. Buscar reservas por documento de cliente");
                Console.WriteLine("0. Cerrar sesión");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1: ModificarCostoHabitacion(); break;
                    case 2: hotel.Reportes.OcupacionPorTipo(); Pausa(); break;
                    case 3: hotel.Reportes.WebVsPresencial(); Pausa(); break;
                    case 4: GenerarReporteIngresos(); break;
                    case 5: CalcularIngresosProyectados(); break;
                    case 6: GestionarPromociones(); break;
                    case 7: ListarClientes(); break;
                    case 8: BuscarReservasPorCliente(); break;
                    case 0: LogOut(); break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 0);
        }

        public void ModificarCostoHabitacion()
        {
            Console.Write("\nTipo de habitación a modificar (1=Simple, 2=Doble, 3=Matrimonial): ");
            if (!int.TryParse(Console.ReadLine(), out int tipo)) { Error(); return; }
            Console.Write("Nuevo precio por noche: ");
            if (!double.TryParse(Console.ReadLine(), out double nuevo)) { Error(); return; }

            int afectadas = hotel.Habitaciones.CambiarPrecioPorTipo(tipo, nuevo);
            Console.WriteLine($"{afectadas} habitaciones actualizadas a ${nuevo}.");
            Console.ReadKey();
        }

        public void GenerarReporteIngresos()
        {
            Console.Write("\nFecha inicio (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime ini)) { Error(); return; }
            Console.Write("Fecha fin (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime fin)) { Error(); return; }

            double total = 0, web = 0, presencial = 0;
            int cantTotal = 0, cantWeb = 0, cantPresencial = 0;

            foreach (var r in hotel.Reservas.Listar())
            {
                if (!r.Estado) continue;
                if (r.FechaEntrada < ini || r.FechaEntrada > fin) continue;

                double costo = r.CalcularCosto();
                total += costo;
                cantTotal++;
                if (r.origen == "Web") { web += costo; cantWeb++; }
                else if (r.origen == "Presencial") { presencial += costo; cantPresencial++; }
            }

            Console.WriteLine($"\n    Ingresos {ini:yyyy-MM-dd} a {fin:yyyy-MM-dd}"); // Con F2 se redondea a 2 decimales
            Console.WriteLine($"Web        : ${web,10:F2}  ({cantWeb} reservas)"); // pero fijo el profesor de Metodos experimentales me pone problema
            Console.WriteLine($"Presencial : ${presencial,10:F2}  ({cantPresencial} reservas)"); // >:c
            Console.WriteLine($"TOTAL      : ${total,10:F2}  ({cantTotal} reservas)");
            Console.ReadKey();
        }

        public void BuscarReservasPorCliente()
        {
            Console.Write("\nIngrese documento del cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int doc)) { Error(); return; }

            Cliente c = hotel.Clientes.BuscarPorDocumento(doc);
            if (c == null)
            {
                Console.WriteLine("No existe un cliente con ese documento.");
                Pausa();
                return;
            }

            var reservas = hotel.Reservas.BuscarPorCliente(doc);
            Console.WriteLine($"\nCliente: {c.GetNombre()} (Doc {c.GetId()}) — {reservas.Count} reserva(s)");
            foreach (var r in reservas)
            {
                string estado = r.Estado ? "Activa" : "Cancelada";
                string promo = r.promocionAplicada != null ? $" | Promo: {r.promocionAplicada.nombre}" : "";
                Console.WriteLine($"  ID {r.IDReserva} | Hab {r.Habitacion?.numero} | " +
                                  $"Entrada: {r.FechaEntrada:yyyy-MM-dd} | Noches: {r.NumNoches} | " +
                                  $"Origen: {r.origen} | {estado} | Total: ${r.CalcularCosto()}{promo}");
            }
            Pausa();
        }

        public void CalcularIngresosProyectados()
        {
            double total = hotel.Reportes.IngresosProyectados();
            Console.WriteLine($"\nIngresos proyectados (mes de 30 días, 100% ocupación): ${total}");
            Console.ReadKey();
        }

        public void GestionarPromociones()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("--- Gestión de Promociones ---");
                Console.WriteLine("1. Listar promociones");
                Console.WriteLine("2. Crear promoción");
                Console.WriteLine("3. Eliminar promoción");
                Console.WriteLine("0. Volver");
                if (!int.TryParse(Console.ReadLine(), out opcion)) { Error(); continue; }

                switch (opcion)
                {
                    case 1: ListarPromociones(); break;
                    case 2: CrearPromocion(); break;
                    case 3: EliminarPromocion(); break;
                    case 0: break;
                    default: Error(); break;
                }
            } while (opcion != 0);
        }

        private void ListarPromociones()
        {
            Console.WriteLine("\nPromociones registradas:");
            if (hotel.Promociones.Count == 0)
            {
                Console.WriteLine("(ninguna)");
            }
            else
            {
                for (int i = 0; i < hotel.Promociones.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {hotel.Promociones[i]}");
                }
            }
            Pausa();
        }

        private void CrearPromocion()
        {
            Console.Write("Nombre: ");
            string nom = Console.ReadLine();
            Console.Write("Descuento (%): ");
            if (!double.TryParse(Console.ReadLine(), out double desc)) { Error(); return; }
            Console.Write("Fecha inicio (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime ini)) { Error(); return; }
            Console.Write("Fecha fin (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime fin)) { Error(); return; }
            Console.Write("Tipo aplica (0=todas, 1=Simple, 2=Doble, 3=Matrimonial): ");
            if (!int.TryParse(Console.ReadLine(), out int tipo)) { Error(); return; }

            hotel.Promociones.Add(new Promocion(nom, desc, ini, fin, tipo));
            Console.WriteLine("Promoción creada.");
            Pausa();
        }

        private void EliminarPromocion()
        {
            ListarPromociones();
            if (hotel.Promociones.Count == 0) return;
            Console.Write("Número de promoción a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > hotel.Promociones.Count) { Error(); return; }
            hotel.Promociones.RemoveAt(idx - 1);
            Console.WriteLine("Promoción eliminada.");
            Pausa();
        }

        public void ListarClientes()
        {
            Console.WriteLine("\n--- Clientes registrados ---");
            var lista = hotel.Clientes.Listar();
            if (lista.Count == 0)
            {
                Console.WriteLine("(ninguno)");
            }
            else
            {
                foreach (var c in lista)
                {
                    Console.WriteLine($"Doc {c.GetId()} - {c.GetNombre()} - Reservas: {c.historial.Count}");
                }
            }
            Pausa();
        }

        private void Error()
        {
            Console.WriteLine("Entrada inválida.");
            Console.ReadKey();
        }

        private void Pausa()
        {
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
