   M S B A M L   `   `   `  ���� F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename      BatchRename.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:BatchRename     -     � WindowStartupLocation$  CenterScreen=� � Icon$ Assets/app.png�� � Title$ Batch Rename��$	��450��$	��800�� � 
ResizeMode$ NoResize=� � 	Resources    DefaultButton �� 
TargetType �� Property �� Value �� 
TargetType	 �� Triggers
 P� Property P� Value �� EndPoint �� 
StartPoint � Color � Offset  DefaultOperationButton �� CornerRadius  
DefaultTab ��Name �� Triggers �� 
TargetName  OperationItemStyle  ListViewItemStyle  GridViewColumnHeaderGripper �� Padding  ListViewHeaderStyle ��Name �� TextAlignment n�Name��%j	  &	        &	 �    &	 1    &	 �    &	 .    &	 ,    &	 �    7  �� " ��.���  � ��
 ���� ��  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=�	 P� 
 � {�$ False��. ���  �@J�Opacity$	 0.5Q���  �@�Cursor$
 None��P� 
 �@J�IsMouseOver$
 True��. ���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ #ffffff��$ 0Q�� $ #649fd1��$ 1Q���  � ��
 �������  �@�Cursor$
 Hand���� " ��.���  � ��
 ���� ��  � �� �� " ��.R��� $ 15��#��z��._��� $��Center=�$��Center=�	 P� 
 � {�$ False��. ���  �@J�Opacity$	 0.5Q���  �@�Cursor$
 None��P� 
 �@J�IsMouseOver$
 True��. ���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ #ffffff��$ 0Q�� $ #649fd1��$ 1Q���  � ��
 �������  �@�Cursor$
 Hand���� " ��.���  � ��
 �������  � �� �� " ��.R��� $��Border��$��1,1,1,0q�
�������$ 4,4,0,0��$	��2,0q�#��z��._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,2q� P� 
 �@��
IsSelected$
 True��. ���  � �� �#dbdbdbP� 
 �@��
IsSelected$ False��. ��� $ Border�� � ��
 �����	 P� 
 �@J�IsMouseOver$
 True��. ���  �@�Cursor$
 Hand���� " ��.���  � ��
 �   ���  � �� �� " ��.R��� $��1q�#��z��#��z��$ 10��._��� 	 P� 
 �@J�IsMouseOver$
 true��. ���  � �� �#dbdbdb��  � �� �#9c9c9cP� 
 �@��
IsSelected$
 true��. ���  � �� �#9c9c9c��  � �� �#dbdbdb��  � ��
 ������� " ��.���  � ��
 �   �	 P� 
 �@J�IsMouseOver$
 true��. ���  � �� �#dbdbdb��  � �� �#9c9c9cP� 
 �@��
IsSelected$
 true��. ���  � �� �#9c9c9c��  � �� �#dbdbdb��  � ��
 ������� " n�.���  � ��$ 18����  � ��
 ���� ��  � �� �� " n�.R��� # z��
������ ._��� $��Center=�$��1��#��z���� " ��.���  � �� �� " ��.R�� .+��� $��0,0,0,1q�
���   �
������ ._��� $ ContentHeader��#��z��#��z��$ Center=�n� $ PART_HeaderGripper��$��Right=�$��0q�8��  ��  �@�OverridesDefaultStyle$
 True����  �@J�	AllowDrop$ False����  � ��
 �   ���  � ��$	 120����  � ��$ 13(�.��]� $
��True��.+��� $	��Top=�
�������$��0,0,0,1q�._���  �� Orientation$ 
Horizontal=�$	��Top=�$��30��.+��� -   $��StartButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $#��Assets/appbar_start_batch.png��$��15��$��15��$��5q� �� BitmapScalingMode$ HighQuality=��� $��Start Batch��$��Center=�$��0,0,10,0q�]� $
��Left=�$
��True��$	��240��.+��� 
�������$��0,0,1,1q�$	��Top=�._��� $ 
Horizontal=�$��30��.+��� -   $��AddMethodButton��#��[  .���� $ 
Horizontal=�.+��� $"��Assets/appbar_method_add.png��$��15��$��15��$��5q�$ HighQuality=��� $��
Add Method��$��Center=�$��0,0,10,0q� � ContextMenu �� $��Center=����� .R��� -   ���� *
RuleName+ �� Mode$ OneWay=��� -   $��UpButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $"��Assets/appbar_arrow_down.png��$��15��$��15��$��5q�$ HighQuality=��� -   $��UpTopButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $��Assets/appbar_up_top.png��$��15��$��15��$��5q�$ HighQuality=��� -   $��
DownButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $ ��Assets/appbar_arrow_up.png��$��15��$��15��$��5q�$ HighQuality=��� -   $��DownBottomButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $#��Assets/appbar_down_bottom.png��$��15��$��15��$��5q�$ HighQuality=��� 
�������$��0,0,1,1q�$	��Top=�._��� $ 
Horizontal=�$��30��.+��� -	   $��
SaveButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $��Assets/appbar_save.png��$��15��$��15��$��5q�$ HighQuality=��� -
   $��
OpenButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $��Assets/appbar_open.png��$��15��$��15��$��5q�$ HighQuality=��� -   $��OperationsList��$��0q�#��[ $
��10,2q����� .R��� $ Vertical=�$��Center=� �� .@�m� -   $
��Edit��.+��� $��Center=�$��10,0,5,0q�$
��Bold"����� *Name+�� $ 
Horizontal=�$��Center=�.+��� $��Center=�$��10,0,5,0q�$
��Wrap=�$	��150������ *Description+�� $��DeleteButton��-   $|�False��#��[ $��Center=�
 � Tag �� .���� $ 
Horizontal=�.+��� $��Assets/appbar_delete.png��$��15��$��15��$��5q�$ HighQuality=��� $ 
Horizontal=�$	��Top=�$
��10,3q�.+��� $��Output: ��$
��Bold"��� -   $��LoadingOutput��$��CharacterEllipsis=�� -   $��
LoadingBar��$	��Top=�$��10��$��	20,5,20,5q����#bdd2ff�� -   $��RenameTabControl��
�������$��1,1,0,0q�.@��� #���  $��Rename Files�� � FocusVisualStyle Z� .��]� $
��True��.+��� 
�������$��0,0,0,1q�$	��Top=�._��� $ 
Horizontal=�$��30��.+��� -   $��AddFileButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $��Assets/appbar_add.png��$��15��$��15��$��5q�$ HighQuality=��� $	��Add��$��Center=�$��0,0,10,0q��� -   $��ExcludeFileButton��$|�False��#���   .���� $ 
Horizontal=�.+��� $��Assets/appbar_exclude.png��$��15��$��15��$��5q�$ HighQuality=��� $��Remove��$��Center=�$��0,0,10,0q��� -   $��RenameFilesList��$��0q�#��[ $
��10,2q� �� View �� ! �� ColumnHeaderContainerStyle# [   �� AllowsColumnReorder$  False��.P��� $��Filename��! �� Width$	! 120��" �� DisplayMemberBinding" �� *Name+# �� UpdateSourceTrigger$# PropertyChanged=��� $��New Filename��$	! 120��" �� *	NewName+$# PropertyChanged=��� $
��Path��$	! 155��" �� *Path+$# PropertyChanged=��� $��Error��$	! 150��" �� *Error+$# PropertyChanged=�