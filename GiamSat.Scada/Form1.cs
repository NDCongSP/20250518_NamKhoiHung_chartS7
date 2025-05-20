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

        double _torque = 0, _max = 0, _target = 0, _trigger = 0;

        //List<double> timeData = new List<double>();
        List<string> timeData = new List<string>();
        List<double> torqueData = new List<double>();
        List<double> _targetData = new List<double>();
        List<double> _maxData = new List<double>();

        private model _config = new model();

        private System.Threading.CancellationTokenSource _cts;

        private Random rnd = new Random();

        int _xLable = 0;
        bool _isResetChart = true;

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
                            _chart2.Series[0].Values.Add(_torque);
                            _chart2.Series[1].Values.Add(_target);
                            _chart2.Series[2].Values.Add(_max);
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

            #region Chart initialize
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
                LineSmoothness = 0,
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
                LineSmoothness = 0,
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

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    LoadDataFromFile(openFileDialog1.FileName);
            //    CapNhatChartLine();
            //}
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
            timeData.Clear();
            torqueData.Clear();

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
                    timeData.Add(t.ToString());
                    torqueData.Add(tq);
                    _targetData.Add(tg);
                    _maxData.Add(tm);
                }
            }
        }

        private void CapNhatChartLine()
        {
            _chart2.Series[0].Values.Clear();
            _chart2.Series[1].Values.Clear();
            _chart2.Series[2].Values.Clear();

            _chart2.VisualElements.Clear();

            _chart2.VisualElements.Add(new VisualElement
            {
                X = 1, // hoặc vị trí bạn muốn (tính theo tọa độ X trên chart)
                Y = _max,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = $"Max = {_max}",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });
            _chart2.VisualElements.Add(new VisualElement
            {
                X = 1,
                Y = _target,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                UIElement = new System.Windows.Controls.TextBlock
                {
                    Text = $"Target = {(_target)}",
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = System.Windows.FontWeights.Bold
                }
            });

            List<string> labels = new List<string>();

            for (int i = 0; i < timeData.Count; i++)
            {
                _chart2.Series[0].Values.Add(torqueData[i]);
                _chart2.Series[1].Values.Add(_targetData[i]);
                _chart2.Series[2].Values.Add(_maxData[i]);
                labels.Add(timeData[i].ToString());
            }

            _chart2.AxisX[0].Labels = labels;

            _chart2.Update(true, true);
        }

        private void UpdateChart()
        {
            GlobalVariable.InvokeIfRequired(this, () =>
            {
                // Tạo giá trị mới
                //double newTorque = rnd.Next(10000, 45000); // Tùy chỉnh min/max phù hợp
                double newTorque = _torque; // Tùy chỉnh min/max phù hợp
                double newTarget = _target;
                double newMax = _max;

                // Thêm dữ liệu mới và kiểm tra số lượng
                var torqueSeries = _chart2.Series[0].Values;
                var maxSeries = _chart2.Series[1].Values;
                var targetSeries = _chart2.Series[2].Values;
                //var labels = _chart2.AxisX[0].Labels.ToList();


                if (torqueSeries.Count >= _config.maxPoints)
                {
                    var pointRemove = torqueSeries.Count - _config.maxPoints;

                    for (int i = 0; i < pointRemove; i++)
                    {
                        torqueSeries.RemoveAt(i);
                        maxSeries.RemoveAt(i);
                        targetSeries.RemoveAt(i);
                        timeData.RemoveAt(i);
                    }
                    //torqueSeries.RemoveAt(0);
                    //maxSeries.RemoveAt(0);
                    //targetSeries.RemoveAt(0);
                    //timeData.RemoveAt(0);
                }

                torqueSeries.Add(newTorque);
                maxSeries.Add(newMax);
                targetSeries.Add(newTarget);
                //timeData.Add(DateTime.Now.Second.ToString());
                timeData.Add(_xLable.ToString());
                _chart2.AxisX[0].Labels = timeData;

                #region  Cập nhật lại VisualElements để giữ label cho Max và Target
                _chart2.VisualElements.Clear();
                _chart2.VisualElements.Add(new VisualElement
                {
                    X = 2,
                    Y = _max,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    UIElement = new System.Windows.Controls.TextBlock
                    {
                        Text = $"Max = {_max}",
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontWeight = System.Windows.FontWeights.Bold
                    }
                });
                _chart2.VisualElements.Add(new VisualElement
                {
                    X = 2,
                    Y = _target,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    UIElement = new System.Windows.Controls.TextBlock
                    {
                        Text = $"Target = {_target}",
                        Foreground = System.Windows.Media.Brushes.Black,
                        FontWeight = System.Windows.FontWeights.Bold
                    }
                });
                #endregion

                // Cập nhật lại label cho trục X
                _xLable += 1;

                _chart2.Update(false, true);
            });
        }

        private void _btnExport_Click(object sender, EventArgs e)
        {
            var doc = CreateDocument();
            var renderer = new PdfDocumentRenderer(true) { Document = doc };
            renderer.RenderDocument();

            var outputPath = Path.Combine(Application.StartupPath, "TorqueReport.pdf");
            renderer.PdfDocument.Save(outputPath);
            Process.Start("explorer.exe", outputPath); // Mở file sau khi xuất
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
                _chart2.Series[0].Values.Clear();
                _chart2.Series[1].Values.Clear();
                _chart2.Series[2].Values.Clear();

                _chart2.VisualElements.Clear();

                if (_chart2.AxisX[0]?.Labels is IList<string> labels && !labels.IsReadOnly)
                {
                    labels.Clear();
                }
                else
                {
                    _chart2.AxisX[0].Labels = new List<string>();
                }

                _chart2.Update(true, true);
            });
        }

        private void _easyDriverConnector_Started(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            //foreach (var item in _ovensInfo)
            {
                //easyDriverConnector1.GetTag($"{item.Path}/Temperature").QualityChanged += Temperature_QualityChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value").ValueChanged += Value_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max").ValueChanged += Max_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target").ValueChanged += Target_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Trigger").ValueChanged += Trigger_ValueChanged;

                Value_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value")
                    , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value")
                    , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value").Value));
                Max_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Max").Value));
                Target_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Target").Value));
                Trigger_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Trigger")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Trigger")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Trigger").Value));
            }
        }

        #region Event tag value change

        private void Value_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _torque = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Max_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _max = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Target_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _target = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Trigger_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                _trigger = double.TryParse(e.NewValue, out double value) ? value : 0;

                if (_trigger != 0 && _isResetChart == true) { _isResetChart = false; }

                if (!_isResetChart)
                {
                    ResetChart();
                    _xLable = 0;
                    _isResetChart = true;
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        #endregion

        #endregion

        private Document CreateDocument()
        {
            var bitmap = new Bitmap(_chart2.Width, _chart2.Height);
            _chart2.DrawToBitmap(bitmap, _chart2.ClientRectangle);
            bitmap.Save("torque_chart.png", ImageFormat.Png);

            var doc = new Document();
            var section = doc.AddSection();

            // Setup margins
            section.PageSetup.TopMargin = "1cm";
            section.PageSetup.BottomMargin = "1cm";
            section.PageSetup.LeftMargin = "2cm";
            section.PageSetup.RightMargin = "2cm";

            // Watermark
            var watermark = section.Headers.Primary.AddParagraph("CONFIDENTIAL");
            watermark.Format.Font.Size = 50;
            watermark.Format.Font.Color = Colors.LightGray;
            watermark.Format.Font.Bold = true;
            watermark.Format.Alignment = ParagraphAlignment.Center;
            watermark.Format.SpaceBefore = "8cm";

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
                "OPERATOR: ZAHADI\n" +
                "DATE: 09 APR 25 (10:29:50)\n" +
                "TOOL: Series Jar\n" +
                "JOB NO.: ASSEMBLE HQ650 JAR\n" +
                "SERIES:\n" +
                "TOOL S/N: OSC115748A"
            );
            row.Cells[2].AddParagraph(
                "LOG MASTER™ II S/N: LM2-586\n" +
                "TORQUEMASTER™ S/N: 8025 - 5020\n" +
                "CONSOLE S/N: 8018 - 5032"
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
            dataRow.Cells[0].AddParagraph("LFJ-KM");
            dataRow.Cells[1].AddParagraph("40,600").Format.Alignment = ParagraphAlignment.Right;
            dataRow.Cells[2].AddParagraph("40,960").Format.Alignment = ParagraphAlignment.Right;

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

            // Footer text (optional)
            var www = section.AddParagraph("www.nov.com");
            www.Format.Font.Color = Colors.DarkBlue;
            www.Format.SpaceBefore = "1cm";
            www.Format.Alignment = ParagraphAlignment.Left;

            return doc;
        }

        private Document CreateDocumentBK()
        {
            var bitmap = new Bitmap(_chart2.Width, _chart2.Height);
            _chart2.DrawToBitmap(bitmap, _chart2.ClientRectangle);
            bitmap.Save("torque_chart.png", ImageFormat.Png);

            var doc = new Document();
            var section = doc.AddSection();

            // Cài đặt lề trang
            section.PageSetup.TopMargin = "1cm";
            section.PageSetup.BottomMargin = "1cm";
            section.PageSetup.LeftMargin = "1.5cm";
            section.PageSetup.RightMargin = "1.5cm";

            // ====== THÊM WATERMARK "CONFIDENTIAL" ======
            var watermark = section.Headers.Primary.AddParagraph("CONFIDENTIAL");
            watermark.Format.Font.Size = 50;
            watermark.Format.Font.Color = Colors.LightGray;
            watermark.Format.Font.Bold = true;
            watermark.Format.Alignment = ParagraphAlignment.Center;
            watermark.Format.SpaceBefore = "8cm"; // căn giữa chiều dọc

            // ====== THÊM LOGO GÓC DƯỚI PHẢI ======
            var logoPath = "LogoPT.png"; // bạn có thể dùng đường dẫn tuyệt đối nếu cần
            var imageLogo = section.Footers.Primary.AddImage(logoPath);
            imageLogo.Width = "3cm";
            imageLogo.LockAspectRatio = true;
            imageLogo.Left = ShapePosition.Right;
            imageLogo.Top = ShapePosition.Bottom;
            imageLogo.WrapFormat.Style = WrapStyle.Through;

            // Tiêu đề
            var title = section.AddParagraph("CONNECTION TORQUE LOG");
            title.Format.Font.Size = 14;
            title.Format.Font.Bold = true;
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.SpaceAfter = "0.5cm";

            // Bảng thông tin đầu trang
            MigraDoc.DocumentObjectModel.Tables.Table infoTable = section.AddTable();
            infoTable.AddColumn("7cm");
            infoTable.AddColumn("7cm");

            void AddInfoRow(string left, string right)
            {
                var row = infoTable.AddRow();
                row.Cells[0].AddParagraph(left);
                row.Cells[1].AddParagraph(right);
                row.Cells[0].Format.Alignment = ParagraphAlignment.Justify;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Justify;
            }

            AddInfoRow("OPERATOR: ZAHADI", "LOG MASTER™ II S/N: LM2-586");
            AddInfoRow("DATE: 09 APR 25 (10:29:50)", "TORQUEMASTER™ S/N: 8025 - 5020");
            AddInfoRow("TOOL: Series Jar", "CONSOLE S/N: 8018 - 5032");
            AddInfoRow("JOB NO.: ASSEMBLE HQ650 JAR", "");
            AddInfoRow("SERIES:", "");
            AddInfoRow("TOOL S/N: OSC115748A", "");

            // Tiêu đề bảng dữ liệu
            //section.AddParagraph("\nLOGGED TORQUE VALUES").Format.Font.Bold = true;
            var logTitle = section.AddParagraph("\nLOGGED TORQUE VALUES");
            logTitle.Format.Font.Bold = true;
            logTitle.Format.Alignment = ParagraphAlignment.Justify;

            // Bảng dữ liệu torque
            MigraDoc.DocumentObjectModel.Tables.Table dataTable = section.AddTable();
            dataTable.Borders.Width = 0.5;
            dataTable.AddColumn("2cm");
            dataTable.AddColumn("5cm");
            dataTable.AddColumn("4cm");
            dataTable.AddColumn("4cm");

            var headerRow = dataTable.AddRow();
            headerRow.Shading.Color = Colors.LightGray;
            headerRow.Cells[0].AddParagraph(" ");
            headerRow.Cells[1].AddParagraph("Connection Name");
            headerRow.Cells[2].AddParagraph("Target Torque\n(lb-ft)");
            headerRow.Cells[3].AddParagraph("Logged Torque\n(lb-ft)");

            var dataRow = dataTable.AddRow();
            dataRow.Cells[0].AddParagraph("1");
            dataRow.Cells[1].AddParagraph("LFJ-KM");
            dataRow.Cells[2].AddParagraph("40,600");
            dataRow.Cells[3].AddParagraph("40,960");

            section.AddParagraph("\n");

            // Thêm hình ảnh biểu đồ torque nếu có
            string chartImagePath = Path.Combine(Application.StartupPath, "torque_chart.png");
            if (File.Exists(chartImagePath))
            {
                var image = section.AddImage(chartImagePath);
                image.Width = "16cm";
                image.LockAspectRatio = true;
            }
            else
            {
                section.AddParagraph("[Không tìm thấy ảnh biểu đồ]");
            }

            // Footer website
            section.AddParagraph("\nwww.nov.com").Format.Alignment = ParagraphAlignment.Left;

            return doc;
        }
    }
}