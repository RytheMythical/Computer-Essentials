   M S B A M L   `   `   `  ����L  HRecordings Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Recordings_ViewerRecordings_Viewer  5   	   "    Recordings_Viewer.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Recordings_Viewer     5   	   7eo(http://schemas.essentialobjects.com/wpf/     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6=     � Title$  
MainWindow��5	   	   $
��1200��6   $
��1200��6*    � WindowState$ 	Maximized=�67    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $f `pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml?�6%   %    5      .��5      � .+�5   
   r� -   5         r�Name$ VideoPlayer��5      $
��Left=�6,   $
��1073��6G   $��	10,10,0,0q�6U   $	��Top=�6h   $
��1172��6�   � 5   
   -   5         �Name$ VideoControlsGrid��5      $
��Left=�6*   $��58��6E   $��10,1101,0,0q�6Q   $	��Top=�6f   $
��1172��6~   ���	#FFDCD6D6.+�5      �� -   5         ��Name$ 
PlayButton��5      $
��Play��6)   $
��Left=�68   $��48��6S   $��	10,10,0,0q�6_   $	��Top=�6r   $	��148��6�   6�   �� 5      -   5        $ PauseButton��5      $��Pause��6*   $
��Left=�6:   $��48��6U   $��
183,10,0,0q�6a   $	��Top=�6u   $	��124��6�   6�   �� 5      -   5         ��Name$ MinutesTextBox��5      $
��Left=�6.   $��23��6I   $��
405,29,0,0q�6U   	 �� TextWrapping$
	 Wrap=�6i   $�� ��6}   $	��Top=�6�   $��40��6�   �� 5      -   5        $ HoursTextBox��5      $
��Left=�6,   $��23��6G   $��
352,29,0,0q�6S   $
	 Wrap=�6g   $�� ��6{   $	��Top=�6�   $��36��6�   �� 5      -   5        $ SecondsTextBox��5      $
��Left=�6.   $��23��6I   $��
463,29,0,0q�6U   $
	 Wrap=�6i   $�� ��6}   $	��Top=�6�   $��47��6�   �� 5      $��:��6   $
��Left=�6    $��
389,28,0,0q�6;   $	��Top=�6O   
 J� RenderTransformOrigin$
 
0.273,-0.2&�6g   �� 5      $��:��6   $
��Left=�6    $��
447,28,0,0q�6;   $	��Top=�6O   $
 
0.273,-0.2&�6g   �� 5      -   5         ��Name$ DurationLabel��5      $��Label��6+   $
��Left=�6;   $��
527,31,0,0q�6V   $	��Top=�6j   �� 5      -	   5        $ 
ExitButton��5      $
��Exit��6)   $
��Left=�68   $��45��6S   $��1039,10,0,0q�6_   $	��Top=�6t   $	��123��6�   ���	#FFB73A3A6�   5      � 5   
   -
   5        $ SelectionGrid��5      $
��Left=�6&   $
��1154��6A   $��10,5,0,0q�6O   $	��Top=�6a   $
��1172��6y   .+�5      �� -   5         ��Name$ DateSelectionBox��5      $
��Left=�60   $	��375��6K   $��	10,55,0,0q�6X   $	��Top=�6k   $
��1152��6�   ���	#FFDECFC56�   �� 5       $��Select Date:��6   $
��Left=�6+   $��10,0,0,0q�6F   $	��Top=�6X   $��30(�6p   �� 5!      $��Select Period:��6   $
��Left=�6-   $��
10,435,0,0q�6H   $	��Top=�6\   $��30(�6t   �� 5"      -   5         ��Name$ PeriodComboBox��5"      $
��Left=�6/   $��43��6J   $��
10,488,0,0q�6V   $	��Top=�6j   $	��287��6�   �� 5#      -   5        $
��Play��5#      $
��Left=�6$   $��44��6?   $��
17,561,0,0q�6K   $	��Top=�6_   $	��280��6w   6�   �� 5$      -   5        $
��Exit��5$      $
��Left=�6$   $��49��6?   $��1036,575,0,0q�6K   $	��Top=�6a   $	��126��6y   ���	#FFB73A3A6�   �� 5%      -   5        $ SaveVideoButton��5%      $��
Save Video��6.   $
��Left=�6C   $��38��6^   $��
17,615,0,0q�6j   $	��Top=�6~   $	��280��6�   6�   5&      '  !MaterialDesignThemes.Wpf.Snackbar  5'   
   -   5          Name$ UniversalSnackbar��5'   "   $
��Left=�6=   $��79��6X   $��
1,1075,0,0q�6d   $	��Top=�6x   $
��1171��6�      MessageQueue 6�   �� *UniversalSnackBar+5(      5)      