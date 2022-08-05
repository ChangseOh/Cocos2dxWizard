using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Cocos2dxWizard
{
    public partial class Form1 : Form
    {
        Button selTool, selDest, createBtn, resetBtn;
        ComboBox languageBox;
        TextBox toolText, projectNameText, packageNameText, destText;

        bool isPortrait = true;

        public Form1()
        {
            InitializeComponent();

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            //selTool = (Button)

            selTool = this.button3;
            selDest = this.button4;
            createBtn = this.button1;
            resetBtn = this.button2;

            languageBox = this.comboBox1;//(ComboBox)resources.GetObject("language");

            toolText = this.textBox3;
            projectNameText = this.textBox4;
            packageNameText = this.textBox1;
            destText = this.textBox2;

            this.radioButton1.Checked = true;
            this.radioButton2.Checked = false;

            this.FormClosing += new FormClosingEventHandler(saveSetting);

            loadSetting();
        }

        private void button1_Click(object sender, EventArgs e)//Create
        {
            this.richTextBox1.Text = "";
            //if(toolText!="cocos.bat")
            bool stop = false;
            if (toolText.Text.Length <= 0)
            {
                stop = true;
                this.richTextBox1.Text += "Error : No cocos.bat folder\n";
            }
            if (packageNameText.Text.Length <= 0)
            {
                stop = true;
                this.richTextBox1.Text += "Error : No Package Name\n";
            }
            if (languageBox.SelectedIndex < 0)
            {
                stop = true;
                this.richTextBox1.Text += "Error : Select use Language\n";
            }
            if (destText.Text.Length <= 0)
            {
                stop = true;
                this.richTextBox1.Text += "Error : No destination folder\n";
            }
            if (projectNameText.Text.Length <= 0)
            {
                stop = true;
                this.richTextBox1.Text += "Error : No Project Name\n";
            }

            if (stop)
                return;

            string[] language = { "cpp", "js", "lua" };

            if (toolText.Text.IndexOf("cocos.bat") < 0 && toolText.Text.IndexOf("cocos") < 0)
                toolText.Text += "cocos.bat";

            ProcessStartInfo psi = new ProcessStartInfo();
            
            if (toolText.Text.Contains("3.3"))
                isPortrait = false;

            string argu = toolText.Text + " new"
                + " -p " + packageNameText.Text
                + " -l " + language[languageBox.SelectedIndex]
                + " -d " + destText.Text
                + (isPortrait ? " --portrait" : "")
                + " " + projectNameText.Text;

            this.richTextBox1.Text = argu + "\n=================\n";

            psi.FileName = "cmd.exe";
            psi.Arguments = "/C " + argu;

            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;

            Process proc = Process.Start(psi);

            while (true)
            {
                string txt = proc.StandardOutput.ReadLine();
                if (txt == null)
                    break;

                this.richTextBox1.Text += txt;
                this.richTextBox1.Refresh();
                Console.WriteLine(txt);
            }

            Debug.WriteLine("" + psi.Arguments);
        }
        private void button2_Click(object sender, EventArgs e)//Reset
        {
            toolText.Text = "";
            packageNameText.Text = "ex)com.mycompany.example";
            languageBox.SelectedIndex = -1;
            destText.Text = "";
            isPortrait = true;
            projectNameText.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)//Sel tools folder
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "cocos.bat|cocos.bat";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                toolText.Text = ofd.FileName;
            }
            //Debug.WriteLine("Debug " + sender);
        }
        private void button4_Click(object sender, EventArgs e)//Sel dest folder
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                destText.Text = fbd.SelectedPath;
        }
        private void etcCheck(object sender, EventArgs e)//Sel dest folder
        {
            //Debug.WriteLine("Debug " + languageBox.Items[languageBox.SelectedIndex]);
            packageNameText.Text = trimmedSpace(packageNameText.Text);
            destText.Text = trimmedSpace(destText.Text);
            projectNameText.Text = trimmedSpace(projectNameText.Text);
        }

        string trimmedSpace(string src)
        {
            int start = 0;
            int num = 0;
            string tmp = src;

            while(tmp.IndexOf(" ")>0)
            {
                num = tmp.IndexOf(" ");
                string tmp1 = tmp.Substring(0, num);
                start = num + 1;
                tmp1 += tmp.Substring(num + 1);
                tmp = tmp1;
            }

            return tmp;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            isPortrait = (this.radioButton1.Checked);
        }

        void saveSetting(object sender, FormClosingEventArgs e)
        {

            StreamWriter sw = new StreamWriter("./setting.txt");

            sw.WriteLine(toolText.Text);
            sw.WriteLine(packageNameText.Text);
            sw.WriteLine(languageBox.SelectedIndex.ToString());
            sw.WriteLine(destText.Text);
            sw.WriteLine(isPortrait ? "true" : "false");
            sw.WriteLine(projectNameText.Text);

            sw.Close();

        }
        void loadSetting()
        {
            if(!File.Exists("./setting.txt"))
                return;

            StreamReader sr = new StreamReader("./setting.txt");
            toolText.Text = sr.ReadLine();
            packageNameText.Text = sr.ReadLine();
            languageBox.SelectedIndex = Convert.ToInt32(sr.ReadLine());
            destText.Text = sr.ReadLine();
            string ip = sr.ReadLine();
            isPortrait = (ip == "true");
            projectNameText.Text = sr.ReadLine();

            sr.Close();

            this.radioButton1.Checked = isPortrait;
            this.radioButton2.Checked = !isPortrait;

        }
    }
}
