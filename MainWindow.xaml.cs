using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;



//Здесь будут отображаться все данные, которые занесены в бд.
//На вкладке заявление будут отображаться те, кто заполнил заявление(как я поняла на поступление или что-то типо того)
//На вкладке профессионалитет будут отображаться те, кто заполнил заявление именно на профессионалитет
//Ну и соотвественно у анкеты будут те, кто заполнял анкету





namespace DataGrid
{
    public partial class MainWindow : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public MainWindow(string userid, string password)
        {
            InitializeComponent();
            log.Text = userid;
            pass.Text = password;
            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            members.Add(new Member { Number = "1",Ball = "300", Name = "John Doe", Specialization = "Программист", Position = "9 класса" });


                con = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Database=practic;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Account where login=@userid and pass=@password", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@password", password);
                reader = cmd.ExecuteReader();
                reader.Read();
                name.Text = reader.GetString(3);
                reader.Close();



                string sqlQuery = "SELECT id as Number, ФИО AS Name, Гражданство AS Ball, Класс  as Position1, Основание as Specialization,Достижения as Position2, Специальность as Position  FROM Postyplenie ";


                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                // Привязка данных к DataGrid
                membersDataGrid.ItemsSource = dataTable.DefaultView;

            

            //это было типо для проверки отображения данных



        }


        //я не уверена нужен ли будет тебе этот класс если ты вставляешь данные из бд, если надо можешь удалить
        public class Member
        {
            public string Number { get; set; }
            public string Ball { get; set; }
            public string Name { get; set; }
            public string Specialization { get; set; }
            public string Position { get; set; }
            public string Position1 { get; set; }
            public string Position2 { get; set; }
        }
        

        //Это как я поняла для подстветки надписи или хз, но короче думаю лучше не удалять
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            //переход на вкладку анкеты
            Anketa n = new Anketa(log.Text, pass.Text);
            n.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Postyplenie postyplenie = new Postyplenie(log.Text, pass.Text);
            postyplenie.Show(); this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Proffesionalitet p = new Proffesionalitet(log.Text, pass.Text);
            p.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //выйти из системы или грубо говоря обратно переход на вкладку входа
            Login f = new Login();
            f.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(log.Text, pass.Text);
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //закрыть программу
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //при нажатии на эту кнопку в таблице должны отображатся данные из бд которые заполняли заявление 
            MainWindow mainWindow = new MainWindow(log.Text, pass.Text);
            mainWindow.Show();
            this.Close();

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //при нажатии на эту кнопку в таблице должны отображатся данные из бд которые заполняли заявление на профессионалитет
            MainWindow1 m1 = new MainWindow1(log.Text, pass.Text);
            m1.Show();
            this.Close();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //при нажатии на эту кнопку в таблице должны отображатся данные из бд которые заполняли анкету
            MainWindow2 mainWindow = new MainWindow2(log.Text, pass.Text);
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string sqlQuery = $"SELECT id as Number, ФИО AS Name, Гражданство AS Ball, Класс  as Position1, Основание as Specialization,Достижения as Position2 , Специальность as Position  FROM Postyplenie where ФИО='{textBoxFilter.Text}'";


            SqlCommand command = new SqlCommand(sqlQuery, con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            // Привязка данных к DataGrid
            membersDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)membersDataGrid.SelectedItem;
                string numberValue = row["Number"].ToString();
                
                // Используйте значение столбца "#" (Number) здесь по вашему усмотрению
                string sqlQuery = $"DELETE FROM Postyplenie WHERE id='{numberValue}'";


                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                // Привязка данных к DataGrid
                membersDataGrid.ItemsSource = dataTable.DefaultView;
            }

            string sqlQuery1 = "SELECT id as Number, ФИО AS Name, Гражданство AS Ball, Класс  as Position1, Основание as Specialization,Достижения as Position2 , Специальность as Position  FROM Postyplenie ";


            SqlCommand command1 = new SqlCommand(sqlQuery1, con);
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
            DataTable dataTable1 = new DataTable();

            adapter1.Fill(dataTable1);

            // Привязка данных к DataGrid
            membersDataGrid.ItemsSource = dataTable1.DefaultView;


        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            MainWindow3 mainWindow = new MainWindow3(log.Text, pass.Text);
            mainWindow.Show();
            this.Close();
        }
    }
   

}