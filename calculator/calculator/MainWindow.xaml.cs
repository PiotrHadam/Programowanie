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
                textBoxCalc.Clear();
                isResult = false;
            }

            isError();

            //Czyszcznie tekstu przed wpisaniem nowej liczby, gdy wpisane jest tylko 0 lub gdy został wpisany operator
            if ((textBoxCalc.Text == "0") || (operationPerformed))
            {
                textBoxCalc.Clear();
            }

            //Dopisanie symbolu z przycisku do tekstu
            if (textBoxCalc.Text.Length < 12)
                textBoxCalc.Text += b.Content.ToString();

            //Wpisanie znaku "."
            if (b.Equals(buttonDot))
            {
                //Gdy znaku "." nie poprzedza operator
                if (operationPerformed == false)
                {
                    //Dopisanie "." tylko gdy nie ma jej jeszcze w tekście
                    if (!textBoxCalc.Text.Contains("."))
                    {
                        textBoxCalc.Text = textBoxCalc.Text + ".";
                    }
                    isZeroCommaNecessery = false;
                }
                //Gdy znak "." jest poprzedzony bezpośrednio przez operator
                else
                {
                    textBoxCalc.Text = "0.";
                    isZeroCommaNecessery = true;
                }
            }
            operationPerformed = false;
        }

        //WPISANIE ZNAKU OPERATORA
        private void ButtonFun_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            int lengthText = textBoxCalc.Text.Length;

            //Sprawdzenie, czy została wpisana liczba
            if (textBoxCalc.Text.Length > 0)
            {
                if (textBoxCalc.Text[lengthText - 1] == '.')
                {
                    textBoxCalc.Text = textBoxCalc.Text.Remove(lengthText - 1); //jeśli liczba zakończona "." - usunięcie jej z tekstu
                }

                isError();

                //Sprawdzenie możliwości dzielenia
                if (divideByZero())
                {
                    textBoxCalc.Text = errorDiv;
                    operationPerformed = true;
                    operation = "";
                }
                //Użycie operatora jednoargumentowego zmiany znaku liczby
                else if (b.Equals(buttonSign))
                {
                    value = Double.Parse(textBoxCalc.Text);
                    value *= -1;
                    textBoxCalc.Text = value.ToString();
                    textBlockEq.Text += value;
                }
                //Użycie operatora jednoargumentowego pierwiastka z danej liczby
                else if (b.Equals(buttonRoot))
                {
                    if (value >= 0)
                    {
                        value = Double.Parse(textBoxCalc.Text);
                        double val = value;
                        value = Math.Sqrt(val);
                        textBoxCalc.Text = value.ToString();
                        textBlockEq.Text += buttonRoot.Content.ToString() + val;
                    }
                    else
                    {
                        textBoxCalc.Text = error;
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
                //jeśli nie, pobranie wartości z pola textBoxCalc
                else
                {                
                    operation = b.Content.ToString();
                    value = Double.Parse(textBoxCalc.Text);
                    textBlockEq.Text = value + " " + operation;                   
                    operationPerformed = true;
                }
            }
        }

        //UŻYCIE ZNAKU "="
        private void ButtonEq_Click(object sender, RoutedEventArgs e)
        {
            isResult = true;

            textBlockEq.Text = "";
            PerformClickEquals();
        }

        //FUNKCJE USUWAJĄCE TEKST
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            //Zresetowanie kalkulatora
            if (b.Equals(buttonC))
            {
                textBoxCalc.Clear();
                value = 0;
                operation = "";
                textBlockEq.Text = "";
            }
            //Usunięcie ostatniego znaku
            else if (b.Equals(buttonDel))
            {
                if (textBoxCalc.Text.Length > 0)
                    textBoxCalc.Text = textBoxCalc.Text.Substring(0, (textBoxCalc.Text.Length - 1));
            }
        }

        //FUNKCJA OBLICZAJĄCA
        private void PerformClickEquals()
        {
            if (divideByZero())
            {
                textBoxCalc.Text = errorDiv;
            }
            else if (textBoxCalc.Text == errorDiv)
            {
                textBoxCalc.Text = "0";
            }
            else
            {
                double newValue = Double.Parse(textBoxCalc.Text);
                
                if (operation == "+" && operationPerformed == false)
                {
                    textBoxCalc.Text = (value + newValue).ToString();
                }
                else if (operation == "-" && operationPerformed == false)
                {
                    textBoxCalc.Text = (value - newValue).ToString();
                }
                else if (operation == "*" && operationPerformed == false)
                {
                    textBoxCalc.Text = (value * newValue).ToString();
                }
                else if (operation == "/" && operationPerformed == false)
                {
                    textBoxCalc.Text = (value / newValue).ToString();
                }

                lengthCheck();
                value = Double.Parse(textBoxCalc.Text);
                operation = "";
            }
            operationPerformed = false;

        }

        //FUNKCJA SPRAWDZAJĄCA MOŻLIWOŚĆ DZIELENIA
        private bool divideByZero()
        {
            if (operation == "/" && textBoxCalc.Text == "0")
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
            if (textBoxCalc.Text == errorDiv || textBoxCalc.Text == error)
            {
                textBoxCalc.Text = "0";
            }
        }

        //FUNKCJA SPRAWDZAJĄCA DŁUGOŚĆ WYŚWIETLANEGO TEKSTU
        private void lengthCheck()
        {
            if (textBoxCalc.Text.Length > 18)
            {
                textBoxCalc.FontSize = 22;
            }
        }

        //OBSŁUGA KLAWIATURY
        private void TextBoxCalc_KeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 88 || key == 2 || key == 85 || key == 87 || key == 84 || key == 89 || key == 6); //możliwe do użycia tylko klawisze z liczbami, operatorami dodawania, mnożenia, odejmowania i dzielenia oraz Enter i Backspace

            if (key == 6) //działanie dla klawisza Enter - równoznaczne z przyciskiem "="
            {
                isResult = true;
                textBlockEq.Text = "";
                PerformClickEquals();
            }
            else if (key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 88) //obsługa wpisywania liczb i "."
            {
                if (isResult && isZeroCommaNecessery == false)
                {
                    textBoxCalc.Clear();
                    isResult = false;
                }

                isError();

                if ((textBoxCalc.Text == "0") || (operationPerformed))
                {
                    textBoxCalc.Clear();
                }

                if (key == 88) //kropka
                {

                    if (operationPerformed == false)
                    {
                        if (!textBoxCalc.Text.Contains("."))
                        {
                            textBoxCalc.Text = textBoxCalc.Text + ".";
                            textBoxCalc.Select(textBoxCalc.Text.Length, 0);

                        }
                        isZeroCommaNecessery = false;
                    }
                    else
                    {
                        textBoxCalc.Text = "0.";
                        isZeroCommaNecessery = true;
                    }
                    e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 2 || key == 85 || key == 87 || key == 84 || key == 89 || key == 6); //zablokowanie możliwości wpisania kolejnej "." do jednej liczby

                }
                operationPerformed = false;
            }
            else if (key == 84 || key == 85 || key == 87 || key == 89) //obsługa operatorów
            {
                int lengthText = textBoxCalc.Text.Length;

                if (textBoxCalc.Text.Length > 0)
                {

                    if (textBoxCalc.Text[lengthText - 1] == '.')
                    {
                        textBoxCalc.Text = textBoxCalc.Text.Remove(lengthText - 1);
                    }

                    isError();
                    if (divideByZero())
                    {
                        textBoxCalc.Text = errorDiv;
                        operationPerformed = true;
                        operation = "";
                    }
                    else if (value != 0)
                    {   
                        PerformClickEquals();
                        switch (key)
                        {
                            case 84: operation = "*"; break;
                            case 85: operation = "+"; break;
                            case 87: operation = "-"; break;
                            case 89: operation = "/"; break;
                        }
                        textBlockEq.Text = value + " " + operation;                  
                        operationPerformed = true;
                    }
                    else
                    {
                        value = Double.Parse(textBoxCalc.Text);
                        switch (key)
                        {
                            case 84: operation = "*"; break;
                            case 85: operation = "+"; break;
                            case 87: operation = "-"; break;
                            case 89: operation = "/"; break;
                        }
                        textBlockEq.Text = value + " " + operation;
                        operationPerformed = true;
                    }
                }
                e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 88 || key == 2); //zablokowanie możliwości wpisania drugiego znaku operatora z rzędu
            }
        }
    }
}