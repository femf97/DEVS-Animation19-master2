using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPaint
{
    public partial class BackgroundPopUp : Form
    {
        Document doc;
        public BackgroundPopUp(Document document)
        {
            InitializeComponent();
            doc = document;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            doc.setBgOption(1);
            Visible = false;
            doc.UpdateAllViews();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doc.setBgOption(2);
            Visible = false;
            doc.UpdateAllViews();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doc.setBgOption(3);
            Visible = false;
            doc.UpdateAllViews();
        }

        private void PopupClosing(object sender, FormClosingEventArgs e)
        {
            doc.UpdateAllViews();
        }
    }
}
