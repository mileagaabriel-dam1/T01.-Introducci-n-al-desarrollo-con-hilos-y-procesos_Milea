using System.Diagnostics;

namespace HILOS
{
    // Reto 3: Tareas y tiempo
    // Tres "descargas" simuladas de 1, 2 y 3 segundos, primero en secuencia
    // y después a la vez con Task.WhenAll. Al final se repite con Thread.Sleep.
    internal class Reto3_TareasYTiempo
    {
        public static async Task Ejecutar()
        {
            var reloj = Stopwatch.StartNew();
            await Descargar("archivo1", 1);
            await Descargar("archivo2", 2);
            await Descargar("archivo3", 3);
            Console.WriteLine($"Secuencial: {reloj.Elapsed.TotalSeconds:F2} s\n");

            reloj.Restart();
            await Task.WhenAll(Descargar("archivo1", 1), Descargar("archivo2", 2), Descargar("archivo3", 3));
            Console.WriteLine($"En paralelo con WhenAll: {reloj.Elapsed.TotalSeconds:F2} s\n");

            // Misma prueba, pero con Thread.Sleep: bloquea el hilo en vez de liberarlo
            reloj.Restart();
            await Task.WhenAll(DescargarConSleep("archivo1", 1), DescargarConSleep("archivo2", 2), DescargarConSleep("archivo3", 3));
            Console.WriteLine($"WhenAll con Thread.Sleep: {reloj.Elapsed.TotalSeconds:F2} s");
        }

        static async Task Descargar(string nombre, int segundos)
        {
            Console.WriteLine($"  Empieza {nombre} ({segundos} s) en hilo {Environment.CurrentManagedThreadId}");
            await Task.Delay(segundos * 1000); // espera sin ocupar ningún hilo
            Console.WriteLine($"  Termina {nombre}");
        }

        // Sin ningún await antes del Sleep, el método bloquea al que lo llama hasta terminar,
        // así que las "descargas" acaban ejecutándose una detrás de otra.
        static async Task DescargarConSleep(string nombre, int segundos)
        {
            Console.WriteLine($"  Empieza {nombre} ({segundos} s) en hilo {Environment.CurrentManagedThreadId}");
            Thread.Sleep(segundos * 1000);
            Console.WriteLine($"  Termina {nombre}");
            await Task.CompletedTask;
        }
    }
}