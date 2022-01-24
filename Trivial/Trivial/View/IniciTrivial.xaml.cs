using System;
using Trivial.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Trivial.View
{
    /// <summary>
    /// Lógica de interacción para IniciTrivial.xaml
    /// </summary>
    public partial class IniciTrivial : Window
    {
        
        List<Model.Tema> temesEscollits = new List<Model.Tema>();
        public IniciTrivial(ListView llistaPreguntes)
        {
            InitializeComponent();
            
        }

        private void btnComença_Click(object sender, RoutedEventArgs e)
        {
            int index = 0,indexLlista=0;

            ((PreguntesViewModel)DataContext).PreguntesTotalsJoc = (int)sldNPreguntes.Value;
            if (cmbDificultat.SelectedItem != null)
            {
                foreach (var item in stktemesSeleccionats.Children)
                {
                    if (((CheckBox)item).IsChecked == true) temesEscollits.Add((Model.Tema)((CheckBox)item).Content);
                }
                
                while( index <sldNPreguntes.Value && indexLlista< ((PreguntesViewModel)DataContext).Preguntes.LlistaPreguntes.Count )
                {
                    if (((PreguntesViewModel)DataContext).Preguntes.LlistaPreguntes[indexLlista].Dificultat == (Model.Dificultat)cmbDificultat.SelectedItem && temesEscollits.Contains(((PreguntesViewModel)DataContext).Preguntes.LlistaPreguntes[indexLlista].Tema))
                    {
                        ((PreguntesViewModel)DataContext).Preguntes.PreguntesJoc.Add(((PreguntesViewModel)DataContext).Preguntes.LlistaPreguntes[indexLlista]);
                        index++;
                        indexLlista++;
                    }
                    indexLlista++;
                }
                if (index < sldNPreguntes.Value) ((PreguntesViewModel)DataContext).PreguntesTotalsJoc = index;
                if (((PreguntesViewModel)DataContext).Preguntes.PreguntesJoc.Count<=0) MessageBox.Show("No s'ha trobat cap pregunta amb els seguents criteris");
                else
                {
                    Window finestraJoc = new jocView();
                    finestraJoc.DataContext = this.DataContext;
                    finestraJoc.Show();
                }
            }
            else MessageBox.Show("Selecciona tots els camps");
        }
    }
}
