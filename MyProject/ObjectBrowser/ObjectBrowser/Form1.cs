using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using System.IO;
namespace ObjectBrowser
{
    public partial class Form1 : Form
    {
        static Assembly assembly = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((label1.Text= openFileDialog1.FileName) != null)
                    {
                        assembly=Assembly.LoadFile(label1.Text);

                       // assembly = Assembly.Load(assembly2.GetName().FullName);
                        moduleName.Text = assembly.GetName().Name;

                        Type[] types = assembly.GetTypes();


                        foreach (Type temp in types)
                        {
                            moduleName.Text += "\r\n" + temp.FullName;
                            MethodInfo[] allmethod = temp.GetMethods();
                            foreach (MethodInfo temp2 in allmethod)
                            {
                                moduleName.Text += "\r\n" +"  "+ temp2.Name;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (methodName.Text != null)
            {
                Type type = assembly.GetType(methodName.Text);
                object runner = Activator.CreateInstance(type);
                MethodInfo mth = type.GetMethod("click");
                mth.Invoke(runner, null);
            }
        }
    }
}
