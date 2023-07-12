using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VS_Project_Hadi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        /*Comment: this code is lodded when starting, and it makes the coruses droplist unavailable
         and reads the student textfile to the student name droplist*/
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;

            string stu;

            StreamReader studentFile;

            studentFile = File.OpenText("students.txt");

            comboBox1.Items.Clear();

            while (!studentFile.EndOfStream)
            {
                stu = studentFile.ReadLine();

                comboBox1.Items.Add(stu);

            }
            studentFile.Close();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /*Comment: this code is lodded when the Faculty droplist is changed,
          it first checks if any of the radioButtons is checked if so it enables the 
          coruses droplist, then checks witch of the radio buttons is checked, and reads the
          value of the Faculty droplist and opens the textFile accordingly */
        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                comboBox3.Enabled = true;
            }
            comboBox3.Items.Clear();

            if (radioButton1.Checked)
            {
                string cor1;

                StreamReader coruseFile1;

                coruseFile1 = File.OpenText(radioButton1.Text + "\\" + comboBox4.Text + "\\coruse.txt");

                comboBox3.Items.Clear();

                while (!coruseFile1.EndOfStream)
                {
                    cor1 = coruseFile1.ReadLine();

                    comboBox3.Items.Add(cor1);

                }
                coruseFile1.Close();
            }

            if (radioButton2.Checked)
            {
                string cor2;

                StreamReader coruseFile2;

                coruseFile2 = File.OpenText(radioButton2.Text + "\\" + comboBox4.Text + "\\coruse.txt");

                comboBox3.Items.Clear();

                while (!coruseFile2.EndOfStream)
                {
                    cor2 = coruseFile2.ReadLine();

                    comboBox3.Items.Add(cor2);

                }
                coruseFile2.Close();
            }

        }


        /*Comment: this code is lodded when the first radiobutton(Morning) is changed,
          if so it clears the coruses droplist values*/
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            comboBox3.Text = null;
            comboBox3.Items.Clear();
        }



        /*Comment: this code is lodded when the Add coruse Button is clicked,
          the fisrt if statement checks if the name/semester droplist is not emtpy
          the second if statement saves the name/semester droplist values(only once) 
          the third if statement checks so that the hours don't bass 21 hours
          the fourth if statement checks if the coruses droplist is not null
          and the fith if statement sees if the coruse is a lab or not */

        int hour = 0;
        string name = null;
        string sems = null;
        int num = 1;

        private void round_Button1_Click(object sender, EventArgs e)
        {
            /*if statement 1*/
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                /*if statement 2*/
                if (num == 1)
                {
                    name = comboBox1.Text;
                    sems = comboBox2.Text;
                    num += 1;
                }

                /*if statement 3*/
                if (hour < 21)
                {
                    /*if statement 4*/
                    if (comboBox3.Text != "" && name == comboBox1.Text && sems == comboBox2.Text)
                    {
                        int h;

                        string[] itm = comboBox3.Text.Split('|');

                        /*if statement 5*/
                        if (itm[1].StartsWith("A"))
                        {
                            h = 3;
                            hour = hour + 3;
                        }
                        /*else statement 5*/
                        else
                        {
                            h = 1;
                            hour = hour + 1;
                        }

                        ListViewItem studentView = new ListViewItem(itm[0]);
                        studentView.SubItems.Add(itm[1]);
                        studentView.SubItems.Add(h.ToString());
                        listView1.Items.Add(studentView);


                        label6.Text = "Total hours: " + hour.ToString();

                        comboBox3.Text = null;
                    }

                    /*else statement 4*/
                    else
                    {
                        MessageBox.Show("coruse not selected / wrong name / wrong semester", "Error");
                    }

                }

                /*else statement 3*/
                else
                {
                    MessageBox.Show("cannot add coruse,past 21 hours", "Error");
                }
            }

            /*else statement 1*/
            else
            {
                MessageBox.Show("Name/Semester not selected", "Error");
            }
        }

        /*Comment: this code is lodded when the delete coruse Button is clicked,
         it removes the selected course*/
        private void round_Button2_Click(object sender, EventArgs e)
        {
    
           

           

           if (listView1.SelectedItems[0].SubItems[1].Text.StartsWith("A"))
           {
               hour = hour - 3;
           }
           else
           {
               hour = hour - 1;
           }
               

           listView1.Items.Remove(listView1.SelectedItems[0]);

           label6.Text = "Total hours: " + hour.ToString();

        }

        /*Comment: this code is lodded when the Register Coruses Button is clicked,
         it saves the the listView content in a textFile*/
        int i = 1;
        private void round_Button3_Click(object sender, EventArgs e)
        {
            
            
            StreamWriter regFile;

            regFile = File.CreateText("Registered\\"+"regCoruses"+ i.ToString() +".txt");
            

            int k = 0;
            regFile.WriteLine("{0}  |  {1}  |  {2}", "Coruse Name", "Coruse Code", "Hours");
            foreach (ListViewItem item in listView1.Items)
            {
                regFile.WriteLine("{0}  |  {1}  |  {2}", listView1.Items[k].Text, listView1.Items[k].SubItems[1].Text, listView1.Items[k].SubItems[2].Text);

                k += 1;
            }

            regFile.WriteLine("{0}", "Total Hours :" + hour);

            regFile.Close();


            i += 1;
        }

        
    }
}
