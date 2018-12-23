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

namespace GM
{
    public partial class GM : Form
    {
        public GM()
        {
            InitializeComponent();
            RegisterCallback();
            LoadServerList();
        }

        #region 回调函数
        private void clear_Click(object sender, EventArgs e)
        {
            inputCmd.Text = string.Empty;
            output.Text = string.Empty;
        }

        private void sure_Click(object sender, EventArgs e)
        {
            string cmd = inputCmd.Text.Trim();

            List<int> indexs = new List<int>();
            int selectedServerCount = GetSelectedServerCount(ref indexs);
            if (selectedServerCount > 1)
            {
                MessageBox.Show("所选服务器个数过多！");
                return;
            }
            else if (selectedServerCount <= 0)
            {
                MessageBox.Show("至少选择一个服务器！");
                return;
            }
            else if(string.IsNullOrEmpty(cmd))
            {
                MessageBox.Show("请填写 cmd");
                return;
            }
            else
            {
                ReqServer(cmd);
            }
        }

        #endregion

        #region 自定义函数
        public readonly string serverListConfigFilePath = @"../../Src/Config/ServerListConfig.txt";
        // serverName:serverIP:serverPort
        private List<Tuple<string, string, string>> tp = new List<Tuple<string, string, string>>();

        private void LoadServerList()
        {
            serverList.Items.Clear();
            tp.Clear();
            if (File.Exists(serverListConfigFilePath))
            {
                string[] lines = File.ReadAllLines(serverListConfigFilePath, Encoding.UTF8);
                for (int i = 0; i < lines.Length; ++i)
                {
                    string line = lines[i];
                    if (!string.IsNullOrEmpty(line))
                    {
                        serverList.Items.Add(line);
                        string[] segments = line.Split('|');
                        if (segments.Length == 3)
                        {
                            tp.Add(new Tuple<string, string, string>(segments[0], segments[1], segments[2]));
                        }
                    }
                }
            }
            if (serverList.Items.Count > 0)
            {
                serverList.SetItemChecked(0, true);
            }
        }
        private void RegisterCallback()
        {
            // 注册网络回调
        }
        public int GetSelectedServerCount(ref List<int> indexList)
        {
            int cnt = serverList.CheckedItems.Count;
            indexList.Clear();
            for (int i = 0; i < cnt; ++i)
            {
                indexList.Add(serverList.CheckedIndices[i]);
            }
            return cnt;
        }
        public bool ParseLine(string line, out string[] cmd)
        {
            bool ret = false;
            cmd = new string[2];
            if (!string.IsNullOrEmpty(line))
            {
                line = line.Trim();
                string[] segments = line.Split(' ');
                if (segments.Length == 1) // 无参数指令
                {
                    cmd[0] = segments[0];
                    cmd[1] = "";
                    ret = true;
                }
                else if (segments.Length >= 2) // 有参数指令
                {
                    cmd[0] = segments[0];
                    cmd[1] = line.Substring(segments[0].Length);
                    ret = true;
                }
            }
            return ret;
        }
        public List<string[]> ParseLine(string[] lines)
        {
            List<string[]> cmds = new List<string[]>();
            for (int i = 0; i < lines.Length; ++i)
            {
                string[] cmd = null;
                if (ParseLine(lines[i], out cmd))
                {
                    cmds.Add(cmd);
                }
            }
            return cmds;
        }
        #endregion

        private void selectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < serverList.Items.Count; ++i)
            {
                serverList.SetItemChecked(i, selectAll.Checked);
            }
        }

        private void selectBack_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < serverList.Items.Count; ++i)
            {
                serverList.SetItemChecked(i, !serverList.GetItemChecked(i));
            }
        }

        private void load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "文本文件|*.txt";
            ofd.Title = "选择GM Cmd配置文件：";
            ofd.ShowDialog();

            string fileFullPath = ofd.FileName;
            if (!string.IsNullOrEmpty(fileFullPath) && File.Exists(fileFullPath))
            {
                string[] lines = File.ReadAllLines(fileFullPath);

                output.Lines = lines;

                List<string[]> cmds = ParseLine(lines);
                cmds.ForEach(cmd => 
                {
                    ReqServer(cmd[0], cmd[1]);
                });
            }
        }
        private void ReqServer(string cmdWithArg)
        {
            string[] cmd = null;
            if (ParseLine(cmdWithArg, out cmd))
            {
                ReqServer(cmd[0], cmd[1]);
            }
        }
        private void ReqServer(string cmd, string arg)
        {

        }
    }
}
