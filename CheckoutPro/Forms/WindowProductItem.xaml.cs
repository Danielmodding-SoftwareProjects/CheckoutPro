﻿using CheckoutPro.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static CheckoutPro.Class.ClassProduct;

namespace CheckoutPro.Forms
{
    /// <summary>
    /// Interaktionslogik für WindowProductItem.xaml
    /// </summary>
    public partial class WindowProductItem : Window
    {
        public static WindowProductItem windowProductItemInstance;

        public WindowProductItem()
        {
            InitializeComponent();

            windowProductItemInstance = this;


            



            foreach (ClassProduct produkt in MainWindow.mainWindowInstance.ListboxMainWindowProducts.Items)
            {
                if (!ComboBoxGruppe.Items.Contains(produkt.Group))
                {
                    ComboBoxGruppe.Items.Add(produkt.Group);

                }
            }

            if(ComboBoxGruppe.Text == "Sammlung")
            {
                ComboBoxGruppe.SelectedIndex = 0;
            }
        }


        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ClassProduct produkt = new ClassProduct();
            //produkt.ID = TextBoxID.Text;
            produkt.Name = TextBoxName.Text;
            produkt.Desc = TextBoxDesc.Text;
            produkt.Icon = TextBoxIcon.Text;
            produkt.Preis = Convert.ToDouble(TextBoxPreis.Text);
            produkt.BackgroundColor = ColorpickerItemBackgroundColor.SelectedColor.ToString();
            produkt.BorderColor = ColorpickerItemBorderColor.SelectedColor.ToString();
            produkt.Group = ComboBoxGruppe.Text;

            MainWindow.mainWindowInstance.classProducts.Add(produkt);
            MainWindow.mainWindowInstance.ListboxMainWindowProducts.Items.Refresh();
            MainWindow.mainWindowInstance.GroupListBox();



            this.Close();
        }
    }
}
