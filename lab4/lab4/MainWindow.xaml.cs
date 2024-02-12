using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowResult(string text)
        {
            result.Visibility = Visibility.Visible;
            result.Text = text;
        }

        private void replace_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string from = from_tb.Text;
                string to = to_tb.Text;
                string text = text_tb.Text;
                if (text != "")
                {

                    text = text.Replace(from, to);

                    ShowResult(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void fix_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = text_tb.Text;
                if (text != "")
                {
                    var replace_up = new Regex((@"(^|\n|(^|\n)(—|-)\s|(\.|!|\?)\s)([A-Z]|[А-Я])"), RegexOptions.IgnoreCase);
                    text = replace_up.Replace(text, m => m.ToString().ToUpper());
                    ShowResult(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static bool IsPalindrome(string word)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            string reversedWord = new string(charArray);

            return word.Equals(reversedWord, StringComparison.OrdinalIgnoreCase);
        }
        static double EvaluateExpression(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);

            DataRow row = table.NewRow();
            table.Rows.Add(row);

            return double.Parse((string)row["expression"]);
        }

        private void palindromes_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = text_tb.Text;
                if (text != "")
                {
                    List<string> palindromes = new List<string>();
                    string[] words = text.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        if (word.Length > 1 && IsPalindrome(word))
                        {
                            palindromes.Add(word);
                        }
                    }

                    string result = string.Join(", ", palindromes);
                    ShowResult(result);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void count_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = text_tb.Text;
                if (text != "")
                {
                    //string[] words = text.Split(new[] { ' ', ',', '.', '!', '?' });
                    string[] words = text.Split(' ');
                    int res = words.Count();
                    ShowResult(res.ToString() + " word(s)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void exp_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = text_tb.Text;
                if (text != "")
                {
                    double result = EvaluateExpression(text);
                    ShowResult("Result = " + result.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void enc_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = text_tb.Text;
                if (text != "")
                {
                    string res = EncodeText(text);
                    ShowResult(res);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dec_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = text_tb.Text;
                if (text != "")
                {
                    string res = DecodeText(text);
                    ShowResult(res);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static string EncodeText(string text)
        {
            StringBuilder encoded = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                byte ost = (byte)(i % 3);
                char encodedChar = (char)(c + ost + 1);
                encoded.Append(encodedChar);
            }
            return encoded.ToString();
        }

        static string DecodeText(string text)
        {
            StringBuilder decoded = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                byte ost = (byte)(i % 3);
                char symb = (char)(c - ost - 1);
                decoded.Append(symb);
            }
            return decoded.ToString();
        }
    }
}
