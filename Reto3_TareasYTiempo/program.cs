// Menú para elegir qué reto ejecutar
using HILOS;

Console.WriteLine("1) Reto 1 - Mi primer hilo");
Console.WriteLine("2) Reto 2 - Condición de carrera");
Console.WriteLine("3) Reto 3 - Tareas y tiempo");
Console.Write("Elige un reto: ");
string? opcion = Console.ReadLine();
Console.WriteLine();

switch (opcion)
{
    case "1": Reto1_MiPrimerHilo.Ejecutar(); break;
    case "2": Reto2_CondicionCarrera.Ejecutar(); break;
    case "3": await Reto3_TareasYTiempo.Ejecutar(); break;
    default: Console.WriteLine("Opción no válida"); break;
}