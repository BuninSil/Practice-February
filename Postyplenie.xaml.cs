using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Reflection;
using Xceed.Words.NET;

namespace DataGrid
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Postyplenie : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Postyplenie(string userid, string password)
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

        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(log.Text, pass.Text);
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Anketa n = new Anketa(log.Text, pass.Text);
            n.Show();
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
            Proffesionalitet g = new Proffesionalitet(log.Text, pass.Text);
            g.Show();
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //распечатать заявление и согласие на обработку данных
            var fileName = @"processingPersonalData.docx";
            var fileName1 = @"professionalitet.docx";

            var items = new Dictionary<string, string>
            {
                {"<FIO>", FIO.textBox.Text },
                {"<DATABIRTH>", ddsAas.textBox.Text },
                {"<PLACEBIRTH>", palaserod.textBox.Text },
                {"<SNILS>", FormStudy.textBox.Text },
                {"<ADDRES>", adrresjiv.textBox.Text },
                {"<TELEFON>", telefon.textBox.Text },
                {"<EMAIL>", emailvvodd.textBox.Text },
                {"<CITIZENSHIP>", grajd.textBox.Text },
                {"<PASS>", dokilich.textBox.Text },
                {"<SERIANOMER>", serianomer.textBox.Text },
                {"<KOGDAKEM>", kogdavdan.textBox.Text },
                {"<NAMESPECIALITY>", comboBox1.Text },
                {"<FORMA>", comboBox2.Text },
                {"<OSNOVANIE>", comboBox3.Text },
                {"<OBRAZOVANIE>", comboBox6.Text },
                {"<ISP>", OrigOrCopy_Copy1.textBox.Text },
                {"<BOLNOI>", combobox9.Text },
                {"<GOD>", okonch.textBox.Text },
                {"<SCHOOL>", school.textBox.Text },
                {"<DATAV>", datavdachi.textBox.Text },
                {"<SERIA>", FormPay.textBox.Text },
                {"<INLANG>", inlangs.textBox.Text },
                {"<POR>", vpolnpor.textBox.Text },
                {"<DOSTIG>", indivdost.textBox.Text },
                {"<OBSHAGA>", comboBox5.Text },
                {"<MOTHER>", mother.textBox.Text },
                {"<FATHER>", father.textBox.Text },
                {"<BEDACBASHKOY>", comboBox7.Text },
            };
            Process(fileName, items);

            var item = new Dictionary<string, string>
            {
                {"<FIO>", FIO.textBox.Text },
                {"<DATABIRTH>", ddsAas.textBox.Text },
                {"<PLACEBIRTH>", palaserod.textBox.Text },
                {"<SNILS>", FormStudy.textBox.Text },
                {"<ADDRES>", adrresjiv.textBox.Text },
                {"<TELEFON>", telefon.textBox.Text },
                {"<EMAIL>", emailvvodd.textBox.Text },
                {"<CITIZENSHIP>", grajd.textBox.Text },
                {"<PASS>", dokilich.textBox.Text },
                {"<SERIANOMER>", serianomer.textBox.Text },
                {"<KOGDAKEM>", kogdavdan.textBox.Text },
                {"<NAMESPECIALITY>", comboBox1.Text },
                {"<FORMA>", comboBox2.Text },
                {"<OSNOVANIE>", comboBox3.Text },
                {"<OBRAZOVANIE>", comboBox6.Text },
                {"<GOD>", okonch.textBox.Text },
                {"<SCHOOL>", school.textBox.Text },
                {"<DATAV>", datavdachi.textBox.Text },
                {"<SERIA>", FormPay.textBox.Text },
                {"<INLANG>", inlangs.textBox.Text },
                {"<POR>", vpolnpor.textBox.Text },
                {"<DOSTIG>", indivdost.textBox.Text },
                {"<OBSHAGA>", comboBox5.Text },
                {"<MOTHER>", mother.textBox.Text },
                {"<FATHER>", father.textBox.Text },
                {"<BEDACBASHKOY>", comboBox7.Text },
            };
            Process1(fileName1, item);
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
                SqlCommand cmd = new SqlCommand($@"insert into Postyplenie 
                                    (ФИО,Гражданство,Основание,Класс,Достижения,Специальность) 
                                    values 
                                    ('{FIO.textBox.Text}',
                                     '{grajd.textBox.Text}',
                                     '{comboBox3.Text}',
                                     '{comboBox6.Text}',
                                     '{indivdost.textBox.Text}',
                                     '{comboBox1.Text}')", con);
                reader = cmd.ExecuteReader();
                reader.Read();
                MessageBox.Show("Сохранено!");
          
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3(log.Text, pass.Text);
            window3.Show();
            this.Close();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            
        }

        private void comboBox11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void YearEnd_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void combobox9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FIO_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
