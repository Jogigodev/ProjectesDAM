using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3enRalla
{
    public class Partida
    {
        private int nJugadors;
        private char[,] tauler;
        private char jugadorGuanyador;
        private Jugador jugador1;
        private Jugador jugador2;
        private bool acabada;
        private char torn;
       

        public Partida()
        {
            Tauler = new char[3,3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
            NJugadors = 0;
            JugadorGuanyador = '-';
            Acabada = false;
            Torn = 'X';
            
        }
        public char[,] Tauler { get => tauler; set => tauler = value; }
        public int NJugadors { get => nJugadors; set => nJugadors = value; }
        public char JugadorGuanyador { get => jugadorGuanyador; set => jugadorGuanyador = value; }
        public Jugador Jugador1 { get => jugador1; set => jugador1 = value; }
        public Jugador Jugador2 { get => jugador2; set => jugador2 = value; }
        public char Torn { get => torn; set => torn = value; }
        public bool Acabada { get => acabada; set => acabada = value; }
        

        
        public void AfegirJugador()
        {
            
            if (NJugadors ==0)
            {
                 Jugador1 = new Jugador('X');
            }
            else  { Jugador2 = new Jugador('O'); }
            
            NJugadors++;

        }
        public string Jugar(Jugador jugador,int fila,int columna)
        {
            string msg = "";
            
                if (jugador.Nom == 'X')
                {
                    if (Tauler[fila, columna] == '-')
                    {
                        Tauler[fila, columna] = 'X';
                        Jugador1.Tirades++;
                        msg = "ok";
                        Torn = 'O';
                    }
                    else msg = "error";

                }
                else
                {
                    if (Tauler[fila, columna] == '-')
                    {
                        Tauler[fila, columna] = 'O';
                        Jugador2.Tirades++;
                        msg = "ok";
                        Torn = 'X';
                    }
                    else msg = "error";
                }
            return msg;
        }
            



            
       
        public void PartidaFinalitzada()
        {
            if (EsVictoria(this.Jugador1.Nom)) JugadorGuanyador=Jugador1.Nom;
            else if (EsVictoria(this.Jugador2.Nom)) JugadorGuanyador = Jugador2.Nom;
            else if(Jugador1.Tirades+Jugador2.Tirades==9) JugadorGuanyador = 'E';

            Acabada = true;
            
        }

        private bool EsVictoria(char jugador)
        {
            bool victoria = false;
            if ((Tauler[0, 0] == jugador && Tauler[0, 1] == jugador && Tauler[0, 2] == jugador) || (Tauler[1, 0] == jugador && Tauler[1, 1] == jugador && Tauler[1, 2] == jugador)
                || (Tauler[2, 0] == jugador && Tauler[2, 1] == jugador && Tauler[2, 2] == jugador) || (Tauler[0, 0] == jugador && Tauler[1, 1] == jugador && Tauler[2, 2] == jugador)
                || (Tauler[0, 2] == jugador && Tauler[1, 1] == jugador && Tauler[2, 0] == jugador) || (Tauler[0, 0] == jugador && Tauler[1, 0] == jugador && Tauler[2, 0] == jugador)
                || (Tauler[0, 1] == jugador && Tauler[1, 1] == jugador && Tauler[2, 1] == jugador) || (Tauler[0, 2] == jugador && Tauler[1, 2] == jugador && Tauler[2, 2] == jugador)) 
            {
                victoria = true;
            }
            return victoria;

        }

        public class Jugador
        {
            private char nom;
            private int tirades;

            public Jugador(char nom)
            {
                Nom = nom;
                Tirades = 0;
            }
            public char Nom { get => nom; set => nom = value; }
            public int Tirades { get => tirades; set => tirades = value; }
        }
        
    }

}
