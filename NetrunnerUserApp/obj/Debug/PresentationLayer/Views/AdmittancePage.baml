   M S B A M L   `   `   `  ����K  GNetrunnerUserApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:NetrunnerUserApp.PresentationLayer.Services+NetrunnerUserApp.PresentationLayer.Services  5      =    7NetrunnerUserApp.PresentationLayer.Views.AdmittancePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Iimg9clr-namespace:NetrunnerUserApp.PresentationLayer.Services     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  RegisterPage��5	       N� ShowsNavigationUI .   N� 
Background
 ����� � 	Resources 5
      @   :NetrunnerUserApp.PresentationLayer.Services.ImageConverter   imageConverter  PasswordFont  BasicButton �� 
TargetType �� Property �� Value �� 
TargetType��%�   &	        &	      &	 U       5   
   6   *�5   
   .*/PresentationLayer/Resources/#PasswordFont)�6W   �� 5   
   " ��6$   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� 5      5      5      5      .��5      ]� .+�5   
   � .+�5      �� $	��100��6   $	��Top=�6"   
�������6:   �� 5      $	��100��6   $	��Top=�6$   $
��Left=�6<   ��5      �� *
TitleImage+ �� 	Converter# [  �� 5      $	��Top=�6   $��Right=�6-   $��25q�6I   $��50��6U   $��24��6d   $��80��6p   $��12(�6{   $
��Bold"�6�   	 � Cursor$
	 Hand��6�   ��5      �� *SecondaryButtonName+��6=   �� *SecondaryCommand+�� 5      $��Center=�6   $��Center=�64   
����·�$��NetrunnerLeague��5      $��30(�60   
���   �
���   �.��5       � 
 � ColumnDefinitions
 5!      �� 5"      $	��100 �6+   �� 5#      $	��100 �6+   �� 5$      $	��100 �6+    � RowDefinitions 5&      �� 5'      ��6(   �� *UsernameHeight+�� 5(      ��6(   �� *PasswordHeight+�� 5)      ��6(   �� *EmailHeight+�� 5*      $��50 �6(   �� 5+      .+�5-      �� $��	Username:��6    $
��Bold"�61   $��16(�6C   
���   �$��0��5.       $��0��60   $��Center=�6=   �� 5/      $��1��6   $��2��6.   $��0��6B   $��12(�6O   $��24��6]   $	 IBeam��6i   ��6x   ��  �� Path$ CurrentInput.Username��� 50      $��	Password:��6    $
��Bold"�61   $��16(�6C   
���   �$��0��51       $��1��60   $��Center=�6=   �� 52      $��1��6   $��2��6.   $��1��6B   $��10(�6O   $��Center=�6]   $��24��6   $	 IBeam��6�   #��[ 53      ��6I   �� $ CurrentInput.Password��� 54      $��Email:��6    $
��Bold"�6.   $��16(�6@   
���   �$��0��55       $��2��60   $��Center=�6=   �� 56      $��1��6   $��2��6.   $��2��6B   $��12(�6O   $��24��6]   $	 IBeam��6i   ��6x   �� $ CurrentInput.Email��� 57      #��[ 6   $	��Top=�6B   $��Center=�6Z   $��0��6w   $��3��6�   $
	 Hand��6�   ��58      �� *GoToResetPasswordCommandMode+{�6N   �� *ResetPasswordEnabled+.��59      �� $��Reset Password��6$   $��	Underline{�6:   
����  �$��10(�6h    J� Opacity 5:   $   �� *ResetPasswordVisibility+5;      �� 5<      #��[ 6   $	��Top=�6B   $��Center=�6Z   $��2��6w   $��3��6�   $
	 Hand��6�   ��5=      �� * GoToRecoverUsernameCommandMode+{�6P   �� *RecoverUsernameEnabled+.��5>      �� $��Recover Username��6$   $��	Underline{�6<   
����  �$��10(�6j    5?   $   �� *RecoverUsernameVisibility+5@      �� 5A      $��1��6   $��3��6-   $��24��6:   $��12(�6F   $
��Bold"�6T   $
	 Hand��6f   ��5B      �� *PrimaryButtonName+��6C   �� *PrimaryCommand+�� 5C      $��0��6    $��3��60   $��4��6D   $��12(�6p   
���  ��$
��Wrap=�6�   ��6Q   �� *SystemMessage+6�   5D      5E      5F      5G      5H      