using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
        }


        private void LoadTasks() {
            // załadowanie każdej linijki z pliku todo.txt jako osobne zadanie
            if (File.Exists("todo.txt"))
            {
                string[] lines = File.ReadAllLines("todo.txt");

                foreach (var item in lines)
                {
                    TodoPanel.Items.Add(item);
                }
            }
        }
        private void AddTodoButton_Click(object sender, RoutedEventArgs e)
        {
            // dodawanie nowego zadania do listy

            string todoText = TodoInput.Text;
            if (!string.IsNullOrEmpty(todoText))
            {
                TodoPanel.Items.Add(todoText);
                TodoInput.Clear();

                using (StreamWriter streamWriter = new StreamWriter("todo.txt", true))  
                    // drugi argument 'true' oznacza, że plik ma być dopisany
                {
                    streamWriter.WriteLine(todoText);
                }
            }
            
        }

        private void MarkAsDone_Click(object sender, RoutedEventArgs e)
        {
            // oznaczanie wybranego zadania jako zrobione

            // pobieranie zaznaczonej lini jako tekst
            string selectedItem = TodoPanel.SelectedItem.ToString();


            if (selectedItem != null)
            {

                using (StreamWriter streamWriter = new StreamWriter("todo.txt", false))
                    // drugi argument 'false' oznacza, że plik nie ma być dopisany, plik będzie pisany od nowa
                {
                    // dodanie to pliku todo.txt tylko tych lini które nie są zaznaczone przez użytkownika
                    foreach (var item in TodoPanel.Items.Cast<string>().ToArray())
                    {
                        if (selectedItem != item)
                        {
                           streamWriter.WriteLine(item);
                        }
                        
                    }
                }
                // usunięcie z listy
                TodoPanel.Items.Remove(selectedItem);
                
            }
        }

        private void TodoPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // zmiana widoczności przycisku
            // funkcja potrzebna do tego by użytkownik mógł usuwać zadania tylko gdy juz kliknął na listboxa
            MarkAsDoneButton.IsEnabled = true;
        }
    }
}