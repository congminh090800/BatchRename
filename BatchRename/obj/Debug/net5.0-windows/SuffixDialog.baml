   M S B A M L   `   `   `  ����F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename  5   	       BatchRename.SuffixDialog   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BatchRename     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Suffix Configuration��5	   	   $	��180��6&   $	��350��63    � 	Resources 5
         validationTemplate �� Orientation  textBoxInError �� 
TargetType �� Triggers P� Property P� Value �� Property	 �� Value
 �� Self �� RelativeSource �� Path��%v  &	        &	 �     �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� 6.   �� 5      
���  ��$��20(�6-   $�� !��6;   6G   5      5      �� 5   
   " ��6'    5      P� 5       �@=�HasError$
 True��69   . �5      ��  �@�ToolTip	 60   �� # Z
 $) #(Validation.Errors)[0].ErrorContent�6�   5      5      5      .��5      �� .+�5   
   �� $��Suffix��6    �� Left$ 21��6"   
 �� Top$ 27��63    J� RenderTransformOrigin$ 0.79,-1.429&�6C   5      �� 5   
   -   5        $��TextBox��5   4    =� ErrorTemplate# [  6D   #��[ 6�   $	 124��6�   $ 27��6�   $	��189��6�   .��6�   ��5       �� 5!      $ Suffix�6    �� UpdateSourceTrigger$ PropertyChanged=�6(    �� ValidationRules 5"         BatchRename.ValidCharacter  5#         BatchRename.Required  5$      5&      .��5'      5(      �� 5*   
   -   5        $��
SaveButton��5*      $ 68��6<   $ 90��6M   $��75��6]   $��28��6h   ${�False��6t   .��5+      Save5,      �� 5.   
   -   5        $��CancelButton��5.      $	 182��6%   $ 90��67   $��74��6G   $��28��6R    �� IsCancel$
 True��6^   .��5/      Cancel50      51      52      