using DelmonPrize;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuunalParty
{
    public partial class Form1 : Form
    {
        Sqlconnection Sqlconn = new Sqlconnection();
        SpeechAudioFormatInfo info = new SpeechAudioFormatInfo(6, AudioBitsPerSample.Sixteen, AudioChannel.Mono);
        SpeechSynthesizer synth = new SpeechSynthesizer();



        SqlDataReader dr;
        SqlDataReader dr2;
        Random random = new Random();

        int randomNumber = 0;
        int CompanyID = 0;
        //int min = 1;
        //int max = 5;
        string companyname = "";
        string fullname = "";

        List<int> numbers = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            string Celebrationemoje = "\uD83C\uDF89";
            string Sadnessemoje = "☹️";
            SqlParameter paramCompanyID = new SqlParameter("@C1", SqlDbType.Int);
            SqlParameter paramWinnerID = new SqlParameter("@C2", SqlDbType.Int);

            try
            {
                Sqlconn.OpenConection();


                dr = Sqlconn.DataReader("Select * From Prize ");
                if (dr.HasRows)
                {
                    randomNumber = random.Next(1, 500);
                    paramWinnerID.Value = randomNumber;
                    txtfullname.Text = "";
                    lblmsg.Text = "";
                    while (dr.Read())
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
                            lblmsg.Text = Celebrationemoje + " Congratulations   '" + fullname + "' ,  The Holder of Raffle Coupon No.:  '" + randomNumber.ToString() + "' " + Celebrationemoje;
                            synth.Speak(lblmsg.Text);

                            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\amin\Source\Repos\AuunalParty\AuunalParty\award.wav");
                            player.Play();





                        }


                    }

                }
                else
                {


                    //  lblMsg.Text = "";
                    lblmsg.Text = "We are sad to inform you that the scheduled number of prizes has ended... " +
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
    }
}
