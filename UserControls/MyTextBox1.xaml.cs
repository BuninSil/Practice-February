using System.Windows;
using System.Windows.Controls;

namespace DataGrid.UserControls
{
    public partial class MyTextBox1 : UserControl
    {
        public MyTextBox1()
        {
            InitializeComponent();
        }

        public string Hint1
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint1", typeof(string), typeof(MyTextBox1));


        public string Caption1
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption1", typeof(string), typeof(MyTextBox1));

    }
}

