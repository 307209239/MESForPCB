using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            var client = new Greeter.GreeterClient(channel);
            var reply = client.SayHelloAsync(
                new HelloRequest { Name = textBox1.Text}).GetAwaiter().GetResult();
            MessageBox.Show("Greeting: " + reply.Message);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Create a new instance in code or add via the designer
            //Set the ChromiumWebBrowser.Address property to your Url if you use the designer.
            chromiumWebBrowser1.Load("http://localhost:5000/swagger/index.html");
            
        }
    }
}
