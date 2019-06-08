using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace blogcreator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            Coding_options = new StackPanel();
            InitializeComponent();
        }
        private void Type_Checked(object sender, RoutedEventArgs e)
        {

        }

        const string Address_practice = "d:\\Users\\wdyyy\\Documents\\GitHub\\wdyyy.github.io\\_posts\\practices\\";
        const string Address_bioinfo = "d:\\Users\\wdyyy\\Documents\\GitHub\\wdyyy.github.io\\_posts\\bioinformatics\\";
        const string Address_blog = "d:\\Users\\wdyyy\\Documents\\GitHub\\wdyyy.github.io\\_posts\\blogs\\";
        private void Article_type_coding_radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            Coding_options.Visibility = Visibility.Visible;
        }
        private void Article_type_bio_radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            Coding_options.Visibility = Visibility.Collapsed;
            Article_Title_Textbox.Text = "";
            Article_excerpt_Textbox.Text = "";
            Article_Key_Textbox.Text = "";
            Article_Tags_Textbox.Text = "";

        }
        private void Article_type_blog_radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            Coding_options.Visibility = Visibility.Collapsed;
            Article_Title_Textbox.Text = "";
            Article_excerpt_Textbox.Text = "";
            Article_Key_Textbox.Text = "";
            Article_Tags_Textbox.Text = "";
        }

        //获取时间的函数
        private string Get_Date()
        {
            DateTime date = DateTime.Now;
            return date.ToString("yyyy-MM-dd");
        }
        //生成内容的函数
        private string Generate_content (string title, string date, string excerpt, string key, string tag)
        {
            string output = "";
            output = $"---\ntitle: {Article_Title_Textbox.Text}\ndate: {Get_Date()}\nkey: {Article_Key_Textbox.Text}\nexcerpt: {Article_excerpt_Textbox.Text}\ntag:\n- {Article_Tags_Textbox.Text}";
            if (Header_type_dark_radiobutton.IsChecked == true)
            {
                output += $"\nheader:\n  theme: dark\narticle_header:\n  theme: dark\n  background_image:\n    gradient: 'linear-gradient(135deg,rgba(0,0,0, .5),rgba(0,0,0, .5))'\n    src: \n---\n\n";
            }
            else if (Header_type_light_radiobutton.IsChecked == true)
            {
                output += $"\nheader:\n  theme: light\narticle_header:\n  theme: light\n  background_image:\n    gradient: 'linear-gradient(135deg,rgba(255,255,255, .5),rgba(255,255,255, .5))'\n    src: \n---\n\n";
            }
            else
            {
                output += "\n---\n\n";
            }
            int practices_count = 1;
            if (Article_type_coding_radiobutton.IsChecked == true)
            {
                if(int.TryParse(Coding_practices?.Text, out int parse_practice_count))
                {
                    practices_count = parse_practice_count;
                }
                else
                {
                    Coding_chapter.Text = Coding_chapter.Text.TrimEnd(Coding_chapter.Text.LastOrDefault());
                }
                output += $"# 练习{Coding_chapter.Text}\n";
                for (int i = 1; i <= practices_count; i++)
                {
                    output += $"## 练习{i}\n\n题目描述：\n\n程序代码：\n\n```cpp\n\n```\n\n";
                }
            }
            return output;
        }
        //创建文件并打开的函数
        private string CreatFile(string name, string file_URL, string content)
        {
            string path = file_URL + name;
            FileStream fsOBJ = new FileStream(path,FileMode.Create);
            StreamWriter file_writter = new StreamWriter(fsOBJ);
            file_writter.Write(content);
            file_writter.Flush();
            file_writter.Close();
            Process.Start(path);
            return $"------生成完毕------\n------文件地址------\n{file_URL}\n------文件名------\n{name}";
        }

        private void Generator_button_Click(object sender, RoutedEventArgs e)
        {
            string file_name = $"{Get_Date()}-{Article_Title_Textbox.Text}.md";
            string user_content = Generate_content(Article_Title_Textbox.Text, Get_Date(), Article_excerpt_Textbox.Text, Article_Key_Textbox.Text, Article_Tags_Textbox.Text);
            if (Article_type_coding_radiobutton.IsChecked == true)
            {
                Result_TextBlock.Text = CreatFile(file_name, Address_practice, user_content);
            }
            else if (Article_type_bio_radiobutton.IsChecked == true)
            {
                Result_TextBlock.Text = CreatFile(file_name, Address_bioinfo, user_content);
            }
            else if (Article_type_blog_radiobutton.IsChecked == true)
            {
                Result_TextBlock.Text = CreatFile(file_name, Address_blog, user_content);
            }
        }
        private void Coding_chapter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Article_Title_Textbox.Text = $"第{Coding_chapter.Text}章编程题";
            Article_excerpt_Textbox.Text = $"《c++ primer plut 6th》课后编程题第{Coding_chapter.Text}章";
            Article_Key_Textbox.Text = $"code_practice_Chapter{Coding_chapter.Text}";
            Article_Tags_Textbox.Text = "C++ Primer Plus";
        }
    }
}
