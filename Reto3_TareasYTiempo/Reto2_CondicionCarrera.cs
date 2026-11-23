namespace HILOS
{
    // Reto 2: Condición de carrera
    // Dos hilos incrementan la misma variable compartida. Se prueba sin protección,
    // con lock y con Interlocked.Increment, cinco veces cada uno.
    internal class Reto2_CondicionCarrera
    {
        static int contador = 0;
        static readonly object candado = new();

        public static void Ejecutar()
        {
            Console.Write("Iteraciones por hilo (Enter = 100000): ");
            string? texto = Console.ReadLine();
            int iteraciones = string.IsNullOrWhiteSpace(texto) ? 100_000 : int.Parse(texto);
            Console.WriteLine($"Resultado esperado: {iteraciones * 2:N0}\n");

            Probar("Sin protección", iteraciones, () => contador++);
            Probar("Con lock", iteraciones, () => { lock (candado) { contador++; } });
            Probar("Con Interlocked", iteraciones, () => Interlocked.Increment(ref contador));
        }

        static void Probar(string nombre, int iteraciones, Action incrementar)
        {
            int esperado = iteraciones * 2;
            Console.WriteLine($"--- {nombre} ---");
            for (int ejecucion = 1; ejecucion <= 5; ejecucion++)
            {
                contador = 0;
                var reloj = System.Diagnostics.Stopwatch.StartNew();

                var h1 = new Thread(() => { for (int i = 0; i < iteraciones; i++) incrementar(); });
                var h2 = new Thread(() => { for (int i = 0; i < iteraciones; i++) incrementar(); });
                h1.Start(); h2.Start();
                h1.Join(); h2.Join();

                reloj.Stop();
                string estado = contador == esperado ? "OK" : $"faltan {esperado - contador:N0}";
                Console.WriteLine($"Ejecución {ejecucion}: {contador,12:N0}  ({estado})  {reloj.ElapsedMilliseconds} ms");
            }
            Console.WriteLine();
        }
    }
}