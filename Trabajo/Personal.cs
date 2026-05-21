using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class Personal : Usuario
    {
        public Personal(string nombre, int id, Hotel hotel)
            : base(nombre, id, hotel)
        {
        }

        public override void Menu()
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine($"--- Menú Personal ({nombre}) - {hotel.nombre} ---");
                Console.WriteLine("1. Verificar Disponibilidad");
                Console.WriteLine("2. Registrar Reserva Presencial");
                Console.WriteLine("3. Modificar Habitación");
                Console.WriteLine("4. Cancelar Reserva");
                Console.WriteLine("5. Finalizar Estancia");
                Console.WriteLine("6. Registrar Servicio Adicional");
                Console.WriteLine("7. Consultar Información de Huésped");
                Console.WriteLine("8. Calcular costo total de estancia");
                Console.WriteLine("0. Cerrar sesión");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Error();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        VerificarDisponibilidad();
                        break;

                    case 2:
                        RegistrarReservaPresencial();
                        break;

                    case 3:
                        ModificarHabitacion();
                        break;

                    case 4:
                        CancelarReserva();
                        break;

                    case 5:
                        FinalizarEstancia();
                        break;

                    case 6:
                        RegistrarServicioAdicional();
                        break;

                    case 7:
                        ConsultarInformacionHuesped();
                        break;

                    case 8:
                        CalcularCostoEstancia();
                        break;

                    case 0:
                        LogOut();
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }

            } while (opcion != 0);
        }

        // 1. Verificar disponibilidad
        public void VerificarDisponibilidad()
        {
            Console.WriteLine("\n--- Habitaciones Disponibles ---");

            bool hayDisponibles = false;

            foreach (var h in hotel.Habitaciones.ListarTodas())
            {
                if (h.EstaDisponible())
                {
                    Console.WriteLine(h.VerDescripcion());
                    hayDisponibles = true;
                }
            }

            if (!hayDisponibles)
            {
                Console.WriteLine("No hay habitaciones disponibles.");
            }

            Pausa();
        }

        // 2. Registrar reserva presencial
        public void RegistrarReservaPresencial()
        {
            Console.WriteLine("\n--- Registrar Reserva Presencial ---");

            Console.Write("Documento del cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int doc))
            {
                Error();
                return;
            }

            Cliente cliente = hotel.Clientes.BuscarPorDocumento(doc);

            if (cliente == null)
            {
                Console.Write("Nombre del cliente: ");
                string nombreCliente = Console.ReadLine();

                cliente = new Cliente(nombreCliente, doc, hotel);
                hotel.Clientes.Registrar(cliente);

                Console.WriteLine("Cliente registrado correctamente.");
            }

            Console.Write("Tipo habitación (1=Simple, 2=Doble, 3=Matrimonial): ");
            if (!int.TryParse(Console.ReadLine(), out int tipo))
            {
                Error();
                return;
            }

            List<Habitacion> disponibles = hotel.Habitaciones.ListarDisponiblesPorTipo(tipo);

            if (disponibles.Count == 0)
            {
                Console.WriteLine("No hay habitaciones disponibles de ese tipo.");
                Pausa();
                return;
            }

            Console.WriteLine("\nHabitaciones disponibles:");
            foreach (var h in disponibles)
            {
                Console.WriteLine(h.VerDescripcion());
            }

            Console.Write("\nNúmero de habitación: ");
            string numero = Console.ReadLine();

            Habitacion seleccionada = hotel.Habitaciones.BuscarPorNumero(numero);

            if (seleccionada == null || !seleccionada.EstaDisponible())
            {
                Console.WriteLine("Habitación inválida o no disponible.");
                Pausa();
                return;
            }

            Console.Write("Fecha entrada (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime fecha))
            {
                Error();
                return;
            }

            Console.Write("Número de noches: ");
            if (!int.TryParse(Console.ReadLine(), out int noches) || noches <= 0)
            {
                Error();
                return;
            }

            Reserva reserva = new Reserva
            {
                IDReserva = hotel.Reservas.SiguienteId(),
                FechaEntrada = fecha,
                FechaSalida = fecha.AddDays(noches),
                NumNoches = noches,
                Estado = true,
                Habitacion = seleccionada,
                cliente = cliente,
                origen = "Presencial"
            };

            seleccionada.ActualizarEstado(false);

            cliente.historial.Add(reserva);
            hotel.Reservas.Agregar(reserva);

            Console.WriteLine($"\nReserva registrada exitosamente.");
            Console.WriteLine($"ID Reserva: {reserva.IDReserva}");
            Console.WriteLine($"Costo total: ${reserva.CalcularCosto()}");

            Pausa();
        }

        // 3. Modificar habitación
        public void ModificarHabitacion()
        {
            Console.Write("\nIngrese ID de la reserva: ");

            if (!int.TryParse(Console.ReadLine(), out int idReserva))
            {
                Error();
                return;
            }

            Reserva reserva = hotel.Reservas.BuscarPorId(idReserva);

            if (reserva == null || !reserva.Estado)
            {
                Console.WriteLine("Reserva no encontrada o cancelada.");
                Pausa();
                return;
            }

            Console.WriteLine($"\nHabitación actual: {reserva.Habitacion.numero}");

            Console.Write("Nuevo número de habitación: ");
            string nuevoNumero = Console.ReadLine();

            Habitacion nuevaHabitacion = hotel.Habitaciones.BuscarPorNumero(nuevoNumero);

            if (nuevaHabitacion == null || !nuevaHabitacion.EstaDisponible())
            {
                Console.WriteLine("Habitación inválida o no disponible.");
                Pausa();
                return;
            }

            // Liberar habitación anterior
            reserva.Habitacion.ActualizarEstado(true);

            // Asignar nueva habitación
            reserva.Habitacion = nuevaHabitacion;
            nuevaHabitacion.ActualizarEstado(false);

            Console.WriteLine("Habitación modificada correctamente.");

            Pausa();
        }

        // 4. Cancelar reserva
        public void CancelarReserva()
        {
            Console.Write("\nIngrese ID de la reserva: ");

            if (!int.TryParse(Console.ReadLine(), out int idReserva))
            {
                Error();
                return;
            }

            Reserva reserva = hotel.Reservas.BuscarPorId(idReserva);

            if (reserva == null)
            {
                Console.WriteLine("Reserva no encontrada.");
                Pausa();
                return;
            }

            reserva.Cancelar();

            Pausa();
        }

        // 5. Finalizar estancia
        public void FinalizarEstancia()
        {
            Console.Write("\nIngrese ID de la reserva: ");

            if (!int.TryParse(Console.ReadLine(), out int idReserva))
            {
                Error();
                return;
            }

            Reserva reserva = hotel.Reservas.BuscarPorId(idReserva);

            if (reserva == null || !reserva.Estado)
            {
                Console.WriteLine("Reserva no encontrada o ya finalizada.");
                Pausa();
                return;
            }

            reserva.Habitacion.ActualizarEstado(true);
            reserva.Estado = false;

            Console.WriteLine("Estancia finalizada correctamente.");
            Console.WriteLine($"Total pagado: ${reserva.CalcularCosto()}");

            Pausa();
        }

        // 6. Registrar servicio adicional
        public void RegistrarServicioAdicional()
        {
            Console.Write("\nIngrese ID de la reserva: ");

            if (!int.TryParse(Console.ReadLine(), out int idReserva))
            {
                Error();
                return;
            }

            Reserva reserva = hotel.Reservas.BuscarPorId(idReserva);

            if (reserva == null || !reserva.Estado)
            {
                Console.WriteLine("Reserva no encontrada o no activa.");
                Pausa();
                return;
            }

            Console.WriteLine("\n--- Catálogo de Servicios ---");

            var catalogo = Servicio.Catalogo();

            for (int i = 0; i < catalogo.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {catalogo[i]}");
            }

            Console.Write("Seleccione servicio: ");

            if (!int.TryParse(Console.ReadLine(), out int opcion) ||
                opcion < 1 || opcion > catalogo.Count)
            {
                Error();
                return;
            }

            reserva.Servicios.Add(catalogo[opcion - 1]);

            Console.WriteLine("Servicio agregado correctamente.");

            Pausa();
        }

        // 7. Consultar información huésped
        public void ConsultarInformacionHuesped()
        {
            Console.Write("\nIngrese documento del cliente: ");

            if (!int.TryParse(Console.ReadLine(), out int doc))
            {
                Error();
                return;
            }

            Cliente cliente = hotel.Clientes.BuscarPorDocumento(doc);

            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                Pausa();
                return;
            }

            Console.WriteLine($"\n--- Información de {cliente.GetNombre()} ---");
            Console.WriteLine($"Documento: {cliente.GetId()}");
            Console.WriteLine($"Reservas realizadas: {cliente.historial.Count}");

            foreach (var r in cliente.historial)
            {
                string estado = r.Estado ? "Activa" : "Finalizada/Cancelada";

                Console.WriteLine(
                    $"ID {r.IDReserva} | Habitación {r.Habitacion.numero} | " +
                    $"Entrada: {r.FechaEntrada:yyyy-MM-dd} | " +
                    $"Noches: {r.NumNoches} | Estado: {estado}"
                );
            }

            Pausa();
        }

        public void CalcularCostoEstancia()
        {
            Console.Write("\nIngrese ID de la reserva: ");

            if (!int.TryParse(Console.ReadLine(), out int idr))
            {
                Error();
                return;
            }

            Reserva r = hotel.Reservas.BuscarPorId(idr);

            if (r == null)
            {
                Console.WriteLine("Reserva no encontrada.");
                Pausa();
                return;
            }

            Console.WriteLine("\n--- Detalle de la Reserva ---");

            Console.WriteLine($"Cliente: {r.cliente.GetNombre()}");
            Console.WriteLine($"Habitación: {r.Habitacion.numero}");
            Console.WriteLine($"Tipo: {TipoHabitacion(r.Habitacion.tipo)}");
            Console.WriteLine($"Noches: {r.NumNoches}");
            Console.WriteLine($"Precio por noche: ${r.Habitacion.precioPorNoche}");

            double costoHabitacion =
                r.Habitacion.precioPorNoche * r.NumNoches;

            Console.WriteLine($"\nCosto habitación: ${costoHabitacion}");

            double costoServicios = 0;

            Console.WriteLine("\nServicios adicionales:");

            if (r.Servicios.Count == 0)
            {
                Console.WriteLine("No tiene servicios.");
            }
            else
            {
                foreach (var s in r.Servicios)
                {
                    Console.WriteLine($"- {s.nombre}: ${s.costo}");
                    costoServicios += s.costo;
                }
            }

            Console.WriteLine($"\nCosto servicios: ${costoServicios}");

            double total = r.CalcularCosto();

            Console.WriteLine($"\nTOTAL ESTANCIA: ${total}");

            Pausa();
        }

        private string TipoHabitacion(int tipo)
        {
            switch (tipo)
            {
                case 1: return "Simple";
                case 2: return "Doble";
                case 3: return "Matrimonial";
                default: return "Desconocido";
            }
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