   M S B A M L   `   `   `  ����   EmployeeAccounting#    EmployeeAccounting.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  
MainWindow��$	��450��$	��800��$	��425��$	��725��$	��550��$	��900��.��� 
����d�.+��� $��АЦТИ��$��Consolas)�$��34(�
�������$
��Left=�$	��Top=�
�������$��
0, 0, 3, 3q��� $)��#Учёт сотрудников 0.1��$��Consolas)�$��16(�$
��Left=�$��Bottom=�
�������
�������$��
0, 3, 3, 0q�� -   $��LoadingGrid��$y�Hidden=�$	��500��$	��300��$��Center=�$��Center=� K� ZIndex$ 1��.+�� 
���   � J� Opacity$	 0.5Q��� -   $��LoadingLabel��$'��!Ожидание сервера..��$��Center=�$��Center=�
�������$��24(�$
��Bold"��� -   $��MainGroupBox��$'��!Панель управления��$	��500��$	��300��$��Center=�$��Center=�
�������.��� .+��� -   $��ButtonsStackPanel��$��Center=�$��Center=� �� Orientation$ 
Horizontal=�.+��� -   $��ViewEmployeeButton��$.��(Просмотр и управление��$	��165��$��40��$��Consolas)�$��13(�
�������
����d�
��������� $y�Hidden=�$��60���� -   $��AddEmployeeButton��$+��%Добавить сотрудника��$	��165��$��40��$��Consolas)�$��13(�
�������
����d�
�������� -   $��AddEmployeeGrid��$y�Hidden=�.+��� -   $��NameTextBox��$	��125��$��25��$
��Left=�$	��Top=�$��75, 50, 0, 0q�$��Consolas)�$
��Left=�$��Center=��� -	   $��PhoneTextBox��$	��125��$��25��$
��Left=�$	��Top=�$��75, 100, 0, 0q�$��Consolas)�$
��Left=�$��Center=��� -
   $��AddressTextBox��$	��125��$��25��$
��Left=�$	��Top=�$��75, 150, 0, 0q�$��Consolas)�$
��Left=�$��Center=��� -   $��PositionComboBox��$	��125��$��25��$
��Left=�$	��Top=�$��75, 200, 0, 0q�$��Consolas)�$
��Left=�$��Center=�� -   $��AdditionalButtons��.+��� -   $��PassportLoadButton��$	��135��$��25��$)��#Загрузить паспорт..��$��Consolas)�
����d�
�������
�������$��Right=�$	��Top=�$��0, 50, 75, 0q��� -   $��SnilsLoadButton��$	��135��$��25��$%��Загрузить снилс..��$��Consolas)�
����d�
�������
�������$��Right=�$	��Top=�$��0, 100, 75, 0q��� -   $��InsurancePolicyLoadButton��$	��135��$��25��$%��Загрузить полис..��$��Consolas)�
����d�
�������
�������$��Right=�$	��Top=�$��0, 150, 75, 0q��� -   $��CreateEmployeeButton��$	��125��$��25��$��Добавить��$��Consolas)�
����d�
�������
�������$��Right=�$��Bottom=�$��0, 0, 150, 15q��� -   $��GoBackButton��$y�Hidden=�$	��125��$��25��$��
Назад��$��Consolas)�
����d�
�������
�������$��Right=�$��Bottom=�$��0, 0, 15, 15q�� -   $��ViewEmployeesGrid��$y�Hidden=�.+��� -   $��EmployeesComboBox��$	��125��$��25��$
��Left=�$	��Top=�$��75, 15, 0, 0q�$��Consolas)�$
��Left=�$	��Top=��� -   $��PassportViewButton��$	��135��$��25��$#��Открыть паспорт��$��Consolas)�
����d�
�������
�������$��Right=�$	��Top=�$��0, 50, 75, 0q��� -   $��SnilsViewButton��$	��135��$��25��$��Открыть снилс��$��Consolas)�
����d�
�������
�������$��Right=�$	��Top=�$��0, 100, 75, 0q��� -   $��InsurancePolicyViewButton��$	��135��$��25��$��Открыть полис��$��Consolas)�
����d�
�������
�������$��Right=�$	��Top=�$��0, 150, 75, 0q��� -   $��RemoveEmployeeButton��$	��125��$��25��$��Удалить��$��Consolas)�
����d�
�������
�������$��Right=�$��Bottom=�$��0, 0, 150, 15q��� -   $��UpdateEmployeeButton��$	��125��$��25��$��Обновить��$��Consolas)�
����d�
�������
�������$��Right=�$��Bottom=�$��0, 0, 290, 15q�