namespace Trabajo
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
           Usuario.LogIn(); 
        }

        public static void InterfazClientes()
        {
            Console.Clear();
            Console.WriteLine("Bienvenido a Velisse Hotel");
            Console.WriteLine("¿Qué desea hacer? \n1. Realizar Reserva Online \n2. Consultar Disponibilidad \n3. Consultar Precios \n4. Consultar Reserva \n5. Cancelar Reserva");
        }

        public static void InterfazPersonal()
        {
            Console.WriteLine("¿Qué desea hacer? \n1. Verificar Disponibilidad \n2. Registrar Reserva Presencial \n3. Modificar Habitación \n4. Cancelar Reserva");
        }

        public static void InterfazAdmin()
        {

        }

    }
}
