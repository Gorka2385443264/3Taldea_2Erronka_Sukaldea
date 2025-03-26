using System;
using System.Collections.Generic;

namespace cocina
{
    public class Platera
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; } // Nombre del plato
        public virtual string Deskribapena { get; set; } // Descripción del plato
        public virtual string Kategoria { get; set; } // Categoría del plato
        public virtual double Prezioa { get; set; } // Precio del plato
        public virtual bool Menu { get; set; } // Indica si es parte del menú
        public virtual DateTime CreatedAt { get; set; } // Fecha de creación
        public virtual DateTime UpdatedAt { get; set; } // Fecha de actualización
        public virtual int CreatedBy { get; set; } // Creado por
        public virtual int UpdatedBy { get; set; } // Actualizado por
        public virtual DateTime? DeletedAt { get; set; } // Fecha de eliminación (si aplica)
        public virtual int? DeletedBy { get; set; } // Eliminado por (si aplica)
    }

    public class Pedido
    {
        public virtual int Id { get; set; }
        public virtual int Mesa { get; set; } // Número de mesa
        public virtual string Nota { get; set; } // Notas adicionales
        public virtual bool Preparando { get; set; } // Estado "Preparando"
        public virtual bool Done { get; set; } // Estado "Entregado"
        public virtual DateTime? EskaeraOrdua { get; set; } // Hora de la solicitud
        public virtual DateTime? DoneAt { get; set; } // Hora de entrega
        public virtual Platera Plato { get; set; } // Relación con la clase Platera
        public virtual string Egoera { get; set; } // Estado del pedido

        // Propiedad auxiliar para obtener el nombre del plato
        public virtual string NombrePlato => Plato?.Izena ?? "No tiene plato";

        // Método para crear un nuevo pedido
        public virtual void CrearPedido(Pedido nuevoPedido)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(nuevoPedido);
                transaction.Commit();
            }
        }

        // Método para obtener un pedido por ID
        public virtual Pedido ObtenerPedidoPorId(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<Pedido>(id);
            }
        }

        // Método para actualizar un pedido
        public virtual void ActualizarPedido(Pedido pedido)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(pedido);
                transaction.Commit();
            }
        }

        // Método para eliminar un pedido
        public virtual void EliminarPedido(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var pedido = session.Get<Pedido>(id);
                if (pedido != null)
                {
                    session.Delete(pedido);
                    transaction.Commit();
                }
            }
        }
    }
}
