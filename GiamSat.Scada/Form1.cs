using EasyScada.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Serilog;
using System.IO;
using EasyScada.Winforms.Controls;
using System.Runtime.InteropServices;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using Separator = LiveCharts.Wpf.Separator;
using LiveCharts.Definitions.Charts;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Drawing.Imaging;
using System.Data;
using MigraDoc.DocumentObjectModel.Shapes;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace GiamSat.Scada
{
    public partial class Form1 : Form
    {
        private EasyDriverConnector _easyDriverConnector;

        private Timer _timer = new Timer();

        string _fileName;
        string _filePath;

        double _torque1 = 0, _max1 = 0, _target1 = 0, _trigger = 0;
        double _torque2 = 0, _max2 = 0, _target2 = 0;

        //List<double> timeData = new List<double>();
        List<string> timeData1 = new List<string>();
        List<double> torqueData1 = new List<double>();
        List<double> _targetData1 = new List<double>();
        List<double> _maxData1 = new List<double>();

        List<string> timeData2 = new List<string>();
        List<double> torqueData2 = new List<double>();
        List<double> _targetData2 = new List<double>();
        List<double> _maxData2 = new List<double>();

        private model _config = new model();

        private System.Threading.CancellationTokenSource _cts;

        private Random rnd = new Random();

        int _xLable1 = 0, _xLable2 = 0;
        bool _isResetChart = true;

        bool _isTab1 = true;// biến để xác định tab nào đang được chọn để xuất PDF.

        public void Start()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested && _trigger == 1)
                {
                    try
                    {
                        // 👉 Thực hiện công việc ở đây
                        Console.WriteLine($"Thời gian: {DateTime.Now:HH:mm:ss}");

                        // Cập nhật dữ liệu vào chart
                        GlobalVariable.InvokeIfRequired(this, () =>
                        {
                            _chart1.Series[0].Values.Add(_torque1);
                            _chart1.Series[1].Values.Add(_target1);
                            _chart1.Series[2].Values.Add(_max1);
                        });

                        await Task.Delay(_config.LogInterval, token); // Chờ 1 giây
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                }
            }, token);
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        public Form1()
        {
            InitializeComponent();

            #region Khởi tạo easy drirver connector
            _easyDriverConnector = new EasyDriverConnector();
            _easyDriverConnector.ConnectionStatusChaged += _easyDriverConnector_ConnectionStatusChaged;
            _easyDriverConnector.BeginInit();
            _easyDriverConnector.EndInit();
            _labSriverStatus.Text = _easyDriverConnector.ConnectionStatus.ToString();

            _easyDriverConnector.Started += _easyDriverConnector_Started;
            if (_easyDriverConnector.IsStarted)
            {
                _easyDriverConnector_Started(null, null);
            }
            #endregion

            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
        }

        private void _easyDriverConnector_ConnectionStatusChaged(object sender, ConnectionStatusChangedEventArgs e)
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                //_pnStatus.Text = e.NewStatus.ToString();
                _labSriverStatus.BackColor = GetConnectionStatusColor(e.NewStatus);
                _labSriverStatus.Text = _easyDriverConnector.ConnectionStatus.ToString();
            });
        }

        private System.Drawing.Color GetConnectionStatusColor(ConnectionStatus status)
        {
            switch (status)
            {
                case ConnectionStatus.Connected:
                    return System.Drawing.Color.Lime;
                case ConnectionStatus.Connecting:
                case ConnectionStatus.Reconnecting:
                    return System.Drawing.Color.Orange;
                case ConnectionStatus.Disconnected:
                    return System.Drawing.Color.Red;
                default:
                    return System.Drawing.Color.White;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn tắt app?!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            _easyDriverConnector.ConnectionStatusChaged -= _easyDriverConnector_ConnectionStatusChaged;
            _easyDriverConnector.Started -= _easyDriverConnector_Started;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

            _timer.Interval = _config.LogInterval;
            _timer.Tick += _timer_Tick;
            _timer.Enabled = true;
            _btnUpdate.Click += _btnLoad_Click;
            _btnExport.Click += _btnExport_Click;
            _btnStartStop.Click += _btnStartStop_Click;

            #region Chart initialize
            #region torque 1
            //_chart2.BackColor = System.Drawing.Color.Black;
            _chart1.DataTooltip.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66));
            _chart1.DataTooltip.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 242, 244));
            //_chart2.DataTooltip.Content = new System.Windows.Controls.TextBlock
            //{
            //    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 202, 58)),
            //    FontSize = 12,
            //    FontWeight = System.Windows.FontWeights.Bold
            //};
            _chart1.LegendLocation = LegendLocation.Top;
            _chart1.Series = new SeriesCollection();

            _chart1.Series.Add(new LineSeries()
            {
                Title = "TORQUE",
                Values = new ChartValues<double>(),
                StrokeThickness = 2,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(25, 130, 196)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 10,
                DataLabels = true,
                //PointGeometry = null,//hidden point
                PointForeground =
                    new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49))
            });
            _chart1.Series.Add(new LineSeries()
            {
                Title = "Max",
                Values = new ChartValues<double>(),
                StrokeThickness = 2,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 202, 58)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 10,
                PointForeground =
                    new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49)),
                //DataLabels = true,
                //LabelPoint = point => "Max = " + point.Y.ToString("N0")
            });
            _chart1.Series.Add(new LineSeries()
            {
                Title = "Target",
                Values = new ChartValues<double>(),
                StrokeThickness = 2,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 47, 2)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 10,
                PointForeground =
                    new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49)),
                //DataLabels = true,
                //LabelPoint = point => "Max = " + point.Y.ToString("N0")
            });

            _chart1.AxisX.Add(new Axis()
            {
                Title = "Time (Sec)",
                LabelFormatter = value => new System.DateTime((long)(value)).ToString(),
                //LabelFormatter = value => " " + value.ToString("F0") + " ", // đẩy ra bằng khoảng trắng
                Labels = new string[] { },
                //MinValue=0,
                //MaxValue=60,
                LabelsRotation = 0,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66)),
                IsMerged = false,
                Separator = new Separator
                {
                    Step = 1,
                    //Margin = new System.Windows.Thickness(0, 10, 0, 0),  // Đẩy label xuống,
                    StrokeThickness = 1,
                    //StrokeDashArray = new System.Windows.Media.DoubleCollection(2),
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2, 2 }), // nét đứt
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66))
                }
            });
            _chart1.AxisY.Add(new Axis
            {
                Title = "TORQUE (LB-FT)",
                LabelFormatter = value => value.ToString("N0"),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66)),
                IsMerged = false,
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    //StrokeDashArray = new System.Windows.Media.DoubleCollection(4),
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2, 2 }), // nét đứt
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66))
                }
            });

            //hiển thị label lên line
            _chart1.VisualElements.Add(new VisualElement
            {
                X = 1, // hoặc vị trí bạn muốn (tính theo tọa độ X trên chart)
                Y = 35000,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = "Max = 42600",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });
            _chart1.VisualElements.Add(new VisualElement
            {
                X = 1,
                Y = 30000,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = "Target = 40600",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });
            #endregion
            #region torque 2
            //_chart2.BackColor = System.Drawing.Color.Black;
            _chart2.DataTooltip.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66));
            _chart2.DataTooltip.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 242, 244));
            //_chart2.DataTooltip.Content = new System.Windows.Controls.TextBlock
            //{
            //    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 202, 58)),
            //    FontSize = 12,
            //    FontWeight = System.Windows.FontWeights.Bold
            //};
            _chart2.LegendLocation = LegendLocation.Top;
            _chart2.Series = new SeriesCollection();

            _chart2.Series.Add(new LineSeries()
            {
                Title = "TORQUE",
                Values = new ChartValues<double>(),
                StrokeThickness = 2,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(25, 130, 196)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 10,
                DataLabels = true,
                //PointGeometry = null,//hidden point
                PointForeground =
                    new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49))
            });
            _chart2.Series.Add(new LineSeries()
            {
                Title = "Max",
                Values = new ChartValues<double>(),
                StrokeThickness = 2,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 202, 58)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 10,
                PointForeground =
                    new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49)),
                //DataLabels = true,
                //LabelPoint = point => "Max = " + point.Y.ToString("N0")
            });
            _chart2.Series.Add(new LineSeries()
            {
                Title = "Target",
                Values = new ChartValues<double>(),
                StrokeThickness = 2,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 47, 2)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 10,
                PointForeground =
                    new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49)),
                //DataLabels = true,
                //LabelPoint = point => "Max = " + point.Y.ToString("N0")
            });

            _chart2.AxisX.Add(new Axis()
            {
                Title = "Time (Sec)",
                LabelFormatter = value => new System.DateTime((long)(value)).ToString(),
                //LabelFormatter = value => " " + value.ToString("F0") + " ", // đẩy ra bằng khoảng trắng
                Labels = new string[] { },
                //MinValue=0,
                //MaxValue=60,
                LabelsRotation = 0,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66)),
                IsMerged = false,
                Separator = new Separator
                {
                    Step = 1,
                    //Margin = new System.Windows.Thickness(0, 10, 0, 0),  // Đẩy label xuống,
                    StrokeThickness = 1,
                    //StrokeDashArray = new System.Windows.Media.DoubleCollection(2),
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2, 2 }), // nét đứt
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66))
                }
            });
            _chart2.AxisY.Add(new Axis
            {
                Title = "TORQUE (LB-FT)",
                LabelFormatter = value => value.ToString("N0"),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66)),
                IsMerged = false,
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    //StrokeDashArray = new System.Windows.Media.DoubleCollection(4),
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2, 2 }), // nét đứt
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(43, 45, 66))
                }
            });

            //hiển thị label lên line
            _chart2.VisualElements.Add(new VisualElement
            {
                X = 1, // hoặc vị trí bạn muốn (tính theo tọa độ X trên chart)
                Y = 35000,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = "Max = 42600",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });
            _chart2.VisualElements.Add(new VisualElement
            {
                X = 1,
                Y = 30000,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = "Target = 40600",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });
            #endregion
            #endregion

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    LoadDataFromFile(openFileDialog1.FileName);
            //    CapNhatChartLine();
            //}

            _tab.SelectedIndexChanged += _tab_SelectedIndexChanged;
            _tab.SelectedIndex = 1; // Mặc định chọn tab đầu tiên
            _tab.SelectedIndex = 0; // Mặc định chọn tab đầu tiên
        }

        private void _tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = (TabControl)sender;

            if (tab.SelectedIndex == 0)
            {
                _isTab1 = true;

                _labMax1.Visible = _labTarget1.Visible = _labTorque1.Visible = _easyTextBoxMax1.Visible = _easyTextBoxTarget1.Visible = _easyLabelTorque1.Visible = true;
                _labMax2.Visible = _labTarget2.Visible = _labTorque2.Visible = _easyTextBoxMax2.Visible = _easyTextBoxTarget2.Visible = _easyLabelTorque2.Visible = false;
            }
            else
            {
                _isTab1 = false;
                
                _labMax1.Visible = _labTarget1.Visible = _labTorque1.Visible = _easyTextBoxMax1.Visible = _easyTextBoxTarget1.Visible = _easyLabelTorque1.Visible = false;
                _labMax2.Visible = _labTarget2.Visible = _labTorque2.Visible = _easyTextBoxMax2.Visible = _easyTextBoxTarget2.Visible = _easyLabelTorque2.Visible = true;
            }
        }

        private void _btnStartStop_Click(object sender, EventArgs e)
        {
            _trigger = _trigger == 0 ? 1 : 0; // Toggle trigger state

            if (_trigger != 0 && _isResetChart == true) { _isResetChart = false; }

            if (!_isResetChart)
            {
                ResetChart();
                _xLable1 = 0;
                _isResetChart = true;
            }

            var btn = (Button)sender;

            btn.Text = _trigger == 1 ? "STOP" : "START";
            btn.BackColor = _trigger == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Gray;
        }

        #region Events
        private void _btnLoad_Click(object sender, EventArgs e)
        {
            using (var nf = new Settings())
            {
                nf.ShowDialog();

                // Read the JSON file
                string jsonContent = File.ReadAllText(_filePath);
                _config = JsonConvert.DeserializeObject<model>(jsonContent);
                _timer.Interval = _config.LogInterval;
            }
        }
        private void LoadDataFromFile(string path)
        {
            timeData1.Clear();
            torqueData1.Clear();

            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length >= 2
                    && double.TryParse(parts[0], out double t)
                    && double.TryParse(parts[1], out double tq)
                    && double.TryParse(parts[2], out double tg)
                    && double.TryParse(parts[3], out double tm))
                {
                    timeData1.Add(t.ToString());
                    torqueData1.Add(tq);
                    _targetData1.Add(tg);
                    _maxData1.Add(tm);
                }
            }
        }

        private void CapNhatChartLine()
        {
            _chart1.Series[0].Values.Clear();
            _chart1.Series[1].Values.Clear();
            _chart1.Series[2].Values.Clear();

            _chart1.VisualElements.Clear();

            _chart1.VisualElements.Add(new VisualElement
            {
                X = 1, // hoặc vị trí bạn muốn (tính theo tọa độ X trên chart)
                Y = _max1,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = $"Max = {_max1}",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });
            _chart1.VisualElements.Add(new VisualElement
            {
                X = 1,
                Y = _target1,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = $"Target = {(_target1)}",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });

            List<string> labels = new List<string>();

            for (int i = 0; i < timeData1.Count; i++)
            {
                _chart1.Series[0].Values.Add(torqueData1[i]);
                _chart1.Series[1].Values.Add(_targetData1[i]);
                _chart1.Series[2].Values.Add(_maxData1[i]);
                labels.Add(timeData1[i].ToString());
            }

            _chart1.AxisX[0].Labels = labels;

            _chart1.Update(true, true);
        }

        private void UpdateChart()
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                #region TORQUE 1
                // Tạo giá trị mới
                //double newTorque = rnd.Next(10000, 45000); // Tùy chỉnh min/max phù hợp
                double newTorque1 = _torque1; // Tùy chỉnh min/max phù hợp
                double newTarget1 = _target1;
                double newMax1 = _max1;

                // Thêm dữ liệu mới và kiểm tra số lượng
                var torqueSeries1 = _chart1.Series[0].Values;
                var maxSeries1 = _chart1.Series[1].Values;
                var targetSeries1 = _chart1.Series[2].Values;
                //var labels = _chart2.AxisX[0].Labels.ToList();

                if (torqueSeries1.Count >= _config.maxPoints)
                {
                    var pointRemove = torqueSeries1.Count - _config.maxPoints;

                    for (int i = 0; i < pointRemove; i++)
                    {
                        torqueSeries1.RemoveAt(i);
                        maxSeries1.RemoveAt(i);
                        targetSeries1.RemoveAt(i);
                        timeData1.RemoveAt(i);
                    }
                    //torqueSeries.RemoveAt(0);
                    //maxSeries.RemoveAt(0);
                    //targetSeries.RemoveAt(0);
                    //timeData.RemoveAt(0);
                }

                torqueSeries1.Add(newTorque1);
                maxSeries1.Add(newMax1);
                targetSeries1.Add(newTarget1);
                //timeData.Add(DateTime.Now.Second.ToString());
                timeData1.Add(_xLable1.ToString());
                _chart1.AxisX[0].Labels = timeData1;

                #region  Cập nhật lại VisualElements để giữ label cho Max và Target
                _chart1.VisualElements.Clear();
                _chart1.VisualElements.Add(new VisualElement
                {
                    X = 2,
                    Y = _max1,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    UIElement = new System.Windows.Controls.TextBlock
                    {
                        Text = $"Max = {_max1}",
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontWeight = System.Windows.FontWeights.Bold
                    }
                });
                _chart1.VisualElements.Add(new VisualElement
                {
                    X = 2,
                    Y = _target1,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    UIElement = new System.Windows.Controls.TextBlock
                    {
                        Text = $"Target = {_target1}",
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontWeight = System.Windows.FontWeights.Bold
                    }
                });
                #endregion

                // Cập nhật lại label cho trục X
                _xLable1 += 1;

                _chart1.Update(false, true);
                #endregion

                #region TORQUE 2
                // Tạo giá trị mới
                //double newTorque = rnd.Next(10000, 45000); // Tùy chỉnh min/max phù hợp
                double newTorque2 = _torque2; // Tùy chỉnh min/max phù hợp
                double newTarget2 = _target2;
                double newMax2 = _max2;

                // Thêm dữ liệu mới và kiểm tra số lượng
                var torqueSeries2 = _chart2.Series[0].Values;
                var maxSeries2 = _chart2.Series[1].Values;
                var targetSeries2 = _chart2.Series[2].Values;
                //var labels = _chart2.AxisX[0].Labels.ToList();

                if (torqueSeries2.Count >= _config.maxPoints)
                {
                    var pointRemove = torqueSeries2.Count - _config.maxPoints;

                    for (int i = 0; i < pointRemove; i++)
                    {
                        torqueSeries2.RemoveAt(i);
                        maxSeries2.RemoveAt(i);
                        targetSeries2.RemoveAt(i);
                        timeData2.RemoveAt(i);
                    }
                    //torqueSeries.RemoveAt(0);
                    //maxSeries.RemoveAt(0);
                    //targetSeries.RemoveAt(0);
                    //timeData.RemoveAt(0);
                }

                torqueSeries2.Add(newTorque2);
                maxSeries2.Add(newMax2);
                targetSeries2.Add(newTarget2);
                //timeData.Add(DateTime.Now.Second.ToString());
                timeData2.Add(_xLable2.ToString());
                _chart2.AxisX[0].Labels = timeData2;

                #region  Cập nhật lại VisualElements để giữ label cho Max và Target
                _chart2.VisualElements.Clear();
                _chart2.VisualElements.Add(new VisualElement
                {
                    X = 2,
                    Y = _max2,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    UIElement = new System.Windows.Controls.TextBlock
                    {
                        Text = $"Max = {_max2}",
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontWeight = System.Windows.FontWeights.Bold
                    }
                });
                _chart2.VisualElements.Add(new VisualElement
                {
                    X = 2,
                    Y = _target2,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    UIElement = new System.Windows.Controls.TextBlock
                    {
                        Text = $"Target = {_target2}",
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontWeight = System.Windows.FontWeights.Bold
                    }
                });
                #endregion

                // Cập nhật lại label cho trục X
                _xLable2 += 1;

                _chart2.Update(false, true);
                #endregion
            });
        }

        private void _btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Report";
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog.FileName = $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_TorqueReport.pdf"; // Tên gợi ý

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;

                    //// Ví dụ: Lưu file PDF vào đường dẫn được chọn
                    //File.WriteAllBytes(path, CreatePdfDocument()); // giả sử bạn có hàm trả về byte[]
                    //MessageBox.Show("File saved to: " + path);

                    var doc = CreateDocument();
                    var renderer = new PdfDocumentRenderer(true) { Document = doc };
                    renderer.RenderDocument();

                    //var outputPath = Path.Combine(Application.StartupPath, "TorqueReport.pdf");
                    renderer.PdfDocument.Save(path);
                    Process.Start("explorer.exe", path); // Mở file sau khi xuất
                }
            }
        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            try
            {
                t.Enabled = false;
                if (_trigger >= 1)
                {
                    UpdateChart();
                }

                GlobalVariable.InvokeIfRequired(this, () => { _labTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); });
            }
            catch (Exception ex) { Log.Error(ex, "From _timer_Tick"); }
            finally
            {
                t.Enabled = true;
            }
        }

        private void ResetChart()
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                _chart1.Series[0].Values.Clear();
                _chart1.Series[1].Values.Clear();
                _chart1.Series[2].Values.Clear();

                _chart1.VisualElements.Clear();

                if (_chart1.AxisX[0]?.Labels is IList<string> labels1 && !labels1.IsReadOnly)
                {
                    labels1.Clear();
                }
                else
                {
                    _chart1.AxisX[0].Labels = new List<string>();
                }

                _chart1.Update(true, true);

                //chart 2
                _chart2.Series[0].Values.Clear();
                _chart2.Series[1].Values.Clear();
                _chart2.Series[2].Values.Clear();

                _chart2.VisualElements.Clear();

                if (_chart2.AxisX[0]?.Labels is IList<string> labels2 && !labels2.IsReadOnly)
                {
                    labels2.Clear();
                }
                else
                {
                    _chart2.AxisX[0].Labels = new List<string>();
                }

                _chart2.Update(true, true);
            });

            _xLable1 = _xLable2 = 0;
        }

        private void _easyDriverConnector_Started(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            //foreach (var item in _ovensInfo)
            {
                //easyDriverConnector1.GetTag($"{item.Path}/Temperature").QualityChanged += Temperature_QualityChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value1").ValueChanged += Value1_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max1").ValueChanged += Max1_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target1").ValueChanged += Target1_ValueChanged;

                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value2").ValueChanged += Value2_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max2").ValueChanged += Max2_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target2").ValueChanged += Target2_ValueChanged;

                Value1_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value1")
                    , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value1")
                    , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value1").Value));
                Max1_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max1")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max1")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max1").Value));
                Target1_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target1")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target1")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target1").Value));

                Value2_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value2")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value2")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value2").Value));
                Max2_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max2")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max2")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max2").Value));
                Target2_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target2")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target2")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target2").Value));
            }
        }

        #region Event tag value change
        private void Value1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _torque1 = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Max1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _max1 = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Target1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _target1 = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Value2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _torque2 = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Max2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _max2 = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Target2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _target2 = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        #endregion

        #endregion


        private Document CreateDocument()
        {
            var doc = new Document();
            var section = doc.AddSection();

            // Setup margins
            section.PageSetup.TopMargin = "1cm";
            section.PageSetup.BottomMargin = "1cm";
            section.PageSetup.LeftMargin = "2cm";
            section.PageSetup.RightMargin = "2cm";

            if (_isTab1)
            {
                var bitmap = new Bitmap(_chart1.Width, _chart1.Height);
                _chart1.DrawToBitmap(bitmap, _chart1.ClientRectangle);
                bitmap.Save("torque_chart.png", ImageFormat.Png);
            }
            else
            {
                var bitmap = new Bitmap(_chart2.Width, _chart2.Height);
                _chart2.DrawToBitmap(bitmap, _chart2.ClientRectangle);
                bitmap.Save("torque_chart.png", ImageFormat.Png);
            }

            // Watermark (chèn trong phần thân trang, không gây trang trắng)
            var watermark1 = section.Headers.Primary.AddParagraph("TORQUE");
            watermark1.Format.Font.Size = 20;
            watermark1.Format.Font.Color = Colors.LightGray;
            watermark1.Format.Font.Bold = true;
            watermark1.Format.Alignment = ParagraphAlignment.Center;
            watermark1.Format.SpaceBefore = "8cm";

            //// Logo bottom right
            //var logoPath = "LogoPT.ico";
            //if (File.Exists(logoPath))
            //{
            //    var image = section.Footers.Primary.AddImage(logoPath);
            //    image.Width = "3cm";
            //    image.LockAspectRatio = true;
            //    image.Left = ShapePosition.Right;
            //    image.Top = ShapePosition.Bottom;
            //    image.WrapFormat.Style = WrapStyle.Through;
            //}

            // Title
            var title = section.AddParagraph("CONNECTION TORQUE LOG");
            title.Format.Font.Bold = true;
            title.Format.Font.Size = 14;
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.SpaceAfter = "0.5cm";

            // Bảng thông tin trái/phải (canh đều 2 bên)
            var infoTable = section.AddTable();
            infoTable.Borders.Visible = false;
            infoTable.AddColumn("7.5cm");
            infoTable.AddColumn("3cm");
            infoTable.AddColumn("7.5cm");

            var row = infoTable.AddRow();
            row.Cells[0].AddParagraph(
                $"OPERATOR: {_config.Operator}\n" +
                $"DATE: {DateTime.Now.ToString("dd MMM yy")} ({DateTime.Now.ToString("HH:mm:ss")})\n" +
                $"TOOL: {_config.Tool}\n" +
                $"JOB NO.: {_config.JobNo}\n" +
                $"SERIES:{_config.Series}\n" +
                $"TOOL S/N: {_config.ToolSN}"
            );
            row.Cells[2].AddParagraph(
                $"KH-001: {_config.LogMasterSN}\n" +
                $"TORQUEMASTER™ S/N: {_config.TorqueMasterSN}\n" +
                $"CONSOLE S/N: {_config.ConsoleSN}"
            );

            section.AddParagraph().Format.SpaceAfter = "0.3cm";

            section.AddParagraph("\nLOGGED TORQUE VALUES").Format.Font.Bold = true;

            // Data Table
            var dataTable = section.AddTable();
            dataTable.Borders.Width = 0.75;
            dataTable.AddColumn("6cm");
            dataTable.AddColumn("6cm");
            dataTable.AddColumn("6cm");
            dataTable.Format.Alignment = ParagraphAlignment.Justify;

            var header = dataTable.AddRow();
            header.Shading.Color = Colors.LightGray;
            header.Cells[0].AddParagraph("Connection Name").Format.Font.Bold = true;
            header.Cells[1].AddParagraph("Target Torque\n(lb-ft)").Format.Font.Bold = true;
            header.Cells[2].AddParagraph("Logged Torque\n(lb-ft)").Format.Font.Bold = true;


            var dataRow = dataTable.AddRow();
            dataRow.Cells[0].AddParagraph(_config.ConnectionName);
            if (_isTab1)
            {
                dataRow.Cells[1].AddParagraph(_target1.ToString()).Format.Alignment = ParagraphAlignment.Right;
                dataRow.Cells[2].AddParagraph(_torque1.ToString()).Format.Alignment = ParagraphAlignment.Right;
            }
            else
            {
                dataRow.Cells[1].AddParagraph(_target2.ToString()).Format.Alignment = ParagraphAlignment.Right;
                dataRow.Cells[2].AddParagraph(_torque2.ToString()).Format.Alignment = ParagraphAlignment.Right;
            }

            // Chart Image Placeholder
            section.AddParagraph("\n");
            var chartImagePath = "torque_chart.png"; // Ensure chart is generated separately
            if (File.Exists(chartImagePath))
            {
                var chartImage = section.AddImage(chartImagePath);
                chartImage.Width = "16cm";
                chartImage.LockAspectRatio = true;
                chartImage.Top = ShapePosition.Top;
                chartImage.Left = ShapePosition.Center;
            }

            //// Footer text (optional)
            //var www = section.AddParagraph("www.nov.com");
            //www.Format.Font.Color = Colors.DarkBlue;
            //www.Format.SpaceBefore = "1cm";
            //www.Format.Alignment = ParagraphAlignment.Left;

            return doc;
        }

        private Document CreateDocumentManuPages()
        {
            var doc = new Document();
            var section = doc.AddSection();

            // Setup margins
            section.PageSetup.TopMargin = "1cm";
            section.PageSetup.BottomMargin = "1cm";
            section.PageSetup.LeftMargin = "2cm";
            section.PageSetup.RightMargin = "2cm";


            #region TORQUE 1
            var bitmap = new Bitmap(_chart1.Width, _chart1.Height);
            _chart1.DrawToBitmap(bitmap, _chart1.ClientRectangle);
            bitmap.Save("torque_chart.png", ImageFormat.Png);

            // Watermark (chèn trong phần thân trang, không gây trang trắng)
            var watermark1 = section.Headers.Primary.AddParagraph("TORQUE 1");
            watermark1.Format.Font.Size = 20;
            watermark1.Format.Font.Color = Colors.LightGray;
            watermark1.Format.Font.Bold = true;
            watermark1.Format.Alignment = ParagraphAlignment.Center;
            watermark1.Format.SpaceBefore = "8cm";

            //// Logo bottom right
            //var logoPath = "LogoPT.ico";
            //if (File.Exists(logoPath))
            //{
            //    var image = section.Footers.Primary.AddImage(logoPath);
            //    image.Width = "3cm";
            //    image.LockAspectRatio = true;
            //    image.Left = ShapePosition.Right;
            //    image.Top = ShapePosition.Bottom;
            //    image.WrapFormat.Style = WrapStyle.Through;
            //}

            // Title
            var title = section.AddParagraph("CONNECTION TORQUE LOG");
            title.Format.Font.Bold = true;
            title.Format.Font.Size = 14;
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.SpaceAfter = "0.5cm";

            // Bảng thông tin trái/phải (canh đều 2 bên)
            var infoTable = section.AddTable();
            infoTable.Borders.Visible = false;
            infoTable.AddColumn("7.5cm");
            infoTable.AddColumn("3cm");
            infoTable.AddColumn("7.5cm");

            var row = infoTable.AddRow();
            row.Cells[0].AddParagraph(
                $"OPERATOR: {_config.Operator}\n" +
                $"DATE: {DateTime.Now.ToString("dd MMM yy")} ({DateTime.Now.ToString("HH:mm:ss")})\n" +
                $"TOOL: {_config.Tool}\n" +
                $"JOB NO.: {_config.JobNo}\n" +
                $"SERIES:{_config.Series}\n" +
                $"TOOL S/N: {_config.ToolSN}"
            );
            row.Cells[2].AddParagraph(
                $"KH-001: {_config.LogMasterSN}\n" +
                $"TORQUEMASTER™ S/N: {_config.TorqueMasterSN}\n" +
                $"CONSOLE S/N: {_config.ConsoleSN}"
            );

            section.AddParagraph().Format.SpaceAfter = "0.3cm";

            section.AddParagraph("\nLOGGED TORQUE VALUES").Format.Font.Bold = true;

            // Data Table
            var dataTable = section.AddTable();
            dataTable.Borders.Width = 0.75;
            dataTable.AddColumn("6cm");
            dataTable.AddColumn("6cm");
            dataTable.AddColumn("6cm");
            dataTable.Format.Alignment = ParagraphAlignment.Justify;

            var header = dataTable.AddRow();
            header.Shading.Color = Colors.LightGray;
            header.Cells[0].AddParagraph("Connection Name").Format.Font.Bold = true;
            header.Cells[1].AddParagraph("Target Torque\n(lb-ft)").Format.Font.Bold = true;
            header.Cells[2].AddParagraph("Logged Torque\n(lb-ft)").Format.Font.Bold = true;

            var dataRow = dataTable.AddRow();
            dataRow.Cells[0].AddParagraph(_config.ConnectionName);
            dataRow.Cells[1].AddParagraph(_target1.ToString()).Format.Alignment = ParagraphAlignment.Right;
            dataRow.Cells[2].AddParagraph(_torque1.ToString()).Format.Alignment = ParagraphAlignment.Right;

            // Chart Image Placeholder
            section.AddParagraph("\n");
            var chartImagePath = "torque_chart.png"; // Ensure chart is generated separately
            if (File.Exists(chartImagePath))
            {
                var chartImage = section.AddImage(chartImagePath);
                chartImage.Width = "16cm";
                chartImage.LockAspectRatio = true;
                chartImage.Top = ShapePosition.Top;
                chartImage.Left = ShapePosition.Center;
            }

            //// Footer text (optional)
            //var www = section.AddParagraph("www.nov.com");
            //www.Format.Font.Color = Colors.DarkBlue;
            //www.Format.SpaceBefore = "1cm";
            //www.Format.Alignment = ParagraphAlignment.Left;
            #endregion

            // Ngắt trang
            //section.AddPageBreak();

            var section2 = doc.AddSection();

            // Setup margins
            section2.PageSetup.TopMargin = "1cm";
            section2.PageSetup.BottomMargin = "1cm";
            section2.PageSetup.LeftMargin = "2cm";
            section2.PageSetup.RightMargin = "2cm";

            // Setup margins
            section2.PageSetup.TopMargin = "1cm";
            section2.PageSetup.BottomMargin = "1cm";
            section2.PageSetup.LeftMargin = "2cm";
            section2.PageSetup.RightMargin = "2cm";

            #region TORQUE 2
            // Watermark chèn vào Header
            var watermark2 = section2.Headers.Primary.AddParagraph("TORQUE 2");
            watermark2.Format.Font.Size = 20;
            watermark2.Format.Font.Color = Colors.LightGray;
            watermark2.Format.Font.Bold = true;
            watermark2.Format.Alignment = ParagraphAlignment.Center;
            watermark2.Format.SpaceBefore = "8cm";

            //// Logo bottom right
            //var logoPath = "LogoPT.ico";
            //if (File.Exists(logoPath))
            //{
            //    var image = section.Footers.Primary.AddImage(logoPath);
            //    image.Width = "3cm";
            //    image.LockAspectRatio = true;
            //    image.Left = ShapePosition.Right;
            //    image.Top = ShapePosition.Bottom;
            //    image.WrapFormat.Style = WrapStyle.Through;
            //}

            // Title
            var title2 = section2.AddParagraph("CONNECTION TORQUE LOG");
            title2.Format.Font.Bold = true;
            title2.Format.Font.Size = 14;
            title2.Format.Alignment = ParagraphAlignment.Center;
            title2.Format.SpaceAfter = "0.5cm";

            // Bảng thông tin trái/phải (canh đều 2 bên)
            var infoTable2 = section2.AddTable();
            infoTable2.Borders.Visible = false;
            infoTable2.AddColumn("7.5cm");
            infoTable2.AddColumn("3cm");
            infoTable2.AddColumn("7.5cm");

            var row2 = infoTable2.AddRow();
            row2.Cells[0].AddParagraph(
                $"OPERATOR: {_config.Operator}\n" +
                $"DATE: {DateTime.Now.ToString("dd MMM yy")} ({DateTime.Now.ToString("HH:mm:ss")})\n" +
                $"TOOL: {_config.Tool}\n" +
                $"JOB NO.: {_config.JobNo}\n" +
                $"SERIES:{_config.Series}\n" +
                $"TOOL S/N: {_config.ToolSN}"
            );
            row2.Cells[2].AddParagraph(
                $"KH-001: {_config.LogMasterSN}\n" +
                $"TORQUEMASTER™ S/N: {_config.TorqueMasterSN}\n" +
                $"CONSOLE S/N: {_config.ConsoleSN}"
            );

            section2.AddParagraph().Format.SpaceAfter = "0.3cm";

            section2.AddParagraph("\nLOGGED TORQUE VALUES").Format.Font.Bold = true;

            // Data Table
            var dataTable2 = section2.AddTable();
            dataTable2.Borders.Width = 0.75;
            dataTable2.AddColumn("6cm");
            dataTable2.AddColumn("6cm");
            dataTable2.AddColumn("6cm");
            dataTable2.Format.Alignment = ParagraphAlignment.Justify;

            var header2 = dataTable2.AddRow();
            header2.Shading.Color = Colors.LightGray;
            header2.Cells[0].AddParagraph("Connection Name").Format.Font.Bold = true;
            header2.Cells[1].AddParagraph("Target Torque\n(lb-ft)").Format.Font.Bold = true;
            header2.Cells[2].AddParagraph("Logged Torque\n(lb-ft)").Format.Font.Bold = true;

            var dataRow2 = dataTable2.AddRow();
            dataRow2.Cells[0].AddParagraph(_config.ConnectionName);
            dataRow2.Cells[1].AddParagraph(_target2.ToString()).Format.Alignment = ParagraphAlignment.Right;
            dataRow2.Cells[2].AddParagraph(_torque2.ToString()).Format.Alignment = ParagraphAlignment.Right;

            // Chart Image Placeholder
            var bitmap2 = new Bitmap(_chart2.Width, _chart2.Height);
            _chart2.DrawToBitmap(bitmap2, _chart2.ClientRectangle);
            bitmap2.Save("torque_chart2.png", ImageFormat.Png);

            section2.AddParagraph("\n");
            var chartImagePath2 = "torque_chart2.png"; // Ensure chart is generated separately
            if (File.Exists(chartImagePath2))
            {
                var chartImage = section2.AddImage(chartImagePath2);
                chartImage.Width = "16cm";
                chartImage.LockAspectRatio = true;
                chartImage.Top = ShapePosition.Top;
                chartImage.Left = ShapePosition.Center;
            }

            //// Footer text (optional)
            //var www = section.AddParagraph("www.nov.com");
            //www.Format.Font.Color = Colors.DarkBlue;
            //www.Format.SpaceBefore = "1cm";
            //www.Format.Alignment = ParagraphAlignment.Left;
            #endregion

            return doc;
        }
    }
}