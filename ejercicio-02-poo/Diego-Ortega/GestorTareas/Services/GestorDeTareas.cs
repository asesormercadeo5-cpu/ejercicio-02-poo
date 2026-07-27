using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GestorTareas.Models;

namespace GestorTareas.Services
{
    public class GestorDeTareas
    {
        private List<Tarea> _tareas;

        public GestorDeTareas()
        {
            _tareas = new List<Tarea>();
        }

        public void Agregar(Tarea tarea)
        {
            _tareas.Add(tarea);
        }

        public void Completar(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea != null)
            {
                tarea.Completada = true;
                Console.WriteLine("Tarea completada con éxito.");
            }
            else
            {
                Console.WriteLine("Tarea no encontrada.");
            }
        }

        public List<Tarea> ListarTodas() => _tareas;

        public List<Tarea> ListarPorCategoria(string categoria) =>
            _tareas.Where(t => t.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Tarea> ListarPorPrioridad(Prioridad prioridad) =>
            _tareas.Where(t => t.Prioridad == prioridad).ToList();

        public List<Tarea> ObtenerVencidas() =>
            _tareas.OfType<TareaConVencimiento>()
                   .Where(t => DateTime.Compare(t.FechaVencimiento.Date, DateTime.Now.Date) < 0 && !t.Completada)
                   .Cast<Tarea>()
                   .ToList();

        public void Eliminar(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea != null)
            {
                _tareas.Remove(tarea);
                Console.WriteLine("Tarea eliminada.");
            }
            else
            {
                Console.WriteLine("Tarea no encontrada.");
            }
        }

        public void GuardarEnJSON(string archivo)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_tareas, options);
                File.WriteAllText(archivo, json);
                Console.WriteLine($"Datos guardados automáticamente en '{archivo}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        public void CargarDeJSON(string archivo)
        {
            try
            {
                if (File.Exists(archivo))
                {
                    string json = File.ReadAllText(archivo);
                    _tareas = JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();
                    
                    if (_tareas.Any())
                    {
                        int maxId = _tareas.Max(t => t.Id);
                        Tarea.SetUltimoId(maxId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo JSON. Se iniciará una lista vacía. Detalle: {ex.Message}");
                _tareas = new List<Tarea>();
            }
        }
    }
}