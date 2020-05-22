using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniTC.NewControl
{
    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        public PanelTC()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty DoubleClickCommandProperty = 
            DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(PanelTC), 
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentPathProperty = 
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(PanelTC), 
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty CurrentDriveProperty = 
            DependencyProperty.Register("CurrentDrive", typeof(string), typeof(PanelTC), 
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty AvailableDrivesProperty = 
            DependencyProperty.Register("AvailableDrives", typeof(ObservableCollection<string>), typeof(PanelTC), 
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty DirectoryContentProperty = 
            DependencyProperty.Register("DirectoryContent", typeof(ObservableCollection<string>), typeof(PanelTC), 
                new FrameworkPropertyMetadata(null));

        private static readonly DependencyProperty SelectedDirectoryProperty = 
            DependencyProperty.Register("SelectedDirectory", typeof(string), typeof(PanelTC), 
                new FrameworkPropertyMetadata(null));

        public ICommand DoubleClickCommand
        {
            get { return (ICommand)GetValue(DoubleClickCommandProperty); }
            set { SetValue(DoubleClickCommandProperty, value); }
        }

        public string CurrentPath
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }

        public string CurrentDrive
        {
            get { return (string)GetValue(CurrentDriveProperty); }
            set { SetValue(CurrentDriveProperty, value); }
        }

        public ObservableCollection<string> AvailableDrives
        {
            get { return (ObservableCollection<string>)GetValue(AvailableDrivesProperty); }
            set { SetValue(AvailableDrivesProperty, value); }
        }

        public ObservableCollection<string> DirectoryContent
        {
            get { return (ObservableCollection<string>)GetValue(DirectoryContentProperty); }
            set { SetValue(DirectoryContentProperty, value); }
        }

        public string SelectedDirectory
        {
            get { return (string)GetValue(SelectedDirectoryProperty); }
            set { SetValue(SelectedDirectoryProperty, value); }
        }
    }
}
