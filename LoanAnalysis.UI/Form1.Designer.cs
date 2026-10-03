namespace LoanAnalysis.UI
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
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            відкритиCSVToolStripMenuItem = new ToolStripMenuItem();
            експортToolStripMenuItem = new ToolStripMenuItem();
            вихідToolStripMenuItem = new ToolStripMenuItem();
            звітиToolStripMenuItem = new ToolStripMenuItem();
            згенеруватиXLSXзвітToolStripMenuItem = new ToolStripMenuItem();
            згенеруватиDOCXзвітToolStripMenuItem = new ToolStripMenuItem();
            проПрограмуToolStripMenuItem = new ToolStripMenuItem();
            інформаціяToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            cmbIntentFilter = new ComboBox();
            btnApplyFilter = new Button();
            btnResetFilter = new Button();
            dgvData = new DataGridView();
            lblStatus = new StatusStrip();
            panel1 = new Panel();
            button1 = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, звітиToolStripMenuItem, проПрограмуToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { відкритиCSVToolStripMenuItem, експортToolStripMenuItem, вихідToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // відкритиCSVToolStripMenuItem
            // 
            відкритиCSVToolStripMenuItem.Name = "відкритиCSVToolStripMenuItem";
            відкритиCSVToolStripMenuItem.Size = new Size(183, 26);
            відкритиCSVToolStripMenuItem.Text = "Відкрити CSV";
            відкритиCSVToolStripMenuItem.Click += відкритиCSVToolStripMenuItem_Click;
            // 
            // експортToolStripMenuItem
            // 
            експортToolStripMenuItem.Name = "експортToolStripMenuItem";
            експортToolStripMenuItem.Size = new Size(183, 26);
            експортToolStripMenuItem.Text = "Експорт";
            експортToolStripMenuItem.Click += експортToolStripMenuItem_Click;
            // 
            // вихідToolStripMenuItem
            // 
            вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            вихідToolStripMenuItem.Size = new Size(183, 26);
            вихідToolStripMenuItem.Text = "Вихід";
            вихідToolStripMenuItem.Click += вихідToolStripMenuItem_Click;
            // 
            // звітиToolStripMenuItem
            // 
            звітиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { згенеруватиXLSXзвітToolStripMenuItem, згенеруватиDOCXзвітToolStripMenuItem });
            звітиToolStripMenuItem.Name = "звітиToolStripMenuItem";
            звітиToolStripMenuItem.Size = new Size(58, 24);
            звітиToolStripMenuItem.Text = "Звіти";
            звітиToolStripMenuItem.Click += звітиToolStripMenuItem_Click;
            // 
            // згенеруватиXLSXзвітToolStripMenuItem
            // 
            згенеруватиXLSXзвітToolStripMenuItem.Name = "згенеруватиXLSXзвітToolStripMenuItem";
            згенеруватиXLSXзвітToolStripMenuItem.Size = new Size(253, 26);
            згенеруватиXLSXзвітToolStripMenuItem.Text = "Згенерувати XLSX-звіт";
            згенеруватиXLSXзвітToolStripMenuItem.Click += згенеруватиXLSXзвітToolStripMenuItem_Click;
            // 
            // згенеруватиDOCXзвітToolStripMenuItem
            // 
            згенеруватиDOCXзвітToolStripMenuItem.Name = "згенеруватиDOCXзвітToolStripMenuItem";
            згенеруватиDOCXзвітToolStripMenuItem.Size = new Size(253, 26);
            згенеруватиDOCXзвітToolStripMenuItem.Text = "Згенерувати DOCX-звіт";
            згенеруватиDOCXзвітToolStripMenuItem.Click += згенеруватиDOCXзвітToolStripMenuItem_Click;
            // 
            // проПрограмуToolStripMenuItem
            // 
            проПрограмуToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { інформаціяToolStripMenuItem });
            проПрограмуToolStripMenuItem.Name = "проПрограмуToolStripMenuItem";
            проПрограмуToolStripMenuItem.Size = new Size(124, 24);
            проПрограмуToolStripMenuItem.Text = "Про програму";
            // 
            // інформаціяToolStripMenuItem
            // 
            інформаціяToolStripMenuItem.Name = "інформаціяToolStripMenuItem";
            інформаціяToolStripMenuItem.Size = new Size(173, 26);
            інформаціяToolStripMenuItem.Text = "Інформація";
            інформаціяToolStripMenuItem.Click += інформаціяToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Location = new Point(15, 68);
            label1.Name = "label1";
            label1.Size = new Size(185, 20);
            label1.TabIndex = 1;
            label1.Text = "Фільтр за метою кредиту:";
            // 
            // cmbIntentFilter
            // 
            cmbIntentFilter.FormattingEnabled = true;
            cmbIntentFilter.Location = new Point(0, 3);
            cmbIntentFilter.Name = "cmbIntentFilter";
            cmbIntentFilter.Size = new Size(151, 28);
            cmbIntentFilter.TabIndex = 2;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.BackColor = Color.ForestGreen;
            btnApplyFilter.Location = new Point(0, 64);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(128, 29);
            btnApplyFilter.TabIndex = 3;
            btnApplyFilter.Text = "Застосувати";
            btnApplyFilter.UseVisualStyleBackColor = false;
            btnApplyFilter.Click += btnApplyFilter_Click;
            // 
            // btnResetFilter
            // 
            btnResetFilter.BackColor = Color.Silver;
            btnResetFilter.Location = new Point(0, 99);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(94, 29);
            btnResetFilter.TabIndex = 4;
            btnResetFilter.Text = "Скинути";
            btnResetFilter.UseVisualStyleBackColor = false;
            btnResetFilter.Click += btnResetFilter_Click;
            // 
            // dgvData
            // 
            dgvData.BackgroundColor = Color.White;
            dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Location = new Point(243, 31);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 51;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(545, 394);
            dgvData.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.ImageScalingSize = new Size(20, 20);
            lblStatus.Location = new Point(0, 428);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(800, 22);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Готово. Записи не завантажено";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(cmbIntentFilter);
            panel1.Controls.Add(btnApplyFilter);
            panel1.Controls.Add(btnResetFilter);
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(12, 91);
            panel1.Name = "panel1";
            panel1.Size = new Size(211, 174);
            panel1.TabIndex = 7;
            // 
            // button1
            // 
            button1.BackColor = Color.LightBlue;
            button1.Location = new Point(0, 134);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 8;
            button1.Text = "Графіки";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(lblStatus);
            Controls.Add(dgvData);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Аналіз Кредитного Портфеля v1.0";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem відкритиCSVToolStripMenuItem;
        private ToolStripMenuItem експортToolStripMenuItem;
        private ToolStripMenuItem вихідToolStripMenuItem;
        private ToolStripMenuItem звітиToolStripMenuItem;
        private ToolStripMenuItem згенеруватиXLSXзвітToolStripMenuItem;
        private ToolStripMenuItem згенеруватиDOCXзвітToolStripMenuItem;
        private ToolStripMenuItem проПрограмуToolStripMenuItem;
        private ToolStripMenuItem інформаціяToolStripMenuItem;
        private Label label1;
        private ComboBox cmbIntentFilter;
        private Button btnApplyFilter;
        private Button btnResetFilter;
        private DataGridView dgvData;
        private StatusStrip lblStatus;
        private Panel panel1;
        private Button button1;
    }
}
