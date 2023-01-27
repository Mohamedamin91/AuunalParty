using DelmonPrize;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;

namespace AuunalParty
{
    public partial class Form1 : Form
    {
        Sqlconnection Sqlconn = new Sqlconnection();
        SpeechAudioFormatInfo info = new SpeechAudioFormatInfo(6, AudioBitsPerSample.Sixteen, AudioChannel.Mono);
        SpeechSynthesizer synth = new SpeechSynthesizer();
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\amin\Source\Repos\AuunalParty\AuunalParty\award.wav");

        int counter = 0;
        

        SqlDataReader dr;
        SqlDataReader dr2;
        Random random = new Random();

        int randomNumber = 0;
        int CompanyID = 0;
        //int min = 1;
        //int max = 5;
        string companyname = "";
        string fullname = "";
        string lblmsg = "";

        List<int> numbers = new List<int>();
        public Form1()
        {
            InitializeComponent();
            label1.Font = new Font("Times New Roman", 260, FontStyle.Bold);
          //  label1.sc = ContentAlignment.MiddleCenter;
            txtfullname.Font = new Font("Times New Roman", 75, FontStyle.Bold);

            //   Form1 myForm = new Form1();
            // myForm.Size = new System.Drawing.Size(800, 600);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //foreach (Control c in this.Controls)
            //{
            //    if (c is PictureBox)
            //    {
            //        c.Visible = false;
            //    }
            //}
            TransparetBackground(label1);
            TransparetBackground(txtfullname);


        }


        private void btncheck_Click(object sender, EventArgs e)
        {





        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }



public void TransparetBackground(Control C)
{
    C.Visible = false;

    C.Refresh();
    Application.DoEvents();

    Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
    int titleHeight = screenRectangle.Top - this.Top;
    int Right = screenRectangle.Left - this.Left;

    Bitmap bmp = new Bitmap(this.Width, this.Height);
    this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
    Bitmap bmpImage = new Bitmap(bmp);
        bmp = bmpImage.Clone(new Rectangle(C.Location.X+Right, C.Location.Y + titleHeight, C.Width, C.Height), bmpImage.PixelFormat);
    C.BackgroundImage = bmp;

    C.Visible = true;
}
    private void btncheck_Click_1(object sender, EventArgs e)
        {
            

            //  string Celebrationemoje = "\uD83C\uDF89";
            string Sadnessemoje = "☹️";
            SqlParameter paramCompanyID = new SqlParameter("@C1", SqlDbType.Int);
            SqlParameter paramWinnerID = new SqlParameter("@C2", SqlDbType.Int);
            if (txtfullname.Text == "Full Name")
            {
                try
                {
                    Sqlconn.OpenConection();


                    dr = Sqlconn.DataReader("Select * From Prize ");
                    if (dr.HasRows)
                    {
                        randomNumber = random.Next(1, 500);
                        paramWinnerID.Value = randomNumber;
                        //txtfullname.Text = "";
                        //lblmsg.Text = "";
                        //while (dr.Read())
                        {


                            dr2 = Sqlconn.DataReader("SELECT FullName, company from prize  where    [Gifts]=1   and [Selected]=0   and [Attended] =1  and CandID = @C2 ", paramWinnerID);
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    fullname = dr2["FullName"].ToString();
                                    companyname = dr2["company"].ToString();
                                    paramCompanyID.Value = CompanyID;

                                }

                                txtfullname.Text = fullname;
                                Sqlconn.ExecuteQueries("update  prize set selected = 1 where  CandID = @C2 ", paramWinnerID);
                                label1.Text = randomNumber.ToString();
                                listBox1.Items.Add(randomNumber.ToString());
                                lblmsg = " Congratulations   '" + fullname + "' ,  The Holder of Raffle Coupon No.:  '" + randomNumber.ToString() + "' ";
                                synth.Speak(lblmsg);

                                player.Play();



                            }
                           Thread.Sleep(1);

                           // txtfullname.Text = "Full Name";
                      
                            //label1.Text = "Winner";
                            //player.Stop();
                        }

                    }
                    else
                    {


                        //  lblMsg.Text = "";
                        lblmsg = "We are sad to inform you that the scheduled number of prizes has ended... " +
                            " Good luck to those who are not selected this year." +
                            " We look forward to having you with us next year - 2024. " +
                            " Thanks for coming  " + Sadnessemoje;
                        // Page.ClientScript.RegisterStartupScript(typeof(string), "fadeMsg", "fade('" + lblMsg2.ClientID + "');", true);

                    }


                }
                catch (Exception)
                {

                    throw;
                }



            }
            else 
            { }
        }

        private void txtfullname_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
