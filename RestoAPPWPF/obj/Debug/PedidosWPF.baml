   M S B A M L   `   `   `  ����F  BRestoAPPWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RestoAPPWPFRestoAPPWPF  5   	       RestoAPPWPF.PedidosWPF   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RestoAPPWPF     5   	     � Title$  
PedidosWPF��5   	   $��844.661��6   $��1201.529��6-    � 
ResizeMode$ NoResize=�6>    � WindowStyle$
 None=�6T    � ShowInTaskbar .  � WindowStartupLocation$ CenterScreen=�6|    � Icon$ Imagenes/rest.png��6�   .��5	      � -   5         �Name$ grContenedorPrincipal��5	      ��5
   
   �� 5       �� ImageSource$ Imagenes/fondo final.jpg��6   .+�5   
   �� -   5        
������ $
��Left=�5   '   $��49��6B   $��10,0,0,0q�6N   $	��Top=�6`   $
��1103��6x   6�   �� 5   
   -   5         ��Name$ 	btnVolver��5   "   $��Volver��6   $��
63,748,0,0q�66   ���	#FF03030F
�������$��Arial)�5      $��20(�5         RoundedButtonStyle#���   6   $
��Left=�6M   $	��158��6h   $��62��6t   $	��Top=�6�   
�������$
��Bold"�6�   5      �� 5   
   -   5        $ btnActualizar��5      $��1020,748,0,0q�6*   $��Arial)�5      $��20(�5      #���   6   $
��Left=�6M   $��60��6h   $��62��6s   $	��Top=�6   $
��Bold"�6�   ��5      Z� ��5      Z� ��5   �   Z� .��5      �� $%��Imagenes/Ico/actualizar ico.png��6   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 dtGridListarPedidos��5   9   $y�Hidden=�6   {�.  $
��Left=�6V   $	��206��6q   $��	63,10,0,0q�6~   $	��Top=�6�   $	��368��6�   
 J� RenderTransformOrigin$
 0.5,0.5&�6�   z�5       V� 5!      .~�5"      �� �� 5#      �� 5$       �� Angle$ 0.114Q�6&   T� 5%      5&      5(      �� 5)   
   -   5         ��Name$ scroll��5)      $��1��6'   $��63,81,72,114q�64   .��5*      � -   5        $ gridContenedor��5*   (   $y�Visible=�6   $
��1752��6@   $
��1050��6N   ��5+      �� 5,       �� Color$ White��6&    �� Opacity$
 0.92Q�64   5/      50      �� 51   
   -   5        $ 	btnCerrar��51      $
��Left=�6$   $��
1173,0,0,0q�6?   $	��Top=�6S   $��29��6k   $��26��6v   ��52      �� 53      $ White��6"   $
 0.84Q�60   ��55      �� 56      $ White��6"   $
 0.84Q�60   .��58      �� $��Imagenes/Ico/cerrar.png��6   $��20��65   $��23��6A   6N   59      �� 5:   
   -	   5        $ btnMinimizar��5:      $
��Left=�6'   $��
1142,0,0,0q�6B   $	��Top=�6V   $��31��6n   $��26��6y   ��5;      �� 5<      $ White��6"   $
 0.84Q�60   ��5>      �� 5?      $ White��6"   $
 0.84Q�60   .��5A      �� $ ��Imagenes/Ico/minimizar.png��6   6:   5B      5D      5F      