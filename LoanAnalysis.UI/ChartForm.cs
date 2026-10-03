using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LoanAnalysis.Domain;

namespace LoanAnalysis.UI
{
    public partial class ChartForm : Form
    {
        private readonly List<LoanApplication> _data;

        public ChartForm(List<LoanApplication> data)
        {
            InitializeComponent();
            _data = data ?? new List<LoanApplication>();
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            if (cmbChartType.Items.Count > 0)
            {
                cmbChartType.SelectedIndex = 0;
            }
            BuildChart();
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildChart();
        }

        private void BuildChart()
        {
            formsPlot1.Plot.Clear();

            if (!_data.Any()) return;

            formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
            formsPlot1.Plot.XLabel("");
            formsPlot1.Plot.YLabel("");

            int selectedIndex = cmbChartType.SelectedIndex;

            if (selectedIndex == 0) 
            {
                int approved = _data.Count(x => x.ApprovalStatus == 1);
                int rejected = _data.Count(x => x.ApprovalStatus == 0);

                double[] values = { approved, rejected };
                var pie = formsPlot1.Plot.Add.Pie(values);
                pie.ExplodeFraction = 0.05;
                formsPlot1.Plot.Title("Співвідношення схвалених та відхилених заявок");

                formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
                formsPlot1.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.EmptyTickGenerator();
            }
            else if (selectedIndex == 1) 
            {
                formsPlot1.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
                var grouped = _data.GroupBy(x => string.IsNullOrEmpty(x.Intent) ? "Невідомо" : x.Intent)
                                  .Select(g => new { Intent = g.Key, Count = (double)g.Count() })
                                  .ToList();

                double[] values = grouped.Select(g => g.Count).ToArray();
                formsPlot1.Plot.Add.Bars(values);

                formsPlot1.Plot.Title("Кількість заявок за метою кредиту");
                formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                    Enumerable.Range(0, grouped.Count).Select((i) => new ScottPlot.Tick(i, grouped[i].Intent)).ToArray()
                );
            }
            else if (selectedIndex == 2) 
            {
                formsPlot1.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
                double[] xs = _data.Select(x => (double)(x.Applicant?.Income ?? 0)).ToArray();
                double[] ys = _data.Select(x => (double)x.Amount).ToArray();

                formsPlot1.Plot.Add.ScatterLine(xs, ys);
                formsPlot1.Plot.Title("Залежність суми кредиту від доходу");
                formsPlot1.Plot.XLabel("Дохід заявника");
                formsPlot1.Plot.YLabel("Сума кредиту");
            }

            formsPlot1.Refresh();
        }

        private void btnSavePng_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PNG Зображення (*.png)|*.png",
                FileName = "chart_export.png"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                formsPlot1.Plot.SavePng(sfd.FileName, 800, 600);
                MessageBox.Show("Графік успішно збережено у PNG!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
