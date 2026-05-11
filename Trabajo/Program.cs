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
            Console.WriteLine("¿Qué desea hacer? \n1. Realizar Reserva \n2. Consultar Disponibilidad \n3. Consultar Precios \n4. Consultar Reserva \n5. Cancelar Reserva \n6. Salir");
        }

        public static void InterfazPersonal()
        {
            Console.WriteLine("¿Qué desea hacer? \n1. ");
        }

        public static void InterfazAdmin()
        {

        }

    }
}
