   M S B A M L   `   `   `  ����   	MUTLoader    MUTLoader.PlayerManager   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  	MUTLoader��$	��300��$	��350�� � WindowStartupLocation$ CenterScreen=�.���� .@��� $��Sniper��.���  � ColumnDefinitions �� $
��.50* ��� $
��.50* � � RowDefinitions �� �� �� �� �� �� �� .+��� $��0��$��0��$��Center=�$��Center=�$��5,5,5,5q�$
��OVR:���� $��0��$��1��$��Center=�$��Center=�$��5,5,5,5q�$��Name:���� $��0��$��2��$��Center=�$��Center=�$��5,5,5,5q�$��
ID Number:���� $��0��$��3��$��Center=�$��Center=�$��5,5,5,5q�$��
Max Price:���� -    ��Name$ OVRBox��$��1��$��0��$
��Left=�$��Center=�$��
5, 5, 5, 5q�$��38���� -   $ NameBox��$��1��$��1��$
��Left=�$��Center=�$��
5, 5, 5, 5q�$	��100���� -   $ IDBox��$��1��$��2��$
��Left=�$��Center=�$��
5, 5, 5, 5q�$��69���� -   $ PriceBox��$��1��$��3��$
��Left=�$��Center=�$��
5, 5, 5, 5q�$��80���� $��1��$��4��$��2��.+��� -    ��Name$ AddPlayerButton��$��
5, 5, 5, 5q�$��65��$��20��
���� �.����  �� Orientation$ 
Horizontal=�.+��� $	��Add��$��Center=��� $��Icons/plus.png��$��22���� $ 
Horizontal=�$��Center=�.+��� -   $ ModifyPlayerButton��$��
5, 5, 5, 5q�$��65��$��20��
��� ���.���� $ 
Horizontal=�.+��� $��Modify��$��Center=��� $��Icons/pencil.png��$��22���� -   $ RemovePlayerButton��$��
5, 5, 5, 5q�$��65��$��20��
���  ��.���� $ 
Horizontal=�.+��� $��Remove��$��Center=��� $��Icons/minus.png��$��22���� -	   $ ViewPlayersButton��$��0��$��2��$��4��$��Center=�$��Center=�$��
5, 5, 5, 5q�$	��100��$��40��.��View Players�� -
   $ ExecuteButton��$��2��$��0��$��6��$��Center=�$��Center=�$	��100��$��25��
����� �.���� $ 
Horizontal=�.+��� $��GO��$��Center=�$��0,0,5,0q��� $��Icons/rocket-fly.png��$��22���� -   $ 
StopButton��$��1��$��6��$��55��$��Center=�$��25��$��0q�
���  ��.���� $ 
Horizontal=�.+��� $
��STOP��$��Center=��� $ ��Icons/network-firewall.png��$��20���� $��Settings��.���  �� $
��.50* ��� $
��.50* � �� �� �� �� �� �� �� .+��� -   $ DelayButton��$��0��$��0��$��Change refresh rate��$
��Auto��$��Center=�$��25���� -   $ 
HelpButton��$��1��$��0��$
��Help��$	��110��$��Center=�$��25���� -   $ 
TextButton��$��0��$��1��$	��110��$��Center=�$��25������  �� Path$ TextDisplay��� -   $ NumberButton��$��1��$��1��$��Change number��$	��110��$��Center=�$��25���� -   $ ResetSettings��$��1��$��2��$��Reset settings��$	��110��$��Center=�$��25���� -   $ ResetPlayers��$��0��$��2��$��Reset players��$	��110��$��Center=�$��25��