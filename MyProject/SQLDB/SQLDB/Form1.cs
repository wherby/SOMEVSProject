using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int count = 1000;
        private void classBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.classBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.testTaoDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testTaoDataSet.Class' table. You can move, or remove it, as needed.
            this.classTableAdapter.Fill(this.testTaoDataSet.Class);

        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {

        }

        private void classDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            
            //TestTaoDataSet.ClassDataTable a = this.classTableAdapter.GetData();
            //this.classTableAdapter.Insert("BB", count+a.Rows.Count);
            //a.AcceptChanges();
        }
    }
}
