namespace Evolution3
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btCreateDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btCreateDB
            // 
            this.btCreateDB.Location = new System.Drawing.Point(12, 12);
            this.btCreateDB.Name = "btCreateDB";
            this.btCreateDB.Size = new System.Drawing.Size(75, 23);
            this.btCreateDB.TabIndex = 0;
            this.btCreateDB.Text = "Create";
            this.btCreateDB.UseVisualStyleBackColor = true;
            this.btCreateDB.Click += new System.EventHandler(this.btCreateDB_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btCreateDB);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCreateDB;
    }
}

