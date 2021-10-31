using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zL0gg3r
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customizeDesign()
        {
            panelMediaSubmenu.Visible = false;
        }

//        private void hideSubMenu()
//        {
//            if (panelMediaSubmenu.Visible == true)
//                _ = panelMediaSubmenu.Visible == false;
//       }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                // hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void panelSideMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Media_Click(object sender, EventArgs e)
        {

            openCholdForm(new Form3());
            //showSubMenu(panelMediaSubmenu);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openCholdForm(new Form2());

            // code

        }

        private Form activeForm = null;
        private void openCholdForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void panelPlayer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openCholdForm(new Form4());
            //showSubMenu(panelMediaSubmenu);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openCholdForm(new Form5());
            //showSubMenu(panelMediaSubmenu);
        }
    }
}
