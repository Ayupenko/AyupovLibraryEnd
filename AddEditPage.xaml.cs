using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AyupovLibrary
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private FundLibrary _currentLibrary = new FundLibrary();

        public AddEditPage(FundLibrary SelectedLibrary)
        {
            InitializeComponent();

            var FoundLib = LibraryEntities.GetContext().Libraries.Select(p => p.NameLibrary).ToList();

            foreach (var FoundLibItem in FoundLib)
            {
                LibraryComboBox.Items.Add(FoundLibItem);
            }



            if (SelectedLibrary != null)
            {
                _currentLibrary = SelectedLibrary;
                LibraryComboBox.SelectedIndex = _currentLibrary.LibraryID - 1;
            }
            else
            {
                LibraryComboBox.SelectedIndex = 0;
            }




            DataContext = _currentLibrary;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder errors= new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentLibrary.FundName))
            {
                errors.AppendLine("Укажите название фонда");
            }
          
            if (_currentLibrary.QuanBooks <= 0 || _currentLibrary.QuanBooks.GetType() != typeof(int))
            {
                errors.AppendLine("Укажите количество книг");
            }
            if (_currentLibrary.QuanJournals <= 0 || _currentLibrary.QuanJournals.GetType() != typeof(int))
            {
                errors.AppendLine("Укажите количество журналов");
            }
            if (_currentLibrary.QuanNewspapers <= 0 || _currentLibrary.QuanNewspapers.GetType() != typeof(int))
            {
                errors.AppendLine("Укажите количество газет");
            }
            if (_currentLibrary.QuanDissertations <= 0 || _currentLibrary.QuanDissertations.GetType() != typeof(int))
            {
                errors.AppendLine("Укажите количество диссертаций");
            }
            if (_currentLibrary.QuanReferats <= 0 || _currentLibrary.QuanReferats.GetType() != typeof(int))
            {
                errors.AppendLine("Укажите количество рефератов");
            }
            if (_currentLibrary.QuanCollections <= 0 || _currentLibrary.QuanCollections.GetType() != typeof(int))
            {
                errors.AppendLine("Укажите количество Коллекционных");
            }
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentLibrary.FundID == 0)
                LibraryEntities.GetContext().FundLibrary.Add(_currentLibrary);

            try
            {
                LibraryEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


               
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                _currentLibrary.MainImagePath = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }
    }
}
