using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace PythonSwitch
{
    public partial class Form1 : Form
    {
        private string path;
        private string[] splits;
        private int index;
        string curEvn;
        string transEvn;

        public Form1()
        {
            InitializeComponent();
            refreshForm();
        }

        private void refreshForm()
        {
            IDictionary environment = Environment.GetEnvironmentVariables();
            path = environment["Path"].ToString();
            splits = path.Split(';');

            string evn1 = ConfigurationManager.AppSettings["PythonEnv1"];
            string evn2 = ConfigurationManager.AppSettings["PythonEnv2"];

            for (int i = 0; i < splits.Length; i++)
            {
                if (splits[i].IndexOf("Python") >= 0)
                {
                    index = i;
                    break;
                }
            }
            curEvn = splits[index];

            transEvn = evn1;
            if (curEvn == evn1)            
                transEvn = evn2;        

            this.textBox1.Text = curEvn;
            this.textBox2.Text = transEvn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            splits[index] = transEvn;
            string path = string.Join(";",splits);
            Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine);
            MessageBox.Show("Python Environment Switched Successfully!");
            System.Environment.Exit(0);
        }
    }
}
