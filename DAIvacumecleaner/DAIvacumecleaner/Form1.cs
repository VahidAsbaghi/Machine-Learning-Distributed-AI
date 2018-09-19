using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DAIvacumecleaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Panel[,] pa = new Panel[100, 100];
        int[,] mat = new int[100, 100];
        int[, ,] mro1 = new int[100, 9, 9];
        Panel alert = new Panel();
        Button bu = new Button();
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        void garbage_loc(int t)
        {
            int ob = Int32.Parse(textBox4.Text);
            int ro = Int32.Parse(textBox3.Text);
            int ga = Int32.Parse(textBox2.Text);
            int[] ro1 = new int[ro + ob + ga];
            int[] ro2 = new int[ro + ob + ga];
            for (int k = 0; k < ro + ga + ob; k++)
            {
                ro1[k] = 0;
                ro2[k] = 0;
            }
            int temp = 0;
            int i = 0;
            Random ra = new Random();
            while (i < ro + ob + ga)
            {
                temp = 0;
                int ro3 = ra.Next(0, t);
                int ro4 = ra.Next(0, t);
                ro1[i] = ro3;
                ro2[i] = ro4;
                if (i != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (ro1[j] != ro3 || ro2[j] != ro4)
                            temp++;
                    }
                }
                if (temp == i)
                {
                   
                    if (i < ro)
                    {
                        pa[ro3, ro4].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+@"\pic\robot4.jpg");// (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic/robot4.jpg");
                        pa[ro3, ro4].BackgroundImageLayout = ImageLayout.Zoom;
                        mat[ro3, ro4] = Int32.Parse('3' + i.ToString() + '4');
                    }
                    else if (i < ro + ga)
                    {
                        pa[ro3, ro4].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +@"\pic\i3.jpg");
                        pa[ro3, ro4].BackgroundImageLayout = ImageLayout.Zoom;
                        mat[ro3, ro4] = 1;
                    }
                    else if (i < ro + ga + ob)
                    {
                        pa[ro3, ro4].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                        pa[ro3, ro4].BackgroundImageLayout = ImageLayout.Zoom;
                        mat[ro3, ro4] = 2;
                    }
                    ++i;
                }


            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            int n = Int32.Parse(textBox1.Text);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Point po = new Point(i * 420 / n, j * 420 / n);
                    pa[i, j] = new Panel();
                    pa[i, j].BorderStyle = BorderStyle.FixedSingle;
                    pa[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    Size sz = new Size(420 / n, 420 / n);
                    pa[i, j].Size = sz;
                    pa[i, j].Location = po;
                    mat[i, j] = 0;
                }
            for (int k = 0; k < n; k++)
                for (int j = 0; j < n; j++)
                {
                    panel1.Controls.Add(pa[k, j]);
                }
             garbage_loc(n);

        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(textBox1.Text);
            int ro = Int32.Parse(textBox3.Text);

            for (int k = 0; k < ro; k++)
                for (int m = 0; m < 9; m++)
                    for (int l = 0; l < 9; l++)
                    {
                        mro1[k, m, l] = -1;

                    }

            int[] loc_ro_x = new int[ro];
            int[] loc_ro_y = new int[ro];
            string ss = "";
            for (int p = 0; p < ro; p++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        ss = mat[i, j].ToString();
                        ss = ss.Substring(0, ss.Length - 1);
                        if (ss == '3' + p.ToString())
                        {
                            loc_ro_x[p] = i;
                            loc_ro_y[p] = j;
                        }

                    }
                }
            }

            for (int r = 0; r < ro; r++)//map kardan ba tavajoh be shomareie har robat dar iek jadvale 9*9
            {
                for (int k = 0; k < 5; k++)
                {
                    for (int l = 0; l < 5 - k; l++)
                    {
                        if ((loc_ro_x[r] + k) < n && (loc_ro_y[r] - l) >= 0)
                        {
                            mro1[r, k + 4, 4 - l] = mat[loc_ro_x[r] + k, loc_ro_y[r] - l];
                        }
                        if ((loc_ro_x[r] - k) >= 0 && (loc_ro_y[r] - l) >= 0)
                            mro1[r, 4 - k, 4 - l] = mat[loc_ro_x[r] - k, loc_ro_y[r] - l];
                        if ((loc_ro_x[r] + k) < n && (loc_ro_y[r] + l) < n)
                            mro1[r, 4 + k, 4 + l] = mat[loc_ro_x[r] + k, loc_ro_y[r] + l];
                        if ((loc_ro_x[r] - k) >= 0 && (loc_ro_y[r] + l) < n)
                            mro1[r, 4 - k, 4 + l] = mat[loc_ro_x[r] - k, loc_ro_y[r] + l];

                    }
                }
            }
           
            do_move();
        }
        void do_move()
        {
            int n = Int32.Parse(textBox1.Text);
            int[] move = new int[3];
            
            move = best_value();
            
            int ii = 0, jj = 0, rr = 0,ip=0,jp=0;
            rr = move[0];
            ii = move[1];
            jj = move[2];
            string sss="";
            for(int i=0; i<n; i++)
                for (int j = 0; j < n; j++)
                {
                    sss = mat[i, j].ToString();
                    if (sss.StartsWith("3"))
                    {
                        
                        sss = sss.Substring(1, sss.Length - 2);
                        if (Int32.Parse(sss) == rr)
                        {
                            ip = i;
                            jp = j;
                        }
                    }
                }
          
            pa[ip, jp].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
            mat[ip,jp]=0;

                if (ii == 0 && jj == 1)
                {
                    pa[ip + ii, jp + jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\robot4.jpg");
                    mat[ip + ii, jp + jj] = Int32.Parse('3' + rr.ToString() + '4');
                }
                else if (ii == 1 && jj == 0)
                {
                    pa[ip + ii, jp + jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\robot1.jpg");
                    mat[ip + ii, jp + jj] = Int32.Parse('3' + rr.ToString() + '1');
                }
                else if (ii == -1 && jj == 0)
                {
                    pa[ip + ii, jp + jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\robot3.jpg");
                    mat[ip + ii, jp + jj] = Int32.Parse('3' + rr.ToString() + '3');
                }
                else if (ii == 0 && jj == -1)
                {
                    pa[ip + ii, jp + jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\robot2.jpg");
                    mat[ip + ii, jp + jj] = Int32.Parse('3' + rr.ToString() + '2');
                }
           
        }

        int[] best_value()
        {
             
            int ro=Int32.Parse(textBox3.Text);
            double[,,] state_val = new double[ro,9, 9];
            Stack<int> s = new Stack<int>();
            Stack<double> s1=new Stack<double>();
            Stack<int> isave = new Stack<int>();
            Stack<int> jsave = new Stack<int>();
            int ip=0,jp=0,temp=0;
            double temp1=0,gama=0.9;
            int[,,] sign=new int[ro,9,9];
            for (int l = 0; l < ro; l++)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        sign[l, i, j] = 0;
                        if (mro1[l, i, j] == 1)
                            state_val[l, i, j] = 100;
                        else
                            state_val[l, i, j] = 0;
                    }
            }
            double maxi = 0;
            for (int l = 0; l < ro; l++)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (mro1[l, i, j] == 1)
                        {
                            s.Push(mro1[l, i, j]);
                            s1.Push(100);
                            isave.Push(i);
                            jsave.Push(j);
                        }
                        while (s.Count() != 0)
                        {
                            ip = isave.Pop();
                            jp = jsave.Pop();
                            temp = s.Pop();
                            temp1 = s1.Pop();
                            if (ip < 8)
                            {
                                if (mro1[l, ip + 1, jp] == 0)
                                {

                                    if ((gama * temp1) > state_val[l, ip + 1, jp])
                                    {
                                        s.Push(mro1[l, ip + 1, jp]);
                                        s1.Push(gama * temp1);
                                        isave.Push(ip + 1);
                                        jsave.Push(jp);
                                        state_val[l, ip + 1, jp] = gama * temp1;
                                    }
                                }
                            }
                            if (ip > 0)
                            {
                                if (mro1[l, ip - 1, jp] == 0 )
                                {
                                    if ((gama * temp1) > state_val[l, ip - 1, jp])
                                    {
                                        s.Push(mro1[l, ip - 1, jp]);
                                        s1.Push(gama * temp1);
                                        isave.Push(ip - 1);
                                        jsave.Push(jp);
                                        state_val[l, ip - 1, jp] = gama * temp1;
                                    }
                                }
                            }
                            if (jp < 8)
                            {
                                if (mro1[l, ip, jp + 1] == 0 )
                                {
                                    if ((gama * temp1) > state_val[l, ip, jp + 1])
                                    {
                                        s1.Push(gama * temp1);
                                        s.Push(mro1[l, ip, jp + 1]);
                                        isave.Push(ip);
                                        jsave.Push(jp + 1);
                                        state_val[l, ip, jp + 1] = gama * temp1;
                                    }
                                }
                            }
                            if (jp > 0)
                            {
                                if (mro1[l, ip, jp - 1] == 0)
                                {
                                    if ((gama * temp1) > state_val[l, ip, jp - 1])
                                    {
                                        s.Push(mro1[l, ip, jp - 1]);
                                        s1.Push(gama * temp1);
                                        isave.Push(ip);
                                        jsave.Push(jp - 1);
                                        state_val[l, ip, jp - 1] = gama * temp1;
                                    }
                                }
                            }
                        }
                    }
                }
                if (state_val[l, 5, 4] > maxi)
                    maxi = state_val[l, 5, 4];
                if (state_val[l, 4, 5] > maxi)
                    maxi = state_val[l, 4, 5];
                if (state_val[l, 3, 4] > maxi)
                    maxi = state_val[l, 3, 4];
                if (state_val[l, 4, 3] > maxi)
                    maxi = state_val[l, 4, 3];
                state_val[l, 4, 4] = gama * maxi;
            }
          
            double max=0;
            int rr = 0;
            int r = 0;
            ip = 0;
            jp = 0;
            for (r = 0; r < ro; r++)
            {
                if (state_val[r, 4, 4] > max)
                {
                    max = state_val[r, 4, 4];
                    rr = r;
                }
            }
            max = 0;
            if (state_val[rr, 5, 4] > max)
            {
                max = state_val[rr, 5, 4];
                ip = 1;
                jp = 0;
            }
            if (state_val[rr, 4, 5] > max)
            {
                max = state_val[rr, 4, 5];
                ip = 0;
                jp = 1;
            }
            if (state_val[rr, 3, 4] > max)
            {
                max = state_val[rr, 3, 4];
                ip = -1;
                jp = 0;
            }
            if (state_val[rr, 4, 3] > max)
            {
                max = state_val[rr, 4, 3];
                ip = 0;
                jp = -1;
            }
            for (int l = 0; l < ro; l++)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        state_val[l, i, j] = 0;
                    }
            }
            int[] ret=new int[3];
            ret[0]=rr;
            ret[1]=ip;
            ret[2]=jp;
            return ret;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int abs = Int32.Parse(textBox4.Text);
            int n=Int32.Parse(textBox1.Text);
            int count=0,ii=0,jj=0;
            Random rand = new Random();
            int ra = rand.Next(0, abs-1);
            for (int i = 0; i < n; i++)
            {
                if ((count-1) == ra)
                    break;
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] == 2)
                        count++;
                    if ((count-1) == ra)
                    {
                        ii = i;
                        jj = j;
                        break;
                    }
                }
            }
            int raa = rand.Next(1, 6);
            if (raa == 1 || raa == 2)
            {
                if ((ii + 1) < n && mat[ii + 1, jj] == 0)
                {
                    mat[ii + 1, jj] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii + 1, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((ii - 1) >= 0 && mat[ii - 1, jj] == 0)
                {
                    mat[ii - 1, jj] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii - 1, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((jj - 1) >= 0 && mat[ii, jj - 1] == 0)
                {
                    mat[ii, jj - 1] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii, jj - 1].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((jj + 1) < n && mat[ii, jj + 1] == 0)
                {
                    mat[ii, jj + 1] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii, jj + 1].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
            }
            else if (raa==3||raa==4)
            {
                if ((jj - 1) >= 0 && mat[ii, jj - 1] == 0)
                {
                    mat[ii, jj - 1] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii, jj - 1].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((jj + 1) < n && mat[ii, jj + 1] == 0)
                {
                    mat[ii, jj + 1] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii, jj + 1].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((ii + 1) < n && mat[ii + 1, jj] == 0)
                {
                    mat[ii + 1, jj] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii + 1, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((ii - 1) >= 0 && mat[ii - 1, jj] == 0)
                {
                    mat[ii - 1, jj] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii - 1, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                
            }
            else
            {
                if ((jj + 1) < n && mat[ii, jj + 1] == 0)
                {
                    mat[ii, jj + 1] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii, jj + 1].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((ii - 1) >= 0 && mat[ii - 1, jj] == 0)
                {
                    mat[ii - 1, jj] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii - 1, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((jj - 1) >= 0 && mat[ii, jj - 1] == 0)
                {
                    mat[ii, jj - 1] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii, jj - 1].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
                else if ((ii + 1) < n && mat[ii + 1, jj] == 0)
                {
                    mat[ii + 1, jj] = 2;
                    mat[ii, jj] = 0;
                    pa[ii, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\imagefill.jpg");
                    pa[ii + 1, jj].BackgroundImage = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\pic\untitled1.jpg");
                }
            }

        }

    }
}
