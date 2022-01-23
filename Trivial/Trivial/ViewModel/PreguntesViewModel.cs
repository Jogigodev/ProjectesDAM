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

namespace Trivial.ViewModel
{
    public class PreguntesViewModel:ObservableBase
    {
        private Preguntes preguntes;
        private Pregunta preguntaActual;
        public PreguntesViewModel()
        {
            Preguntes = new Preguntes();
            Preguntes.LlistaPreguntes.Add(new Pregunta { Id = "1", Enunciat = "La Marta es droga?", Dificultat = 1, Tema = Tema.Ciència, RespostaCorrecta = "Si", Respostes = new ObservableCollection<string>() { "si", "por supuesto", "efectivament", "Llogicament" } });




            AfegeixPreguntaCommand = new RelayCommand(p => AfegeixPregunta(p), p => EsPotAfegirPregunta(p));
            EditaPreguntaCommand = new RelayCommand(p => EditaPregunta(p), p => EsPotEditarPregunta(p));
            EliminaPreguntaCommand = new RelayCommand(p => EliminaPregunta(p), p => EsPotEliminarPregunta(p));
            CarregaCommand = new RelayCommand(ruta => Carrega(ruta.ToString()));
            DesaCommand = new RelayCommand(ruta => Desa(ruta.ToString()));
        }

        private bool EsPotEliminarPregunta(object p)
        {
            return Preguntes.LlistaPreguntes.Count >= 1;
        }

        private void EliminaPregunta(object p)
        {
            Preguntes.LlistaPreguntes.Remove(PreguntaActual);
        }

        private bool EsPotEditarPregunta(object p)
        {
            return Preguntes.LlistaPreguntes.Count >= 1;
        }

        private void EditaPregunta(object p)
        {
            throw new NotImplementedException();
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

        public RelayCommand AfegeixPreguntaCommand { get; private set; }
        public RelayCommand EditaPreguntaCommand { get; private set; }
        public RelayCommand EliminaPreguntaCommand { get; private set; }
        public RelayCommand CarregaCommand { get; private set; }
        public RelayCommand DesaCommand { get; private set; }
        public Array PossiblesTemas
        {
            get => Enum.GetValues(typeof(Tema));
        }

        private void Desa(string ruta)
        {
            using (TextWriter fitxer = new StreamWriter(ruta))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Pregunta));
                serializer.Serialize(fitxer, Preguntes);
            }
        }

        private void Carrega(string ruta)
        {
            using (TextReader fitxer = new StreamReader(ruta))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Pregunta));
                Preguntes = (Preguntes)serializer.Deserialize(fitxer);
            }
        }

       

    }
}
