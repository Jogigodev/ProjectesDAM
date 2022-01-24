using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Trivial.Infrastructure;
using Trivial.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace Trivial.ViewModel
{
    public class PreguntesViewModel:ObservableBase
    {
        private Preguntes preguntes;
        private Pregunta preguntaActual;
        private string respostaSeleccionada;
        private int NEncerts;
        private int NErrors;
        private int preguntesTotalsJoc;
        public PreguntesViewModel()
        {
            Preguntes = new Preguntes();
            
            Preguntes.PreguntesJoc = new ObservableCollection<Pregunta>();



            AfegeixPreguntaCommand = new RelayCommand(p => AfegeixPregunta(p), p => EsPotAfegirPregunta(p));
            EditaPreguntaCommand = new RelayCommand(p => EditaPregunta(), p => EsPotEditarPregunta(p));
            EliminaPreguntaCommand = new RelayCommand(p => EliminaPregunta(), p => EsPotEliminarPregunta(p));
            CarregaCommand = new RelayCommand(ruta => Carrega(ruta.ToString()));
            DesaCommand = new RelayCommand(ruta => Desa(ruta.ToString()));
            SeguentPreguntaCommand= new RelayCommand(p => SeguentPregunta(), p => EsPotSeguentPregunta(p));
            JocFinalitzatCommand= new RelayCommand(p => JocFinalitzat(), p => EsPotFinalitzar(p));
        }

        private bool EsPotFinalitzar(object p)
        {
            return Preguntes.PreguntesJoc.Count == 0;
        }

        private void JocFinalitzat()
        {
            MessageBox.Show($"Preguntes totals: {PreguntesTotalsJoc} Encerts {NEncerts} Errors {NErrors}");
            NEncerts = 0;
            NErrors = 0;
            PreguntesTotalsJoc = 0;
        }

        private bool EsPotSeguentPregunta(object p)
        {
            return Preguntes.PreguntesJoc.Count > 0;
        }

        private void SeguentPregunta()
        {
            
           
            
                
                if (Preguntes.PreguntesJoc[0].RespostaCorrecta == respostaSeleccionada) NEncerts++;
                else NErrors++;
                Preguntes.PreguntesJoc.RemoveAt(0);
                
            
            
            
                
                
            
            

        }

        private bool EsPotEliminarPregunta(object p)
        {
            return Preguntes.LlistaPreguntes.Count >0;
        }

        private void EliminaPregunta()
        {
            
            Preguntes.LlistaPreguntes.Remove(PreguntaActual);
            
        }

        private bool EsPotEditarPregunta(object p)
        {
           return Preguntes.LlistaPreguntes.Count > 0; 
        }

        private void EditaPregunta()
        {
            
            Preguntes.LlistaPreguntes[Preguntes.LlistaPreguntes.IndexOf(PreguntaActual)] = PreguntaActual;
            PreguntaActual = new Pregunta();
        }

        private bool EsPotAfegirPregunta(object p)
        {
            return true;
        }

        private void AfegeixPregunta(object p)
        {
            Preguntes.LlistaPreguntes.Add(PreguntaActual);
            PreguntaActual = new Pregunta();

        }
        

        public Preguntes Preguntes { get => preguntes; set => SetProperty(ref preguntes, value); }
        public Pregunta PreguntaActual { get => preguntaActual; set => SetProperty(ref preguntaActual, value); }

        public string RespostaSeleccionada { get => respostaSeleccionada; set => SetProperty(ref respostaSeleccionada, value); }
        public RelayCommand AfegeixPreguntaCommand { get; private set; }
        public RelayCommand EditaPreguntaCommand { get; private set; }
        public RelayCommand EliminaPreguntaCommand { get; private set; }
        public RelayCommand CarregaCommand { get; private set;}
        public RelayCommand DesaCommand { get; private set; }
        public RelayCommand SeguentPreguntaCommand { get; private set; }
        public RelayCommand JocFinalitzatCommand { get; private set; }
        public Array PossiblesTemas
        {
            get => Enum.GetValues(typeof(Tema));
        }
        public Array PossiblesDificultats
        {
            get => Enum.GetValues(typeof(Dificultat));
        }
        public int PreguntesTotalsJoc { get => preguntesTotalsJoc; set => SetProperty(ref preguntesTotalsJoc, value); }

        private void Desa(string ruta)
        {
            using (TextWriter fitxer = new StreamWriter(ruta))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Preguntes));
                serializer.Serialize(fitxer, Preguntes);
            }
        }

        private void Carrega(string ruta)
        {
            using (TextReader fitxer = new StreamReader(ruta))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Preguntes));
                Preguntes = (Preguntes)serializer.Deserialize(fitxer);
                foreach (Pregunta item in Preguntes.LlistaPreguntes)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        item.Respostes.Remove("");
                    }
                    
                }
            }
        }

       

    }
}
