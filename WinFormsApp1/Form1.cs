namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Visible = false;   // Hide the initial RichTextBox
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt|PL Notepad File (*.tnf)|*.pnf";    // Set the file types for the save file dialog
            string filename = saveFileDialog1.FileName;
            AddNewTab();  // Add the first tab
        }
        private int tabCount = 1; // Counter for naming new tabs
        private void AddNewTab()
        {
            var tabPage = new TabPage($"Document {tabCount++}");
            var richTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill
            };
            tabPage.Controls.Add(richTextBox);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void closeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void SaveDocument()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, richTextBox1.Text);
            MessageBox.Show("File succesfully saved!");
        }
        private RichTextBox GetCurrentRichTextBox()
        {
            if (tabControl1.SelectedTab?.Controls[0] is RichTextBox rtb)    // Check if the selected tab has a RichTextBox
                return rtb;
            return null;
        }
        private void savToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rtb = GetCurrentRichTextBox();  // Get the RichTextBox from the current tab
            if (rtb != null)        // Check if there is a RichTextBox to save
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                string filename = saveFileDialog1.FileName;
                File.WriteAllText(filename, rtb.Text);
                tabControl1.SelectedTab.Tag = filename; // Store the file path in the tab's Tag property
                tabControl1.SelectedTab.Text = Path.GetFileName(filename); // Update the tab title to the file name
                MessageBox.Show("File succesfully saved!");
            }
            else
            {
                MessageBox.Show("No document to save.");
            }
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
            // Create a new tab and add a RichTextBox with the file's content
            var tabPage = new TabPage(Path.GetFileName(filename));   // Use the file name as the tab title
            var richTextBox = new RichTextBox   // Create a new RichTextBox
            {
                Dock = DockStyle.Fill,
                Text = fileText // Set the text to the file's content
            };
            tabPage.Controls.Add(richTextBox); // Add the RichTextBox to the tab page
            tabPage.Tag = filename; // Store the file path in the tab's Tag property
            tabControl1.TabPages.Add(tabPage); // Add the tab page to the TabControl
            tabControl1.SelectedTab = tabPage; // Select the new tab
            MessageBox.Show("File succesfully opened!");
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();     // Show the new form
        }
        private void SaveAllTabs()
        {
            foreach (TabPage tabPage in tabControl1.TabPages)   // Iterate through all tab pages
            {
                if (tabPage.Controls.Count > 0 && tabPage.Controls[0] is RichTextBox rtb)   // Check if the tab page has a RichTextBox
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                    string filename = saveFileDialog1.FileName;
                    File.WriteAllText(filename, rtb.Text);
                    MessageBox.Show($"File '{tabPage.Text}' succesfully saved!");
                }
            }
            MessageBox.Show("All files succesfully saved!");
        }
        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAllTabs();
        }
        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTab();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rtb = GetCurrentRichTextBox();  // Get the RichTextBox from the current tab
            var tab = tabControl1.SelectedTab;
            if (rtb != null)        // Check if there is a RichTextBox to save
            {
                if (tab.Tag is string existingFilePath) // Check if the tab has an associated file path
                {
                    File.WriteAllText(existingFilePath, rtb.Text); // Save to the existing file path
                    MessageBox.Show("File successfully saved!");
                }
                else
                {
                    // If no existing file path, prompt for a new save location
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                    string filename = saveFileDialog1.FileName;
                    File.WriteAllText(filename, rtb.Text);
                    tab.Tag = filename; // Store the file path in the tab's Tag property
                    tab.Text = Path.GetFileName(filename); // Update the tab title to the file name
                    MessageBox.Show("File successfully saved!");
                }
            }
            else
            {
                MessageBox.Show("No document to save.");
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
