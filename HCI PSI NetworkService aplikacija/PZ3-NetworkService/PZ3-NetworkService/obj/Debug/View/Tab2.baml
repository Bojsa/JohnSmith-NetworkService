   M S B A M L   `   `   `  ����M  IPZ3-NetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:PZ3_NetworkService.ViewPZ3_NetworkService.View  5      "    PZ3_NetworkService.View.Tab2   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:PZ3_NetworkService.View     5      $
��1100��5   #   .��5	      � 
�����_�.+�5      	� $
��Name��6     	� 	GroupName$  G��6    d� 	IsChecked$
 TrueY�6-   $
��Left=�6>   $��	36,34,0,0q�6Y   $	��Top=�6l   	� 5      $
��Type��6   $  G��6   $
��Left=�6Q   $��	36,57,0,0q�6l   $	��Top=�6    6-   �� *IsTypeChecked+6   �� 5       � ToolTip$ Unesite zeljeno ime/tip��6   $��30��6.   $	��100��6:   $
��Left=�6F   $��
107,38,0,0q�6b    �� TextWrapping$
 Wrap=�6v   $	��Top=�6�   ��6�   �� *SearchContent+6�   �� 5      $% Pretraga po zadatom kriterijumu��6   $��_Search��65   $	��100��6G   $��
107,86,0,0q�6S   $
��Left=�6�   $��40��6�   $	��Top=�6�   ��6g   �� *SearchCommand+6�   �� 5      $��20��6   $	��100��6   $��Type:��6'   $��
0,45,422,0q�64   $��Right=�6H   $	��Top=�6d   �� 5      $��20��6   $��52��6   $	��ID:��6%   $��
313,45,0,0q�60   $
��Left=�6D   $	��Top=�6_   �� 5      $��
313,70,0,0q�6>   $��30��6R   $	��Top=�6^   $
��Left=�6v   $	��100��6�   ��6   �� *CurrentRoad.UserId+ �� Mode$ TwoWay=�6�   �� 5      $��20��6   
���  ��$��313,105,0,0q�6a   $	��Top=�6v   $
��Left=�6�   $	��129��6�   ��6   �� *&$CurrentRoad.ValidationErrors[UserId]+6�   �� 5      $��20��6   
���  ��$��578,105,0,0q�6h   $
��Left=�6}   $	��147��6�   $	��Top=�6�   ��6   �� *-+CurrentRoad.RoadType.ValidationErrors[Type]+6�   �� 5      $��20��6   $	��100��6   $��RoadNum:��6&   $��
447,45,0,0q�66   $
��Left=�6J   $	��Top=�6e   �� 5      $��30��6   $	��100��6   $��
447,70,0,0q�6W   $
��Left=�6k   $	��Top=�6�   ��6%   �� *CurrentRoad.RoadNum+$ TwoWay=�6�   �� 5      $��20��6   
���  ��$��447,105,0,0q�6b   $
��Left=�6w   $	��126��6�   $	��Top=�6�   ��6   �� *'%CurrentRoad.ValidationErrors[RoadNum]+6�   �� 5      $ Izaberite tip puta��6   $��30��6*   $	��100��66   $��
578,70,0,0q�6�   $	��Top=�6�   $
��Left=�6�   ��6B   ��  �� Path$ Types� �� SelectedItem 6g   �� *CurrentRoad.RoadType.Name+6�   �� 5      $! Dodavanje novog puta u bazu��6   $
��_Add��61   $	��100��6@   $��
720,70,0,0q�6L   $
��Left=�6   $��30��6�   $	��Top=�6�   ��6`   �� *
AddCommand+6�   �� 5      $0 *Prvo selektujte zeljeni put pa ga obrisite��6   $��Remove��6@   $
��Left=�6s   $��959,111,0,0q�6�   $	��Top=�6�   $	��113��6�   $��37��6�   ��6Q   �� *RemoveCommand+6�   &   System.Windows.Controls.DataGrid  5!      $
��Left=�5#   >   $��10,155,0,10q�6Y      CanUserResizeRows .  $   HorizontalScrollBarVisibility$
 Auto=�6�   $
��1062��6�   $��Khmer UI)�6�   $��14(�6�   
���   �	   GridLinesVisibility$
	 None=�5$   *   
������ $��3q�6_   
   
IsReadOnly
 .    SelectionMode$ Single=�6�      AutoGenerateColumns .  ��5"      �� *Roads+ �� UpdateSourceTrigger$ PropertyChanged=� 5#      �� *SelectedRoad+��5$   �   Z�    Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ User ID��6      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6*   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *UserId+  5(      $
 Name��6   $	 200 6'    63   �� *	RoadNum+  5)      $ RoadType��6   $	 200 6+    67   �� *RoadType.Name+4  .System.Windows.Controls.DataGridTemplateColumn  5*      $ Image��6   $	 350 6,      CellTemplate 5+      �� 5,      .R�5-   	   �� $	��104��62   $	��135��6?   $��0,5,0,5q�6K   $��Uniform=�6\   $��Center=�6n   $��Center=�6�   ��6   �� *RoadType.ImgSrc+6�   5.   	   50        51      $ 
Last Value��6   $	 110 6-    69   �� *Value+53      �� 54      $ Celina za pretragu��6   
���   �$��1q�6<   $
��Left=�6P   $	��126��6k   $��	25,22,0,0q�6x   $	��Top=�6�   $	��198��6�   �� 55      $��	SearchBox��6
   $
��Left=�6   $��25,0,0,0q�69   $	��Top=�6K   �� 56      $ Celina za dodavanje puta��6   
���   �$��1q�6B   $
��Left=�6V   $	��126��6q   $��
256,22,0,0q�6~   $	��Top=�6�   $	��616��6�   �� 57      $��AddBox��6
   $
��Left=�6   $��	274,0,0,0q�66   $	��Top=�6I   59      5:      