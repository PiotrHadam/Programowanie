   M S B A M L   `   `   `  ����A  =MiniTC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MiniTC.ViewModelMiniTC.ViewModel  5   	   5clr-namespace:MiniTC.PropertiesMiniTC.Properties  5   	   5clr-namespace:MiniTC.NewControlMiniTC.NewControl  5   	       MiniTC.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -vmclr-namespace:MiniTC.ViewModel     5   	   0resxclr-namespace:MiniTC.Properties     5   	   0userclr-namespace:MiniTC.NewControl     5   	     � Title$  MiniTC��5
   	   $	��600��6   $	��600��6%    � 
ResizeMode$ CanMinimize=�61   
������� � DataContext 5      $  MiniTC.ViewModel.ViewModelMain  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��9* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
      MiniTC.NewControl.PanelTC  -   5          Name$ 	leftPanel��5   4   $��0��6   $��0��6$      AvailableDrives 5      �� *Left.AvailableDrives+   CurrentDrive 5      �� *Left.CurrentDrive+ �� Mode$ TwoWay=�	   CurrentPath	 5      �� *Left.CurrentPath+$ TwoWay=�
   DirectoryContent
 5      �� *Left.DirectoryContent+   SelectedDirectory 5      �� *Left.SelectedDirectory+$ TwoWay=�   DoubleClickCommand 5      �� *Left.ChangeDirectory+  5    
   -   5        $ 
rightPanel��5    4   $��0��6   $��1��6$    5!      �� *Right.AvailableDrives+ 5"      �� *Right.CurrentDrive+$ TwoWay=�	 5#      �� *Right.CurrentPath+$ TwoWay=�
 5$      �� *Right.DirectoryContent+ 5%      �� *Right.SelectedDirectory+$ TwoWay=� 5&      �� *Right.ChangeDirectory+�� 5(   
   $��1��6   $��2��6   $��10q�62   $��80��6>   ��5)      �� *
CopyFile+��6.   �� !   MiniTC.Properties.Resources   CopyButtonContent �� Source# Z 5*      5+      