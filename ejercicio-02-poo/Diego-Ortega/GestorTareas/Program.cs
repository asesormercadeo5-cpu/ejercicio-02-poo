using System;
using System.Collections.Generic;
using GestorTareas.Models;
using GestorTareas.Services;

namespace GestorTareas
{
    class Program
    {
        static void Main(string[] args)
        {
            string archivoJson = "tareas.json";
            GestorDeTareas gestor = new GestorDeTareas();
            
            // Carga persistente al iniciar
            gestor.CargarDeJSON(archivoJson);

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== GESTOR DE TAREAS ===");
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Listar todas");
                Console.WriteLine("3. Listar por categoría");
                Console.WriteLine("4. Listar por prioridad");
                Console.WriteLine("5. Marcar como completada");
                Console.WriteLine("6. Mostrar tareas vencidas");
                Console.WriteLine("7. Eliminar tarea");
                Console.WriteLine("8. Exportar a JSON");
                Console.WriteLine("9. Salir");
                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AgregarTarea(gestor);
                        break;
                    case "2":
                        MostrarLista(gestor.ListarTodas(), "Todas las tareas");
                        break;
                    case "3":
                        Console.Write("Ingrese la categoría a buscar: ");
                        string cat = Console.ReadLine();
                        MostrarLista(gestor.ListarPorCategoria(cat), $"Tareas en categoría '{cat}'");
                        break;
                    case "4":
                        Console.Write("Seleccione Prioridad (0: Baja, 1: Media, 2: Alta, 3: Critica): ");
                        if (Enum.TryParse(Console.ReadLine(), out Prioridad prio))
                        {
                            MostrarLista(gestor.ListarPorPrioridad(prio), $"Tareas de prioridad {prio}");
                        }
                        else
                        {
                            Console.WriteLine("Opción de prioridad inválida.");
                            Pausar();
                        }
                        break;
                    case "5":
                        Console.Write("Ingrese ID de la tarea a completar: ");
                        if (int.TryParse(Console.ReadLine(), out int idCompletar))
                            gestor.Completar(idCompletar);
                        Pausar();
                        break;
                    case "6":
                        MostrarLista(gestor.ObtenerVencidas(), "Tareas vencidas");
                        break;
                    case "7":
                        Console.Write("Ingrese ID de la tarea a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int idEliminar))
                            gestor.Eliminar(idEliminar);
                        Pausar();
                        break;
                    case "8":
                        Console.WriteLine("\n--- EXPORTACIÓN (IExportable) ---");
                        foreach (var t in gestor.ListarTodas())
                        {
                            Console.WriteLine(t.Exportar());
                        }
                        Pausar();
                        break;
                    case "9":
                        gestor.GuardarEnJSON(archivoJson);
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Pausar();
                        break;
                }
            }
        }

        static void AgregarTarea(GestorDeTareas gestor)
        {
            Console.Write("¿Tiene vencimiento? (S/N): ");
            bool conVencimiento = Console.ReadLine()?.Trim().ToUpper() == "S";

            Tarea nuevaTarea = conVencimiento ? new TareaConVencimiento() : new Tarea();

            Console.Write("Título: ");
            nuevaTarea.Titulo = Console.ReadLine();
            
            Console.Write("Descripción: ");
            nuevaTarea.Descripcion = Console.ReadLine();
            
            Console.Write("Categoría: ");
            nuevaTarea.Categoria = Console.ReadLine();

            Console.Write("Prioridad (0: Baja, 1: Media, 2: Alta, 3: Critica): ");
            if (Enum.TryParse(Console.ReadLine(), out Prioridad p))
                nuevaTarea.Prioridad = p;

            if (conVencimiento && nuevaTarea is TareaConVencimiento tv)
            {
                Console.Write("Fecha de vencimiento (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime fecha))
                    tv.FechaVencimiento = fecha;
                else
                    tv.FechaVencimiento = DateTime.Now.AddDays(1);
            }

            gestor.Agregar(nuevaTarea);
            Console.WriteLine("¡Tarea agregada exitosamente!");
            Pausar();
        }

        static void MostrarLista(List<Tarea> lista, string titulo)
        {
            Console.WriteLine($"\n--- {titulo.ToUpper()} ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay tareas para mostrar.");
            }
            else
            {
                // Demostración de Polimorfismo: llama a MostrarInfo()
                foreach (var tarea in lista)
                {
                    tarea.MostrarInfo(); 
                }
            }
            Pausar();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
