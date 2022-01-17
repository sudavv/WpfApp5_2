using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;

namespace WpfApp3_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

     

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {//black
            if (Ink1 != null)
            {
                Ink1.DefaultDrawingAttributes.Color = Colors.Black;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ink1 != null)
            {             
                DrawingAttributes inkDA = new DrawingAttributes();                      
                inkDA.Height = inkDA.Width = Convert.ToInt32(((sender as ComboBox).SelectedValue as TextBlock).Text);
                inkDA.FitToCurve = false;
                inkDA.Color = Ink1.DefaultDrawingAttributes.Color;
                Ink1.DefaultDrawingAttributes = inkDA;//.Height = Convert.ToInt32(((sender as ComboBox).SelectedValue as TextBlock));
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (Ink1 != null)
            {
               string mode = ((sender as ComboBox).SelectedValue as TextBlock).Text;

                if ((((sender as ComboBox).SelectedValue as TextBlock).Text).Equals("Ластик"))
                {
                    Ink1.EditingMode = InkCanvasEditingMode.EraseByPoint;
                }
                else
                {
                    Ink1.EditingMode = InkCanvasEditingMode.Ink;
                }
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {//red
            if (Ink1 != null)
            {
                Ink1.DefaultDrawingAttributes.Color = Colors.Red;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {//выход           
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {//сохранить
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы с кривыми (*.strokes)|*.strokes|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                Ink1.Strokes.Save(fs);
                fs.Close();
            }
                    



        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {//открыть
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.strokes)|*.strokes|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var fs = new FileStream(openFileDialog.FileName,
                FileMode.Open, FileAccess.Read);
                StrokeCollection strcol = new StrokeCollection(fs);

                Ink1.Strokes = strcol;
            }
        }
    }

}
