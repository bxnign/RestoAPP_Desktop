   M S B A M L   `   `   `  ����F  BRestoAPPWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RestoAPPWPFRestoAPPWPF  5   	       RestoAPPWPF.PorcionesWPF   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RestoAPPWPF     5   	     � Title$  PorcionesWPF��5   	   $��844.661��6   $��1201.529��6/    � WindowStyle$
 None=�6@    � 
ResizeMode$ NoResize=�6S    � ShowInTaskbar .  � WindowStartupLocation$ CenterScreen=�6    � Icon$ Imagenes/rest.png��6�   .��5	      � ��5
   
   �� 5       �� ImageSource$ Imagenes/fondo final.jpg��6    �� Opacity$ 1Q�6@   .+�5   
   � $��	43,75,0,0q�6   $
��Left=�6"   $	��320��6=   $	��697��6I   $	��Top=�6V   ��5      �� 5       �� Color$ White��6"   $ 0.925Q�60   .+�5      �� ���	#FF03030F$
��Left=�6)   $��74��6D   
���   �$	��Top=�6_   $	��320��6w   5      �� 5      $
��Left=�6   $��74��6/   $��	204,0,0,0q�6;   $	��Top=�6N   $��75��6f   $&�� Imagenes/Ico/personas banner.png��6q   	 J� RenderTransformOrigin$	 0.764,0.527&�6�   �� 5      $
��Menu��6   $
��Left=�6#   $��10,0,0,0q�6>   $	��Top=�6P   $	��310��6h   
�������$��48(�6�   $��74��6�   $��Italic$�6�   $
��Bold"�6�   �� 5      -   5        
 ��Name$
 btnirAgregarStock��5      $��	84,94,0,0q�61   ���	#FF03030F
�������$��Arial)�5      $��20(�5         RoundedButtonStyle#���   6   $
��Left=�6M   $	��128��6h   $	��116��6t   $	��Top=�6�   .��5      �� $��68��6   $%��Imagenes/Ico/agregar banner.png��6$   $��Center=�6M   $	��Top=�6j   $	 	0.528,0.5&�6�   $��74��6�   5      �� 5      -   5        $
 btnirModificarStock��5   ;   $y�Hidden=�6   {�.  $��
78,317,0,0q�6X   ���	#FF03030F
�������$��Arial)�5      $��20(�5       #���   6   $	��116��6N   $	��Top=�6[   $
��Left=�6s   $	��128��6�   .��5!      �� $��68��6   $&�� Imagenes/Ico/modificar boton.png��6$   $��Center=�6N   $	��Top=�6k   $	 	0.528,0.5&�6�   $��74��6�   5"      �� 5$      -   5        $
 btnirListarStock��5$      $��
84,248,0,0q�6/   ���	#FF03030F
�������$��Arial)�5'      $��20(�5(      #���   6   $
��Left=�6M   $	��128��6h   $	��116��6t   $	��Top=�6�   .��5)      �� $��68��6   $%��Imagenes/Ico/listar  banner.png��6$   $��Center=�6M   $	��Top=�6j   $	 	0.528,0.5&�6�   $��74��6�   5*      �� 5+      {�.  $��Agregar��6&   $
��Left=�68   $��135,180,0,0q�6S   $	��Top=�6h   $��66��6�   
�������6�   �� 5,      $y�Hidden=�6   {�.  $��	Modificar��6:   $
��Left=�6N   $��130,407,0,0q�6i   $	��Top=�6~   $��66��6�   
�������6�   �� 5-      {�.  $��Listar��6&   $
��Left=�67   $��136,338,0,0q�6S   $	��Top=�6h   $��76��6�   
�������6�   5/      � 50   
   -   5         �Name$ grInfo��50      $��390,75,21,44q�6   $y�Visible=�65   {�. ��51      �� 52      $ White��6"   $
 0.93Q�60   54      � 55   
   -   5        $ 	grAgregar��55      $y�Hidden=�6"   {�. $��390,145,21,44q�6G   ��56      �� 57      $ White��6"   $ 0.925Q�60   .+�59      �� ���	#FF03030F$
��Left=�6*   $��78��6E   
���   �$	��Top=�6`   $	��785��6x   $��	0,-74,0,0q�6�   5:      �� 5<      $��	Porciones��6   $
��Left=�6(   $��	3,-74,0,0q�6C   $	��Top=�6V   $	��344��6n   
�������$��48(�6�   $��74��6�   $��Italic$�6�   $
��Bold"�6�   �� 5=      $
��Left=�6   $��74��6/   $��675,-74,0,0q�6;   $	��Top=�6P   $��64��6h   $%��Imagenes/Ico/agregar banner.png��6s   $	 
1.391,0.77&�6�   �� 5>      $��Nombre porcion:��6   $
��Left=�6.   $��	50,84,0,0q�6I   $	��Top=�6\   $	 0.198,0.885&�6t   �� 5?      $��Cantidad porcion:��6   $
��Left=�60   $��
50,244,0,0q�6K   $	��Top=�6_   �� 5@      -   5         ��Name$ cboNombreProd��5@      $
��Left=�6.   $��26��6I   $��189,164,0,0q�6U   $	��Top=�6j   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5A      -   5         ��Name$ txtCantidad��5A   $    �� 	MaxLength$ 5��6   $
��Left=�69   $��23��6T   $��189,247,0,0q�6`    �� TextWrapping$
 Wrap=�6u   $	��Top=�6�   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5B      -   5        $
 
btnAgregar��5B      $
��Left=�6*   $��556,475,0,0q�6E   $	��Top=�6Z   $	��162��6r   $��77��6~   ���	#FF33DC70
�������$��Arial)�5E      $��20(�5F      #���   6   .��5H      �� $��68��6   $$��Imagenes/Ico/guardar boton.png��6$   $��Center=�6L   $	��Top=�6i   $	 	0.528,0.5&�6�   $��74��6�   5I      �� 5J      -	   5        $ txtNombrePorcion��5J   %   $ 30��6   $
��Left=�6?   $��23��6Z   $��
189,87,0,0q�6f   $
 Wrap=�6z   $	��Top=�6�   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5K      $��Nombre producto:��6   $
��Left=�6/   $��
50,164,0,0q�6J   $	��Top=�6^   �� 5L      $��Precio porcion:��6   $
��Left=�6.   $��
50,330,0,0q�6I   $	��Top=�6]   �� 5M      -
   5        $ 	txtPrecio��5M   $   $ 5��6   $
��Left=�67   $��23��6R   $��189,332,0,0q�6^   $
 Wrap=�6s   $	��Top=�6�   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5N      -   5         ��Name$ lbldistribucion��5N   )   $y�Visible=�6   $�� ��6B   $
��Left=�6M   $��391,244,0,0q�6h   $	��Top=�6}   $	��283��6�   $��14(�6�   $
��Bold"�6�   5O      � 5P   
   -   5        $ grModificar��5P      $y�Hidden=�6$   {�. $��390,145,21,44q�6I   ��5Q      �� 5R      $ White��6"   $ 0.925Q�60   .+�5T      �� ���	#FF03030F$
��Left=�6*   $��78��6E   
���   �$	��Top=�6`   $	��785��6x   $��	0,-74,0,0q�6�   5U      �� 5V      $��	Porciones��6   $
��Left=�6(   $��	3,-74,0,0q�6C   $	��Top=�6V   $	��344��6n   
�������$��48(�6�   $��74��6�   $��Italic$�6�   $
��Bold"�6�   �� 5W      $
��Left=�6   $��74��6/   $��675,-74,0,0q�6;   $	��Top=�6P   $��64��6h   $&�� Imagenes/Ico/modificar boton.png��6s   $	 
1.391,0.77&�6�   �� 5X      $��Id porcion:��6   $
��Left=�6*   $��	50,84,0,0q�6E   $	��Top=�6X   $	 0.198,0.885&�6p   �� 5Y      -   5        $ txtIdPorcionMod��5Y   $   $ 3��6   $
��Left=�6=   $��26��6X   $��
189,84,0,0q�6d   $	��Top=�6x   $	��178��6�   $
 Wrap=�6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5Z      $��Nombre porcion:��6   $
��Left=�6.   $��
50,147,0,0q�6I   $	��Top=�6]   $	 0.198,0.885&�6u   �� 5[      -   5        $ txtNombrePorcionMod��5[   %   $ 30��6   $
��Left=�6B   $��23��6]   $��189,147,0,0q�6i   $
 Wrap=�6~   $	��Top=�6�   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5\      $��Cantidad porcion:��6   $
��Left=�60   $��
50,265,0,0q�6K   $	��Top=�6_   �� 5]      -   5        $ txtCantidadMod��5]   $   $ 5��6   $
��Left=�6<   $��23��6W   $��189,267,0,0q�6c   $
 Wrap=�6x   $	��Top=�6�   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5^      $��Nombre producto:��6   $
��Left=�6/   $��
50,208,0,0q�6J   $	��Top=�6^   �� 5_      -   5        $ cboNombreProductoMod��5_      $
��Left=�65   $��26��6P   $��189,208,0,0q�6\   $	��Top=�6q   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5`      -   5        $ lbldistribucionmod��5`   (   $y�Hidden=�6   $�� ��6D   $
��Left=�6O   $��399,262,0,0q�6j   $	��Top=�6   $	��283��6�   $��14(�6�   $
��Bold"�6�   �� 5a      $��Precio porcion:��6   $
��Left=�6.   $��
50,330,0,0q�6I   $	��Top=�6]   �� 5b      -   5        $ txtPrecioMod��5b   $   $ 5��6   $
��Left=�6:   $��23��6U   $��189,332,0,0q�6a   $
 Wrap=�6v   $	��Top=�6�   $	��178��6�   ���	#FFAAA9A9
���   �$��14(�6�   6�   �� 5c      -   5        $
 btnGuardarMod��5c      $
��Left=�6-   $��556,475,0,0q�6H   $	��Top=�6]   $	��162��6u   $��77��6�   ���	#FF33DC70
�������$��Arial)�5f      $��20(�5g      #���   6   .��5i      �� $��68��6   $$��Imagenes/Ico/guardar boton.png��6$   $��Center=�6L   $	��Top=�6i   $	 	0.528,0.5&�6�   $��74��6�   5j      5l      � 5m   
   -   5        $ grListar��5m      $��390,145,21,44q�6!   $y�Hidden=�68   {�. ��5n      �� 5o      $ White��6"   $ 0.925Q�60   .+�5q      �� ���	#FF03030F$
��Left=�6*   $��78��6E   
���   �$	��Top=�6`   $	��785��6x   $��	0,-74,0,0q�6�   5r      �� 5s      $��	Porciones��6   $
��Left=�6(   $��	3,-74,0,0q�6C   $	��Top=�6V   $	��344��6n   
�������$��48(�6�   $��74��6�   $��Italic$�6�   $
��Bold"�6�   �� 5t      $
��Left=�6   $��74��6/   $��675,-74,0,0q�6;   $	��Top=�6P   $��64��6h   $%��Imagenes/Ico/listar  banner.png��6s   $	 
1.391,0.77&�6�   �� 5u      $��Buscar��6   $
��Left=�6%   $��
57,157,0,0q�6@   $	��Top=�6T   $	��287��6l   
���   �$��24(�6�   $��46��6�   $��Italic$�6�   �� 5v      -   5        $ txtBuscarid��5v   $   $ 4��6   $
��Left=�69   $��23��6T   $��
57,208,0,0q�6`   $
 Wrap=�6t   $	��Top=�6�   $	��241��6�   
���   �$	 0.484,2.565&�6�   6"  �� 5w      -   5        $
 	btnBuscar��5w      $��	btnBuscar��6(   $
��Left=�6<   $��303,184,0,0q�6W   $	��Top=�6l   $��50��6�   $��47��6�   $	 -0.14,0.024&�6�   ��6�   Z� ��6�   Z� ��5x      �� 5y      $$ Imagenes/Ico/buscar banner.png��6!    j� Stretch$ UniformToFill=�6N   5{      �� 5|      -   5        $
 btnModificar��5|      $��ir A Modificar��6+   $
��Left=�6D   $��381,165,0,0q�6_   $	��Top=�6t   $	��128��6�   $��66��6�   $��Center=�6�   ���	#FF03030F
�������$��Arial)�5      $��20(�5�      #���   6   6L   �� 5�      -   5        $
 btnEliminar��5�      $��Eliminar��6*   $
��Left=�6=   $��559,165,0,0q�6X   $	��Top=�6m   $	��128��6�   $��66��6�   $��Center=�6�   ���	#FFC90A0A
�������$��Arial)�5�      $��20(�5�      #���   6   6L   &   System.Windows.Controls.DataGrid  5�      -   5          Name$ dtgridListaPorcion��5�         
IsReadOnly . $y�Visible=�6]   {�. $
��Left=�6�   $	��287��6�   $��
57,255,0,0q�6�   $	��Top=�6�   $	��665��6�   
���   ����	#FFA8A4A4��6E   �� 6�   5�      �� 5�   
   -   5        $
 	btnCerrar��5�      $
��Left=�6%   $��1171,0,-0.471,0q�6@   $	��Top=�6Y   $��31��6q   $	 0.662,0.275&�6|   $��28��6�   
�������$
��Bold"�6�   $��Microsoft Sans Serif)�6�   $��18(�6  ��5�      �� 5�      $ 	#FFCBC7C7��6"   $ 0.635Q�64   ��5�      �� 5�      $ 	#FFCBC7C7��6"   $ 0.635Q�64   .��5�      �� $��Imagenes/Ico/cerrar.png��6   67   5�      �� 5�   
   -   5        $
 btnminimizar��5�      $
��Left=�6(   $��
1139,0,0,0q�6C   $	��Top=�6W   $��32��6o   $	 0.662,0.275&�6z   $��28��6�   
�������$��Goudy Stout)�6�   $
��Bold"�6�   $��16(�6  ��5�      �� 5�      $ 	#FFCBC7C7��6"   $ 0.635Q�64   ��5�      �� 5�      $ 	#FFCBC7C7��6"   $ 0.635Q�64   .��5�      �� $ ��Imagenes/Ico/minimizar.png��6   6:   5�      �� 5�   
   -   5        
������ $
��Left=�5�   '   $��44��6B   $��10,0,0,0q�6N   $	��Top=�6a   $
��1124��6y   6�   5�      5�      