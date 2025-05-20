using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiamSat.Scada
{
    public partial class Settings : Form
    {
        private model _config = new model();
        string _fileName;
        string _filePath;

        public Settings()
        {
            InitializeComponent();

            Load += Settings_Load;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            _btnSave.Click += _btnSave_Click;

            // Automatically use the same directory as the executable
            _fileName = "model.json";
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);

            // Check if file exists
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("File does not exist. Creating default JSON file...");
                string defaultJson = JsonConvert.SerializeObject(_config, Formatting.Indented);
                File.WriteAllText(_filePath, defaultJson);
                Console.WriteLine("File created successfully.\n");
            }

            // Read the JSON file
            string jsonContent = File.ReadAllText(_filePath);
            _config = JsonConvert.DeserializeObject<model>(jsonContent);

            ShowControls();

            #region Events
            _txtOperator.TextChanged += (s, o) => _config.Operator = _txtOperator.Text;
            _txtLogMaster.TextChanged += (s, o) => _config.LogMasterSN = _txtLogMaster.Text;
            _txtTorqueMaster.TextChanged += (s, o) => _config.TorqueMasterSN = _txtTorqueMaster.Text;
            _txtConsole.TextChanged += (s, o) => _config.ConsoleSN = _txtConsole.Text;
            _txtDate.TextChanged += (s, o) => _config.Date = _txtDate.Text;
            _txtTool.TextChanged += (s, o) => _config.Tool = _txtTool.Text;
            _txtJobNo.TextChanged += (s, o) => _config.JobNo = _txtJobNo.Text;
            _txtSeries.TextChanged += (s, o) => _config.Series = _txtSeries.Text;
            _txtToolSN.TextChanged += (s, o) => _config.ToolSN = _txtToolSN.Text;
            _txtConnectionName.TextChanged += (s, o) => _config.ConnectionName = _txtConnectionName.Text;
            _txtLogInterval.TextChanged+=(s,o) =>
            {
                if (int.TryParse(_txtLogInterval.Text, out int interval))
                {
                    _config.LogInterval = interval;
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid number.");
                    _txtLogInterval.Text = _config.LogInterval.ToString();
                }
            };
            _txtPointNum.TextChanged += (s, o) =>
            {
                if (int.TryParse(_txtPointNum.Text, out int pointNum))
                {
                    _config.maxPoints = pointNum;
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid number.");
                    _txtPointNum.Text = _config.maxPoints.ToString();
                }
            };
            #endregion
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            string defaultJson = JsonConvert.SerializeObject(_config, Formatting.Indented);
            File.WriteAllText(_filePath, defaultJson);
            MessageBox.Show($"Successful.");
        }

        private void ShowControls()
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _txtOperator.Text = _config.Operator;
                _txtLogMaster.Text = _config.LogMasterSN;
                _txtTorqueMaster.Text = _config.TorqueMasterSN;
                _txtConsole.Text = _config.ConsoleSN;
                _txtDate.Text = _config.Date;
                _txtTool.Text = _config.Tool;
                _txtJobNo.Text = _config.JobNo;
                _txtSeries.Text = _config.Series;
                _txtToolSN.Text = _config.ToolSN;
                _txtConnectionName.Text = _config.ConnectionName;
                _txtLogInterval.Text = _config.LogInterval.ToString();
                _txtPointNum.Text = _config.maxPoints.ToString();
            });
        }
    }
}
