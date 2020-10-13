
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
            this.flatButton1 = new First.FlatButton();
            this.SuspendLayout();
            // 
            // flatButton1
            // 
            this.flatButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.flatButton1.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.flatButton1.ClickImage = null;
            this.flatButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.flatButton1.ForeColor = System.Drawing.Color.White;
            this.flatButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.flatButton1.HoverImage = null;
            this.flatButton1.Location = new System.Drawing.Point(939, 795);
            this.flatButton1.Name = "flatButton1";
            this.flatButton1.Size = new System.Drawing.Size(294, 147);
            this.flatButton1.TabIndex = 2;
            this.flatButton1.Text = "flatButton1";
            this.flatButton1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1516, 1119);
            this.Controls.Add(this.flatButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.flatButton1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private FlatButton flatButton1;
    }
}

