using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClipTeleporter
{
    public partial class FormMain : Form
    {
        KeyboardHook hook = new KeyboardHook();
        ClipHandler ClipHandler = new ClipHandler();
        bool initializing = true;

        public FormMain()
        {
            InitializeComponent();
            this.CenterToScreen();

            this.Icon = Properties.Resources.Icon;
            this.notifyIcon.Icon = Properties.Resources.Icon;

            this.notifyIcon.Text = "ClipTeleporter";
            this.notifyIcon.Visible = true;

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Open", ContextMenuOpen);
            menu.MenuItems.Add("Exit", ContextMenuExit);
            this.notifyIcon.ContextMenu = menu;

            this.Resize += WindowResize;
            this.FormClosing += WindowClosing;

            this.WindowState = FormWindowState.Minimized;

            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(Hook_KeyPressed);
            hook.RegisterHotKey(ClipTeleporter.ModifierKeys.Control | ClipTeleporter.ModifierKeys.Alt, Keys.C);
            hook.RegisterHotKey(ClipTeleporter.ModifierKeys.Control | ClipTeleporter.ModifierKeys.Alt, Keys.V);

            dataGridView.DataSource = ClipHandler.Clips;

            initializing = false;
        }

        private void ContextMenuOpen(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ContextMenuExit(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void WindowResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        async void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key.ToString() == "C")
            {
                string token_password = Clipboard.GetText();
                string message = await ClipHandler.GetClip(token_password);
                notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
            }
            else if (e.Key.ToString() == "V")
            {
                string message = await ClipHandler.SendClip();
                notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (initializing) return;
            ClipHandler.SaveClips();
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (initializing) return;
            ClipHandler.SaveClips();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string message = await ClipHandler.SendClip();
            notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
        }

        private async void btnReceive_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count < 1) return;
            string token_password = dataGridView.SelectedCells[0].OwningRow.Cells["Token"].Value.ToString();
            string message = await ClipHandler.GetClip(token_password);
            notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
        }

        private void btnCopyToken_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count < 1) return;
            string token_password = dataGridView.SelectedCells[0].OwningRow.Cells["Token"].Value.ToString();
            Clipboard.SetText(token_password);
            notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", "Copied: " + token_password, ToolTipIcon.Info);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            string txt = "TODO:\n";
            txt += "* Define server URL\n";
            txt += "* Define hotkeys\n";
            txt += "* Launch on Windows startup\n";
            MessageBox.Show(txt, "ClipTeleporter");
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            string txt = "ClipTeleporter " + typeof(Clip).Assembly.GetName().Version + "\n";
            txt += "Copyright (c) 2024 Thomas Winkel";
            MessageBox.Show(txt, "ClipTeleporter");
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                btnCopyToken.Enabled = true;
                btnReceive.Enabled = true;
            }
            else
            {
                btnCopyToken.Enabled = false;
                btnReceive.Enabled = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "json files (*.json)|*.json";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "MyClips.json";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter file = File.CreateText(saveFileDialog.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, ClipHandler.Clips);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "json files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader file = File.OpenText(openFileDialog.FileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        BindingList<Clip> newClips = (BindingList<Clip>)serializer.Deserialize(file, typeof(BindingList<Clip>));
                        foreach (Clip clip in newClips)
                        {
                            int index = ClipHandler.Clips.IndexOf(ClipHandler.Clips.Where(c => c.Token == clip.Token).FirstOrDefault());
                            if (index >= 0)
                            {
                                ClipHandler.Clips[index] = clip;
                            }
                            else
                            {
                                ClipHandler.Clips.Add(clip);
                            }
                        }
                        ClipHandler.SaveClips();
                    }
                }
            }
        }

        private void tbToken_Enter(object sender, EventArgs e)
        {
            tbToken.ForeColor = SystemColors.WindowText;
            tbToken.Text = "";
        }

        private void tbToken_Leave(object sender, EventArgs e)
        {
            tbToken.ForeColor = SystemColors.GrayText;
            tbToken.Text = "enter token...";
        }

        private async void tbToken_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string token = tbToken.Text;
                dataGridView.Select();
                
                string message = await ClipHandler.GetClip(token);
                notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
            }
        }
    }
}