using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grpc.Core;
using CefSharp.WinForms;

namespace MES.WinformClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var channel = new Channel("localhost", 5001, ChannelCredentials.Insecure);
            var client = new User.UserClient(channel);
            var user = client.Login(new LoginModel(){UserName = "",Password = "",Domain = ""});
            if (user!=null)
            {
                UserInfo.User = user;
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Create a new instance in code or add via the designer
            //Set the ChromiumWebBrowser.Address property to your Url if you use the designer.
            chromiumWebBrowser1.Load("http://localhost:5000/swagger/index.html");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (UserInfo.User!=null)
            {
                

               
                    var channel = new Channel("localhost", 5001, ChannelCredentials.Insecure);
                    var client = new User.UserClient(channel);
                    var re = client.Logout(new LogoutModel());
                
                
            }
        }
    }
}
