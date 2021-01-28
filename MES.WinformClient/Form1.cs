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
    }
}
