using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeetleFight
{
    public partial class beetlefield : Form
    {
        #region Data
        private Beetle beetleclass1 = new Beetle();
        List<Beetle> beetlelist = new List<Beetle>();
        #endregion

        private void MoveBeetles(Beetle beetle)
        {
            foreach (Beetle bee in beetlelist)
            {
                if(bee.beetle!=beetle.beetle)
                {
                    if ((beetle.beetle.Location.X+50 >bee.beetle.Location.X && beetle.beetle.Location.X<= bee.beetle.Location.X+50 && beetle.beetle.Location.Y + 50>bee.beetle.Location.Y&&beetle.beetle.Location.Y< bee.beetle.Location.Y+50))// if the ball coordination is in the other beetle area the dirction of the movemet will switch
                    {
                        beetle.collision();
                    }
                }
            }
            #region X_movement
            Action a = () => { beetle.beetle.Location = new Point(beetle.beetle.Location.X + beetle.stepX, beetle.beetle.Location.Y); };// mooving the pic by chanching the location in the action 
            this.BeginInvoke(a);
            #region updating the previouse steps
            beetle.previousStepX = beetle.stepX;
            beetle.previousStepY = beetle.stepY;
            #endregion
            #endregion
            #region Y_movement
            Action a2 = () => { beetle.beetle.Location = new Point(beetle.beetle.Location.X, beetle.beetle.Location.Y + beetle.stepY); };// to change the moving direction when the coordinayhion of th pic hits the boarder };
            this.BeginInvoke(a2);
            #endregion
            #region UPDATESTEP
            beetle.UpdateSteps(this.ClientSize.Width, this.ClientSize.Height,beetle.beetle);// the fun checks if the ball hits the window border and change the vlue of the steps if its true
            #endregion
        }
        public beetlefield()
        {
            InitializeComponent();
        }  
        private  async void Form1_Load(object sender, EventArgs e)
        {
            NewBall_click(sender,new MouseEventArgs(MouseButtons,clicks:2,2,2,3));
            //PictureBox b = beetleclass1.CreatOne(this.ClientSize.Width, this.ClientSize.Height);
            //this.Controls.Add(b);
            //beetlelist.Add(new Beetle(b));
            BackColor = Color.White;
            while (true)
            {
                foreach (Beetle bb in beetlelist)
                {
                    bb.beetle.Click += new EventHandler((o, a) => { beetlelist.Remove(bb); bb.beetle.Dispose(); });
                    Task.Factory.StartNew(() => { MoveBeetles(bb); });

                }
                await Task.Delay(10);
                if (beetlelist.Count == 0)
                {
                    DialogResult result = MessageBox.Show("You WIN ", "Retry ?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                           Form1_Load(sender, e);
                    else
                       this.Close();

                }
              
            }

        }
        private void NewBall_click(object sender, MouseEventArgs e)
        {
            PictureBox b= beetleclass1.CreatOne(this.ClientSize.Width, this.ClientSize.Height);
            this.Controls.Add(b);
            Beetle ba = new Beetle(b);
            beetlelist.Add(ba);            
           
        }
        
    }
}
