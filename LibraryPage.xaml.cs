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
    /// Логика взаимодействия для LibraryPage.xaml
    /// </summary>
    public partial class LibraryPage : Page
    {
       
        public LibraryPage()
        {
           
            InitializeComponent();

            var currentLibrary = LibraryEntities.GetContext().FundLibrary.ToList();

            LibraryListView.ItemsSource = currentLibrary;
            ComboType.SelectedIndex=0;

            List<FundLibrary> currentLibrarys = LibraryEntities.GetContext().FundLibrary.ToList();

            allLibraryCount.Text = currentLibrarys.Count().ToString();

            LibraryListView.ItemsSource = currentLibrarys;

            UpdateLibrary();

        }

        private void UpdateLibrary()
        {
            var currentLibrary=LibraryEntities.GetContext().FundLibrary.ToList();
            if(ComboType.SelectedIndex==0) 
            {
            
            
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentLibrary=currentLibrary.Where(p=>(p.QuanBooks >=0 && p.QuanBooks <=10)).ToList();

            }
            if (ComboType.SelectedIndex == 2)
            {
                currentLibrary = currentLibrary.Where(p => (p.QuanBooks >= 10 && p.QuanBooks <= 20)).ToList();

            }
            if (ComboType.SelectedIndex == 3)
            {
                currentLibrary = currentLibrary.Where(p => (p.QuanBooks >= 20 && p.QuanBooks <= 30)).ToList();

            }
            if (ComboType.SelectedIndex == 4)
            {
                currentLibrary = currentLibrary.Where(p => (p.QuanBooks >= 30)).ToList();

            }

            currentLibrary = currentLibrary.Where(p=>p.FundName.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Libraries.NameLibrary.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if(RButtonDown.IsChecked.Value)
            {
                currentLibrary=currentLibrary.OrderByDescending(p=>p.FundName).ToList();
            }
            if (RButtonUP.IsChecked.Value)
            {
                currentLibrary = currentLibrary.OrderBy(p => p.FundName).ToList();
            }
            
            currentLibraryCount.Text = currentLibrary.Count().ToString();

            LibraryListView.ItemsSource = currentLibrary;

            LibraryListView.ItemsSource=currentLibrary;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateLibrary();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLibrary();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RButtonUP_Checked(object sender, RoutedEventArgs e)
        {
            UpdateLibrary();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateLibrary();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as FundLibrary));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                LibraryEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LibraryListView.ItemsSource=LibraryEntities.GetContext().FundLibrary.ToList();
            }
        }

       

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentLibrary=(sender as Button).DataContext as FundLibrary;
            if(MessageBox.Show("Вы точно хотите выполнить удаление?","Внимание!", MessageBoxButton.YesNo,MessageBoxImage.Question)
                ==MessageBoxResult.Yes)
            {
                try
                {
                    LibraryEntities.GetContext().FundLibrary.Remove(currentLibrary);
                    LibraryEntities.GetContext().SaveChanges();

                    LibraryListView.ItemsSource = LibraryEntities.GetContext().FundLibrary.ToList();

                    UpdateLibrary();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
