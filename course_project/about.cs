using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenGL;
using SharpGL;
namespace course_project
{
    public partial class about : MetroFramework.Forms.MetroForm
    {
        public about()
        {
            InitializeComponent();
            gl = this.openGLControl1.OpenGL;
            timer1.Start();
        }

        private void about_Load(object sender, EventArgs e)
        {

        }

        static double xrot = 0.0;
        static double yrot = 0.0;
        static double zrot = 0.0;
        SharpGL.OpenGL gl;
        private void timer1_Tick(object sender, EventArgs e)
        {

            gl.Rotate(xrot, 1, 0, 0);
            gl.Rotate(yrot, 0, 1, 0);
            gl.Rotate(zrot, 0, 0, 1);
            xrot = xrot + 0.11;
            yrot = yrot + 0.11;
            zrot = zrot + 0.11;
        }
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs e)
        {
            // 创建一个GL对象


            gl.Clear(OpenGL.Gl.COLOR_BUFFER_BIT | OpenGL.Gl.DEPTH_BUFFER_BIT);    // 清空屏幕
            gl.ClearColor(1.0f, 1.0f, 1.0f,0);
            gl.LoadIdentity();                    // 重置
            gl.Translate(0.0f, 0.0f, -6.0f);    // 设置坐标，距离屏幕距离为6

            gl.Rotate(xrot, 1.0f, 0.0f, 0.0f);    // 绕X轴旋转
            gl.Rotate(yrot, 0.0f, 1.0f, 0.0f);    // 绕Y轴旋转
            gl.Rotate(zrot, 0.0f, 0.0f, 1.0f);    // 绕Z轴旋转
            gl.Begin(Gl.QUADS);
            gl.Color(1.0, 1.0, 0.0);
            gl.Vertex(1.0, 1.0, -1.0);
            gl.Color(0.0, 1.0, 0.0);
            gl.Vertex(-1.0, 1.0, -1.0);
            gl.Color(0.0, 1.0, 1.0);
            gl.Vertex(-1.0, 1.0, 1.0);
            gl.Color(1.0, 1.0, 1.0);
            gl.Vertex(1.0, 1.0, 1.0);

            gl.Color(1.0, 0.0, 1.0);
            gl.Vertex(1.0, -1.0, 1.0);
            gl.Color(0.0, 0.0, 1.0);
            gl.Vertex(-1.0, -1.0, 1.0);
            gl.Color(0.0, 0.0, 0.0);
            gl.Vertex(-1.0, -1.0, -1.0);
            gl.Color(1.0, 0.0, 0.0);
            gl.Vertex(1.0, -1.0, -1.0);

            gl.Color(1.0, 1.0, 1.0);
            gl.Vertex(1.0, 1.0, 1.0);
            gl.Color(0.0, 1.0, 1.0);
            gl.Vertex(-1.0, 1.0, 1.0);
            gl.Color(0.0, 0.0, 1.0);
            gl.Vertex(-1.0, -1.0, 1.0);
            gl.Color(1.0, 0.0, 1.0);
            gl.Vertex(1.0, -1.0, 1.0);

            gl.Color(1.0, 0.0, 0.0);
            gl.Vertex(1.0, -1.0, -1.0);
            gl.Color(0.0, 0.0, 0.0);
            gl.Vertex(-1.0, -1.0, -1.0);
            gl.Color(0.0, 1.0, 0.0);
            gl.Vertex(-1.0, 1.0, -1.0);
            gl.Color(1.0, 1.0, 0.0);
            gl.Vertex(1.0, 1.0, -1.0);

            gl.Color(0.0, 1.0, 1.0);
            gl.Vertex(-1.0, 1.0, 1.0);
            gl.Color(0.0, 1.0, 0.0);
            gl.Vertex(-1.0, 1.0, -1.0);
            gl.Color(0.0, 0.0, 0.0);
            gl.Vertex(-1.0, -1.0, -1.0);
            gl.Color(0.0, 0.0, 1.0);
            gl.Vertex(-1.0, -1.0, 1.0);

            gl.Color(1.0, 1.0, 0.0);
            gl.Vertex(1.0, 1.0, -1.0);
            gl.Color(1.0, 1.0, 1.0);
            gl.Vertex(1.0, 1.0, 1.0);
            gl.Color(1.0, 0.0, 1.0);
            gl.Vertex(1.0, -1.0, 1.0);
            gl.Color(1.0, 0.0, 0.0);
            gl.Vertex(1.0, -1.0, -1.0);
            gl.End();


        }
    }
}
