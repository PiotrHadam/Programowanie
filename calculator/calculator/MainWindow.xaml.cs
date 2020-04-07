using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace calculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double value = 0; //pobrana wartość
        private bool operationPerformed = false; //czy został wpisany operator
        private bool isResult = false; //czy został uzyty operator "="
        private bool isZeroCommaNecessery = false; //czy jest potrzebne wpisanie 0 przed znakiem "."
        private string operation = ""; //pobrany operator
        private string error = "Error!"; //wiadomość przy błędzie
        private string errorDiv = "No division by 0!"; //wiadomość o błędzie przy dzieleniu

        //KONSTRUKTOR
        public MainWindow()
        {
            InitializeComponent();
        }

        //WPROWADZANIE LICZB I ZNAKU "."
        private void ButtonNum_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            //Czyszczenie tekstu przed wpisaniem nowej liczby, gdy został użyty klawisz "=" i nie jest konieczne wpisanie 0
            if (isResult && isZeroCommaNecessery == false)
            {
                textBlockCalc.Text = "";
                isResult = false;
            }

            isError();

            //Czyszcznie tekstu przed wpisaniem nowej liczby, gdy wpisane jest tylko 0 lub gdy został wpisany operator
            if ((textBlockCalc.Text == "0") || (operationPerformed))
            {
                textBlockCalc.Text = "";
            }

            //Wpisanie znaku "."
            if (b.Equals(buttonDot))
            {
                //Gdy znaku "." nie poprzedza operator
                if (operationPerformed == false)
                {
                    //Dopisanie "." tylko gdy nie ma jej jeszcze w tekście
                    if (!textBlockCalc.Text.Contains(","))
                    {
                        textBlockCalc.Text = textBlockCalc.Text + ",";
                    }
                    isZeroCommaNecessery = false;
                }
                //Gdy znak "." jest poprzedzony bezpośrednio przez operator
                else
                {
                    textBlockCalc.Text = "0,";
                    isZeroCommaNecessery = true;
                }
            }

            //Dopisanie symbolu z przycisku do tekstu
            else if (textBlockCalc.Text.Length < 12)
                textBlockCalc.Text += b.Content.ToString();

            
            operationPerformed = false;
        }

        //WPISANIE ZNAKU OPERATORA
        private void ButtonFun_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            int lengthText = textBlockCalc.Text.Length;

            //Sprawdzenie, czy została wpisana liczba
            if (textBlockCalc.Text.Length > 0)
            {
                if (textBlockCalc.Text[lengthText - 1] == ',')
                {
                    textBlockCalc.Text = textBlockCalc.Text.Remove(lengthText - 1); //jeśli liczba zakończona "." - usunięcie jej z tekstu
                }

                isError();

                //Sprawdzenie możliwości dzielenia
                if (divideByZero())
                {
                    textBlockCalc.Text = errorDiv;
                    operationPerformed = true;
                    operation = "";
                }
                //Użycie operatora jednoargumentowego zmiany znaku liczby
                else if (b.Equals(buttonSign))
                {
                    double val = Double.Parse(textBlockCalc.Text);
                    val *= -1;
                    textBlockCalc.Text = val.ToString();
                }
                //Użycie operatora jednoargumentowego pierwiastka z danej liczby
                else if (b.Equals(buttonRoot))
                {
                    if (value >= 0)
                    {
                        double val = Double.Parse(textBlockCalc.Text);
                        val = Math.Sqrt(val);
                        textBlockCalc.Text = val.ToString();
                    }
                    else
                    {
                        textBlockCalc.Text = error;
                        textBlockEq.Text = "";
                        value = 0;
                    }
                }
                //sprawdzenie, czy wartość zmiennej value została zaktualizowana, jeśli tak wykonanie działania jak dla znaku "="
                else if (value != 0)
                {                  
                    PerformClickEquals();                     
                    operation = b.Content.ToString();
                    textBlockEq.Text = value + " " + operation;
                    operationPerformed = true;
                }
                //jeśli nie, pobranie wartości z pola textBlockCalc
                else
                {                
                    operation = b.Content.ToString();
                    value = Double.Parse(textBlockCalc.Text);
                    textBlockEq.Text = value + " " + operation;                   
                    operationPerformed = true;
                }
            }
        }

        //UŻYCIE ZNAKU "="
        private void ButtonEq_Click(object sender, RoutedEventArgs e)
        {          
            textBlockEq.Text = "";
            PerformClickEquals();
            isResult = true;
        }

        //FUNKCJE USUWAJĄCE TEKST
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            isError();

            //Zresetowanie kalkulatora
            if (b.Equals(buttonC))
            {
                textBlockCalc.Text = "";
                value = 0;
                operation = "";
                textBlockEq.Text = "";
            }
            //Usunięcie ostatniego znaku
            else if (b.Equals(buttonDel))
            {
                if (textBlockCalc.Text.Length > 0)
                    textBlockCalc.Text = textBlockCalc.Text.Substring(0, (textBlockCalc.Text.Length - 1));
            }
        }

        //FUNKCJA OBLICZAJĄCA
        private void PerformClickEquals()
        {
            if (divideByZero())
            {
                textBlockCalc.Text = errorDiv;
            }
            else if (textBlockCalc.Text == errorDiv || textBlockCalc.Text == error)
            {
                textBlockCalc.Text = "0";
            }
            else
            {
                double newValue = Double.Parse(textBlockCalc.Text);
                
                if (operation == "+" && operationPerformed == false)
                {
                    textBlockCalc.Text = (value + newValue).ToString();
                }
                else if (operation == "-" && operationPerformed == false)
                {
                    textBlockCalc.Text = (value - newValue).ToString();
                }
                else if (operation == "*" && operationPerformed == false)
                {
                    textBlockCalc.Text = (value * newValue).ToString();
                }
                else if (operation == "/" && operationPerformed == false)
                {
                    textBlockCalc.Text = (value / newValue).ToString();
                }
                
                value = Double.Parse(textBlockCalc.Text);
                operation = "";
            }
            operationPerformed = false;

        }

        //FUNKCJA SPRAWDZAJĄCA MOŻLIWOŚĆ DZIELENIA
        private bool divideByZero()
        {
            if (operation == "/" && textBlockCalc.Text == "0")
            {
                operation = "";
                textBlockEq.Text = "";
                return true;
            }
            else
            {
                return false;
            }
        }

        //FUNKCJA SPRAWDZAJĄCA CZY TEKST WYŚWIETLA BŁĄD
        private void isError()
        {
            if (textBlockCalc.Text == errorDiv || textBlockCalc.Text == error)
            {
                textBlockCalc.Text = "0";
                value = 0;
            }
        }

        //OBSŁUGA KLAWIATURY
        private void TextBlockCalc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.NumPad1 || e.Key == Key.D1) ButtonNum_Click(button1, null);
            if (e.Key == Key.NumPad2 || e.Key == Key.D2) ButtonNum_Click(button2, null);
            if (e.Key == Key.NumPad3 || e.Key == Key.D3) ButtonNum_Click(button3, null);
            if (e.Key == Key.NumPad4 || e.Key == Key.D4) ButtonNum_Click(button4, null);
            if (e.Key == Key.NumPad5 || e.Key == Key.D5) ButtonNum_Click(button5, null);
            if (e.Key == Key.NumPad6 || e.Key == Key.D6) ButtonNum_Click(button6, null);
            if (e.Key == Key.NumPad7 || e.Key == Key.D7) ButtonNum_Click(button7, null);
            if (e.Key == Key.NumPad8 || e.Key == Key.D8) ButtonNum_Click(button8, null);
            if (e.Key == Key.NumPad9 || e.Key == Key.D9) ButtonNum_Click(button9, null);
            if (e.Key == Key.NumPad0 || e.Key == Key.D0) ButtonNum_Click(button0, null);
            if (e.Key == Key.Add || e.Key == Key.OemPlus) ButtonFun_Click(buttonPlus, null);
            if (e.Key == Key.Subtract || e.Key == Key.OemMinus) ButtonFun_Click(buttonMinus, null);
            if (e.Key == Key.Multiply) ButtonFun_Click(buttonMulti, null);   
            if (e.Key == Key.Divide || e.Key == Key.OemQuestion) ButtonFun_Click(buttonDiv, null);
            if (e.Key == Key.R) ButtonFun_Click(buttonRoot, null);
            if (e.Key == Key.Enter) ButtonEq_Click(buttonEq, null);
            if (e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Decimal) ButtonNum_Click(buttonDot, null);
            if (e.Key == Key.Back) ButtonClear_Click(buttonDel, null);
            if (e.Key == Key.C) ButtonClear_Click(buttonC, null);
            if (e.Key == Key.Escape) Close();
        }
    }
}