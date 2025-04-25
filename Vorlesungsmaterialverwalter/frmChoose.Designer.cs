namespace Vorlesungsmaterialverwalter
{
    partial class frmChoose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChoose));
            this.label1 = new System.Windows.Forms.Label();
            this.btnAutomatic = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 181);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "OR";
            // 
            // btnAutomatic
            // 
            this.btnAutomatic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnAutomatic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutomatic.FlatAppearance.BorderSize = 0;
            this.btnAutomatic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutomatic.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutomatic.ForeColor = System.Drawing.Color.White;
            this.btnAutomatic.Location = new System.Drawing.Point(57, 110);
            this.btnAutomatic.Margin = new System.Windows.Forms.Padding(2);
            this.btnAutomatic.Name = "btnAutomatic";
            this.btnAutomatic.Size = new System.Drawing.Size(270, 47);
            this.btnAutomatic.TabIndex = 1;
            this.btnAutomatic.Text = "automatic Detection";
            this.btnAutomatic.UseVisualStyleBackColor = false;
            // 
            // btnManual
            // 
            this.btnManual.BackColor = System.Drawing.Color.Gray;
            this.btnManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManual.FlatAppearance.BorderSize = 0;
            this.btnManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManual.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManual.ForeColor = System.Drawing.Color.White;
            this.btnManual.Location = new System.Drawing.Point(57, 225);
            this.btnManual.Margin = new System.Windows.Forms.Padding(2);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(270, 47);
            this.btnManual.TabIndex = 0;
            this.btnManual.Text = "manual Upload";
            this.btnManual.UseVisualStyleBackColor = false;
            // 
            // frmChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(388, 404);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAutomatic);
            this.Controls.Add(this.btnManual);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(625, 200);
            this.Name = "frmChoose";
            this.Text = "Choose a Methode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAutomatic;
        private System.Windows.Forms.Button btnManual;
    }
}