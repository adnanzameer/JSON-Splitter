namespace JSONSplitter
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnGenerateBlockTypes = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.txtCSTemplate = new System.Windows.Forms.TextBox();
            this.txtCSHTMLTemplate = new System.Windows.Forms.TextBox();
            this.lblCSTemplate = new System.Windows.Forms.Label();
            this.lblCSHTMLTemplate = new System.Windows.Forms.Label();
            this.lblBlockTypeNames = new System.Windows.Forms.Label();
            this.txtBlockTypeNames = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(28, 41);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 41);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Select Path";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnGenerateBlockTypes
            // 
            this.btnGenerateBlockTypes.Enabled = false;
            this.btnGenerateBlockTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateBlockTypes.Location = new System.Drawing.Point(268, 998);
            this.btnGenerateBlockTypes.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateBlockTypes.Name = "btnGenerateBlockTypes";
            this.btnGenerateBlockTypes.Size = new System.Drawing.Size(276, 41);
            this.btnGenerateBlockTypes.TabIndex = 2;
            this.btnGenerateBlockTypes.Text = "Create Block Types";
            this.btnGenerateBlockTypes.UseVisualStyleBackColor = true;
            this.btnGenerateBlockTypes.Click += new System.EventHandler(this.CreateBlockContent_Click);
            // 
            // lblPath
            // 
            this.lblPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPath.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(148, 41);
            this.lblPath.Margin = new System.Windows.Forms.Padding(2);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(396, 42);
            this.lblPath.TabIndex = 6;
            this.lblPath.Text = "No path selected";
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fbd
            // 
            this.fbd.ShowNewFolderButton = false;
            // 
            // txtCSTemplate
            // 
            this.txtCSTemplate.Location = new System.Drawing.Point(28, 455);
            this.txtCSTemplate.Multiline = true;
            this.txtCSTemplate.Name = "txtCSTemplate";
            this.txtCSTemplate.Size = new System.Drawing.Size(776, 213);
            this.txtCSTemplate.TabIndex = 7;
            // 
            // txtCSHTMLTemplate
            // 
            this.txtCSHTMLTemplate.Location = new System.Drawing.Point(28, 722);
            this.txtCSHTMLTemplate.Multiline = true;
            this.txtCSHTMLTemplate.Name = "txtCSHTMLTemplate";
            this.txtCSHTMLTemplate.Size = new System.Drawing.Size(776, 238);
            this.txtCSHTMLTemplate.TabIndex = 8;
            // 
            // lblCSTemplate
            // 
            this.lblCSTemplate.AutoSize = true;
            this.lblCSTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSTemplate.Location = new System.Drawing.Point(24, 422);
            this.lblCSTemplate.Name = "lblCSTemplate";
            this.lblCSTemplate.Size = new System.Drawing.Size(145, 20);
            this.lblCSTemplate.TabIndex = 9;
            this.lblCSTemplate.Text = ".cs File Template";
            // 
            // lblCSHTMLTemplate
            // 
            this.lblCSHTMLTemplate.AutoSize = true;
            this.lblCSHTMLTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSHTMLTemplate.Location = new System.Drawing.Point(24, 688);
            this.lblCSHTMLTemplate.Name = "lblCSHTMLTemplate";
            this.lblCSHTMLTemplate.Size = new System.Drawing.Size(253, 20);
            this.lblCSHTMLTemplate.TabIndex = 10;
            this.lblCSHTMLTemplate.Text = "Razor View (.cshtml) Template";
            // 
            // lblBlockTypeNames
            // 
            this.lblBlockTypeNames.AutoSize = true;
            this.lblBlockTypeNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlockTypeNames.Location = new System.Drawing.Point(24, 120);
            this.lblBlockTypeNames.Name = "lblBlockTypeNames";
            this.lblBlockTypeNames.Size = new System.Drawing.Size(147, 20);
            this.lblBlockTypeNames.TabIndex = 12;
            this.lblBlockTypeNames.Text = "Block Type Name";
            // 
            // txtBlockTypeNames
            // 
            this.txtBlockTypeNames.Location = new System.Drawing.Point(28, 153);
            this.txtBlockTypeNames.Multiline = true;
            this.txtBlockTypeNames.Name = "txtBlockTypeNames";
            this.txtBlockTypeNames.Size = new System.Drawing.Size(776, 234);
            this.txtBlockTypeNames.TabIndex = 11;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(832, 1078);
            this.Controls.Add(this.lblBlockTypeNames);
            this.Controls.Add(this.txtBlockTypeNames);
            this.Controls.Add(this.lblCSHTMLTemplate);
            this.Controls.Add(this.lblCSTemplate);
            this.Controls.Add(this.txtCSHTMLTemplate);
            this.Controls.Add(this.txtCSTemplate);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnGenerateBlockTypes);
            this.Controls.Add(this.btnBrowse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Generate Content Types";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnGenerateBlockTypes;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.TextBox txtCSTemplate;
        private System.Windows.Forms.TextBox txtCSHTMLTemplate;
        private System.Windows.Forms.Label lblCSTemplate;
        private System.Windows.Forms.Label lblCSHTMLTemplate;
        private System.Windows.Forms.Label lblBlockTypeNames;
        private System.Windows.Forms.TextBox txtBlockTypeNames;
    }
}

