   M S B A M L   `   `   `  ����H  DTelkomCareBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TelkomCareBotTelkomCareBot  5   	       TelkomCareBot.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TelkomCareBot     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5
      � .+�5   
   �� -   5         ��Name$ StartButton��5      $
��🌐��6&   $��50(�63   $
��Left=�6B   $��
53,106,0,0q�6]   $	��Top=�6q   $��87��6�   $	��208��6�   
����·�6�   �� 5   
   $��DemiBold"�6   $��20(�6&   $��	Send Data��64   $
��Left=�6H   $��107,214,0,0q�6c   $	��Top=�6x       TelkomCareBot.ToggleButton  5   
   -   5          Name$ SendData��5          TelkomCareBot.ToggleButton   MouseLeftButtonDown$! SendDataMouseLeftButtonDown��6�   ${�False��6/   $
��Left=�6A   $	��120��6\   $��
75,250,0,0q�6i   $	��Top=�6}   $	��164��6�   6�   �� 5   
   $��DemiBold"�6   $��20(�6&   $��	Start Web��65   $
��Left=�6I   $��
109,64,0,0q�6d   $	��Top=�6x   �� 5   
   -   5         ��Name$ DisplayName��5      $��60(�6%   $
��Bold"�63   $��TelkomeCareBot��6E   $
��Left=�6^   $��291,105,0,0q�6y   $	��Top=�6�    J� RenderTransformOrigin$ 0.634,0.365&�6�   
���  ��6�   �� 5   
   -   5         ��Name$ SleepInputBox��5      $��20(�6)   $
��Left=�67   $��301,276,0,0q�6R   $��Sleep Value��6g    �� TextWrapping$
 Wrap=�6z   $	��Top=�6�   $��Center=�6�   $��Center=�6�   $	��118��6�   $��69��6   �� 5   
   $��20(�6   $��DemiBold"�6   $��
Sleep Time��64   $
��Left=�6I   $��299,214,0,0q�6d   $	��Top=�6y   �� 5   
   $��20(�6   $��ms��6   $
��Left=�6+   $��423,292,0,0q�6F   $	��Top=�6[   �� 5   
   -   5        $ LimitInputBox��5      $��20(�6)   $
��Left=�67   $��479,276,0,0q�6R   $��Limit Value��6g   $
 Wrap=�6z   $	��Top=�6�   $��Center=�6�   $��Center=�6�   $	��118��6�   $��69��6   �� 5   
   $��20(�6   $��items��6   $
��Left=�6.   $��602,292,0,0q�6I   $	��Top=�6^   �� 5   
   $��20(�6   $��DemiBold"�6   $��Data Send Limit��64   $
��Left=�6N   $��479,214,0,0q�6i   $	��Top=�6~   �� 5   
   -   5        $ 	itemLabel��5      $��20(�6#   $��	ExtraBold"�61   $��(items)��6H   $
��Left=�6Z   $��480,238,0,0q�6u   $	��Top=�6�   5      5      