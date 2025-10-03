using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Lab3_BT2
{
    public partial class Form1 : Form
    {
        private string currentFile = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily font in fonts.Families)
            {
                cmbFonts.Items.Add(font.Name);
            }
            cmbFonts.SelectedItem = "Tahoma";

            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int s in sizes)
                cmbSize.Items.Add(s.ToString());
            cmbSize.SelectedItem = "14";

            richText.Font = new Font("Tahoma", 14);
        }

        private void NewFile()
        {
            richText.Clear();
            richText.Font = new Font("Tahoma", 14);
            cmbFonts.SelectedItem = "Tahoma";
            cmbSize.SelectedItem = "14";
            currentFile = "";
        }
        private void btnNew_Click(object sender, EventArgs e) => NewFile();
        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e) => NewFile();

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.FileName.EndsWith(".rtf"))
                    richText.LoadFile(dlg.FileName, RichTextBoxStreamType.RichText);
                else
                    richText.LoadFile(dlg.FileName, RichTextBoxStreamType.PlainText);

                currentFile = dlg.FileName;
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Rich Text (*.rtf)|*.rtf";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    currentFile = dlg.FileName;
                    richText.SaveFile(currentFile, RichTextBoxStreamType.RichText);
                }
            }
            else
            {
                richText.SaveFile(currentFile, RichTextBoxStreamType.RichText);
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSave_Click(object sender, EventArgs e) => SaveFile();
        private void lưuNộiDungVănBảnCtrlSToolStripMenuItem_Click(object sender, EventArgs e) => SaveFile();

        private void btnBold_Click(object sender, EventArgs e) => ChangeStyle(FontStyle.Bold);
        private void btnItalic_Click(object sender, EventArgs e) => ChangeStyle(FontStyle.Italic);
        private void btnUnderline_Click(object sender, EventArgs e) => ChangeStyle(FontStyle.Underline);

        private void ChangeStyle(FontStyle style)
        {
            if (richText.SelectionFont != null)
            {
                FontStyle newStyle = richText.SelectionFont.Style ^ style;
                richText.SelectionFont = new Font(richText.SelectionFont, newStyle);
            }
        }

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richText.SelectionFont != null && cmbFonts.SelectedItem != null)
            {
                richText.SelectionFont = new Font(
                    cmbFonts.SelectedItem.ToString(),
                    richText.SelectionFont.Size,
                    richText.SelectionFont.Style);
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richText.SelectionFont != null && cmbSize.SelectedItem != null)
            {
                float newSize = float.Parse(cmbSize.SelectedItem.ToString());
                richText.SelectionFont = new Font(
                    richText.SelectionFont.FontFamily,
                    newSize,
                    richText.SelectionFont.Style);
            }
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                richText.SelectionFont = fontDlg.Font;
                richText.SelectionColor = fontDlg.Color;
            }
        }

        private void thoátToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
