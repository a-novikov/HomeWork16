   M S B A M L   `   `   `  ����E  AHomeWork16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:HomeWork16
HomeWork16  5   	       HomeWork16.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:HomeWork16     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��70* �6   �� 5      $	��30* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ gridView��5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5         SelectionMode$ Single=�5         AutoGenerateColumns .     
IsReadOnly .  ��5      ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        id��,  &System.Windows.Controls.DataGridColumn   Header$ Id��5!   %   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 5    %   �� *id+5"   %     5$      -   5        surname��$ Фамилия��5&   %   
 5%   %   �� *	surname+5&   %     5(      -   5        name��$ Имя��5*   %   
 5)   %   �� *name+5*   %     5,      -   5        
patronymic��$ Отчество��5.   %   
 5-   %   �� *
patronymic+5.   %     51      -   5        	phone��$ Телефон��53   %   
 52   %   �� *phone+53   %     56      -   5        	email��$ E-mail��58   %   
 57   %   �� *email+58   %    � ContextMenu 5<      �� 5=      5@      5B      5D      5E      