using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LoanAnalysis.Data;
using LoanAnalysis.Domain;

namespace LoanAnalysis.UI
{
    public partial class Form1 : Form
    {
        private List<LoanApplication> _allApplications = new List<LoanApplication>();
        private readonly CsvDataProvider _csvProvider = new CsvDataProvider();
        private readonly JsonDataProvider _jsonProvider = new JsonDataProvider();
        private readonly XmlDataProvider _xmlProvider = new XmlDataProvider();
        private readonly XlsxDataProvider _xlsxProvider = new XlsxDataProvider();

        private readonly XlsxReportService _xlsxReportService = new XlsxReportService();
        private readonly DocxReportService _docxReportService = new DocxReportService();

        private StatusStrip _statusStrip;
        private ToolStripStatusLabel _statusPathLabel;
        private ToolStripStatusLabel _statusCountLabel;
        private ToolStripStatusLabel _statusTimeLabel;

        private List<string> _recentFiles = new List<string>();

        public Form1()
        {
            InitializeComponent();
            SetupStatusBar();
        }

        private void SetupStatusBar()
        {
            _statusStrip = new StatusStrip();
            _statusPathLabel = new ToolStripStatusLabel("Файл: Не завантажено") { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            _statusCountLabel = new ToolStripStatusLabel("Записів: 0") { BorderSides = ToolStripStatusLabelBorderSides.Left };
            _statusTimeLabel = new ToolStripStatusLabel($"Оновлено: {DateTime.Now:HH:mm:ss}") { BorderSides = ToolStripStatusLabelBorderSides.Left };

            _statusPathLabel.ForeColor = Color.White;
            _statusCountLabel.ForeColor = Color.White;
            _statusTimeLabel.ForeColor = Color.White;

            _statusStrip.Items.AddRange(new ToolStripItem[] { _statusPathLabel, _statusCountLabel, _statusTimeLabel });
            Controls.Add(_statusStrip);
        }

        private void UpdateStatusBar(string filePath, int count)
        {
            _statusPathLabel.Text = $"Файл: {Path.GetFileName(filePath)}";
            _statusCountLabel.Text = $"Записів: {count}";
            _statusTimeLabel.Text = $"Оновлено: {DateTime.Now:HH:mm:ss}";
        }

        private void AddToRecentFiles(string filePath)
        {
            if (!_recentFiles.Contains(filePath))
            {
                _recentFiles.Insert(0, filePath);
                if (_recentFiles.Count > 5) _recentFiles.RemoveAt(5);

                UpdateRecentFilesMenu();
            }
        }
        private void UpdateRecentFilesMenu()
        {
            var fileMenuItem = menuStrip1.Items.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text.Contains("Файл"));
            if (fileMenuItem == null) return;

            var recentMenu = fileMenuItem.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(m => m.Text.Contains("Останні"));

            if (recentMenu == null)
            {
                recentMenu = new ToolStripMenuItem("Останні файли");
                fileMenuItem.DropDownItems.Add(recentMenu);
            }

            recentMenu.DropDownItems.Clear();

            foreach (var file in _recentFiles)
            {
                var item = new ToolStripMenuItem(Path.GetFileName(file)) { Tag = file };
                item.Click += (s, e) =>
                {
                    string path = (s as ToolStripMenuItem)?.Tag?.ToString();
                    if (File.Exists(path)) LoadDataFromFile(path);
                    else MessageBox.Show("Файл не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                };
                recentMenu.DropDownItems.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 950;
            this.Height = 650;

            string defaultPath = "loan_data.csv";
            if (File.Exists(defaultPath))
            {
                LoadDataFromFile(defaultPath);
            }
        }

        private void LoadDataFromFile(string filePath)
        {
            try
            {
                string ext = Path.GetExtension(filePath).ToLower();
                if (ext == ".json")
                    _allApplications = _jsonProvider.LoadData(filePath);
                else if (ext == ".xml")
                    _allApplications = _xmlProvider.LoadData(filePath);
                else if (ext == ".xlsx")
                    _allApplications = _xlsxProvider.LoadData(filePath);
                else
                    _allApplications = _csvProvider.LoadData(filePath);

                var cmbFilter = Controls.Find("cmbIntentFilter", true).FirstOrDefault() as ComboBox;
                if (cmbFilter != null)
                {
                    var intents = _allApplications.Select(a => a.Intent).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                    intents.Insert(0, "Усі категорії");
                    cmbFilter.DataSource = intents;
                }

                DisplayData(_allApplications);
                UpdateStatusBar(filePath, _allApplications.Count);
                AddToRecentFiles(filePath);
                Logger.Log($"Успішно завантажено {_allApplications.Count} записів з файлу: {filePath}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Помилка завантаження файлу {filePath}", ex);
                MessageBox.Show($"Помилка читання файлу: {ex.Message}", "Помилка I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<LoanApplication> GetFilteredData()
        {
            var cmbFilter = Controls.Find("cmbIntentFilter", true).FirstOrDefault() as ComboBox;
            string selectedIntent = cmbFilter?.SelectedItem?.ToString();

            if (selectedIntent == "Усі категорії" || string.IsNullOrEmpty(selectedIntent))
            {
                return _allApplications;
            }
            return _allApplications.Where(a => a.Intent == selectedIntent).ToList();
        }

        private void DisplayData(List<LoanApplication> list)
        {
            var dgv = Controls.Find("dgvData", true).FirstOrDefault() as DataGridView
                      ?? Controls.OfType<DataGridView>().FirstOrDefault();

            if (dgv != null)
            {
                var displayList = list.Select(a => new
                {
                    ID = a.Id,
                    Вік = a.Applicant?.Age,
                    Дохід = a.Applicant?.Income,
                    Житло = a.Applicant?.HomeOwnership,
                    Стаж = a.Applicant?.EmploymentLength,
                    Мета_Кредиту = a.Intent,
                    Сума = a.Amount,
                    Ставка = a.InterestRate,
                    Дефолт = a.CreditHistory?.HistoricalDefault == 1 ? "Так" : "Ні",
                    Рейтинг = a.CreditHistory?.CreditScore,
                    Статус = a.ApprovalStatus == 1 ? "Схвалено" : "Відхилено"
                }).ToList();

                dgv.DataSource = displayList;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            var filtered = GetFilteredData();
            DisplayData(filtered);
            UpdateStatusBar("Фільтровані дані", filtered.Count);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            var cmbFilter = Controls.Find("cmbIntentFilter", true).FirstOrDefault() as ComboBox;
            if (cmbFilter != null && cmbFilter.Items.Count > 0)
            {
                cmbFilter.SelectedIndex = 0;
            }
            DisplayData(_allApplications);
            UpdateStatusBar("Усі дані", _allApplications.Count);
        }

        private void відкритиCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Підтримувані файли (*.csv;*.json;*.xml;*.xlsx)|*.csv;*.json;*.xml;*.xlsx|CSV (*.csv)|*.csv|JSON (*.json)|*.json|XML (*.xml)|*.xml|Excel (*.xlsx)|*.xlsx",
                Title = "Оберіть файл для імпорту"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromFile(ofd.FileName);
            }
        }

        private void експортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentData = GetFilteredData();
            if (!currentData.Any())
            {
                MessageBox.Show("Немає даних для експорту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "JSON (*.json)|*.json|XML (*.xml)|*.xml|Excel (*.xlsx)|*.xlsx|CSV (*.csv)|*.csv",
                Title = "Експорт даних"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string ext = Path.GetExtension(sfd.FileName).ToLower();
                    if (ext == ".json") _jsonProvider.SaveData(sfd.FileName, currentData);
                    else if (ext == ".xml") _xmlProvider.SaveData(sfd.FileName, currentData);
                    else if (ext == ".xlsx") _xlsxProvider.SaveData(sfd.FileName, currentData);
                    else _csvProvider.SaveData(sfd.FileName, currentData);

                    Logger.Log($"Експорт виконано у файл: {sfd.FileName}");
                    MessageBox.Show("Експорт успішно виконано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Помилка під час експорту", ex);
                    MessageBox.Show($"Помилка експорту: {ex.Message}", "Помилка I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void згенеруватиXLSXзвітToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentData = GetFilteredData();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Звіт (*.xlsx)|*.xlsx",
                Title = "Зберегти XLSX звіт",
                FileName = "Loan_Analysis_Report.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _xlsxReportService.GenerateReport(currentData, sfd.FileName);
                    Logger.Log($"XLSX звіт збережено: {sfd.FileName}");
                    MessageBox.Show("XLSX-звіт з умовним форматуванням успішно згенеровано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Помилка створення XLSX звіту", ex);
                    MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void згенеруватиDOCXзвітToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentData = GetFilteredData();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Word документ (*.docx)|*.docx",
                Title = "Зберегти DOCX звіт",
                FileName = "Loan_Analysis_Report.docx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string chartPath = Path.Combine(Path.GetTempPath(), "chart_temp.png");
                    GeneratePlotImage(currentData, chartPath);

                    _docxReportService.GenerateReport(currentData, sfd.FileName, chartPath);
                    Logger.Log($"DOCX звіт збережено: {sfd.FileName}");
                    MessageBox.Show("DOCX-звіт з титульною сторінкою та графіком успішно згенеровано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Помилка створення DOCX звіту", ex);
                    MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GeneratePlotImage(List<LoanApplication> data, string savePath)
        {
            var plt = new ScottPlot.Plot();

            int approved = data.Count(x => x.ApprovalStatus == 1);
            int rejected = data.Count(x => x.ApprovalStatus == 0);

            double[] values = { approved, rejected };
            var pie = plt.Add.Pie(values);
            pie.ExplodeFraction = 0.05;

            plt.Title("Співвідношення схвалених та відхилених заявок");
            plt.SavePng(savePath, 600, 400);
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void звітиToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void інформаціяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Лабораторна робота №2\nАналіз даних про кредити\nРозроблено на C# Windows Forms (3-tier architecture)",
                            "Про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currentData = GetFilteredData();
            if (!currentData.Any())
            {
                MessageBox.Show("Немає даних для побудови графіків!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChartForm chartForm = new ChartForm(currentData);
            chartForm.ShowDialog();
        }
    }
}