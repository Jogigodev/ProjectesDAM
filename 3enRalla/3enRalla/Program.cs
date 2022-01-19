using System;

namespace _3enRalla
{
    class Program
    {
        static void Main(string[] args)
        {
            Servidor s = new Servidor();
            s.ListenAsync();
            Console.ReadKey();
            s.StopListen();
        }
    }
}
