   M S B A M L   `   `   `  ����I  EGitLab Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:GitLab_DesktopGitLab_Desktop  5   	       GitLab_Desktop.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:GitLab_Desktop     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  
MainWindow��5	   	   $��662.125��6   $��1094.278��6-    � 
ResizeMode$ CanMinimize=�6>    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $f `pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml?�6%   %    5      .��5      � .+�5   
   � -   5         �Name$ 	LoginGrid��5      $
��Left=�6"   $	��197��6=   $��240,149,0,0q�6J   $	��Top=�6_   $	��606��6w   ���	#FFE2D5D5.+�5      �� -   5         ��Name$  PersonalAccessTokenTextBox��5      $
��Left=�6:   $��35��6U   $��	10,62,0,0q�6a    �� TextWrapping$
 Wrap=�6t   $	��Top=�6�   $	��586��6�   �� 5      $��Personal Access Token:��6   $
��Left=�65   $��	10,42,0,0q�6P   $	��Top=�6c   �� 5      -   5         ��Name$ LoginButton��5      $��Login��6*   $
��Left=�6:   $��223,149,0,0q�6U   $	��Top=�6j   $	��124��6�   6�   �� 5      $��Login To GitLab��6   $
��Left=�6.   $��	184,0,0,0q�6I   $	��Top=�6\   $��30(�6t   5      '  !MaterialDesignThemes.Wpf.Snackbar  5   
   -   5        	  Name$	 UniversalSnackBar��5   "   $
��Left=�6=   $	��180��6X   $��
0,453,-1,0q�6e   $	��Top=�6y   $
��1089��6�   � 5   
   -   5        $ RepositoriesGrid��5      $
��Left=�6)   $	��613��6D   $��	10,10,0,0q�6Q   $	��Top=�6d   $
��1068��6|   .+�5      �� -   5        
 ��Name$
 LocalRepositoriesList��5      $
��Left=�65   $	��237��6P   $��	10,49,0,0q�6]   $	��Top=�6p   $	��422��6�   ���	#FFD8F5DC6�   �� 5      $!��Repositories on this device��6   $
��Left=�6:   $��	10,10,0,0q�6U   $	��Top=�6h   $��20(�6�   $��Elephant)�6�   �� 5      -   5        $
 CloudRepositoriesListBox��5      $
��Left=�68   $	��232��6S   $��
578,54,0,0q�6`   $	��Top=�6t   $	��363��6�   ���	#FFA5DAD36�   �� 5      $��Repositories on the cloud��6   $
��Left=�68   $��
578,10,0,0q�6S   $	��Top=�6g   $��20(�6   $��Elephant)�6�   5      5      5       