using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course_project
{
    public partial class manage : MetroFramework.Forms.MetroForm
    {
        string id, name;
        public manage(string id,string name)
        {
            this.id = id;
            this.name = name;
            InitializeComponent();
        }

     

        private void manage_Load(object sender, EventArgs e)
        {
            this.Text += name;
        }

        private void 新建账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new_account nac = new new_account();
            nac.MdiParent = this;
            nac.WindowState = FormWindowState.Maximized;
            nac.Dock = DockStyle.Fill;
            nac.Show();
        }

        private void 更改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pwd ch = new change_pwd(id, "manage");
            ch.MdiParent = this;
            ch.WindowState = FormWindowState.Maximized;
            ch.Dock = DockStyle.Fill;
            ch.Show();
        }

        private void 删除账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_account del = new delete_account();
            del.MdiParent = this;
            del.WindowState = FormWindowState.Maximized;
            del.Dock = DockStyle.Fill;
            del.Show();
        }

        private void 重置密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pwd_recover p = new pwd_recover();
            p.MdiParent = this;
            p.WindowState = FormWindowState.Maximized;
            p.Dock = DockStyle.Fill;
            p.Show();
        }

        private void 按条件生成图表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            analysis a = new analysis();
            a.MdiParent = this;
            a.WindowState = FormWindowState.Maximized;
            a.Dock = DockStyle.Fill;
            a.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.Show();
        }

        private void metroUserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
