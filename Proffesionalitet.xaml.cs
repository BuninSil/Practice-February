using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Reflection;
using Xceed.Words.NET;

namespace DataGrid
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Proffesionalitet : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Proffesionalitet(string userid, string password)
        {
            InitializeComponent();
            log.Text = userid;
            pass.Text = password;
           
                con = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Database=practic;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Account where login=@userid and pass=@password", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@password", password);
                reader = cmd.ExecuteReader();
                reader.Read();
                name.Text = reader.GetString(3);
 
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Anketa anketa = new Anketa(log.Text, pass.Text);
            anketa.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow(log.Text, pass.Text);
            m.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Postyplenie g = new Postyplenie(log.Text, pass.Text);
            g.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Proffesionalitet proffesionalitet = new Proffesionalitet(log.Text, pass.Text);
            proffesionalitet.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Login login = new Login();  
            login.Show();
            this.Close();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //распечатать заявление на профессионалитет и соглашения 
            if (check.IsChecked == true) { }
            var fileName = @"soglasie_obuchayuschegosya.docx";

            var items = new Dictionary<string, string>
            {
                {"<FIO>", FIO.textBox.Text },
                {"<NUM1>", ShifrSpeciality.textBox.Text },
                {"<NUM2>", FormPay.textBox.Text },
                {"<ADRES1>", registradres.textBox.Text },
                {"<ADRES2>", adresjiv.textBox.Text },
                {"<PUNKT>", comboBox1.Text },
            };
            Process(fileName, items);
        }
        internal void Process(string fileName, Dictionary<string, string> items)
        {
            try
            {
                string executableLocation = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string filePath = System.IO.Path.Combine(executableLocation, fileName);

                using (DocX document = DocX.Load(filePath))
                {
                    foreach (var item in items)
                    {
                        document.ReplaceText(item.Key, item.Value);
                    }

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string newFileName = System.IO.Path.Combine(desktopPath, System.IO.Path.GetFileNameWithoutExtension(fileName) + "1.docx"); // Новый путь для сохранения на рабочем столе

                    document.SaveAs(newFileName);

                    MessageBox.Show("Файл успешно сохранен на рабочем столе с новым именем: " + System.IO.Path.GetFileName(newFileName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

            //сохранить данные в бд

           
                con = new SqlConnection(@"Database=practic;Data Source=localhost\SQLEXPRESS;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand($@"insert into Proffesionalitet 
                                    (Паспорт , ФИО , Выдан , Согласие  ,Регистрация , Проживание ) 
                                    values 
                                    ('{ShifrSpeciality.textBox.Text}',
                                     '{FIO.textBox.Text}',
                                     '{FormPay.textBox.Text}',
                                     '{comboBox1.Text}',
                                     '{registradres.textBox.Text}',
                                     '{adresjiv.textBox.Text}')", con);
                reader = cmd.ExecuteReader();
                reader.Read();
                MessageBox.Show("Сохранено!");
            
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Window3 s = new Window3(log.Text, pass.Text);
            s.Show();
            this.Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void KodSpeciality_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void NameSpeciality_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void comboBox8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.White);
        }

       
    }
}
