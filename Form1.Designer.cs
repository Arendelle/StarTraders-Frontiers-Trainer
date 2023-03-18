namespace STF_Trainer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTurn = new System.Windows.Forms.TextBox();
            this.buttonTurn = new System.Windows.Forms.Button();
            this.gameLoop = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "游戏进程状态：";
            // 
            // textBoxTurn
            // 
            this.textBoxTurn.Location = new System.Drawing.Point(98, 69);
            this.textBoxTurn.Name = "textBoxTurn";
            this.textBoxTurn.Size = new System.Drawing.Size(89, 25);
            this.textBoxTurn.TabIndex = 1;
            // 
            // buttonTurn
            // 
            this.buttonTurn.Location = new System.Drawing.Point(215, 65);
            this.buttonTurn.Name = "buttonTurn";
            this.buttonTurn.Size = new System.Drawing.Size(91, 28);
            this.buttonTurn.TabIndex = 2;
            this.buttonTurn.Text = "修改";
            this.buttonTurn.UseVisualStyleBackColor = true;
            this.buttonTurn.Click += new System.EventHandler(this.buttonTurn_Click);
            // 
            // gameLoop
            // 
            this.gameLoop.Interval = 300;
            this.gameLoop.Tick += new System.EventHandler(this.gameLoop_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "回合";
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(203, 319);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(130, 27);
            this.buttonSaveFile.TabIndex = 4;
            this.buttonSaveFile.Text = "打开存档目录...";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(345, 358);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonTurn);
            this.Controls.Add(this.textBoxTurn);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "ST:F Trainer by Arendelle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTurn;
        private System.Windows.Forms.Button buttonTurn;
        private System.Windows.Forms.Timer gameLoop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSaveFile;
    }
}

