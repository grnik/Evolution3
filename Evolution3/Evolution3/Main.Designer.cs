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
            this.btInitDB = new System.Windows.Forms.Button();
            this.btRun = new System.Windows.Forms.Button();
            this.btExecute = new System.Windows.Forms.Button();
            this.btDBFilling = new System.Windows.Forms.Button();
            this.btClearResult = new System.Windows.Forms.Button();
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
            // btInitDB
            // 
            this.btInitDB.Location = new System.Drawing.Point(12, 42);
            this.btInitDB.Name = "btInitDB";
            this.btInitDB.Size = new System.Drawing.Size(75, 23);
            this.btInitDB.TabIndex = 1;
            this.btInitDB.Text = "Init DB";
            this.btInitDB.UseVisualStyleBackColor = true;
            this.btInitDB.Click += new System.EventHandler(this.btInitDB_Click);
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(13, 104);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(75, 23);
            this.btRun.TabIndex = 2;
            this.btRun.Text = "Run";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // btExecute
            // 
            this.btExecute.Location = new System.Drawing.Point(152, 104);
            this.btExecute.Name = "btExecute";
            this.btExecute.Size = new System.Drawing.Size(75, 23);
            this.btExecute.TabIndex = 3;
            this.btExecute.Text = "Execute";
            this.btExecute.UseVisualStyleBackColor = true;
            this.btExecute.Click += new System.EventHandler(this.btExecute_Click);
            // 
            // btDBFilling
            // 
            this.btDBFilling.Location = new System.Drawing.Point(93, 42);
            this.btDBFilling.Name = "btDBFilling";
            this.btDBFilling.Size = new System.Drawing.Size(75, 23);
            this.btDBFilling.TabIndex = 4;
            this.btDBFilling.Text = "DB filling";
            this.btDBFilling.UseVisualStyleBackColor = true;
            this.btDBFilling.Click += new System.EventHandler(this.btDBFilling_Click);
            // 
            // btClearResult
            // 
            this.btClearResult.Location = new System.Drawing.Point(175, 42);
            this.btClearResult.Name = "btClearResult";
            this.btClearResult.Size = new System.Drawing.Size(75, 23);
            this.btClearResult.TabIndex = 5;
            this.btClearResult.Text = "Clear Result";
            this.btClearResult.UseVisualStyleBackColor = true;
            this.btClearResult.Click += new System.EventHandler(this.btClearResult_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btClearResult);
            this.Controls.Add(this.btDBFilling);
            this.Controls.Add(this.btExecute);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.btInitDB);
            this.Controls.Add(this.btCreateDB);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCreateDB;
        private System.Windows.Forms.Button btInitDB;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.Button btExecute;
        private System.Windows.Forms.Button btDBFilling;
        private System.Windows.Forms.Button btClearResult;
    }
}

