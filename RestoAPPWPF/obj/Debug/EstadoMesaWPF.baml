   M S B A M L   `   `   `  ����F  BRestoAPPWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RestoAPPWPFRestoAPPWPF  5   	       RestoAPPWPF.EstadoMesaWPF   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RestoAPPWPF     5   	     � Title$  EstadoMesaWPF��5   	   $��844.661��6   $��1201.529��60    � 
ResizeMode$ NoResize=�6A    � ShowInTaskbar .  � WindowStartupLocation$ CenterScreen=�6m    � Icon$ Imagenes/rest.png��6�   .��5	      � -   5         �Name$ grContenedorPrincipal��5	      ��5
   
   �� 5       �� ImageSource$ Imagenes/fondo final.jpg��6   .+�5   
   �� -   5         ��Name$ 	btnVolver��5   "   $��Volver��6   $��
63,731,0,0q�66   ���	#FF03030F
�������$��Arial)�5      $��20(�5         RoundedButtonStyle#���   6   $
��Left=�6M   $	��158��6h   $��62��6t   $	��Top=�6�   
�������$
��Bold"�6�   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dtgridMesasEstado��5   9   $y�Hidden=�6   {�.  $
��Left=�6T   $	��206��6o   $��	63,10,0,0q�6|   $	��Top=�6�   $	��368��6�   	 J� RenderTransformOrigin$	 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5      
 �� Angle$
 0.114Q�6&   T� 5      5      5      �� 5   
   -   5         ��Name$ scroll��5      $��1��6'   $��63,81,72,114q�64   .��5      � -   5        $ gridContenedor��5   (   $y�Visible=�6   $
��1777��6@   $
��1085��6N   ��5       �� 5!       �� Color$ White��6&    �� Opacity$
 0.92Q�64   5#      5$      �� 5%   
   -   5        $ btnActualizar��5%      $��980,713,0,0q�6*   $��Arial)�5(      $��20(�5)      #���   6   $
��Left=�6M   $��60��6h   $��62��6s   $	��Top=�6   $
��Bold"�6�   ��5&      Z� ��5'      Z� ��5)   �   Z� .��5*      �� $%��Imagenes/Ico/actualizar ico.png��6   5,      5.      �� 5/   
   $��
Actualizar��6   $
��Left=�6%   $��992,767,0,0q�6@   $	��Top=�6U   
�������{�.  $
��Bold"�6�   50      51      