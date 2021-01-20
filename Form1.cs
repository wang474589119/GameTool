using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && (e.KeyChar != 8) && (e.KeyChar != 46))
                e.Handled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int iMax = 20;
            if (textBox1.Text != null && textBox1.Text != "")
            {
                if (int.Parse(textBox1.Text) > iMax)
                {
                    textBox1.Text = iMax.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = getTextBoxCount(groupBox2.Controls);
            if (count >= 35)
            {
                MessageBox.Show("最大支持35个兵装");
            }
            TextBox textBox = new TextBox();
            textBox.Text = "0";
            textBox.Name = "mbox" + count;
            textBox.Size = new Size(23, 21);
            textBox.Location = new Point(53 + 80 * (count / 7), 43 + 30 * (count % 7));

            Label label = new Label();
            label.Text = "顺序" + (count + 1);
            label.Name = "mlable" + count;
            label.AutoSize = true;
            label.Location = new Point(6 + 80 * (count / 7), 46 + 30 * (count % 7));
            groupBox2.Controls.Add(label);
            groupBox2.Controls.Add(textBox);
        }
        private int getTextBoxCount(Control.ControlCollection controlCollection)
        {
            int count = 0;
            foreach (var item in controlCollection)
            {
                if (item is TextBox)
                {
                    count++;
                }
            }
            return count;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int iMax = 20;
            if (textBox2.Text != null && textBox2.Text != "")
            {
                if (int.Parse(textBox2.Text) > iMax)
                {
                    textBox2.Text = iMax.ToString();
                }
            }
        }
    }
}
