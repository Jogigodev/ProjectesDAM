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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace Trivial.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LlistatPreguntes : Window
    {
        public LlistatPreguntes()
        {
            InitializeComponent();
            
        }

        

        private void btnAfegeix_Click(object sender, RoutedEventArgs e)
        {
            Window finestra = new PreguntaView();
            finestra.DataContext = this.DataContext;
            ((PreguntesViewModel)DataContext).PreguntaActual = new Model.Pregunta();
            finestra.ShowDialog();
        }

        private void btnDesa_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fitxer XML | *.xml";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = "Tria el nom del fitxer";
            if (saveFileDialog.ShowDialog().Value)
            {
                ((PreguntesViewModel)DataContext).DesaCommand.Execute(saveFileDialog.FileName);
            }
        }

        private void btnCarrega_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fitxer XML | *.xml";
            openFileDialog.CheckPathExists = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Title = "Tria el nom del fitxer";
            if (openFileDialog.ShowDialog().Value)
            {
                ((PreguntesViewModel)DataContext).CarregaCommand.Execute(openFileDialog.FileName);
            }
        }

        private void btnEdita_Click(object sender, RoutedEventArgs e)
        {
            
            Window finestraEdita = new PreguntaEditaView();
            finestraEdita.DataContext = this.DataContext;
            ((PreguntesViewModel)DataContext).PreguntaActual = (Model.Pregunta)lstPreguntes.SelectedItem;
            finestraEdita.Show();
        }

        private void lstPreguntes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((PreguntesViewModel)DataContext).PreguntaActual = lstPreguntes.SelectedItem as Model.Pregunta;
        }

        

        private void btnJuga_Click(object sender, RoutedEventArgs e)
        {
            Window finestraJoc = new IniciTrivial( lstPreguntes );
            finestraJoc.DataContext = this.DataContext;
            finestraJoc.Show();
        }
    }
}
