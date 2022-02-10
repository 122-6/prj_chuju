using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prj_chuju
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.BackColor = Color.White;
            this.Button2.BackColor = Color.FromArgb(1,245,245,245);
            this.Button3.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button4.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button5.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button6.BackColor = Color.FromArgb(1, 245, 245, 245);

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.Panel5.Visible = false;
            this.Panel6.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Button1.BackColor = Color.White;
            this.Button2.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button3.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button4.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button5.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button6.BackColor = Color.FromArgb(1, 245, 245, 245);

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.Panel5.Visible = false;
            this.Panel6.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Button1.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button2.BackColor = Color.White;
            this.Button3.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button4.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button5.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button6.BackColor = Color.FromArgb(1, 245, 245, 245);

            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.Panel5.Visible = false;
            this.Panel6.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            this.Button1.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button2.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button3.BackColor = Color.White;
            this.Button4.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button5.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button6.BackColor = Color.FromArgb(1, 245, 245, 245);

            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = true;
            this.Panel4.Visible = false;
            this.Panel5.Visible = false;
            this.Panel6.Visible = false;
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            this.Button1.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button2.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button3.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button4.BackColor = Color.White;
            this.Button5.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button6.BackColor = Color.FromArgb(1, 245, 245, 245);

            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = true;
            this.Panel5.Visible = false;
            this.Panel6.Visible = false;
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            this.Button1.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button2.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button3.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button4.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button5.BackColor = Color.White;
            this.Button6.BackColor = Color.FromArgb(1, 245, 245, 245);

            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.Panel5.Visible = true;
            this.Panel6.Visible = false;
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            this.Button1.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button2.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button3.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button4.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button5.BackColor = Color.FromArgb(1, 245, 245, 245);
            this.Button6.BackColor = Color.White;

            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.Panel5.Visible = false;
            this.Panel6.Visible = true;
        }
    }
}