namespace Lootboy
{
    partial class LootBoyForm
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
            this.token = new System.Windows.Forms.TextBox();
            this.TokenBtn = new System.Windows.Forms.Button();
            this.gemLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.coinLbl = new System.Windows.Forms.Label();
            this.buyFSA = new System.Windows.Forms.Button();
            this.getLoot = new System.Windows.Forms.Button();
            this.buyAny = new System.Windows.Forms.Button();
            this.purTb = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // token
            // 
            this.token.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.token.Location = new System.Drawing.Point(12, 12);
            this.token.Name = "token";
            this.token.Size = new System.Drawing.Size(267, 20);
            this.token.TabIndex = 0;
            // 
            // TokenBtn
            // 
            this.TokenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TokenBtn.Location = new System.Drawing.Point(288, 10);
            this.TokenBtn.Name = "TokenBtn";
            this.TokenBtn.Size = new System.Drawing.Size(128, 23);
            this.TokenBtn.TabIndex = 1;
            this.TokenBtn.Text = "Вход по токену";
            this.TokenBtn.UseVisualStyleBackColor = true;
            this.TokenBtn.Click += new System.EventHandler(this.TokenBtn_Click);
            // 
            // gemLbl
            // 
            this.gemLbl.AutoSize = true;
            this.gemLbl.Location = new System.Drawing.Point(12, 121);
            this.gemLbl.Name = "gemLbl";
            this.gemLbl.Size = new System.Drawing.Size(70, 13);
            this.gemLbl.TabIndex = 7;
            this.gemLbl.Text = "Кристаллов:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "SteamLogin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "SteamPass";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(81, 49);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(131, 20);
            this.tbName.TabIndex = 10;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(81, 73);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(131, 20);
            this.tbPass.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(233, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Вход Steam";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // coinLbl
            // 
            this.coinLbl.AutoSize = true;
            this.coinLbl.Location = new System.Drawing.Point(12, 139);
            this.coinLbl.Name = "coinLbl";
            this.coinLbl.Size = new System.Drawing.Size(42, 13);
            this.coinLbl.TabIndex = 13;
            this.coinLbl.Text = "Монет:";
            // 
            // buyFSA
            // 
            this.buyFSA.Location = new System.Drawing.Point(2, 171);
            this.buyFSA.Name = "buyFSA";
            this.buyFSA.Size = new System.Drawing.Size(151, 23);
            this.buyFSA.TabIndex = 14;
            this.buyFSA.Text = "Купить и открыть FSA";
            this.buyFSA.UseVisualStyleBackColor = true;
            this.buyFSA.Click += new System.EventHandler(this.button2_Click);
            // 
            // getLoot
            // 
            this.getLoot.Location = new System.Drawing.Point(233, 116);
            this.getLoot.Name = "getLoot";
            this.getLoot.Size = new System.Drawing.Size(128, 23);
            this.getLoot.TabIndex = 15;
            this.getLoot.Text = "Получить награду";
            this.getLoot.UseVisualStyleBackColor = true;
            this.getLoot.Click += new System.EventHandler(this.button3_Click);
            // 
            // buyAny
            // 
            this.buyAny.Location = new System.Drawing.Point(2, 202);
            this.buyAny.Name = "buyAny";
            this.buyAny.Size = new System.Drawing.Size(151, 23);
            this.buyAny.TabIndex = 16;
            this.buyAny.Text = "Купить и открыть: ";
            this.buyAny.UseVisualStyleBackColor = true;
            this.buyAny.Click += new System.EventHandler(this.buyAny_Click);
            // 
            // purTb
            // 
            this.purTb.Location = new System.Drawing.Point(168, 204);
            this.purTb.Name = "purTb";
            this.purTb.Size = new System.Drawing.Size(173, 20);
            this.purTb.TabIndex = 17;
            // 
            // openFileDialog
            // 
            this.openFileDialog.CheckFileExists = false;
            this.openFileDialog.FileName = "loot.txt";
            // 
            // LootBoyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 237);
            this.Controls.Add(this.purTb);
            this.Controls.Add(this.buyAny);
            this.Controls.Add(this.getLoot);
            this.Controls.Add(this.buyFSA);
            this.Controls.Add(this.coinLbl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gemLbl);
            this.Controls.Add(this.TokenBtn);
            this.Controls.Add(this.token);
            this.Name = "LootBoyForm";
            this.Text = "Lootboy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox token;
        private System.Windows.Forms.Button TokenBtn;
        private System.Windows.Forms.Label gemLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label coinLbl;
        private System.Windows.Forms.Button buyFSA;
        private System.Windows.Forms.Button getLoot;
        private System.Windows.Forms.Button buyAny;
        private System.Windows.Forms.TextBox purTb;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

