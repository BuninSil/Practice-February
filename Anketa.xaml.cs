using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;
using System;
using Xceed.Words.NET;

namespace DataGrid
{

    public partial class Anketa : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Anketa(string userid, string password)
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

        //Это как я поняла для подстветки надписи или хз,
        //но короче не удалять
        //(пиздить надо уметь, а я не особо,
        //соу удачи писать код к этой параше)
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
   

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(log.Text, pass.Text); 
            mainWindow.Show();
            this.Close();
            //переход на вкладку заявления, где будут отображаться данные из бд
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Anketa anketa = new Anketa(log.Text, pass.Text);
            anketa.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Postyplenie postyplenie = new Postyplenie(log.Text, pass.Text);
            postyplenie.Show(); this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           Proffesionalitet pro = new Proffesionalitet(log.Text, pass.Text);
            pro.Show(); this.Close();   
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Close();
            //выйти из системы типо
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //ну тут концепция такая, что жмешь на кнопку и открывается вордовский файл и там уже в ворде жмешь на распечатать
            var fileName = @"Anketa1.docx";
            var items = new Dictionary<string, string>
            {
                {"<KOD>", KodSpeciality.textBox.Text },
                {"<BUDJ_OR_HOZ>", FormPay.textBox.Text },
                {"<SHIFR>", ShifrSpeciality.textBox.Text },
                {"<FOTO>", Photo.textBox.Text },
                {"<O_OR_Z>", FormStudy.textBox.Text },
                {"<FIO>", FIO.textBox.Text },
                {"<SCHOOL>", NameSchool.textBox.Text },
                {"<DATAKONZA>", YearEnd.textBox.Text },
                {"<AGE>", Age.textBox.Text },
                {"<INLANG>", Language.textBox.Text },
                {"<SREDNBAL>", Ball.textBox.Text },
                {"<SN>", Attestat.textBox.Text },
                {"<ORIG_OR_KOPI>", OrigOrCopy.textBox.Text },
                {"<NAMESPECIALITY>", NameSpeciality.textBox.Text },
            };
            Process(fileName, items);
        }
        internal void Process(string fileName, Dictionary<string, string> items)
        {
            try
            {
                string executableLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string filePath = Path.Combine(executableLocation, fileName);

                using (DocX document = DocX.Load(filePath))
                {
                    foreach (var item in items)
                    {
                        document.ReplaceText(item.Key, item.Value);
                    }

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string newFileName = Path.Combine(desktopPath, Path.GetFileNameWithoutExtension(fileName) + "_modified.docx"); // Новый путь для сохранения на рабочем столе

                    document.SaveAs(newFileName);

                    MessageBox.Show("Файл успешно сохранен на рабочем столе с новым именем: " + Path.GetFileName(newFileName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            
                con = new SqlConnection(@"Database=practic;Data Source=localhost\SQLEXPRESS;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand($@"insert into anketa 
                                    (Код, Шифр, Наименование, Оплата, Обучение, Фото, ФИО, Школа, Год_окончания, Изучаемый_язык, Возраст, Средний_балл, Серия_номер_аттестата, [Оригинал_копия]) 
                                    values 
                                    ('{KodSpeciality.textBox.Text}',
                                     '{ShifrSpeciality.textBox.Text}',
                                     '{NameSpeciality.textBox.Text}',
                                     '{FormPay.textBox.Text}',
                                     '{FormStudy.textBox.Text}',
                                     '{Photo.textBox.Text}',
                                     '{FIO.textBox.Text}',
                                     '{NameSchool.textBox.Text}',
                                     '{YearEnd.textBox.Text}',
                                     '{Language.textBox.Text}',
                                     '{Age.textBox.Text}',
                                     '{Ball.textBox.Text}',
                                     '{Attestat.textBox.Text}',
                                     '{OrigOrCopy.textBox.Text}')", con);
                reader = cmd.ExecuteReader();
                reader.Read();
                MessageBox.Show("Сохранено!");
  
            //Сохранить данные в бд
            //На самом деле я немного хз как это реализовать, потому что как с поискам в заявках(string Search = textBoxFilter.Text.Trim()) он не хочет

        }

        private void KodSpeciality_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NameSchool_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
