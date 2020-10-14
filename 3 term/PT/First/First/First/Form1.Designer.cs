
namespace First
{
    partial class Form1 : FlatForm
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
            if(disposing && (components != null))
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
            this.copyButton = new First.FlatButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.edited = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pasteButton = new First.FlatButton();
            this.deleteButton = new First.FlatButton();
            this.archiveButton = new First.FlatButton();
            this.extractButton = new First.FlatButton();
            this.newFileButton = new First.FlatButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.newFolderButton = new First.FlatButton();
            this.cutButton = new First.FlatButton();
            this.SuspendLayout();
            // 
            // copyButton
            // 
            this.copyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.copyButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.copyButton.ClickImage = null;
            this.copyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.copyButton.ForeColor = System.Drawing.Color.White;
            this.copyButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.copyButton.HoverImage = null;
            this.copyButton.Location = new System.Drawing.Point(710, 949);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(281, 86);
            this.copyButton.TabIndex = 2;
            this.copyButton.Text = "Copy selected";
            this.copyButton.UseVisualStyleBackColor = false;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.Black;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.edited,
            this.type,
            this.size});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.listView1.ForeColor = System.Drawing.SystemColors.Window;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(12, 99);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1894, 815);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // name
            // 
            this.name.Text = "Name:";
            this.name.Width = 211;
            // 
            // edited
            // 
            this.edited.Text = "Edited:";
            this.edited.Width = 198;
            // 
            // type
            // 
            this.type.Text = "Type:";
            this.type.Width = 125;
            // 
            // size
            // 
            this.size.Text = "Size:";
            this.size.Width = 308;
            // 
            // pasteButton
            // 
            this.pasteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.pasteButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pasteButton.ClickImage = null;
            this.pasteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.pasteButton.ForeColor = System.Drawing.Color.White;
            this.pasteButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.pasteButton.HoverImage = null;
            this.pasteButton.Location = new System.Drawing.Point(710, 1071);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(281, 86);
            this.pasteButton.TabIndex = 4;
            this.pasteButton.Text = "Paste";
            this.pasteButton.UseVisualStyleBackColor = false;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.deleteButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.deleteButton.ClickImage = null;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.deleteButton.HoverImage = null;
            this.deleteButton.Location = new System.Drawing.Point(361, 949);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(281, 86);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete selected";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // archiveButton
            // 
            this.archiveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.archiveButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.archiveButton.ClickImage = null;
            this.archiveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.archiveButton.ForeColor = System.Drawing.Color.White;
            this.archiveButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.archiveButton.HoverImage = null;
            this.archiveButton.Location = new System.Drawing.Point(1059, 949);
            this.archiveButton.Name = "archiveButton";
            this.archiveButton.Size = new System.Drawing.Size(281, 86);
            this.archiveButton.TabIndex = 6;
            this.archiveButton.Text = "Archive Folder";
            this.archiveButton.UseVisualStyleBackColor = false;
            this.archiveButton.Click += new System.EventHandler(this.archiveButton_Click);
            // 
            // extractButton
            // 
            this.extractButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.extractButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.extractButton.ClickImage = null;
            this.extractButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.extractButton.ForeColor = System.Drawing.Color.White;
            this.extractButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.extractButton.HoverImage = null;
            this.extractButton.Location = new System.Drawing.Point(1059, 1071);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(281, 86);
            this.extractButton.TabIndex = 7;
            this.extractButton.Text = "Extract";
            this.extractButton.UseVisualStyleBackColor = false;
            this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
            // 
            // newFileButton
            // 
            this.newFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.newFileButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.newFileButton.ClickImage = null;
            this.newFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.newFileButton.ForeColor = System.Drawing.Color.White;
            this.newFileButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.newFileButton.HoverImage = null;
            this.newFileButton.Location = new System.Drawing.Point(12, 949);
            this.newFileButton.Name = "newFileButton";
            this.newFileButton.Size = new System.Drawing.Size(281, 86);
            this.newFileButton.TabIndex = 8;
            this.newFileButton.Text = "New File";
            this.newFileButton.UseVisualStyleBackColor = false;
            this.newFileButton.Click += new System.EventHandler(this.newFileButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(12, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1894, 47);
            this.textBox1.TabIndex = 9;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // newFolderButton
            // 
            this.newFolderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.newFolderButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.newFolderButton.ClickImage = null;
            this.newFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.newFolderButton.ForeColor = System.Drawing.Color.White;
            this.newFolderButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.newFolderButton.HoverImage = null;
            this.newFolderButton.Location = new System.Drawing.Point(12, 1071);
            this.newFolderButton.Name = "newFolderButton";
            this.newFolderButton.Size = new System.Drawing.Size(281, 86);
            this.newFolderButton.TabIndex = 10;
            this.newFolderButton.Text = "New Folder";
            this.newFolderButton.UseVisualStyleBackColor = false;
            this.newFolderButton.Click += new System.EventHandler(this.newFolderButton_Click);
            // 
            // cutButton
            // 
            this.cutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.cutButton.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cutButton.ClickImage = null;
            this.cutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cutButton.ForeColor = System.Drawing.Color.White;
            this.cutButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.cutButton.HoverImage = null;
            this.cutButton.Location = new System.Drawing.Point(361, 1071);
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(281, 86);
            this.cutButton.TabIndex = 11;
            this.cutButton.Text = "Cut Selected";
            this.cutButton.UseVisualStyleBackColor = false;
            this.cutButton.Click += new System.EventHandler(this.cutButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1922, 1188);
            this.Controls.Add(this.cutButton);
            this.Controls.Add(this.newFolderButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.newFileButton);
            this.Controls.Add(this.extractButton);
            this.Controls.Add(this.archiveButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.copyButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.copyButton, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.pasteButton, 0);
            this.Controls.SetChildIndex(this.deleteButton, 0);
            this.Controls.SetChildIndex(this.archiveButton, 0);
            this.Controls.SetChildIndex(this.extractButton, 0);
            this.Controls.SetChildIndex(this.newFileButton, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.newFolderButton, 0);
            this.Controls.SetChildIndex(this.cutButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlatButton copyButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader edited;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader size;
        private FlatButton pasteButton;
        private FlatButton deleteButton;
        private FlatButton archiveButton;
        private FlatButton extractButton;
        private FlatButton newFileButton;
        private System.Windows.Forms.TextBox textBox1;
        private FlatButton newFolderButton;
        private FlatButton cutButton;
    }
}

