using Microsoft.Win32;
using ResponcesOnline.Model;
using ResponcesOnline.Model.Tools;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace ResponcesOnline.ViewModel
{
    class CategoryEditViewModel
    {
        private CommandTemplate _addCategoryCommand;
        private CommandTemplate _addImageCommand;
        private Category _category;
        private byte[] _image;
        private string _categoryTitle;
        private BitmapImage _categoryImage;
        private string _selectedCategoryId;

        private ObservableCollection<string> _categories;   
        
        public ObservableCollection<string> Categories
        {
            get => _categories = new ObservableCollection<string>(GetCategories());
        }

        public string SelectedCategory
        {
            get => _selectedCategoryId;
            set
            {
                _selectedCategoryId = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public BitmapImage CategoryImage
        {
            get => _categoryImage;
            set
            {
                _categoryImage = value;
                OnPropertyChanged(nameof(CategoryImage));
            }
        }

        public string CategoryTitle
        {
            get => _categoryTitle;
            set
            {
                _categoryTitle = value;
                OnPropertyChanged(nameof(CategoryTitle));
            }
        }

        public Category NewCategory
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(NewCategory));
            }
        }

        public CommandTemplate AddImageCommand
        {
            get
            {
                if (_addImageCommand == null)
                    _addImageCommand = new CommandTemplate(obj =>
                    {
                        AddImage();
                    });

                return _addImageCommand;
            }
        }

        public CommandTemplate AddCategoryCommand
        {
            get
            {
                if (_addCategoryCommand == null)
                    _addCategoryCommand = new CommandTemplate(obj =>
                    {
                        AddCategory();
                    });

                return _addCategoryCommand;
            }
        }

        public byte[] CategoryImageInBytes
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(CategoryImageInBytes));
            }
        }

        public void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                    NewCategory = new Category();
                    NewCategory.Image = ImageToBytes(bitmap);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void AddCategory()
        {
            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                try
                {
                    CategoryImage = ConvertByteToImage(NewCategory.Image);
                    NewCategory.Title = CategoryTitle;

                    if(string.IsNullOrWhiteSpace(SelectedCategory) == false)
                    {
                        Category category = db.Categories.Where(x => x.Title == SelectedCategory).FirstOrDefault();
                        NewCategory.CategoryId = category.Id;
                    }

                    db.Categories.Add(NewCategory);
                    db.SaveChanges();

                    MessageBox.Show("Категория добавлена успешно !");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public BitmapImage ByteToBitmap(byte[] array)
        {
            using (MemoryStream ms = new MemoryStream(array))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private byte[] ImageToBytes(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
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

        public List<string> GetCategories()
        {
            using (otzovic_dbContext db = new otzovic_dbContext())
            {
                List<string> categoriesString = new List<string>(); 
                List<Category> categories = db.Categories.ToList();

                for (int i = 0; i < categories.Count; i++)
                {
                    categoriesString.Add(categories[i].Title);
                }

                return categoriesString;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
