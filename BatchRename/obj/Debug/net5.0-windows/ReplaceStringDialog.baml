   M S B A M L   `   `   `  ����F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename  5   	   %    BatchRename.ReplaceStringDialog   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BatchRename     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Icon$" Assets/app_configuration.png��5	   	    � Title$ Replace String option��5
   	   $	��200��6'   $	��330��64    � 
ResizeMode$ NoResize=�6@    � 	Resources 5         validationTemplate �� Orientation  textBoxInError �� 
TargetType �� Triggers P� Property	 P� Value
 �� Property �� Value �� Self �� RelativeSource �� Path��%v  &	        &	 �     �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� 6.   �� 5      
���  ��$��20(�6-   $�� !��6;   6G   5      5      �� 5   
   " ��6'    5      P� 5       �@=�HasError$
	 True��69   . �5      �� 
 �@�ToolTip 60   �� # Z $) #(Validation.Errors)[0].ErrorContent�6�   5      5      5      .��5      �� .+�5   
   �� $
��From��6    �� Left$ 33��6    
 �� Top$ 32��61   �� 5   
   -   5        $��FromTextBox��5      $	��189��6&   $ 75��62   $ 30��6C   6U   �� 5    
   $��To��6   $ 33��6   $ 65��6/    J� RenderTransformOrigin$ 0.79,-1.429&�6?   5"      �� 5$   
   -   5        $��	ToTextBox��5$   4   $ 75��6F   $ 65��6W   $	��189��6g    =� ErrorTemplate# [  6s   #��[ 6�   .��6�   ��5%      �� 5&      $ Replacer�6    �� UpdateSourceTrigger$ PropertyChanged=�6*    �� ValidationRules 5'         BatchRename.ValidCharacter  5(         BatchRename.Required  5)      5+      .��5,      5-      �� 5/   
   -   5        $��
SaveButton��5/      $ 76��6<   $	 107��6M   $��75��6^   $��28��6i   .��50      Save51      �� 53   
   -   5        $��CancelButton��53      $	 191��6@   $	 107��6R   $��75��6c   $��28��6n    �� IsCancel$
 True��6z   .��54      Cancel55      56      57      