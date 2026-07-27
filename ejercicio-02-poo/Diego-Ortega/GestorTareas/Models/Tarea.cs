using System;
using System.Text.Json.Serialization;
using GestorTareas.Interfaces;

namespace GestorTareas.Models
{
    [JsonDerivedType(typeof(Tarea), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(TareaConVencimiento), typeDiscriminator: "vencimiento")]
    public class Tarea : IExportable
    {
        private static int _contadorId = 1;

        public int Id { get; private set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Prioridad Prioridad { get; set; }
        public string Categoria { get; set; }
        public bool Completada { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Tarea()
        {
            Id = _contadorId++;
            FechaCreacion = DateTime.Now;
            Completada = false;
        }

        public static void SetUltimoId(int ultimoId)
        {
            _contadorId = ultimoId + 1;
        }

        public virtual void MostrarInfo()
        {
            Console.WriteLine($"[{Id}] {Titulo} | Prioridad: {Prioridad} | Categoría: {Categoria} | Completada: {(Completada ? "Sí" : "No")}");
        }

        public string Exportar()
        {
            return $"{Id}|{Titulo}|{Prioridad}|{Completada}";
        }
    }
}