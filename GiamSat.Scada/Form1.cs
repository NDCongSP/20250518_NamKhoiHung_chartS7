using Dapper;
using EasyScada.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Serilog;
using System.IO;
using EasyScada.Winforms.Controls;
using System.Runtime.InteropServices;


namespace GiamSat.Scada
{
    public partial class Form1 : Form
    {
        private EasyDriverConnector _easyDriverConnector;

        private Timer _timer = new Timer();

        string _fileName;
        string _filePath;

        double _value = 0, _Line_1 = 0, _Line_2 = 0;

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

        private Color GetConnectionStatusColor(ConnectionStatus status)
        {
            switch (status)
            {
                case ConnectionStatus.Connected:
                    return Color.Lime;
                case ConnectionStatus.Connecting:
                case ConnectionStatus.Reconnecting:
                    return Color.Orange;
                case ConnectionStatus.Disconnected:
                    return Color.Red;
                default:
                    return Color.White;
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
            _timer.Interval = 500;
            _timer.Tick += _timer_Tick;
            _timer.Enabled = true;
        }

        #region Events
        private async void _timer_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            try
            {
                t.Enabled = false;
                GlobalVariable.InvokeIfRequired(this, () => { _labTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); });
            }
            catch (Exception ex) { Log.Error(ex, "From _timer_Tick"); }
            finally
            {
                t.Enabled = true;
            }
        }

        private void _easyDriverConnector_Started(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            //foreach (var item in _ovensInfo)
            {
                //easyDriverConnector1.GetTag($"{item.Path}/Temperature").QualityChanged += Temperature_QualityChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value").ValueChanged += Value_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_1").ValueChanged += Line_1_ValueChanged;
                _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_2").ValueChanged += Line_2_ValueChanged;

                Value_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value")
                    , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value")
                    , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Value").Value));
                Line_1_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_1")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_1")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_1").Value));
                Line_2_ValueChanged(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_2")
                   , new TagValueChangedEventArgs(_easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_2")
                   , "", _easyDriverConnector.GetTag($"Local Station/Channel1/Device1/Line_2").Value));
            }
        }

        #region Event tag value change

        private void Value_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Line_1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        private void Line_2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var path = e?.Tag.Parent.Path;

                var newValue = double.TryParse(e.NewValue, out double value) ? value : 0;
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        #endregion

        #endregion
    }
}