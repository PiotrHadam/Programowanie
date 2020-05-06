using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlayersMVVM
{
    /// <summary>
    /// Logika interakcji dla klasy TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {
        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent("Text_Changed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextBoxWithErrorProvider));

        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        void RaiseTextChanged()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(TextChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithErrorProvider), new FrameworkPropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void textHasChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is TextBox)) return;

            TextBox txtbx = (TextBox)sender;
            if (txtbx.Text == "Podaj imię" || txtbx.Text == "Podaj nazwisko")
                txtbx.Foreground = Brushes.Gray;
            else if (txtbx.Text == "")
            {
                txtbx.Foreground = Brushes.Black;
                txtbx.BorderBrush = Brushes.Red;
                txtbx.BorderThickness = new Thickness(2);
                txtbx.ToolTip = "Uzupełnij pole!";
            }
            else
            {
                txtbx.Foreground = Brushes.Black;
                txtbx.BorderBrush = Brushes.Black;
                txtbx.BorderThickness = new Thickness(1);
                txtbx.ToolTip = null;
            }
            RaiseTextChanged();
        }

        private void isFocused(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox)) return;

            TextBox txtbx = (TextBox)sender;
            if (txtbx.Text == "Podaj imię" || txtbx.Text == "Podaj nazwisko")
                txtbx.Text = "";
        }
    }
}
