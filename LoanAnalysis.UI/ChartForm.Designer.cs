namespace LoanAnalysis.UI
{
    partial class ChartForm
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
            panel1 = new Panel();
            btnClose = new Button();
            btnSavePng = new Button();
            cmbChartType = new ComboBox();
            label1 = new Label();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnSavePng);
            panel1.Controls.Add(cmbChartType);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 90);
            panel1.TabIndex = 0;
            panel1.UseWaitCursor = true;
            panel1.Paint += panel1_Paint;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(346, 31);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 3;
            btnClose.Text = "Закрити";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.UseWaitCursor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSavePng
            // 
            btnSavePng.Location = new Point(184, 31);
            btnSavePng.Name = "btnSavePng";
            btnSavePng.Size = new Size(146, 29);
            btnSavePng.TabIndex = 2;
            btnSavePng.Text = "Зберегти в PNG";
            btnSavePng.UseVisualStyleBackColor = true;
            btnSavePng.UseWaitCursor = true;
            btnSavePng.Click += btnSavePng_Click;
            // 
            // cmbChartType
            // 
            cmbChartType.FormattingEnabled = true;
            cmbChartType.Items.AddRange(new object[] { "Кругова (Статус схвалення)", "Стовпчикова (Розподіл за метою)", "Точкова (Дохід vs Сума)" });
            cmbChartType.Location = new Point(12, 32);
            cmbChartType.Name = "cmbChartType";
            cmbChartType.Size = new Size(151, 28);
            cmbChartType.TabIndex = 1;
            cmbChartType.UseWaitCursor = true;
            cmbChartType.SelectedIndexChanged += cmbChartType_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(94, 20);
            label1.TabIndex = 0;
            label1.Text = "Тип графіка:";
            label1.UseWaitCursor = true;
            // 
            // formsPlot1
            // 
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 90);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(800, 360);
            formsPlot1.TabIndex = 1;
            // 
            // ChartForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(formsPlot1);
            Controls.Add(panel1);
            Name = "ChartForm";
            Text = "ChartForm";
            Load += ChartForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Button btnClose;
        private Button btnSavePng;
        private ComboBox cmbChartType;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
    }
}