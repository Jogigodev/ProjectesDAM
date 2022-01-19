using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _3enRalla
{
    public class Servidor
    {
        public HttpListener listener = null;
        public Partida partida ;
        public string msg="";
        string mostrarFormulari()
        {
            return System.IO.File.ReadAllText(@"..\..\..\Formulari.html");
        }
        //Crida asíncrona, no bloqueja la resta de programa.
        public async void ListenAsync()
        {
            //Crida al servidor Web HTTP Api de Windows.
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:51111/");  // Listen on
            listener.Start();                                         // port 51111.
            while (true)
            {
                // Espera una petició del client. Quan la rep continua amb el codi.
                // Perque es posa un await?
                // Perque no volem que el programa quedi interromput parat en aquest punt, el client pot fer la seva feina.
                HttpListenerContext context = await listener.GetContextAsync();
                Console.WriteLine(context.Request.RawUrl);
                //Quan hagim rebut la petició la processem.
                
                var camps = context.Request.RawUrl.Split("?");
                if (camps.Length > 1)
                {
                    var clauvalor = camps[1].Split("&");
                    var clauJugador = clauvalor[0].Split("=");
                    var clauFila = clauvalor[1].Split("=");
                    var clauColumna = clauvalor[2].Split("=");

                    if (clauJugador[1] == "X")
                    {
                        if(partida.Torn=='X')
                        {
                            msg=partida.Jugar(partida.Jugador1, Convert.ToInt32(clauFila[1]), Convert.ToInt32(clauColumna[1]));
                            Console.WriteLine(msg);
                        }
                        else
                        {
                            Error();
                        }
                    }
                    else if (clauJugador[1] == "O")
                    {
                        if (partida.Torn == 'O')
                        {
                            msg=partida.Jugar(partida.Jugador2, Convert.ToInt32(clauFila[1]), Convert.ToInt32(clauColumna[1]));
                            Console.WriteLine(msg);

                        }
                        else
                        {
                            Error();
                        }
                    }

                }
                else
                {
                    if (camps[0] == "/obtenirJugador")
                    {
                        if (partida is null)
                        {
                            partida = new Partida();
                            partida.AfegirJugador();
                            msg = partida.Jugador1.Nom.ToString();
                            Console.WriteLine(partida.Jugador1.Nom.ToString());
                        }
                        else if (partida.NJugadors == 1)
                        {
                            partida.AfegirJugador();
                            msg = partida.Jugador2.Nom.ToString();
                            Console.WriteLine(partida.Jugador2.Nom.ToString());
                        }
                        else
                        {
                            msg="Error,partida complerta";
                            Console.WriteLine("Error,partida complerta");
                        }
                    }
                    else if (camps[0] == "/esborrar")
                    {
                        if (partida != null)
                        {
                            partida = new Partida();
                            msg = "ok";
                            Console.WriteLine("ok");
                        }
                        else
                        {
                            Error();
                        }
                         

                    }
                    else if (camps[0] == "/obtenirTauler")
                    {
                        msg=partida.InfoPartida();
                        Console.WriteLine(msg);
                    }
                }
                

                context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                using (Stream s = context.Response.OutputStream)
                using (StreamWriter writer = new StreamWriter(s))
                    await writer.WriteAsync(msg); //Escriu quan puguis. Continua amb la resta de programa, quan acabis continua per aquí.
            }

        }
        public void StopListen()
        {
            if (listener != null) listener.Stop();
        }
        public void Error()
        {
            msg = "Error";
            Console.WriteLine("Error");
        }
    }
}
