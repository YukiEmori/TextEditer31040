using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31040
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string filename = "";　//(Camel形式(先頭が小文字) (⇔Pascal形式(先頭が大文字)
        public Form1()
        {
            InitializeComponent();
         }
        
        //終了
        private void EndXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }   

        //開く
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                using(StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"),false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    filename = ofdFileOpen.FileName; //現在開いているファイル名を格納する
                }
            }
        }
        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //ファイル名存在するのか？
            if(filename != "")
            {
                //上書き処理
                FileSave(filename);
            }
            else
            {
                SaveNameAToolStripMenuItem_Click(sender, e);             
            }
        }
     

        //名前を付けて保存
        private void SaveNameAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ダイアログを表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }

        //ファイル名を指定しデータを保存
        private void FileSave(string filename)
        {
            using (StreamWriter newTask = new StreamWriter(filename, false, Encoding.GetEncoding("utf-8")))
            {
                newTask.WriteLine(rtTextArea.Text);
            }
        }

        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filename = "";
            rtTextArea.Text = "";
        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
