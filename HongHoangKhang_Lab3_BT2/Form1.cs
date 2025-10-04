using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HongHoangKhang_Lab3_BT2
{
    public partial class Form1 : Form
    {

        private String Filepath = null;
        private bool New = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.ForeColor = fontDlg.Color;
                richTextBox1.Font = fontDlg.Font;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                cmbFonts.Items.Add(font.Name);
            }

            string[] sizes = { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
            foreach (String size in sizes)
            {
                cmbSize.Items.Add(size);
            }
            cmbFonts.SelectedItem = "Tahoma";
            cmbSize.SelectedItem = "14";
        }

        private void newToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Font = new Font("Tahoma", 14);
        }

        private void OpenToolStrip_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text | *.txt | RTF | *.rtf | All file | *.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(open.FileName);
            }
            Filepath = open.FileName;
            
        }

        private void SaveToolStrip_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = save.FileName;
            if(New)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Rich text format (*.rtf)| *.rtf";
                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    Filepath = saveFile.FileName;
                    New = false; //Tài liệu đã có tên
                    try
                    {
                        richTextBox1.SaveFile(Filepath, RichTextBoxStreamType.RichText);
                        MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                try
                {
                    richTextBox1.SaveFile(Filepath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if(richTextBox1.SelectionLength > 0)
            {
                Font font = richTextBox1.SelectionFont;
                FontStyle newStyle;
                if(font.Bold)
                    newStyle = font.Style & ~FontStyle.Bold;
                else
                    newStyle = font.Style | FontStyle.Bold;

                richTextBox1.SelectionFont = new Font(font, newStyle);
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                Font font = richTextBox1.SelectionFont;
                FontStyle newStyle;
                if (font.Italic)
                    newStyle = font.Style & ~FontStyle.Italic;
                else
                    newStyle = font.Style | FontStyle.Italic;

                richTextBox1.SelectionFont = new Font(font, newStyle);
            }
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                Font font = richTextBox1.SelectionFont;
                FontStyle newStyle;
                if (font.Underline)
                    newStyle = font.Style & ~FontStyle.Underline;
                else
                    newStyle = font.Style | FontStyle.Underline;

                richTextBox1.SelectionFont = new Font(font, newStyle);
            }
        }

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(richTextBox1.SelectionLength > 0)
            {
                String newFontName = cmbFonts.SelectedItem.ToString();
                Font font = richTextBox1.SelectionFont;
                Font newfont = new Font(newFontName, font.Size, font.Style);
                richTextBox1.SelectionFont = newfont;
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                if (float.TryParse(cmbSize.SelectedItem.ToString(), out float newSize))
                {
                    Font font = richTextBox1.SelectionFont;
                    Font newfont = new Font(font.FontFamily, newSize, font.Style);
                    richTextBox1.SelectionFont = newfont;
                }
            }
        }
    }
}
