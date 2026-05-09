using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo
{
    internal class Servicios
    {
        public static List <string> nombreServicio()
        {
            return new List<string>
            {
                {"Aseo"},
                {"Spa"},
                {"Turco"},
                {"Gimnasio"},
                {"Parqueadero"}


            };

        }
        public double costo = 35000;

        
        public static void VerServicios()
        {

        }
    }

}
