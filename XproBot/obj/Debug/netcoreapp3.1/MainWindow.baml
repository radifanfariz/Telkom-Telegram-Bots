   M S B A M L   `   `   `  ����B  >XproBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:XproBotXproBot  5   	       XproBot.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:XproBot     5   	   -   5          � Title$  
MainWindow��5   	   $	��300��6   $	��320��6)   .��5      � .+�5   
   �� -   5         ��Name$ StartButton��5      $��30(�6&   $
��🌐��64   $
��Left=�6A   $��
56,152,0,0q�6\   $	��Top=�6p   $��50��6�   $��75��6�   
������6�      XproBot.ToggleButton  5   
   -   5          Name$ SendData��5         XproBot.ToggleButton   MouseLeftButtonDown$! SendDataMouseLeftButtonDown��6�   ${�False��6/   $
��Left=�6A   $��50��6\   $��181,152,0,0q�6h   $	��Top=�6}   $��79��6�   6�   �� 5   
   $��20(�6   $��	Send Data��6   $
��Left=�62   $��170,115,0,0q�6M   $	��Top=�6b   �� 5   
   $��20(�6   $��	Start Web��6   $
��Left=�63   $��
45,115,0,0q�6N   $	��Top=�6b   �� 5   
   -   5         ��Name$ DisplayName��5      $��40(�6%   $
��Bold"�63   $��XproBot��6E   $��Center=�6W   $��0,16,0,0q�6t   $	��Top=�6�    J� RenderTransformOrigin$ 0.634,0.365&�6�   
���  ��6�   �� 5   
   -   5         ��Name$ SleepInputBox��5      $
��Left=�6)   $��
140,88,0,0q�6D   $��Sleep Value��6X    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $��93��6�   6�   �� 5   
   $��DemiBold"�6   $��
Sleep Time��6&   $
��Left=�6;   $��	67,84,0,0q�6V   $	��Top=�6i   �� 5   
   $��ms��6   $
��Left=�6   $��
233,84,0,0q�68   $	��Top=�6L   5      5      