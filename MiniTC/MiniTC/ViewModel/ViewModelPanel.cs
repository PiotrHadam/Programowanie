using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MiniTC.ViewModel
{
    using BaseClasses;

    class ViewModelPanel : ViewModelBase
    {
        private ObservableCollection<string> _availabledrives;
        private ObservableCollection<string> _directorycontent;
        private string _currentdrive;
        private string _currentpath;
        private string _selecteddirectory;

        public ViewModelPanel()
        {
            AvailableDrives = new ObservableCollection<string>(Directory.GetLogicalDrives());            
        }

        public string CurrentPath
        {
            get { return _currentpath; }
            set
            {
                try
                {
                    _currentpath = value;
                    onPropertyChanged(nameof(CurrentPath));
                    UpdateListBox();
                }
                catch { }
            }
        }

        public string CurrentDrive
        {
            get { return _currentdrive; }
            set
            {
                _currentdrive = value;
                onPropertyChanged(nameof(CurrentDrive));
                CurrentPath = _currentdrive;
            }
        }

        public ObservableCollection<string> AvailableDrives
        {
            get { return _availabledrives; }
            set
            {
                _availabledrives = value;
                onPropertyChanged(nameof(AvailableDrives));
            }
        }

        public ObservableCollection<string> DirectoryContent
        {
            get { return _directorycontent; }
            set
            {
                _directorycontent = value;
                onPropertyChanged(nameof(DirectoryContent));
            }
        }

        public string SelectedDirectory
        {
            get { return _selecteddirectory; }
            set
            {
                _selecteddirectory = value;
                onPropertyChanged(nameof(SelectedDirectory));
            }
        }

        private ICommand _changedirectory = null;
        public ICommand ChangeDirectory
        {
            get
            {
                if (_changedirectory == null)
                {
                    _changedirectory = new RelayCommand(
                        arg =>
                        {
                            if (_selecteddirectory == "..")
                                CurrentPath = Directory.GetParent(CurrentPath).FullName;
                            else
                            {
                                if (CurrentPath.EndsWith("\\"))
                                    CurrentPath += _selecteddirectory.Replace("<D> ", "");
                                else
                                    CurrentPath += "\\" + _selecteddirectory.Replace("<D> ", "");
                            }
                        },
                        arg => IfEntryPossible());
                }
                return _changedirectory;
            }
        }

        #region Metody dodatkowe
        private bool IfEntryPossible() //czy możliwe jest przejście do wybranego położenia
        {
            //wstecz
            if (_selecteddirectory == "..")
                return true;

            //wejście do folderu
            if (_selecteddirectory != null && _selecteddirectory.Contains("<D>"))
                return true;

            return false;
        }

        private void UpdateListBox() //wyświetlenie aktualnej listy plików i folderów
        {
            List<string> Content = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(CurrentPath);
                string[] directories = Directory.GetDirectories(CurrentPath);
                DirectoryInfo parentFile = Directory.GetParent(CurrentPath);

                if (parentFile != null)
                    Content.Add("..");
                foreach (var dir in directories)
                    if (!(new DirectoryInfo(dir).Attributes.HasFlag(FileAttributes.Hidden)))
                        Content.Add("<D> " + Path.GetFileName(dir));
                foreach (var fil in files)
                    if (!(new FileInfo(fil).Attributes.HasFlag(FileAttributes.Hidden)))
                        Content.Add("      " + Path.GetFileName(fil));
            }
            catch { }
            DirectoryContent = new ObservableCollection<string>(Content);
        }
        #endregion
    }
}