using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Ordenacio
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class wndOrdenacio : Window
    {
        
        int[] vectorOrdenat;
        Rectangle[] arrayRectangles;
        Random random = new Random();
        List<int> llistaNumeros;
        SolidColorBrush colorCorrecte,colorIncorecte,colorCanvi;
        bool aturat = false;
        
        
        public wndOrdenacio()
        {
           
            InitializeComponent();
            colorCorrecte = new SolidColorBrush();
            colorIncorecte = new SolidColorBrush();
            colorCanvi= new SolidColorBrush();
            cpCorrecte.SelectedColor = Colors.Green;
            cpIncorrecte.SelectedColor = Colors.Red;
            cpIntercanvi.SelectedColor = Colors.Yellow;
        }

        #region Botons Interficie

        private void btnGenera_Click(object sender, RoutedEventArgs e)
        {

            ConfiguracioAcabada();

            arrayRectangles = new Rectangle[(int)sldNelem.Value];

            GeneracioVector();
            for (int i = 0; i < vectorOrdenat.Length; i++)
            {

                Rectangle r = new Rectangle();
                r.Width = cvsOrdenacio.Width / vectorOrdenat.Length;
                if (i + 1 == vectorOrdenat[i]) r.Fill = colorCorrecte;
                else r.Fill = colorIncorecte;
                r.StrokeThickness = sldGruix.Value;
                r.Stroke = Brushes.Black;
                if (comTipus.SelectedIndex == 0)
                {
                    r.RadiusX = sldRadi.Value;
                    r.RadiusY = sldRadi.Value;
                    r.Height = vectorOrdenat[i] * r.Width;
                    Canvas.SetBottom(r, 0);
                    Canvas.SetLeft(r, r.Width * i);
                }
                else
                {
                    r.RadiusX = 1000;
                    r.RadiusY = 1000;
                    r.Height = r.Width;
                    Canvas.SetLeft(r, r.Width * i);
                    Canvas.SetTop(r, cvsOrdenacio.Height - (vectorOrdenat[i] * r.Width));
                }


                cvsOrdenacio.Children.Add(r);
                arrayRectangles[i] = r;
            }
        }
        private void btnOrdena_Click(object sender, RoutedEventArgs e)
        {
            if (comOrdenacio.SelectedIndex == 0)
            {
                SeleccioDirecta(vectorOrdenat);
            }
            else if (comOrdenacio.SelectedIndex == 1)
            {
                cocktailSort(vectorOrdenat);
            }
            else if (comOrdenacio.SelectedIndex == 2)
            {
                QuickSort(vectorOrdenat);
            }
            else if (comOrdenacio.SelectedIndex == 3)
            {
                HeapSort(vectorOrdenat);
            }
        }
        private void btnAtura_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnReinciar_Click(object sender, RoutedEventArgs e)
        {
            SenseConfiguracio();
            cbAleatori.IsChecked = false;
            cbInvertit.IsChecked = false;
            cvsOrdenacio.Children.Clear();
            vectorOrdenat = null;


        }
        private void cpCorrecte_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            colorCorrecte.Color = (Color)e.NewValue;

        }

        private void cpIncorrecte_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            colorIncorecte.Color = (Color)e.NewValue;
        }

        private void cpIntercanvi_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            colorCanvi.Color = (Color)e.NewValue;
        }
        #endregion

        #region Mètodes d'ordenació i creació 
        private void GeneracioVector()
        {
            if (cbAleatori.IsChecked == false && cbInvertit.IsChecked == false)
            {

                vectorOrdenat = CreadorVectorOrdenat((int)sldNelem.Value);

            }
            else if (cbAleatori.IsChecked == true)
            {
                llistaNumeros = new List<int>((int)sldNelem.Value);

                vectorOrdenat = CreadorVectorOrdenatAleatoriament((int)sldNelem.Value, llistaNumeros, random);
            }
            else if (cbInvertit.IsChecked == true)
            {

                vectorOrdenat = CreadorVectorOrdenatInversament((int)sldNelem.Value);
            }
        }

        public static int[] CreadorVectorOrdenat(int mida)
        {
            int[] vector = new int[mida];
            int index = 0;
            for (int i = 1; i <= vector.Length; i++)
            {
                vector[index] = i;
                index++;
            }
            return vector;
        }
        public static int[] CreadorVectorOrdenatInversament(int mida)
        {
            int[] vector = new int[mida];
            int index = 0;
            for (int i = mida; i > 0; i--)
            {
                vector[index] = i;
                index++;
            }
            return vector;
        }
        public static int[] CreadorVectorOrdenatAleatoriament(int mida, List<int> llista, Random r)
        {
            for (int i = 0; i < mida; i++) llista.Add(i + 1);
            
            int[] vector = new int[mida];
            int index = 0, numero;
            for (int i = 1; i <= vector.Length; i++)
            {
                numero = llista[r.Next(0, llista.Count)];
                vector[index] = numero;
                llista.Remove(numero);
                index++;
            }
            return vector;
        }
        private  void Intercanvi( ref int a, ref  int b,  int i, int j)
        {
            double temp;
            temp = a;
            a = b;
            b =(int) temp;
            
           
                
                if (cbIntercanvis.IsChecked == true)
                {
                    arrayRectangles[i].Fill = colorCanvi;
                    arrayRectangles[j].Fill = colorCanvi;
                    Espera(sldTemps.Value);
                    DoEvents();
                }
            if (comTipus.SelectedIndex == 0)
            {
                temp = arrayRectangles[i].Height;
                arrayRectangles[i].Height = arrayRectangles[j].Height;
                arrayRectangles[j].Height = temp;
            }
            else
            {
                temp = Canvas.GetTop(arrayRectangles[i]);
                Canvas.SetTop(arrayRectangles[i], Canvas.GetTop(arrayRectangles[j]));
                Canvas.SetTop(arrayRectangles[j], temp);
            }


            if (i + 1 == a) arrayRectangles[i].Fill = colorCorrecte;
            else arrayRectangles[i].Fill = colorIncorecte;
            if (j + 1 == b) arrayRectangles[j].Fill = colorCorrecte;
            else arrayRectangles[j].Fill = colorIncorecte;


            
                Espera(sldTemps.Value);
                DoEvents();
      
           

        }
        private  void SeleccioDirecta(int[] vector)
        {
            int menor;

            for (int i = 0; i < vector.Length-1; i++)
            {
                
                menor = i;

                for (int j = i + 1; j < vector.Length; j++)
                {
                    if (vector[j] < vector[menor])
                    {
                       
                        menor = j;
                    }
                    
                }

                if (i != menor)
                {
                    Intercanvi( ref vector[i] , ref vector[menor], i,  menor);
                   
                }
            }
            

        }
         void cocktailSort(int[] a)
        {
            bool swapped = true;
            int start = 0;
            int end = a.Length;

            while (swapped == true)
            {

                
                swapped = false;

                
                for (int i = start; i < end - 1; ++i)
                {
                    if (a[i] > a[i + 1])
                    {
                        Intercanvi(ref a[i], ref a[i + 1], i, i + 1);
                       
                        swapped = true;
                    }
                }

                
                if (swapped == false)
                    break;

                
                swapped = false;

                
                end = end - 1;

                
                for (int i = end - 1; i >= start; i--)
                {

                    if (a[i] > a[i + 1])
                    {
                        Intercanvi(ref a[i], ref a[i + 1], i, i + 1);
                       
                        swapped = true;
                    }
                }

                
                start = start + 1;
            }
        }

        void QuickSort(int[] taula)
        {
            
            Particio(taula, 0, taula.Length - 1);
            
        }
        void  Particio(int[] numeros, int esq, int drt)
        {
            int i, j, x;
            i = esq;
            j = drt;
            x = numeros[(esq + drt) / 2];
            do
            {
                while (numeros[i] < x)
                {
                    i++;
                }
                while (x < numeros[j])
                {
                   
                    j--;
                }
                if (i <= j)
                {
                    Intercanvi( ref numeros[i], ref  numeros[j],i,j);
                    i++;
                    j--;
                }
            } while (i <= j);
            if (esq < j)
                Particio(numeros, esq, j );
            if (i < drt)
                 Particio(numeros, i, drt);
           
        }

        public void HeapSort(int[] arr)
        {
            int n = arr.Length;

            
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);

            
            for (int i = n - 1; i > 0; i--)
            {
                
                Intercanvi(ref arr[0], ref arr[i], 0, i);
                

                
                heapify(arr, i, 0);
            }
        }

       
        void heapify(int[] arr, int n, int i)
        {
            int largest = i; 
            int l = 2 * i + 1; 
            int r = 2 * i + 2; 

            
            if (l < n && arr[l] > arr[largest])
                largest = l;

           
            if (r < n && arr[r] > arr[largest])
                largest = r;

            
            if (largest != i)
            {
                
                Intercanvi(ref arr[i], ref arr[largest], i, largest);

                
                heapify(arr, n, largest);
            }
        }

        #endregion

        #region Canvis interficie

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(cbAleatori.IsChecked == true)
            {
                cbInvertit.IsChecked = false;
                cbInvertit.IsEnabled = false;
            }
            else
            {
                cbInvertit.IsEnabled = true;
            }
            if(cbInvertit.IsChecked == true)
            {
                cbAleatori.IsChecked = false;
                cbAleatori.IsEnabled = false;
            }
            else
            {
                cbAleatori.IsEnabled= true;
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comTipus.SelectedIndex == 1) sldRadi.IsEnabled = false;
            else sldRadi.IsEnabled = true;
        }
        private void ConfiguracioAcabada()
        {
            btnOrdena.IsEnabled = true;
            sldNelem.IsEnabled = false;
            sldRadi.IsEnabled = false;            
            comTipus.IsEnabled = false;
            comOrdenacio.IsEnabled = false;
            cbAleatori.IsEnabled = false;
            cbInvertit.IsEnabled = false;
            btnGenera.IsEnabled = false;
        }
        private void SenseConfiguracio()
        {
            sldNelem.IsEnabled = true;
            sldRadi.IsEnabled =true;
            comTipus.IsEnabled = true;
            comOrdenacio.IsEnabled = true;
            cbAleatori.IsEnabled = true;
            cbInvertit.IsEnabled = true;
            btnGenera.IsEnabled = true;
            btnOrdena.IsEnabled = false;
        }
        

        #endregion

        #region Threads
        Thread thread;
        private void Espera(double milliseconds)
        {
            var frame = new DispatcherFrame();
            thread = new Thread((ThreadStart)(() =>
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
                frame.Continue = false;
            }));
            thread.Start();
            Dispatcher.PushFrame(frame);
        }
        static Action action;

        public static void DoEvents()
        {
            action = new Action(delegate { });
            Application.Current?.Dispatcher?.Invoke(
               System.Windows.Threading.DispatcherPriority.Background,
               action);
        }
        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Dispatcher.InvokeShutdown();
            thread?.Abort();
            base.OnClosed(e);
        }
        #endregion
    }
}
