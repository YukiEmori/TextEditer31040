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
        #region ファイル

        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル名存在するのか？
            if (rtTextArea.Modified)
            {
                MsgBox(sender, e);
            }
            else
            {
                filename = "";
                rtTextArea.Text = "";
            }
            
            
        }

        //開く
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(rtTextArea.Modified)
            {
                MsgBox(sender, e);
            }

            //ダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
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
            if (filename != "" && rtTextArea.TextLength > 0)
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


        //終了
        private void EndXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル名存在するのか？
            if (rtTextArea.Modified)
            {
                MsgBox(sender, e);
            }
            //アプリケーション終了
            Application.Exit();
            
        }

        //メッセージボックスを表示する
        private  void MsgBox(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("未保存なものがあります。保存しますか？",
                "質問",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.Yes)
            {
               //「はい」が選択されたとき    
                    SaveToolStripMenuItem_Click(sender, e);
            }
            else if (result == DialogResult.No)
            {
                //「いいえ」が選択されたとき
                rtTextArea.Text = "";
                filename = "";
            }else if (result == DialogResult.Cancel) { 
            }
        }

        #endregion

        #region 編集

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
                     
        }

        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
                rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
                rtTextArea.Copy();           
        }

        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data != null && data.GetDataPresent(DataFormats.Text) == true)
            { 
                rtTextArea.Paste();
            }
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //編集メニュー項目のマスク処理
        private void EbitToolStripMenuItem_Click(object sender, EventArgs e)
        {    
                UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo;
                RedoToolStripMenuItem.Enabled = rtTextArea.CanUndo;
                CutToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
                CopyToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf);
        }

        //色
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ColorDialogクラスのインスタンスを作成
            ColorDialog cd = new ColorDialog();

            //はじめに選択されている色を設定
            cd.Color = rtTextArea.ForeColor;

            //ダイアログを表示する
            if (cd.ShowDialog() == DialogResult.OK)
            {
                //選択された色の取得
                rtTextArea.ForeColor = cd.Color;
            }
        }

        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FontDialogクラスのインスタンスを作成
            FontDialog fd = new FontDialog();

            //初期のフォントを設定
            fd.Font = rtTextArea.Font;
            //初期の色を設定
            fd.Color = rtTextArea.ForeColor;
                    
            //ダイアログを表示する
            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                //rtTextAreaのフォントと色を変える
                rtTextArea.Font = fd.Font;
                rtTextArea.ForeColor = fd.Color;
            }
        }


        #endregion

    }
}

