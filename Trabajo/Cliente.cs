using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Cliente : Usuario
    {
        public List<Reserva> historial;

        public Cliente(string nombre, int documento, Hotel hotel) : base(nombre, documento, hotel)
        {
            this.historial = new List<Reserva>();
        }

        public string GetDocumento() => id.ToString();

        public override void Menu()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido a {hotel.nombre}, {nombre}.");
                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Consultar Disponibilidad");
                Console.WriteLine("2. Consultar Precios");
                Console.WriteLine("3. Realizar Reserva Online");
                Console.WriteLine("4. Consultar mis Reservas");
                Console.WriteLine("5. Cancelar una Reserva");
                Console.WriteLine("6. Solicitar Servicio Adicional");
                Console.WriteLine("7. Ver Promociones");
                Console.WriteLine("0. Cerrar sesión");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1: ConsultarDisponibilidad(); break;
                    case 2: ConsultarPrecios(); break;
                    case 3: RealizarReservaWeb(); break;
                    case 4: ConsultarReserva(); break;
                    case 5: CancelarReservaWeb(); break;
                    case 6: SolicitarServicio(); break;
                    case 7: VerPromociones(); break;
                    case 0: LogOut(); break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 0);
        }

        public void ConsultarDisponibilidad()
        {
            Console.WriteLine("\n    Habitaciones disponibles");
            foreach (var h in hotel.Habitaciones.ListarTodas())
            {
                if (h.EstaDisponible())
                    Console.WriteLine(h.VerDescripcion());
            }
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        public void ConsultarPrecios()
        {
            Console.WriteLine("\n    Precios por tipo de habitación");
            // Mostramos el precio actual leyendo la primera habitación de cada tipo.
            for (int t = 1; t <= 3; t++)
            {
                string nombreTipo = t switch { 1 => "Simple", 2 => "Doble", 3 => "Matrimonial", _ => "?" };
                double precio = 0;
                foreach (var h in hotel.Habitaciones.ListarTodas())
                {
                    if (h.tipo == t) { precio = h.precioPorNoche; break; }
                }
                Console.WriteLine($"{t}. {nombreTipo,-12}: ${precio} por noche");
            }
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        public void RealizarReservaWeb()
        {
            Console.WriteLine("\n    Realizar Reserva Web");
            Console.Write("Tipo de habitación (1=Simple, 2=Doble, 3=Matrimonial): ");
            if (!int.TryParse(Console.ReadLine(), out int tipo)) { Error(); return; }

            List<Habitacion> disponibles = hotel.Habitaciones.ListarDisponiblesPorTipo(tipo);
            if (disponibles.Count == 0)
            {
                Console.WriteLine("No hay habitaciones disponibles de ese tipo.");
                Console.ReadKey();
                return;
            }
            Habitacion seleccionada = disponibles[0];

            Console.Write("Fecha de entrada (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime fecha)) { Error(); return; }

            Console.Write("Número de noches: ");
            if (!int.TryParse(Console.ReadLine(), out int noches) || noches <= 0) { Error(); return; }

            Reserva r = new Reserva
            {
                IDReserva = hotel.Reservas.SiguienteId(),
                FechaEntrada = fecha,
                FechaSalida = fecha.AddDays(noches),
                NumNoches = noches,
                Estado = true,
                Habitacion = seleccionada,
                cliente = this,
                origen = "Web"
            };

            Promocion promo = hotel.BuscarPromocionAplicable(tipo, fecha);
            if (promo != null)
            {
                Console.WriteLine($"\nHay una promoción aplicable: {promo}");
                Console.Write("¿Aplicarla? (s/n): ");
                string resp = Console.ReadLine();
                if (resp != null && resp.Trim().ToLower() == "s")
                {
                    r.promocionAplicada = promo;
                    Console.WriteLine($"Promoción '{promo.nombre}' aplicada.");
                }
            }

            seleccionada.ActualizarEstado(false);
            historial.Add(r);
            hotel.Reservas.Agregar(r);

            Console.WriteLine($"\nReserva creada con ID: {r.IDReserva}. Costo total: ${r.CalcularCosto()}");
            Console.ReadKey();
        }

        public void ConsultarReserva()
        {
            Console.WriteLine($"\n    Historial de {nombre}");
            if (historial.Count == 0)
            {
                Console.WriteLine("No tiene reservas registradas.");
            }
            else
            {
                foreach (var r in historial)
                {
                    string estado = r.Estado ? "Activa" : "Cancelada";
                    Console.WriteLine($"ID {r.IDReserva} | {r.Habitacion?.VerDescripcion()} | " +
                                      $"Entrada: {r.FechaEntrada:yyyy-MM-dd} | Noches: {r.NumNoches} | " +
                                      $"Origen: {r.origen} | Estado: {estado}");
                }
            }
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        public void CancelarReservaWeb()
        {
            Console.Write("\nIngrese ID de reserva a cancelar: ");
            if (!int.TryParse(Console.ReadLine(), out int idr)) { Error(); return; }

            foreach (var r in historial)
            {
                if (r.IDReserva == idr)
                {
                    if (DateTime.Now.Date >= r.FechaEntrada.Date)
                    {
                        Console.WriteLine("No se puede cancelar desde la web una vez iniciada la estancia.");
                        Console.ReadKey();
                        return;
                    }
                    r.Cancelar();
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("Reserva no encontrada en su historial.");
            Console.ReadKey();
        }

        public void SolicitarServicio()
        {
            Console.Write("\nIngrese ID de una reserva activa: ");
            if (!int.TryParse(Console.ReadLine(), out int idr)) { Error(); return; }

            Reserva reserva = null;
            foreach (var r in historial)
            {
                if (r.IDReserva == idr && r.Estado) { reserva = r; break; }
            }
            if (reserva == null)
            {
                Console.WriteLine("Esta reserva no fue encontrada o no esta activa.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n    Catálogo de servicios:");
            var catalogo = Servicio.Catalogo();
            for (int i = 0; i < catalogo.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {catalogo[i].nombre} - ${catalogo[i].costo}");
            }
            Console.Write("Seleccione un servicio: ");
            if (!int.TryParse(Console.ReadLine(), out int sel) || sel < 1 || sel > catalogo.Count) { Error(); return; }

            reserva.Servicios.Add(catalogo[sel - 1]);
            Console.WriteLine($"Servicio '{catalogo[sel - 1].nombre}' añadido a la reserva.");
            Console.ReadKey();
        }

        public void VerPromociones()
        {
            Console.WriteLine("\n    Promociones vigentes");
            if (hotel.Promociones.Count == 0)
            {
                Console.WriteLine("En este momento no hay promociones.");
            }
            else
            {
                foreach (var p in hotel.Promociones)
                {
                    Console.WriteLine(p.ToString());
                }
            }
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private void Error()
        {
            Console.WriteLine("Entrada inválida.");
            Console.ReadKey();
        }
    }
}
