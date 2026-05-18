using System;
using System.Collections.Generic;

namespace Trabajo
{
    internal class GestorClientes
    {
        private List<Cliente> clientes = new List<Cliente>();

        public void Registrar(Cliente c)
        {
            clientes.Add(c);
        }

        public Cliente BuscarPorDocumento(int documento)
        {
            foreach (var c in clientes)
            {
                if (c.GetId() == documento) return c;
            }
            return null;
        }

        public List<Cliente> Listar()
        {
            return clientes;
        }

        public int Cantidad()
        {
            return clientes.Count;
        }
    }
}
