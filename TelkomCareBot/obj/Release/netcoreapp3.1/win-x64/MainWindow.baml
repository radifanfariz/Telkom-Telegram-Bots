   M S B A M L   `   `   `  ���� H  DTelkomCareBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TelkomCareBotTelkomCareBot      TelkomCareBot.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:TelkomCareBot     -     � Title$  
MainWindow��$	��450��$	��800��.��� .+��� -    ��Name$ StartButton��$
��🌐��$��50(�$
��Left=�$��
53,106,0,0q�$	��Top=�$��87��$	��208��
����·��� $��DemiBold"�$��20(�$��	Send Data��$
��Left=�$��107,214,0,0q�$	��Top=�    TelkomCareBot.ToggleButton  -     Name$ SendData��    TelkomCareBot.ToggleButton   MouseLeftButtonDown$! SendDataMouseLeftButtonDown��${�False��$
��Left=�$	��120��$��
75,250,0,0q�$	��Top=�$	��164���� $��DemiBold"�$��20(�$��	Start Web��$
��Left=�$��
109,64,0,0q�$	��Top=��� -    ��Name$ DisplayName��$��60(�$
��Bold"�$��TelkomeCareBot��$
��Left=�$��291,105,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.634,0.365&�
���  ���� -    ��Name$ SleepInputBox��$��20(�$
��Left=�$��301,276,0,0q�$��Sleep Value�� �� TextWrapping$
 Wrap=�$	��Top=�$��Center=�$��Center=�$	��118��$��69���� $��20(�$��DemiBold"�$��
Sleep Time��$
��Left=�$��299,214,0,0q�$	��Top=��� $��20(�$��ms��$
��Left=�$��423,292,0,0q�$	��Top=��� -   $ LimitInputBox��$��20(�$
��Left=�$��479,276,0,0q�$��Limit Value��$
 Wrap=�$	��Top=�$��Center=�$��Center=�$	��118��$��69���� $��20(�$��items��$
��Left=�$��602,292,0,0q�$	��Top=��� $��20(�$��DemiBold"�$��Data Send Limit��$
��Left=�$��479,214,0,0q�$	��Top=��� -   $ 	itemLabel��$��20(�$��	ExtraBold"�$��(items)��$
��Left=�$��480,238,0,0q�$	��Top=�