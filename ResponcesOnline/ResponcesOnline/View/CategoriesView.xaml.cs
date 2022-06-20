using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ResponcesOnline.ViewModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ResponcesOnline.View;
using ResponcesOnline.Model;
using System.IO;
using System.Drawing;

namespace ResponcesOnline
{
    /// <summary>
    /// Логика взаимодействия для CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        List<Button> buttons = new List<Button>();
        List<System.Windows.Controls.Image> images = new List<System.Windows.Controls.Image>();

        public CategoriesWindow()
        {
            InitializeComponent();

            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                List<Category> categories = db.Categories.Where(x => x.CategoryId == null || x.CategoryId == -1).ToList();

                foreach (var item in CategoryWrapPanel.Children)
                {
                    if (item is Button button)
                    {
                        buttons.Add(button);

                        if (button.Content is System.Windows.Controls.Image image)
                        {
                            images.Add(image);
                        }
                    }
                }

                RefreshButtons(categories);
            }
        }

        private void RefreshButtons(List<Category> categories)
        {
            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i] is Button button)
                    {
                        CategoryWrapPanel.Children.Remove(button);
                    }
                }

                for (int i = 0; i < categories.Count; i++)
                {
                    if (buttons[i] is Button button)
                    {
                        BitmapImage image = ConvertByteToImage(categories[i].Image);
                        images[i].Source = image;
                        buttons[i].Content = categories[i].Title;
                        buttons[i].Click += OnClick;
                        CategoryWrapPanel.Children.Add(buttons[i]);
                    }
                }
            }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                Button clickedButton = (Button)sender;
                string buttonData = clickedButton.Content.ToString();

                Category category = db.Categories.Where(x => x.Title == buttonData).FirstOrDefault();
                List<Category> categories = db.Categories.Where(x => x.CategoryId == category.Id).ToList();

                RefreshButtons(categories);
            }
        }

        private BitmapImage ConvertByteToImage(byte[] bytes)
        {
            System.IO.MemoryStream Stream = new System.IO.MemoryStream();
            Stream.Write(bytes, 0, bytes.Length);
            Stream.Position = 0;
            System.Drawing.Image img = System.Drawing.Image.FromStream(Stream);
            BitmapImage bitImage = new BitmapImage();
            bitImage.BeginInit();
            System.IO.MemoryStream MS = new System.IO.MemoryStream();
            img.Save(MS, System.Drawing.Imaging.ImageFormat.Jpeg);
            MS.Seek(0, System.IO.SeekOrigin.Begin);
            bitImage.StreamSource = MS;
            bitImage.EndInit();
            return bitImage;
        }
    }
}
