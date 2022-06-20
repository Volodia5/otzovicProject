using Microsoft.Win32;
using ResponcesOnline.Model;
using ResponcesOnline.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResponcesOnline
{
    /// <summary>
    /// Логика взаимодействия для CategoryEditWindow.xaml
    /// </summary>
    public partial class CategoryEditWindow : Window
    {
        public CategoryEditWindow()
        {
            InitializeComponent();

            CategoryEditViewModel viewModel = new CategoryEditViewModel();

            ItemImage.Source = viewModel.CategoryImage;

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            //openFileDialog.FilterIndex = 1;

            //if (openFileDialog.ShowDialog() == true)
            //{
            //    try
            //    {
            //        ItemImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            //    }
            //    catch (Exception exception)
            //    {
            //        MessageBox.Show(exception.Message);
            //    }
            //}
        }
    }
}
