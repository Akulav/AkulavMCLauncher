﻿using Newtonsoft.Json;
using System;
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
        private readonly string jsonFilePath = Paths.links;
        readonly Form main;
        public SettingForm(Form main)
        {
            InitializeComponent();
            CenterToScreen();
            if (DirectoryLib.GetTextFromJson() != null)
            {
                textBoxNewLink.Text = DirectoryLib.GetTextFromJson();
                textBoxNewLink.Enabled = false;
            }

            this.main = main;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            //Application.Restart();
            main.Close();
            MainForm mf = new MainForm();
            mf.Show();
            Close();
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void AddTextToJson(string text)
        {
            var jsonData = JsonConvert.SerializeObject(text);
            File.WriteAllText(jsonFilePath, jsonData);
        }

        private void RemoveTextFromJson()
        {
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }
        }



        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxNewLink.Text))
            {
                string textToAdd = textBoxNewLink.Text.Trim();
                AddTextToJson(textToAdd);
                textBoxNewLink.Enabled = false;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            RemoveTextFromJson();
            textBoxNewLink.Enabled = true;
            textBoxNewLink.Clear();
        }
    }

}
