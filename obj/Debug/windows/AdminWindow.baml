   M S B A M L   `   `   `  ����B  >CLINICS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CLINICS.windowsCLINICS.windows  5   	   !    CLINICS.windows.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CLINICS.windows     5   	     � Title$  AdminWindow��5   	   $	��800��6   $
��1550��6*    � WindowState$ 	Maximized=�5	   	    � WindowStyle$
 None=�5
   	   $��
Montserrat)�5   	   ��5      �� 5   
    �� Color$ #3d6187��6   6,    � 	Resources 5#       �� 
TargetType �� Property �� Value �� 
TargetType	 �� Triggers
 P� Property P� Value��%�  '��       �� 5$   
   " ��6   .�5%      ��  � ��
 ���� 6+   �� 5&       � ��
 ���� 6,   �� 5'       � ��$ 0q�60   �� 5(       � �� 5)      �� 5*      " ��6&   .R�5+      �� #��z��6!   
������ $��0q�6e   ._�5,      �� $��Center=�6/   $��Center=�6L   5-      5.      50      	 51      P� 52      
 �@J�IsMouseOver . 54      56      57      .��58      �  � ColumnDefinitions 59   
   �� 5:      $��* �6   6+   �� 5;      $	��15* �6   6-   .+�5=   
   �� $��0��6   $	��Top=�6%   $��Center=�6=   $��0 20 0 0q�6Z    K� ZIndex$ 1��6l   .+�5>      �� $��PLAST��6   $��20(�6%   $��Light"�63   
�������$��Center=�6Y   �� 5?      $
��SURG��6   $��20(�6$   $��Light"�62   
�������$��Center=�6X   5@      �� 5A   
   -   5         ��Name$ CloseWindow��5A   !   $��1��6   
������ $��0q�6P    � Cursor$
 Hand��6d   $��42��6�   $��54��6�   
������ $��Right=�6�   $	��Top=�6�   $��0, 15, 15, 0q�6�   $ 1��6  .��5B      �� $��/icons/Exit_inactive.png��6   68   5C      �� 5D   
   $��40��6    �� Orientation$ Vertical=�6    $��Bottom=�67   $��0��6R   $��0 0 0 30q�6b   $ 1��6t   .+�5H      �� -   5        $ ClientButton��5H   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5I      �� $ ��/icons/Client_inactive.png��6   5J      �� 5K      -   5        $ DoctorButton��5K   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5L      �� $ ��/icons/Doctor_inactive.png��6   5M      �� 5N      -   5        $ Service��5N   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5O      �� $!��/icons/Service_inactive.png��6   5P      �� 5Q      -   5        $ DoctorService��5Q   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5R      �� $'��!/icons/DoctorService_inactive.png��6   5S      �� 5T      -   5        $ Registration��5T   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5U      �� $&�� /icons/Registration_inactive.png��6   5V      �� 5W      -   5        $ RegistartionDate��5W   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5X      �� $*��$/icons/RegistrationDate_inactive.png��6   5Y      �� 5Z      -   5        $ RegistrationTime��5Z   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5[      �� $*��$/icons/RegistrationTime_inactive.png��6   5\      �� 5]      -	   5        $ DoctorRating��5]   '   $��0 0 0 20q�6   
������ 
������ $��0q�6�   $
 Hand��6�   .��5^      �� $&�� /icons/DoctorRating_inactive.png��6   5_      5`       � 5a   
   -
   5          �Name$ 
adminFrame��5a        � NavigationUIVisibility$ Hidden=�6$   $��0��6D   $��2��6T   $ 0��6h   5b      5c      5d      