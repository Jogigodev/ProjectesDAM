using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivial.Infrastructure;

namespace Trivial.Model
{
    public class Pregunta:ObservableBase
    {
        String id;
        String enunciat;
        ObservableCollection<String> respostes;
        Dificultat dificultat;
        String respostaCorrecta;
        Tema temaPregunta;

        public Pregunta()
        {
          
             respostes = new ObservableCollection<String>() { "", "", "", "" };

                
        }

       

        public string Id { get => id; set =>SetProperty(ref id ,value); }
        public string Enunciat { get => enunciat; set => SetProperty(ref enunciat, value); }
        public ObservableCollection<String> Respostes { get => respostes; set => SetProperty(ref respostes, value); }
        public Dificultat Dificultat { get => dificultat; set => SetProperty(ref dificultat, value); }
        public Tema Tema { get => temaPregunta; set => SetProperty(ref temaPregunta, value); }
        public string RespostaCorrecta { get => respostaCorrecta; set => SetProperty(ref respostaCorrecta, value); }

        public override string ToString()
        {
            return enunciat;
        }
    }
}
