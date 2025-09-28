namespace BBModLoader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            modBox = new ListBox();
            browseDirButton = new Button();
            gameDirLabel = new Label();
            launchButton = new Button();
            patchButton = new Button();
            launchInfoButton = new Button();
            patchInfoButton = new Button();
            label1 = new Label();
            browseGameDialog = new OpenFileDialog();
            progressBar = new ProgressBar();
            progressLabel = new Label();
            SuspendLayout();
            // 
            // modBox
            // 
            modBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            modBox.BorderStyle = BorderStyle.None;
            modBox.FormattingEnabled = true;
            modBox.Location = new Point(12, 44);
            modBox.Name = "modBox";
            modBox.Size = new Size(280, 384);
            modBox.TabIndex = 0;
            // 
            // browseDirButton
            // 
            browseDirButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseDirButton.BackColor = SystemColors.Window;
            browseDirButton.FlatStyle = FlatStyle.Flat;
            browseDirButton.Location = new Point(442, 56);
            browseDirButton.Name = "browseDirButton";
            browseDirButton.Size = new Size(192, 46);
            browseDirButton.TabIndex = 1;
            browseDirButton.Text = "Browse";
            browseDirButton.UseVisualStyleBackColor = false;
            browseDirButton.Click += browseDirButton_Click;
            // 
            // gameDirLabel
            // 
            gameDirLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gameDirLabel.AutoSize = true;
            gameDirLabel.Location = new Point(316, 63);
            gameDirLabel.Name = "gameDirLabel";
            gameDirLabel.Size = new Size(120, 32);
            gameDirLabel.TabIndex = 2;
            gameDirLabel.Text = "Game File";
            // 
            // launchButton
            // 
            launchButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            launchButton.BackColor = SystemColors.Window;
            launchButton.FlatStyle = FlatStyle.Flat;
            launchButton.Location = new Point(316, 305);
            launchButton.Name = "launchButton";
            launchButton.Size = new Size(250, 46);
            launchButton.TabIndex = 3;
            launchButton.Text = "Launch";
            launchButton.UseVisualStyleBackColor = false;
            launchButton.Click += launchButton_Click;
            // 
            // patchButton
            // 
            patchButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            patchButton.BackColor = SystemColors.Window;
            patchButton.FlatStyle = FlatStyle.Flat;
            patchButton.Location = new Point(316, 357);
            patchButton.Name = "patchButton";
            patchButton.Size = new Size(250, 46);
            patchButton.TabIndex = 4;
            patchButton.Text = "Patch";
            patchButton.UseVisualStyleBackColor = false;
            patchButton.Click += patchButton_Click;
            // 
            // launchInfoButton
            // 
            launchInfoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            launchInfoButton.BackColor = SystemColors.Window;
            launchInfoButton.FlatStyle = FlatStyle.Flat;
            launchInfoButton.Location = new Point(572, 305);
            launchInfoButton.Name = "launchInfoButton";
            launchInfoButton.Size = new Size(62, 46);
            launchInfoButton.TabIndex = 5;
            launchInfoButton.Text = "?";
            launchInfoButton.UseVisualStyleBackColor = false;
            launchInfoButton.Click += launchInfoButton_Click;
            // 
            // patchInfoButton
            // 
            patchInfoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            patchInfoButton.BackColor = SystemColors.Window;
            patchInfoButton.FlatStyle = FlatStyle.Flat;
            patchInfoButton.Location = new Point(572, 357);
            patchInfoButton.Name = "patchInfoButton";
            patchInfoButton.Size = new Size(62, 46);
            patchInfoButton.TabIndex = 6;
            patchInfoButton.Text = "?";
            patchInfoButton.UseVisualStyleBackColor = false;
            patchInfoButton.Click += patchInfoButton_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(74, 32);
            label1.TabIndex = 7;
            label1.Text = "Mods";
            // 
            // browseGameDialog
            // 
            browseGameDialog.DefaultExt = "exe";
            browseGameDialog.Filter = "Executable Files (*.exe)|*.exe";
            browseGameDialog.Title = "Browse...";
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressBar.BackColor = SystemColors.Control;
            progressBar.Location = new Point(316, 269);
            progressBar.Maximum = 3;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(318, 30);
            progressBar.TabIndex = 8;
            // 
            // progressLabel
            // 
            progressLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressLabel.AutoSize = true;
            progressLabel.Location = new Point(419, 224);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(110, 32);
            progressLabel.TabIndex = 9;
            progressLabel.Text = "Waiting...";
            progressLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 450);
            Controls.Add(progressLabel);
            Controls.Add(progressBar);
            Controls.Add(label1);
            Controls.Add(patchInfoButton);
            Controls.Add(launchInfoButton);
            Controls.Add(patchButton);
            Controls.Add(launchButton);
            Controls.Add(gameDirLabel);
            Controls.Add(browseDirButton);
            Controls.Add(modBox);
            MinimumSize = new Size(672, 521);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BB Mod Loader";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox modBox;
        private Button browseDirButton;
        private Label gameDirLabel;
        private Button launchButton;
        private Button patchButton;
        private Button launchInfoButton;
        private Button patchInfoButton;
        private Label label1;
        private OpenFileDialog browseGameDialog;
        private ProgressBar progressBar;
        private Label progressLabel;
    }
}
