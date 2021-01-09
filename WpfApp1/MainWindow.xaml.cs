using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void metroButton19_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog1 = new SaveFileDialog();
            dialog1.Filter = "Все изображения (*.JPG;*.PNG;*.JPEG;*.PNG)|*.JPG;*.PNG;*.JPEG;*.PNG";
            dialog1.Title = "Выбор файла...";
            if (dialog1.ShowDialog() == true)
            {
                File.WriteAllText(dialog1.FileName, Save.Text);
            }
        }
        static string home = "";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            try
            {
                int tmp = rand.Next() % 100000;
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(Save.Text, "images/" + (tmp).ToString() + ".jpg");
                }
                MessageBox.Show("Картинка добавлена", "Картинка добавлена", MessageBoxButton.OK);
                var t = new Uri(Save.Text);
                home = "images/" + (tmp).ToString() + ".jpg";
                Img.Source = new BitmapImage(t); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Message, MessageBoxButton.OK);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string attachFile = home;
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("pavelocheretyany2001@gmail.com", "Pavlo");
            // кому отправляем
            MailAddress to = new MailAddress(Mail.Text);
            // создаем объект сообщения
            MailMessage mail = new MailMessage(from, to);
            // тема письма
            mail.Subject = "Here your image!";
            // текст письма
            mail.Body = category.Text;
            // письмо представляет код html
            mail.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            if (!string.IsNullOrEmpty(attachFile))
            {
                mail.Attachments.Add(new Attachment(attachFile));
            }
            // логин и пароль
            smtp.Credentials = new NetworkCredential("pavelocheretyany2001@gmail.com", "****************");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            mail.Dispose();
        }
    }
}
// https://support.google.com/mail/answer/7126229?hl=ru
// https://overcoder.net/q/170686/%D0%BE%D1%82%D0%B2%D0%B5%D1%82-%D1%81%D0%B5%D1%80%D0%B2%D0%B5%D1%80%D0%B0-%D0%B1%D1%8B%D0%BB-570-%D1%81%D0%BD%D0%B0%D1%87%D0%B0%D0%BB%D0%B0-%D0%BD%D0%B5%D0%BE%D0%B1%D1%85%D0%BE%D0%B4%D0%B8%D0%BC%D0%BE-%D0%B2%D1%8B%D0%BF%D0%BE%D0%BB%D0%BD%D0%B8%D1%82%D1%8C-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4%D1%83-starttls