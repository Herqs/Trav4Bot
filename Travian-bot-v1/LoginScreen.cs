using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Travian_bot_v1
{
    public partial class LoginScreen : Form
    {
        ChromeDriverService service;
        TravianBrowser browser;
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void launchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("user-data-dir="+"Profiles/"+(server.Text+domain.Text+nameBox.Text).ToLower());
                
                string url = http.Text + server.Text + trav.Text + domain.Text + "/";
                browser = new TravianBrowser(service, options, nameBox.Text, passBox.Text, url);

                Thread t1 = new Thread(browser.LoginTravian);
                t1.Start();

                this.Hide();
                var TaskScreen = new TaskScreen(browser);
                TaskScreen.Owner = this;
                TaskScreen.Show();
                TaskScreen.Show();
            }
            catch(Exception ex)
            {
                this.Show();
                MessageBox.Show(ex.Message, "Error creating new browser class");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Testt();
        }

        void LoginScreenClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                browser.close();
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
