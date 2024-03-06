using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Words.NET;
using static DataGrid.MainWindow;

namespace DataGrid
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Window3(string userid, string password)
        {
            InitializeComponent();
            log.Text = userid;
            pass.Text = password;
           
                con = new SqlConnection(@"Database=practic;Data Source=localhost\SQLEXPRESS;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Account where login=@userid and pass=@password", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@password", password);
                reader = cmd.ExecuteReader();
                reader.Read();
                name.Text = reader.GetString(3);
      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Anketa anketa = new Anketa(log.Text, pass.Text);
            anketa.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(log.Text, pass.Text);
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Postyplenie postyplenie = new Postyplenie(log.Text, pass.Text);
            postyplenie.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Proffesionalitet pro = new Proffesionalitet(log.Text, pass.Text);
            pro.Show();
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
            //распечатать новые бланки
            if (check1.IsChecked == true && check2.IsChecked == false)
            {
                var fileName = @"soglasie_predstavitelya.docx";

                var items = new Dictionary<string, string>
                {
                    {"<FIO1>", DataBithday_Loaded.textBox.Text },
                    {"<SN1>", FormPay.textBox.Text },
                    {"<KOGDAKEM1>", KOGDD1.textBox.Text },
                    {"<ADRESSREG1>", adress1.textBox.Text },
                    {"<ADRESSJIV1>", adressj.textBox.Text },
                    {"<OSNOVANIE>", osnovan.textBox.Text },
                    {"<FIO2>", ShifrSpeciality.textBox.Text },
                    {"<SN2>", FormStudy.textBox.Text },
                    {"<KOGDAKEM2>", KOGDD2.textBox.Text },
                    {"<ADRESSREG2>", NameSchool.textBox.Text },
                    {"<ADRESSJIV2>", adress2.textBox.Text },
                };
                Process(fileName, items);
            }
            else if (check1.IsChecked == true && check2.IsChecked == true)
            {
                var fileName = @"soglasie_predstavitelya.docx";
                var fileName1 = @"obrabotkad.docx";

                var items = new Dictionary<string, string>
                {
                   {"<FIO1>", DataBithday_Loaded.textBox.Text },
                    {"<SN1>", FormPay.textBox.Text },
                    {"<KOGDAKEM1>", KOGDD1.textBox.Text },
                    {"<ADRESSREG1>", adress1.textBox.Text },
                    {"<ADRESSJIV1>", adressj.textBox.Text },
                    {"<OSNOVANIE>", osnovan.textBox.Text },
                    {"<FIO2>", ShifrSpeciality.textBox.Text },
                    {"<SN2>", FormStudy.textBox.Text },
                    {"<KOGDAKEM2>", KOGDD2.textBox.Text },
                    {"<ADRESSREG2>", NameSchool.textBox.Text },
                    {"<ADRESSJIV2>", adress2.textBox.Text },
                };
                Process(fileName, items);

                var item = new Dictionary<string, string>
                {
                   {"<FIO1>", DataBithday_Loaded.textBox.Text },
                    {"<SN1>", FormPay.textBox.Text },
                    {"<KOGDAKEM1>", KOGDD1.textBox.Text },
                    {"<ADRESSREG1>", adress1.textBox.Text },
                    {"<ADRESSJIV1>", adressj.textBox.Text },
                    {"<OSNOVANIE>", osnovan.textBox.Text },
                    {"<FIO2>", ShifrSpeciality.textBox.Text },
                    {"<SN2>", FormStudy.textBox.Text },
                    {"<KOGDAKEM2>", KOGDD2.textBox.Text },
                    {"<ADRESSREG2>", NameSchool.textBox.Text },
                    {"<ADRESSJIV2>", adress2.textBox.Text },
                    {"<PUNKT>", punkts.Text },

                };
                Process1(fileName1, item);
            }
            else if (check1.IsChecked == false && check2.IsChecked == true)
            {
                var fileName1 = @"obrabotkad.docx";

                var item = new Dictionary<string, string>
                {
                  {"<FIO1>", DataBithday_Loaded.textBox.Text },
                    {"<SN1>", FormPay.textBox.Text },
                    {"<KOGDAKEM1>", KOGDD1.textBox.Text },
                    {"<ADRESSREG1>", adress1.textBox.Text },
                    {"<ADRESSJIV1>", adressj.textBox.Text },
                    {"<OSNOVANIE>", osnovan.textBox.Text },
                    {"<FIO2>", ShifrSpeciality.textBox.Text },
                    {"<SN2>", FormStudy.textBox.Text },
                    {"<KOGDAKEM2>", KOGDD2.textBox.Text },
                    {"<ADRESSREG2>", NameSchool.textBox.Text },
                    {"<ADRESSJIV2>", adress2.textBox.Text },
                    {"<PUNKT>", punkts.Text },

                };
                Process1(fileName1, item);
            }
            else if (check1.IsChecked == false && check2.IsChecked == true)
            {
                MessageBox.Show("выберите вариант");
            }
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

                    MessageBox.Show("Файл успешно сохранен на рабочем столе " + System.IO.Path.GetFileName(newFileName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        internal void Process1(string fileName1, Dictionary<string, string> items)
        {
            try
            {
                string executableLocation = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string filePath = System.IO.Path.Combine(executableLocation, fileName1);

                using (DocX document = DocX.Load(filePath))
                {
                    foreach (var item in items)
                    {
                        document.ReplaceText(item.Key, item.Value);
                    }

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string newFileName = System.IO.Path.Combine(desktopPath, System.IO.Path.GetFileNameWithoutExtension(fileName1) + "2.docx"); // Новый путь для сохранения на рабочем столе

                    document.SaveAs(newFileName);
                    MessageBox.Show("Файл успешно сохранен на рабочем столе " + System.IO.Path.GetFileName(newFileName));

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
                SqlCommand cmd = new SqlCommand($@"insert into data 
                                    (Ребенок, Родитель, Адрес_Реб, Паспорт_Род, Паспорт_Реб, Адрес_Род) 
                                    values 
                                    ('{ShifrSpeciality.textBox.Text}',
                                     '{DataBithday_Loaded.textBox.Text}',
                                     '{NameSchool.textBox.Text}',
                                     '{FormPay.textBox.Text}',
                                     '{FormStudy.textBox.Text}',
                                     '{adressj.textBox.Text}')", con);
                reader = cmd.ExecuteReader();
                reader.Read();
                MessageBox.Show("Сохранено!");
   
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
