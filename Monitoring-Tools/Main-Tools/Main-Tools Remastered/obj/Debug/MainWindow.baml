   M S B A M L   `   `   `  ����P  LMain-Tools Remastered, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Main_Tools_RemasteredMain_Tools_Remastered  5   	   &     Main_Tools_Remastered.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Main_Tools_Remastered     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  
MainWindow��5	   	   $	��400��6   $��792.5��6)    � WindowStartupLocation$ CenterScreen=�67    � 
ResizeMode$ NoResize=�6\    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $f `pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml?�6%   %    5      .��5      � .+�5   
   � -   5         �Name$ MainPopupGrid��5      $
��Left=�6&   $	��371��6A   $	��Top=�6N   $	��786��6f   .+�5      �� -   5         ��Name$ 
ErrorLabel��5      $#��Internet may not be available��6(   $
��Left=�6P   $��	257,0,0,0q�6k   $	��Top=�6~   $��20(�6�   $
��Bold"�6�   �� 5      -   5         ��Name$ CloseButton��5      $��Close��6*   $
��Left=�6:   $��49��6U   $��638,309,0,0q�6a   $	��Top=�6v   $	��134��6�   $��30(�6�   �� 5      -   5        $ ReasonLabel��5      $��ReasonLabel��6)   $
��Left=�6?   $��	23,64,0,0q�6Z   $	��Top=�6m   $��15(�6�   �� 5      -   5        	 ��Name$	 NetGearCheckBox��5      $��Check for NETGEARED��60   $
��Left=�6N   $��
10,340,0,0q�6i   $	��Top=�6}   �� 5      -   5        $	 GamingModeCheckBox��5      $A��;Gaming Mode (Place new text document on desktop to disable)��63   $
��Left=�6y   $��
10,317,0,0q�6�   $	��Top=�6�   5      '  !MaterialDesignThemes.Wpf.Snackbar  5   
   -   5        
  Name$
 UniversalSnackbar��5   "   $
��Left=�6=   $��83��6X   $��	0,288,0,0q�6d   $	��Top=�6w   $	��786��6�   � 5   
   -   5        $ SpeedTestGrid��5      $
��Left=�6&   $	��371��6A   $	��Top=�6N   $	��786��6f   .+�5      �� $��SpeedTest Results:��6   $
��Left=�61   $��
184,-9,0,0q�6L   $	��Top=�6`   $��50(�6x   �� 5      -	   5        $ DownloadLabel��5      $��Label��6+   $
��Left=�6;   $��	53,98,0,0q�6V   $	��Top=�6i   $��20(�6�   �� 5      -
   5        $ UploadLabel��5      $��Label��6)   $
��Left=�69   $��
53,176,0,0q�6T   $	��Top=�6h   $��20(�6�   �� 5      -   5        $ 	PingLabel��5      $��Label��6'   $
��Left=�67   $��
53,248,0,0q�6R   $	��Top=�6f   $��20(�6~   �� 5       -   5        $ SpeedTestResultButton��5       $��View on SpeedTest��64   $
��Left=�6P   $��35��6k   $��479,326,0,0q�6w   $	��Top=�6�   $	��199��6�   6�   �� 5!      -   5        $ SpeedTestCloseButton��5!      $��Close��63   $
��Left=�6C   $��35��6^   $��683,326,0,0q�6j   $	��Top=�6   $��93��6�   6�   �� 5"      -   5        $ ISPLabel��5"      $��Label��6&   $
��Left=�66   $��
26,326,0,0q�6Q   $	��Top=�6e   5#      5%      5&      