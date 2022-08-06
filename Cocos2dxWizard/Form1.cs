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
using System.Threading;

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
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;

            Process proc = Process.Start(psi);
            proc.StartInfo = psi;
            proc.Start();

            string prjresult = proc.StandardOutput.ReadToEnd();
            this.richTextBox1.Text += prjresult;
            this.richTextBox1.Text += "\n";
            this.richTextBox1.Text += "[Completed] generate project\n";
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();

            proc.WaitForExit();
            proc.Close();

            Thread.Sleep(3000);

            if (checkBox2.Checked)
            {
                correctingCmake();

                this.richTextBox1.Text += "[Edited] CmakeLists.txt\n";
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }

            Thread.Sleep(3000);

            if (checkBox1.Checked)
            {
                ProcessStartInfo cmake_psi = new ProcessStartInfo();
                Process cmake_pro = new Process();

                cmake_psi.FileName = @"cmd";
                cmake_psi.CreateNoWindow = true;
                cmake_psi.UseShellExecute = false;
                cmake_psi.RedirectStandardOutput = true;
                cmake_psi.RedirectStandardInput = true;
                cmake_psi.RedirectStandardError = true;

                cmake_pro.StartInfo = cmake_psi;
                cmake_pro.Start();

                string targetPath = destText.Text + "\\" + projectNameText.Text + "\\proj.win32";
                string targetDrive = targetPath.Substring(0, 2);
                //Process.Start(@targetDrive);
                string cmake_cmd = "cmake .. -G\"Visual Studio 15 2017\" -Tv141";

                cmake_pro.StandardInput.WriteLine(targetDrive);
                cmake_pro.StandardInput.WriteLine(@"cd " + targetPath);
                cmake_pro.StandardInput.WriteLine(cmake_cmd);
                cmake_pro.StandardInput.Close();

                string resulttext = cmake_pro.StandardOutput.ReadToEnd();
                cmake_pro.WaitForExit();
                cmake_pro.Close();

                this.richTextBox1.Text += resulttext;
                this.richTextBox1.Text += "\n";
                this.richTextBox1.Text += "[Completed] win32 cmake\n";

                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
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

        private void button5_Click(object sender, EventArgs e)
        {
            string cmake_cmd = "cmake .. -G\"Visual Studio 15 2017\" -Tv141";
            this.richTextBox1.Text += "[copy to clipboard] " + cmake_cmd;
            System.Windows.Forms.Clipboard.SetText(cmake_cmd);
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
            sw.WriteLine(checkBox1.Checked ? "true" : "false");

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

            string cmc = sr.ReadLine();
            if (cmc != null)
                checkBox1.Checked = (cmc == "true");

            sr.Close();

            this.radioButton1.Checked = isPortrait;
            this.radioButton2.Checked = !isPortrait;

        }

        void correctingCmake()
        {
            string targetPath = destText.Text + "\\" + projectNameText.Text + "\\";
            //string targetDrive = targetPath.Substring(0, 2);
            string fname = targetPath + "CMakeLists.txt";

            string[] flines = File.ReadAllLines(fname);
            for (int i = 0; i < flines.Length; i++)
            {
                if (flines[i] == "# add cross-platforms source files and header files ")
                {
                    flines[i + 1] = "file (GLOB_RECURSE MY_SOURCES Classes/*.cpp)";
                    flines[i + 2] = "file (GLOB_RECURSE MY_HEADERS Classes/*.h)";
                    flines[i + 3] = "list(APPEND GAME_SOURCE ${MY_SOURCES})";
                    flines[i + 4] = "list(APPEND GAME_HEADER ${MY_HEADERS})";
                    flines[i + 5] = "# edited by Cocos2D-x Wizard";
                    flines[i + 6] = "# ";
                    flines[i + 7] = "";
                    flines[i + 8] = "";
                }
            }

            File.WriteAllLines(fname, flines);
        }
    }
}
