using System;

namespace GestorTareas.Models
{
    public class TareaConVencimiento : Tarea
    {
        public DateTime FechaVencimiento { get; set; }

        public int DiasRestantes
        {
            get
            {
                int dias = (FechaVencimiento.Date - DateTime.Now.Date).Days;
                return dias < 0 ? 0 : dias;
            }
        }

        public TareaConVencimiento() : base()
        {
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.WriteLine($"      -> Vence el: {FechaVencimiento.ToShortDateString()} (Faltan {DiasRestantes} días)");
        }
    }
}