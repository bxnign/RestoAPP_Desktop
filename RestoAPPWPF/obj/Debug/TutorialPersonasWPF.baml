   M S B A M L   `   `   `  ����F  BRestoAPPWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RestoAPPWPFRestoAPPWPF  5   	   %    RestoAPPWPF.TutorialPersonasWPF   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RestoAPPWPF     5   	     � Title$  TutorialPersonasWPF��5   	   $��844.661��6%   $��1201.529��66    � 
ResizeMode$ NoResize=�6G    � WindowStyle$
 None=�6]    � ShowInTaskbar .  � WindowStartupLocation$ CenterScreen=�6�    � Icon$ Imagenes/rest.png��6�   ��5	      �� 5
   
    �� ImageSource$ Imagenes/fondo final.jpg��6   .��5      � -   5         �Name$ grPrincipal��5      .+�5   
   �� -   5        
������ $
��Left=�5   '   $��61��6B   $��10,0,0,0q�6N   $	��Top=�6`   $
��1129��6x   6�   � 5   
   -   5        $ gridContenedorDatos��5      $��81,87,84,101q�6.   ��5      �� 5       �� Color$ White��6"   	 �� Opacity$
	 0.93Q�60   .+�5      �� -   5        
 ��Name$
 slider2��5      $
��Left=�6&   $��779,582,0,0q�6A   $	��Top=�6V   $	��178��6n    � LargeChange$	 0.1Q�6�    � Maximum$ 1Q�6�   5      �� 5   
   ���	#FF192444$
��Left=�6%   $��76��6@   $��	81,87,0,0q�6L   
���   �$	��Top=�6n   $
��1037��6�   �� 5   
   $��Tutorial��6   $
��Left=�6#   $��	98,96,0,0q�6>   $	��Top=�6Q   $��52��6i   $	��223��6u   
�������$
��Bold"�6�   $��Italic$�6�   $��36(�6�   �� 5   
   $
��Left=�6   $��52��6+   $��1027,99,0,0q�67   $	��Top=�6L   $��61��6d   $-��'Imagenes/Ico/ajustes_personales ico.png��6o   �� 5   
   -   5         ��Name$ 	btnCerrar��5      $
��Left=�6%   $��1171,0,-0.471,0q�6@   $	��Top=�6Y   $��31��6q    J� RenderTransformOrigin$ 0.662,0.275&�6|   $��28��6�   
�������$
��Bold"�6�   $��Microsoft Sans Serif)�6�   $��18(�6  ��5      �� 5      $ 	#FFCBC7C7��6"   $	 0.635Q�64   ��5       �� 5!      $ 	#FFCBC7C7��6"   $	 0.635Q�64   .��5#      �� $��Imagenes/Ico/cerrar.png��6   67   5$      �� 5%   
   -   5        $ btnminimizar��5%      $
��Left=�6(   $��
1139,0,0,0q�6C   $	��Top=�6W   $��32��6o   $ 0.662,0.275&�6z   $��28��6�   
�������$��Goudy Stout)�6�   $
��Bold"�6�   $��16(�6  ��5&      �� 5'      $ 	#FFCBC7C7��6"   $	 0.635Q�64   ��5)      �� 5*      $ 	#FFCBC7C7��6"   $	 0.635Q�64   .��5,      �� $ ��Imagenes/Ico/minimizar.png��6   6:   5-      r� 5.   
   -   5         r�Name$ video��5.       r� LoadedBehavior$ Manual=�6&   $��136,197,146.6,194.8q�6>   �� 5/   
   -   5        $ btnPlay��5/      $
��Play��6"   $��25��6G   $��401,669,703.6,150.8q�6S   �� 50   
   -	   5        $ btnPause��50      $��Pause��6#   $��25��6J   $��549,669,555.6,150.8q�6V   �� 51   
   -
   5        $ btnStop��51      $
��Stop��6"   $��25��6G   $��703,669,401.6,150.8q�6S   53      54      