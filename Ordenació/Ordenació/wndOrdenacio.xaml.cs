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
        Ellipse[] arrayEllipses;
        Rectangle[] arrayRectangles;
        Random random = new Random();
        List<int> llistaNumeros;
        SolidColorBrush colorCorrecte,colorIncorecte,colorCanvi;
        
        
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



        private void btnGenera_Click(object sender, RoutedEventArgs e)
        {
            PartidaComençada();
            if (comTipus.SelectedIndex == 0)//barres
            {
                arrayRectangles=new Rectangle[(int)sldNelem.Value];
                 
                GeneracioVector();
                for (int i = 0; i < vectorOrdenat.Length; i++)
                {
                    
                    Rectangle r = new Rectangle();
                    r.Width = cvsOrdenacio.Width / vectorOrdenat.Length;
                    r.Fill = CanvisColors(i,vectorOrdenat[i]);
                    r.StrokeThickness = sldGruix.Value;
                    r.Stroke = Brushes.Black;
                    r.Height = vectorOrdenat[i] * r.Width;
                    Canvas.SetBottom(r,0);
                    Canvas.SetLeft(r, r.Width * i);
                    
                    cvsOrdenacio.Children.Add(r);
                    arrayRectangles[i] = r;
                }
              
            }
            else //punts
            {
                arrayEllipses= new Ellipse[(int)sldNelem.Value];
                
                GeneracioVector();
                int j = 0;
                for (int i = 0; i < vectorOrdenat.Length; i++)
                {
                    
                    Ellipse el = new Ellipse();
                    el.Width = cvsOrdenacio.Width / vectorOrdenat.Length;
                    el.Fill = CanvisColors(i, vectorOrdenat[i]);
                    el.StrokeThickness = sldGruix.Value;
                    el.Stroke = Brushes.Black;
                    el.Height = el.Width;
                    Canvas.SetBottom(el,el.Height*j);
                    Canvas.SetLeft(el, el.Width * i);
                    j++;
                    cvsOrdenacio.Children.Add(el);
                    arrayEllipses[i] = el;
                }

            }

        }

        private void GeneracioVector()
        {
            if (cbAleatori.IsChecked == false && cbInvertit.IsChecked == false)
            {
                
                vectorOrdenat = CreadorVectorOrdenat((int)sldNelem.Value);

            }
            else if (cbAleatori.IsChecked == true)
            {
                llistaNumeros=new List<int>((int)sldNelem.Value);
               
                vectorOrdenat = CreadorVectorOrdenatAleatoriament((int)sldNelem.Value, llistaNumeros, random);
            }
            else if (cbInvertit.IsChecked == true)
            {
               
                vectorOrdenat = CreadorVectorOrdenatInversament((int)sldNelem.Value);
            }
        }




        #region Mètodes d'ordenació i creació 
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
            
            if (comTipus.SelectedIndex == 0)
            {
                temp = arrayRectangles[i].Height;
                if (cbIntercanvis.IsChecked == true)
                {
                    arrayRectangles[i].Fill = colorCanvi;
                    arrayRectangles[j].Fill = colorCanvi;
                    Espera(sldTemps.Value);
                    DoEvents();
                }

                arrayRectangles[i].Height = cvsOrdenacio.Height / (int)sldNelem.Value * a;
                arrayRectangles[j].Height = cvsOrdenacio.Height / (int)sldNelem.Value * b;

                arrayRectangles[i].Fill = CanvisColors(i, a);
                arrayRectangles[j].Fill = CanvisColors(j, b);
                Espera(sldTemps.Value);
                DoEvents();
            }
            else
            {
                temp = arrayEllipses[i].Height;
                if (cbIntercanvis.IsChecked == true)
                {
                    arrayEllipses[i].Fill = colorCanvi;
                    arrayEllipses[j].Fill = colorCanvi;
                    Espera(sldTemps.Value);
                    DoEvents();
                }

                arrayEllipses[i].Height = cvsOrdenacio.Height / (int)sldNelem.Value * a;
                arrayEllipses[j].Height = cvsOrdenacio.Height / (int)sldNelem.Value * b;

                arrayEllipses[i].Fill = CanvisColors(i, a);
                arrayEllipses[j].Fill = CanvisColors(j, b);
                Espera(sldTemps.Value);
                DoEvents();
            }
           

        }
        private  void SeleccioDirecta(int[] vector)
        {
            int menor, posicion;

            for (int i = 0; i < vector.Length-1; i++)
            {
                menor = vector[i];
                posicion = i;

                for (int j = i + 1; j < vector.Length; j++)
                {
                    if (vector[j] < menor)
                    {
                        menor = vector[j];
                        posicion = j;
                    }
                    
                }

                if (posicion != i)
                {
                    Intercanvi( ref vector[i] , ref vector[posicion], i,  posicion);
                   
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
            
            Particio(taula, 0, taula.Length - 1, 0);
            
        }
        void  Particio(int[] numeros, int esq, int drt, int nComp)
        {
            int i, j, x;
            i = esq;
            j = drt;
            x = numeros[(esq + drt) / 2];
            do
            {
                while (numeros[i] < x)
                {
                    nComp++;
                    i++;
                }
                while (x < numeros[j])
                {
                    nComp++;
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
                Particio(numeros, esq, j, nComp);
            if (i < drt)
                 Particio(numeros, i, drt, nComp);
           
        }
        void Ajustar(int[] numeros, int esq, int drt)
        {
            int i, j, x;
            
            i = esq;
            j = 2 * esq;
            x = numeros[esq];
            if ((j < drt) && (numeros[j] < numeros[j + 1]))
            {
                
                j++;
            }
            while ((j <= drt) && (x < numeros[j]))
            {
                numeros[i] = numeros[j];
                i = j;
                j = j * 2;
                if ((j < drt) && (numeros[j] < numeros[j + 1]))
                {
                    
                    j++;
                }
            }
            numeros[i] = x;
            
        }
        void  HeapSort(int[] numeros)
        {
            int esq, drt;
            esq = ((numeros.Length - 1) / 2) + 1;
            drt = numeros.Length - 1;
            while (esq > 0)
            {
                esq--;
                 Ajustar(numeros, esq, drt);
            }
            while (drt > 0)
            {
                Intercanvi(  ref numeros[0], ref numeros[drt],0,drt);
                drt--;
                 Ajustar(numeros, 0, drt);
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
        private void PartidaComençada()
        {
            sldNelem.IsEnabled = false;
            sldRadi.IsEnabled = false;            
            comTipus.IsEnabled = false;
            comOrdenacio.IsEnabled = false;
            cbAleatori.IsEnabled = false;
            cbInvertit.IsEnabled = false;
        }
        private void PartidaAcabada()
        {
            sldNelem.IsEnabled = true;
            sldRadi.IsEnabled =true;
            comTipus.IsEnabled = true;
            comOrdenacio.IsEnabled = true;
            cbAleatori.IsEnabled = true;
            cbInvertit.IsEnabled = true;
        }

        #endregion

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

        private void btnReinciar_Click(object sender, RoutedEventArgs e)
        {
            PartidaAcabada();
            cvsOrdenacio.Children.Clear();
            vectorOrdenat = null;
            

        }

        private SolidColorBrush CanvisColors(int index,int valor)
        {
            return index + 1 == valor ? colorCorrecte : colorIncorecte;
        }
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
