using Microsoft.VisualBasic.ApplicationServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace PaintAppCase._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            this.Width = 950;
            this.Height = 700;
            bm = new Bitmap(pic.Width, pic.Height);
            g=Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;

        }

        Bitmap bm;
        Graphics g;
        bool paint=false; 
        Point px, py;
        Pen p= new Pen(Color.Black,1);
        Pen erase= new Pen(Color.White,10);
        int index;
        int x,y,sX,sY,cX,cY;
        ColorDialog cd=new ColorDialog();
        Color new_color;


        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint=true;
            py= e.Location;

            cX = e.X;
            cY = e.Y;
        }

      

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {

            if (paint) 
            {
                if (index==1) 
                {
                    px = e.Location;
                    g.DrawLine(p,px,py);
                    py= px;
                }
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py);
                    py = px;
                }

            }
            pic.Refresh();

            x = e.X; 
            y=e.Y;
            sX = e.X-cX;
            sY = e.Y-cY;
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            if(paint)

            {
                if (index == 3)
                {
                    g.FillEllipse(p.Brush, cX, cY, sX, sY);
                   
               
                }
                if (index == 4)
                {
                    g.FillRectangle(p.Brush, cX, cY, sX, sY);
                 
                }
                if (index == 5)
                {
                    //g.DrawPolygon(p, cX, cY, sX, sY);
                    g.FillPolygon(p.Brush, new PointF[] { new PointF(cX, cY), new PointF(x-cX, y), new PointF(x, y) });
                    //Console.WriteLine("a");
                }
                if (index == 6)
                {
                }
            }
        }

       

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {

            paint=false;

            sX=x-cX;
            sY=y-cY;

            if(index==3)
            {
                g.FillEllipse(p.Brush,cX,cY,sX,sY);
            }
            if(index==4) 
            {
                g.FillRectangle (p.Brush, cX, cY, sX, sY);
            }
            if (index == 5)
            {
                g.FillPolygon(p.Brush, new PointF[] { new PointF(cX, cY), new PointF(x - cX, y), new PointF(x, y) });
            }
            if (index == 6)
            {
                //g.FillPolygon(p.Brush, new PointF[] { new PointF(cX, cY), new PointF(x - cX, y), new PointF(x, y) });
            }

        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color=cd.Color;
            pic_color.BackColor=cd.Color;
            p.Color = cd.Color;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var sfd=new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*" ;
            if(sfd.ShowDialog()==DialogResult.OK)
            {
             Bitmap btm = bm.Clone(new Rectangle(0, 0, pic.Width, pic.Height),bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void Triangle_Click(object sender, EventArgs e)
        {
            index = 5;
           // g.FillPolygon(p.Brush, new Point[] { new Point(150, 100), new Point(100, 200), new Point(200, 200) });

        }

        private void pencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }
        private void Circle_Click(object sender, EventArgs e)
        {
            index= 3;
        }
        private void Square_Click(object sender, EventArgs e)
        {
            index= 4;
        }
        private void SixA_Click(object sender, EventArgs e)
        {
            //index = 6;

            var x_0 = panel1.Width / 2;
            var y_0 = panel1.Height / 2;
            var shape = new PointF[6];

            var r = 70; //70 px radius 

            //Create 6 points
            for (int a = 0; a < 6; a++)
            {
                shape[a] = new PointF(
                    x_0 + r * (float)Math.Cos(a * 60 * Math.PI / 180f),
                    y_0 + r * (float)Math.Sin(a * 60 * Math.PI / 180f));
            }

            g.FillPolygon(p.Brush, shape);
        }
        private void Clean_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            index= 0;
        }
       
}
}