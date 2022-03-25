using System;
using System.Collections;
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

namespace Calcolatrice
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Queue operazioni = new Queue();
        Stack cronologia = new Stack();

        double result = 0;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void generic_Click(object sender, RoutedEventArgs e)
        {
            Risultato.Text += ((Button)sender).Content;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            result = 0;
            Risultato.Text = "";
        }

        private void operator_Click(object sender, RoutedEventArgs e)
        {
            if (Risultato.Text != "")
            {
                try
                {
                    operazioni.Enqueue(Convert.ToDouble(Risultato.Text)); //insert number
                    operazioni.Enqueue(((Button)sender).Content); //insert operator

                    cronologia.Push(Convert.ToDouble(Risultato.Text));

                    Risultato.Text = "";
                }
                catch (Exception ex)
                {
                    Risultato.Text = "";
                }
            }
        }
        private void risultato_button_Click(object sender, RoutedEventArgs e)
        {
            if (Risultato.Text != "")
            {
                try
                {
                    operazioni.Enqueue(Convert.ToDouble(Risultato.Text));

                    result = Convert.ToDouble(operazioni.Dequeue());
                    char content;
                    int n = operazioni.Count;

                    for (int i = 0; i < n/2; i++)
                    {
                        switch (content = (Convert.ToChar(operazioni.Dequeue())))
                        {
                            case '*':
                                result *= Convert.ToDouble(operazioni.Dequeue());
                                break;
                            case '+':
                                result += Convert.ToDouble(operazioni.Dequeue());
                                break;
                            case '-':
                                result -= Convert.ToDouble(operazioni.Dequeue());
                                break;
                            case '/':
                                result /= Convert.ToDouble(operazioni.Dequeue());
                                break;
                        }
                    }
                    Risultato.Text = Convert.ToString(result);
                }
                catch (Exception ex)
                {
                    Risultato.Text = "";
                }
            }
        }
        private void undo_click(object sender, RoutedEventArgs e)
        {
            if (cronologia.Count > 0)
            {
                Risultato.Text = Convert.ToString(cronologia.Pop()); //remove last element inserted in the results stack
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close(); //close form
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) //if the button pressed is the left mouse button
            {
                DragMove(); //move the form
            }
        }

        private void History_button_Click(object sender, RoutedEventArgs e)
        {
            ListBox_cronologia.Items.Clear(); //remove all the elements contained before
            foreach (object obj in cronologia) //add items to listbox before showing it
            {
                ListBox_cronologia.Items.Add(Convert.ToString(obj));
            }

            //change component's visibility.
            label_Cronologia.Visibility = label_Cronologia.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
            ListBox_cronologia.Visibility = ListBox_cronologia.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
