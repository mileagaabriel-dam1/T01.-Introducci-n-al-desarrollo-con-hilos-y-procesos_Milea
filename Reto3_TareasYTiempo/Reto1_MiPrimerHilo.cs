namespace HILOS
{
    // Reto 1: Mi primer hilo
    // Tres versiones seguidas para comparar dónde se pone el Join:
    //   1) sin Join, 2) Join justo después de Start, 3) Join al final.
    internal class Reto1_MiPrimerHilo
    {
        public static void Ejecutar()
        {
            Console.WriteLine("=== 1) Sin Join ===");
            SinJoin();
            Thread.Sleep(3000); // espera a que el hilo de la prueba anterior acabe antes de la siguiente

            Console.WriteLine();
            Console.WriteLine("=== 2) Join justo después de Start ===");
            JoinInmediato();

            Console.WriteLine();
            Console.WriteLine("=== 3) Join al final ===");
            JoinAlFinal();
        }

        static void ContarDel1Al5()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"  [Hilo secundario {Environment.CurrentManagedThreadId}] {i}");
                Thread.Sleep(500);
            }
        }

        static void MensajesPrincipal()
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"[Hilo principal {Environment.CurrentManagedThreadId}] Mensaje propio {i}");
                Thread.Sleep(300);
            }
        }

        static void SinJoin()
        {
            var hilo = new Thread(ContarDel1Al5);
            hilo.Start();
            MensajesPrincipal();
            Console.WriteLine("[Hilo principal] He terminado (el secundario puede seguir contando)");
        }

        static void JoinInmediato()
        {
            var hilo = new Thread(ContarDel1Al5);
            hilo.Start();
            hilo.Join(); // el principal se bloquea aquí hasta que el secundario termine
            MensajesPrincipal();
            Console.WriteLine("[Hilo principal] He terminado");
        }

        static void JoinAlFinal()
        {
            var hilo = new Thread(ContarDel1Al5);
            hilo.Start();
            MensajesPrincipal(); // se ejecuta en paralelo con el secundario
            hilo.Join();         // y solo al final esperamos a que acabe
            Console.WriteLine("[Hilo principal] He terminado, y el secundario también");
        }
    }
}