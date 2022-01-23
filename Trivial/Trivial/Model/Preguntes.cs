using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.Model
{
    public class Preguntes
    {
        public ObservableCollection<Pregunta> LlistaPreguntes { get; set; }
        public ObservableCollection<Pregunta> PreguntesJoc { get; set; }
        public Preguntes()
        {
            LlistaPreguntes = new ObservableCollection<Pregunta>();
        }
    }
}
