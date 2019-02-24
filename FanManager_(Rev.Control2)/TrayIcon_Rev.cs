using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Drawing;


namespace TrayIcon_Rev.Center2
{
    class TrayIcon_Rev : ApplicationContext
    {
        private NotifyIcon TrayIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        public TrayIcon_Rev()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            TrayIcon.Visible = true;
        }

        private void InitializeComponent()
        {
            TrayIcon = new NotifyIcon();
            TrayIcon.Text = "Rev.Center2";
            contextMenuStrip1 = new ContextMenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            //The icon is added to the project resources.
            //Here I assume that the name of the file is 'TrayIcon.ico'
            TrayIcon.Icon = Properties.Resources.favicon;

            //Optional - handle doubleclicks on the icon:
            TrayIcon.DoubleClick += TrayIcon_DoubleClick;

            //Optional - Add a context menu to the TrayIcon:
            
            // 
            // TrayIconContextMenu
            // 
            this.contextMenuStrip1.AccessibleRole = AccessibleRole.None;
            this.contextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.contextMenuStrip1.BackgroundImageLayout = ImageLayout.None;
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 52);
            this.contextMenuStrip1.Text = "Rev.Center2";
           // this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 24);
            this.toolStripMenuItem1.Text = "Open Rev.Center2";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 24);
            this.toolStripMenuItem2.Text = "Exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);

            TrayIcon.ContextMenuStrip = contextMenuStrip1;
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            TrayIcon.Visible = false;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("Rev.Center2.exe");

        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("Rev.Center2.exe");
        }

      const string userRoot = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Rev.Center";
        const string keyName = userRoot + "\\" + "Rev.Center2.0";

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Registry.GetValue(keyName, "ThrottleStop", 0)) == 1)
            {

                foreach (var process in Process.GetProcessesByName("ThrottleStop.exe"))
                {
                    process.Kill();
                }
            }
            foreach (var process in Process.GetProcessesByName("Rev.Center2"))
            {
                process.Kill();
            }
            Application.Exit();

        }

    }
}
