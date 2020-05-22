using System;
using System.Windows.Input;
using System.IO;

namespace MiniTC.ViewModel
{
    using BaseClasses;
    class ViewModelMain : ViewModelBase
    {
        private ViewModelPanel _left;
        public ViewModelPanel Left
        {
            get { return _left; }
            set
            {
                _left = value;
                onPropertyChanged(nameof(Left));
            }
        }

        private ViewModelPanel _right;
        public ViewModelPanel Right
        {
            get { return _right; }
            set
            {
                _right = value;
                onPropertyChanged(nameof(Right));
            }
        }

        public ViewModelMain()
        {
            Left = new ViewModelPanel();
            Right = new ViewModelPanel();
        }


        private ICommand _copyfile = null;
        public ICommand CopyFile
        {
            get
            {
                if (_copyfile == null)
                    _copyfile = new RelayCommand(
                        arg => Copy(), 
                        arg => IfCopyPossible());
                return _copyfile;
            }
        }

        #region Metody dodatkowe
        private void Copy()
        {
            string filePath, directoryPath, fileName;
            if (Left.SelectedDirectory != null) //zaznaczony element w lewym panelu
            {
                filePath = Left.CurrentPath + @"\" + Left.SelectedDirectory.Trim();
                directoryPath = Right.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }
            else //zaznczony element w prawym panelu
            {
                filePath = Right.CurrentPath + @"\" + Right.SelectedDirectory.Trim();
                directoryPath = Left.CurrentPath;
                fileName = Path.GetFileName(filePath);
            }
            File.Copy(filePath, directoryPath + @"\" + fileName);
            Left.CurrentPath = Left.CurrentPath;
            Right.CurrentPath = Right.CurrentPath;
        }

        private bool IfCopyPossible()
        {
            //nie można skopiować w to samo miejsce
            if (Left.CurrentPath == Right.CurrentPath)
                return false;

            //nie można skopiować, gdy w folderze jest już plik o tej samej nazwie
            if ((Right.SelectedDirectory != null && Left.DirectoryContent != null && Left.DirectoryContent.Contains(Right.SelectedDirectory))
                || (Left.SelectedDirectory != null && Right.DirectoryContent != null && Right.DirectoryContent.Contains(Left.SelectedDirectory)))
                return false;

            //zaznaczony plik po lewej i wybrany folder po prawej
            if (Left.SelectedDirectory != null && !Left.SelectedDirectory.Contains("<D>")
                && Left.SelectedDirectory != ".." && Right.CurrentPath != null)
                return true;

            //zaznaczony plik po prawej i wybrany folder po lewej
            if (Right.SelectedDirectory != null && !Right.SelectedDirectory.Contains("<D>")
                && Right.SelectedDirectory != ".." && Left.CurrentPath != null)
                return true;

            return false;
        }
        #endregion
    }
}
