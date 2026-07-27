# 📝 Gestor de Tareas (Consola C#)

Este proyecto es una aplicación de consola desarrollada en C# y .NET, creada como solución al **Ejercicio 2 del Módulo de Programación Orientada a Objetos (POO)**. Permite gestionar tareas diarias aplicando los pilares fundamentales de la POO y guardando los datos de forma persistente.

## 🚀 Características Principales

- **Gestión de Tareas:** Creación de tareas estándar y tareas con fecha de vencimiento.
- **Persistencia de Datos:** Guardado y carga automática de las tareas en un archivo `tareas.json` utilizando `System.Text.Json`.
- **Filtros Avanzados:** Búsqueda de tareas por categoría, por nivel de prioridad o filtrado de tareas vencidas usando LINQ.
- **Exportación:** Implementación de interfaces para exportar los datos de las tareas en un formato de texto plano (Pipe-separated).
- **Menú Interactivo:** Interfaz amigable por consola para realizar operaciones CRUD (Crear, Leer, Actualizar - Completar, Eliminar).

## 🧠 Conceptos de POO Aplicados

Este proyecto fue diseñado para demostrar el dominio de los siguientes conceptos:

- **Clases y Objetos:** Modelado del dominio del problema (`Tarea`, `Categoria`, `Prioridad`).
- **Encapsulamiento:** Uso de propiedades (`{ get; set; }`) para proteger el estado interno de las clases, limitando modificaciones no deseadas (ej. `Id` autoincremental).
- **Herencia:** Creación de la clase `TareaConVencimiento` que extiende la funcionalidad de la clase base `Tarea`.
- **Polimorfismo:** Sobrescritura del método `MostrarInfo()` con la palabra clave `override` para mostrar detalles adicionales dependiendo del tipo de tarea en tiempo de ejecución.
- **Interfaces:** Implementación de la interfaz `IExportable` para estandarizar la exportación de datos en distintos formatos.

## Creador
Diego Ortega

