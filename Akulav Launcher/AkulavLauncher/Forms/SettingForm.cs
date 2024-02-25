using AkulavLauncher.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AkulavLauncher.Forms
{

    public partial class SettingForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private string jsonFilePath = Paths.links;
        private List<Link> links = new List<Link>();
        public static Form main;
        public SettingForm(Form original)
        {
            InitializeComponent();
            main = original;
            links = Utility.links;
            PopulateLinkList();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            main.Enabled = true;
            this.Close();
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void SaveLinks()
        {
            string json = JsonConvert.SerializeObject(links, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }


        private void PopulateLinkList()
        {
            //
            listBoxLinks.Items.Clear();
            foreach (var link in links)
            {
                listBoxLinks.Items.Add(link.Url);
                string searchString = Utility.GetEnabledLink();
                int index = listBoxLinks.Items.IndexOf(searchString);

                if (index != -1)
                {
                    listBoxLinks.SelectedIndex = index;
                    sourceLabel.Text = listBoxLinks.Items[index].ToString();
                }
                else
                {
                    // The string was not found
                }

            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string newUrl = textBoxNewLink.Text.Trim();
            if (!string.IsNullOrEmpty(newUrl))
            {
                links.Add(new Link { Url = newUrl, Enabled = false });
                SaveLinks();
                PopulateLinkList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxLinks.SelectedIndex != -1)
            {
                links.RemoveAt(listBoxLinks.SelectedIndex);
                SaveLinks();
                PopulateLinkList();
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (listBoxLinks.SelectedIndex != -1)
            {
                // Disable all links first
                links.ForEach(link => link.Enabled = false);
                // Enable the selected one
                links[listBoxLinks.SelectedIndex].Enabled = true;
                SaveLinks();
                PopulateLinkList();
            }
        }

        private void LinkManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLinks();
        }

        private void disableBtn_Click(object sender, EventArgs e)
        {
            if (listBoxLinks.SelectedIndex != -1)
            {
                // Disable all links first
                links.ForEach(link => link.Enabled = false);
                // Enable the selected one
                links[listBoxLinks.SelectedIndex].Enabled = false;
                SaveLinks();
                PopulateLinkList();
            }
        }
        
    }

}
