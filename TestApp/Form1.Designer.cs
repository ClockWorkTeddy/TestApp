namespace TestApp
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.gbGroups = new System.Windows.Forms.GroupBox();
            this.dgGroups = new System.Windows.Forms.DataGridView();
            this.gbUsers = new System.Windows.Forms.GroupBox();
            this.dgUsers = new System.Windows.Forms.DataGridView();
            this.gbAdd = new System.Windows.Forms.GroupBox();
            this.bttAdd = new System.Windows.Forms.Button();
            this.cbGroups = new System.Windows.Forms.ComboBox();
            this.tbFio = new System.Windows.Forms.TextBox();
            this.lblGroups = new System.Windows.Forms.Label();
            this.lblFio = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gbGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroups)).BeginInit();
            this.gbUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).BeginInit();
            this.gbAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbGroups
            // 
            this.gbGroups.Controls.Add(this.dgGroups);
            this.gbGroups.Location = new System.Drawing.Point(12, 12);
            this.gbGroups.Name = "gbGroups";
            this.gbGroups.Size = new System.Drawing.Size(731, 189);
            this.gbGroups.TabIndex = 0;
            this.gbGroups.TabStop = false;
            this.gbGroups.Text = "Группы";
            // 
            // dgGroups
            // 
            this.dgGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGroups.Location = new System.Drawing.Point(6, 19);
            this.dgGroups.Name = "dgGroups";
            this.dgGroups.Size = new System.Drawing.Size(719, 164);
            this.dgGroups.TabIndex = 0;
            // 
            // gbUsers
            // 
            this.gbUsers.Controls.Add(this.dgUsers);
            this.gbUsers.Location = new System.Drawing.Point(12, 207);
            this.gbUsers.Name = "gbUsers";
            this.gbUsers.Size = new System.Drawing.Size(731, 173);
            this.gbUsers.TabIndex = 1;
            this.gbUsers.TabStop = false;
            this.gbUsers.Text = "Сотрудники";
            // 
            // dgUsers
            // 
            this.dgUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUsers.Location = new System.Drawing.Point(6, 19);
            this.dgUsers.Name = "dgUsers";
            this.dgUsers.Size = new System.Drawing.Size(719, 148);
            this.dgUsers.TabIndex = 1;
            this.dgUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgUsers_MouseDown);
            // 
            // gbAdd
            // 
            this.gbAdd.Controls.Add(this.bttAdd);
            this.gbAdd.Controls.Add(this.cbGroups);
            this.gbAdd.Controls.Add(this.tbFio);
            this.gbAdd.Controls.Add(this.lblGroups);
            this.gbAdd.Controls.Add(this.lblFio);
            this.gbAdd.Location = new System.Drawing.Point(12, 386);
            this.gbAdd.Name = "gbAdd";
            this.gbAdd.Size = new System.Drawing.Size(731, 52);
            this.gbAdd.TabIndex = 2;
            this.gbAdd.TabStop = false;
            this.gbAdd.Text = "Добавить";
            // 
            // bttAdd
            // 
            this.bttAdd.Location = new System.Drawing.Point(423, 19);
            this.bttAdd.Name = "bttAdd";
            this.bttAdd.Size = new System.Drawing.Size(75, 23);
            this.bttAdd.TabIndex = 4;
            this.bttAdd.Text = "Добавить";
            this.bttAdd.UseVisualStyleBackColor = true;
            this.bttAdd.Click += new System.EventHandler(this.bttAdd_Click);
            // 
            // cbGroups
            // 
            this.cbGroups.FormattingEnabled = true;
            this.cbGroups.Location = new System.Drawing.Point(296, 20);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Size = new System.Drawing.Size(121, 21);
            this.cbGroups.TabIndex = 3;
            // 
            // tbFio
            // 
            this.tbFio.Location = new System.Drawing.Point(49, 22);
            this.tbFio.Name = "tbFio";
            this.tbFio.Size = new System.Drawing.Size(199, 20);
            this.tbFio.TabIndex = 2;
            // 
            // lblGroups
            // 
            this.lblGroups.AutoSize = true;
            this.lblGroups.Location = new System.Drawing.Point(254, 22);
            this.lblGroups.Name = "lblGroups";
            this.lblGroups.Size = new System.Drawing.Size(42, 13);
            this.lblGroups.TabIndex = 1;
            this.lblGroups.Text = "Группа";
            // 
            // lblFio
            // 
            this.lblFio.AutoSize = true;
            this.lblFio.Location = new System.Drawing.Point(7, 23);
            this.lblFio.Name = "lblFio";
            this.lblFio.Size = new System.Drawing.Size(34, 13);
            this.lblFio.TabIndex = 0;
            this.lblFio.Text = "ФИО";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbAdd);
            this.Controls.Add(this.gbUsers);
            this.Controls.Add(this.gbGroups);
            this.Name = "MainForm";
            this.Text = "Мониторинг сотрудников";
            this.gbGroups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGroups)).EndInit();
            this.gbUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).EndInit();
            this.gbAdd.ResumeLayout(false);
            this.gbAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGroups;
        private System.Windows.Forms.DataGridView dgGroups;
        private System.Windows.Forms.GroupBox gbUsers;
        private System.Windows.Forms.DataGridView dgUsers;
        private System.Windows.Forms.GroupBox gbAdd;
        private System.Windows.Forms.Button bttAdd;
        private System.Windows.Forms.ComboBox cbGroups;
        private System.Windows.Forms.TextBox tbFio;
        private System.Windows.Forms.Label lblGroups;
        private System.Windows.Forms.Label lblFio;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

