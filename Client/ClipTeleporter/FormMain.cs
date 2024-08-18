using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ClipTeleporter
{
    public partial class FormMain : Form
    {
        KeyboardHook hook = new KeyboardHook();
        ClipHandler ClipHandler = new ClipHandler();
        BindingList<Clip> Clips = new BindingList<Clip>();
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

            string json = Properties.Settings.Default.AppSettings;
            if (!string.IsNullOrEmpty(json))
            {
                Clips = JsonConvert.DeserializeObject<BindingList<Clip>>(json);
            }

            dataGridView.DataSource = Clips;

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
                AddClip();
                notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
            }
            else if (e.Key.ToString() == "V")
            {
                string message = await ClipHandler.SendClip();
                AddClip();
                notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
            }
        }

        private void AddClip()
        {
            if (ClipHandler.Clip == null) return;
            if (Clips.Where(c => c.Token == ClipHandler.Clip.Token).ToList().Count > 0) return;

            Clips.Add(new Clip
            {
                Date = ClipHandler.Clip.Date,
                Direction = ClipHandler.Clip.Direction,
                Token = ClipHandler.Clip.Token
            });

            Properties.Settings.Default.AppSettings = JsonConvert.SerializeObject(Clips);
            Properties.Settings.Default.Save();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (initializing) return;
            Properties.Settings.Default.AppSettings = JsonConvert.SerializeObject(Clips);
            Properties.Settings.Default.Save();
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (initializing) return;
            Properties.Settings.Default.AppSettings = JsonConvert.SerializeObject(Clips);
            Properties.Settings.Default.Save();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string message = await ClipHandler.SendClip();
            AddClip();
            notifyIcon.ShowBalloonTip(2000, "ClipTeleporter", message, ToolTipIcon.Info);
        }

        private async void btnReceive_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count < 1) return;
            string token_password = dataGridView.SelectedCells[0].OwningRow.Cells["Token"].Value.ToString();
            string message = await ClipHandler.GetClip(token_password);
            AddClip();
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
    }
}