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

namespace AliyunLiveGenerate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string blive = "liveb.tiger8.com.cn";
        string plive = "live.tiger8.com.cn";

        string bkey = ""; //pull key
        string pkey = ""; //push key

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          var result=  LiveGenerate.generate_push_url(plive, pkey);

            var pulresult = LiveGenerate.general_pull_url(blive, bkey);

            this.pushText.Text = result;

            this.pullText.Text = pulresult;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.pushText.Text = "";

            this.pullText.Text = "";
        }
    }
}
