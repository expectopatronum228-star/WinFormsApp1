namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt|PL Notepad File (*.tnf)|*.pnf";    // Set the file types for the save file dialog
            string filename = saveFileDialog1.FileName;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void closeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, richTextBox1.Text);
            MessageBox.Show("File succesfully saved!");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog1.FileName;
            string fileText = File.ReadAllText(filename);   // Read the file's text into a string
            richTextBox1.Text = fileText;       // Set the RichTextBox text to the file's text
            MessageBox.Show("File succesfully opened!");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
