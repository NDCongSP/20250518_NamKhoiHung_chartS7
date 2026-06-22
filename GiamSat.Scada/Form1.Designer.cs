
namespace GiamSat.Scada
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label4 = new System.Windows.Forms.Label();
            this._labSriverStatus = new System.Windows.Forms.Label();
            this._labTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._chart1 = new LiveCharts.WinForms.CartesianChart();
            this._btnUpdate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._btnExport = new System.Windows.Forms.Button();
            this._labMax1 = new System.Windows.Forms.Label();
            this._easyTextBoxMax1 = new EasyScada.Winforms.Controls.EasyTextBox();
            this._easyTextBoxTarget1 = new EasyScada.Winforms.Controls.EasyTextBox();
            this._labTarget1 = new System.Windows.Forms.Label();
            this._labTorque1 = new System.Windows.Forms.Label();
            this._easyLabelTorque1 = new EasyScada.Winforms.Controls.EasyLabel();
            this._easyLabelTorque2 = new EasyScada.Winforms.Controls.EasyLabel();
            this._labTorque2 = new System.Windows.Forms.Label();
            this._easyTextBoxTarget2 = new EasyScada.Winforms.Controls.EasyTextBox();
            this._labTarget2 = new System.Windows.Forms.Label();
            this._easyTextBoxMax2 = new EasyScada.Winforms.Controls.EasyTextBox();
            this._labMax2 = new System.Windows.Forms.Label();
            this._btnStartStop = new System.Windows.Forms.Button();
            this._tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._chart2 = new LiveCharts.WinForms.CartesianChart();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxMax1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxTarget1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyLabelTorque1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyLabelTorque2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxTarget2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxMax2)).BeginInit();
            this._tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 654);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "TT kết nối Driver:";
            // 
            // _labSriverStatus
            // 
            this._labSriverStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labSriverStatus.AutoSize = true;
            this._labSriverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labSriverStatus.Location = new System.Drawing.Point(130, 654);
            this._labSriverStatus.Name = "_labSriverStatus";
            this._labSriverStatus.Size = new System.Drawing.Size(98, 20);
            this._labSriverStatus.TabIndex = 5;
            this._labSriverStatus.Text = "Driver status";
            // 
            // _labTime
            // 
            this._labTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._labTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labTime.Location = new System.Drawing.Point(810, 654);
            this._labTime.Name = "_labTime";
            this._labTime.Size = new System.Drawing.Size(178, 20);
            this._labTime.TabIndex = 2;
            this._labTime.Text = "dd/MM/YYYY HH:mm:ss";
            this._labTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1000, 50);
            this.label1.TabIndex = 7;
            this.label1.Text = "CONNECTION TORQUE LOG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _chart1
            // 
            this._chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chart1.Location = new System.Drawing.Point(3, 3);
            this._chart1.Name = "_chart1";
            this._chart1.Size = new System.Drawing.Size(970, 482);
            this._chart1.TabIndex = 11;
            this._chart1.Text = "cartesianChart1";
            // 
            // _btnUpdate
            // 
            this._btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this._btnUpdate.Location = new System.Drawing.Point(810, 56);
            this._btnUpdate.Name = "_btnUpdate";
            this._btnUpdate.Size = new System.Drawing.Size(178, 33);
            this._btnUpdate.TabIndex = 12;
            this._btnUpdate.Text = "Update Information";
            this._btnUpdate.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // _btnExport
            // 
            this._btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this._btnExport.Location = new System.Drawing.Point(810, 95);
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(178, 33);
            this._btnExport.TabIndex = 13;
            this._btnExport.Text = "Export PDF";
            this._btnExport.UseVisualStyleBackColor = true;
            // 
            // _labMax1
            // 
            this._labMax1.AutoSize = true;
            this._labMax1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._labMax1.Location = new System.Drawing.Point(12, 66);
            this._labMax1.Name = "_labMax1";
            this._labMax1.Size = new System.Drawing.Size(45, 17);
            this._labMax1.TabIndex = 14;
            this._labMax1.Text = "Max 1";
            // 
            // _easyTextBoxMax1
            // 
            this._easyTextBoxMax1.DropDownBackColor = System.Drawing.SystemColors.Control;
            this._easyTextBoxMax1.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._easyTextBoxMax1.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this._easyTextBoxMax1.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._easyTextBoxMax1.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this._easyTextBoxMax1.HightLightStatusTime = 3;
            this._easyTextBoxMax1.Location = new System.Drawing.Point(60, 64);
            this._easyTextBoxMax1.Name = "_easyTextBoxMax1";
            this._easyTextBoxMax1.Role = null;
            this._easyTextBoxMax1.Size = new System.Drawing.Size(100, 20);
            this._easyTextBoxMax1.StringFormat = null;
            this._easyTextBoxMax1.TabIndex = 15;
            this._easyTextBoxMax1.TagPath = "Local Station/Channel1/Device1/Max1";
            this._easyTextBoxMax1.Text = "easyTextBox1";
            this._easyTextBoxMax1.WriteDelay = 200;
            this._easyTextBoxMax1.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // _easyTextBoxTarget1
            // 
            this._easyTextBoxTarget1.DropDownBackColor = System.Drawing.SystemColors.Control;
            this._easyTextBoxTarget1.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._easyTextBoxTarget1.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this._easyTextBoxTarget1.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._easyTextBoxTarget1.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this._easyTextBoxTarget1.HightLightStatusTime = 3;
            this._easyTextBoxTarget1.Location = new System.Drawing.Point(230, 64);
            this._easyTextBoxTarget1.Name = "_easyTextBoxTarget1";
            this._easyTextBoxTarget1.Role = null;
            this._easyTextBoxTarget1.Size = new System.Drawing.Size(100, 20);
            this._easyTextBoxTarget1.StringFormat = null;
            this._easyTextBoxTarget1.TabIndex = 17;
            this._easyTextBoxTarget1.TagPath = "Local Station/Channel1/Device1/Target1";
            this._easyTextBoxTarget1.Text = "easyTextBox2";
            this._easyTextBoxTarget1.WriteDelay = 200;
            this._easyTextBoxTarget1.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // _labTarget1
            // 
            this._labTarget1.AutoSize = true;
            this._labTarget1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._labTarget1.Location = new System.Drawing.Point(160, 66);
            this._labTarget1.Name = "_labTarget1";
            this._labTarget1.Size = new System.Drawing.Size(62, 17);
            this._labTarget1.TabIndex = 16;
            this._labTarget1.Text = "Target 1";
            // 
            // _labTorque1
            // 
            this._labTorque1.AutoSize = true;
            this._labTorque1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._labTorque1.Location = new System.Drawing.Point(330, 66);
            this._labTorque1.Name = "_labTorque1";
            this._labTorque1.Size = new System.Drawing.Size(106, 17);
            this._labTorque1.TabIndex = 18;
            this._labTorque1.Text = "Torque Value 1";
            // 
            // _easyLabelTorque1
            // 
            this._easyLabelTorque1.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this._easyLabelTorque1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._easyLabelTorque1.Location = new System.Drawing.Point(450, 64);
            this._easyLabelTorque1.Name = "_easyLabelTorque1";
            this._easyLabelTorque1.Size = new System.Drawing.Size(100, 23);
            this._easyLabelTorque1.StringFormat = null;
            this._easyLabelTorque1.TabIndex = 19;
            this._easyLabelTorque1.TagPath = "Local Station/Channel1/Device1/Value1";
            this._easyLabelTorque1.Text = "easyLabel1";
            // 
            // _easyLabelTorque2
            // 
            this._easyLabelTorque2.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this._easyLabelTorque2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._easyLabelTorque2.Location = new System.Drawing.Point(450, 98);
            this._easyLabelTorque2.Name = "_easyLabelTorque2";
            this._easyLabelTorque2.Size = new System.Drawing.Size(100, 23);
            this._easyLabelTorque2.StringFormat = null;
            this._easyLabelTorque2.TabIndex = 25;
            this._easyLabelTorque2.TagPath = "Local Station/Channel1/Device1/Value2";
            this._easyLabelTorque2.Text = "easyLabel2";
            // 
            // _labTorque2
            // 
            this._labTorque2.AutoSize = true;
            this._labTorque2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._labTorque2.Location = new System.Drawing.Point(330, 100);
            this._labTorque2.Name = "_labTorque2";
            this._labTorque2.Size = new System.Drawing.Size(106, 17);
            this._labTorque2.TabIndex = 24;
            this._labTorque2.Text = "Torque Value 2";
            // 
            // _easyTextBoxTarget2
            // 
            this._easyTextBoxTarget2.DropDownBackColor = System.Drawing.SystemColors.Control;
            this._easyTextBoxTarget2.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._easyTextBoxTarget2.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this._easyTextBoxTarget2.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._easyTextBoxTarget2.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this._easyTextBoxTarget2.HightLightStatusTime = 3;
            this._easyTextBoxTarget2.Location = new System.Drawing.Point(230, 98);
            this._easyTextBoxTarget2.Name = "_easyTextBoxTarget2";
            this._easyTextBoxTarget2.Role = null;
            this._easyTextBoxTarget2.Size = new System.Drawing.Size(100, 20);
            this._easyTextBoxTarget2.StringFormat = null;
            this._easyTextBoxTarget2.TabIndex = 23;
            this._easyTextBoxTarget2.TagPath = "Local Station/Channel1/Device1/Target2";
            this._easyTextBoxTarget2.Text = "easyTextBox3";
            this._easyTextBoxTarget2.WriteDelay = 200;
            this._easyTextBoxTarget2.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // _labTarget2
            // 
            this._labTarget2.AutoSize = true;
            this._labTarget2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._labTarget2.Location = new System.Drawing.Point(160, 100);
            this._labTarget2.Name = "_labTarget2";
            this._labTarget2.Size = new System.Drawing.Size(62, 17);
            this._labTarget2.TabIndex = 22;
            this._labTarget2.Text = "Target 2";
            // 
            // _easyTextBoxMax2
            // 
            this._easyTextBoxMax2.DropDownBackColor = System.Drawing.SystemColors.Control;
            this._easyTextBoxMax2.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._easyTextBoxMax2.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this._easyTextBoxMax2.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._easyTextBoxMax2.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this._easyTextBoxMax2.HightLightStatusTime = 3;
            this._easyTextBoxMax2.Location = new System.Drawing.Point(60, 98);
            this._easyTextBoxMax2.Name = "_easyTextBoxMax2";
            this._easyTextBoxMax2.Role = null;
            this._easyTextBoxMax2.Size = new System.Drawing.Size(100, 20);
            this._easyTextBoxMax2.StringFormat = null;
            this._easyTextBoxMax2.TabIndex = 21;
            this._easyTextBoxMax2.TagPath = "Local Station/Channel1/Device1/Max2";
            this._easyTextBoxMax2.Text = "easyTextBox4";
            this._easyTextBoxMax2.WriteDelay = 200;
            this._easyTextBoxMax2.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // _labMax2
            // 
            this._labMax2.AutoSize = true;
            this._labMax2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._labMax2.Location = new System.Drawing.Point(12, 100);
            this._labMax2.Name = "_labMax2";
            this._labMax2.Size = new System.Drawing.Size(45, 17);
            this._labMax2.TabIndex = 20;
            this._labMax2.Text = "Max 2";
            // 
            // _btnStartStop
            // 
            this._btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this._btnStartStop.Location = new System.Drawing.Point(600, 56);
            this._btnStartStop.Name = "_btnStartStop";
            this._btnStartStop.Size = new System.Drawing.Size(190, 72);
            this._btnStartStop.TabIndex = 26;
            this._btnStartStop.Text = "Start/Stop";
            this._btnStartStop.UseVisualStyleBackColor = true;
            // 
            // _tab
            // 
            this._tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tab.Controls.Add(this.tabPage1);
            this._tab.Controls.Add(this.tabPage2);
            this._tab.Location = new System.Drawing.Point(12, 134);
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(976, 512);
            this._tab.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._chart1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(968, 486);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TORQUE 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._chart2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(968, 486);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TORQUE 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _chart2
            // 
            this._chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chart2.Location = new System.Drawing.Point(3, 3);
            this._chart2.Name = "_chart2";
            this._chart2.Size = new System.Drawing.Size(970, 482);
            this._chart2.TabIndex = 12;
            this._chart2.Text = "cartesianChart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.MinimumSize = new System.Drawing.Size(820, 600);
            this.Controls.Add(this._tab);
            this.Controls.Add(this._btnStartStop);
            this.Controls.Add(this._easyLabelTorque2);
            this.Controls.Add(this._labTorque2);
            this.Controls.Add(this._easyTextBoxTarget2);
            this.Controls.Add(this._labTarget2);
            this.Controls.Add(this._easyTextBoxMax2);
            this.Controls.Add(this._labMax2);
            this.Controls.Add(this._easyLabelTorque1);
            this.Controls.Add(this._labTorque1);
            this.Controls.Add(this._easyTextBoxTarget1);
            this.Controls.Add(this._labTarget1);
            this.Controls.Add(this._easyTextBoxMax1);
            this.Controls.Add(this._labMax1);
            this.Controls.Add(this._btnExport);
            this.Controls.Add(this._btnUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._labSriverStatus);
            this.Controls.Add(this._labTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHART DRAWING";
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxMax1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxTarget1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyLabelTorque1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyLabelTorque2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxTarget2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._easyTextBoxMax2)).EndInit();
            this._tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _labSriverStatus;
        private System.Windows.Forms.Label _labTime;
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.CartesianChart _chart1;
        private System.Windows.Forms.Button _btnUpdate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Label _labMax1;
        private EasyScada.Winforms.Controls.EasyTextBox _easyTextBoxMax1;
        private EasyScada.Winforms.Controls.EasyTextBox _easyTextBoxTarget1;
        private System.Windows.Forms.Label _labTarget1;
        private System.Windows.Forms.Label _labTorque1;
        private EasyScada.Winforms.Controls.EasyLabel _easyLabelTorque1;
        private EasyScada.Winforms.Controls.EasyLabel _easyLabelTorque2;
        private System.Windows.Forms.Label _labTorque2;
        private EasyScada.Winforms.Controls.EasyTextBox _easyTextBoxTarget2;
        private System.Windows.Forms.Label _labTarget2;
        private EasyScada.Winforms.Controls.EasyTextBox _easyTextBoxMax2;
        private System.Windows.Forms.Label _labMax2;
        private System.Windows.Forms.Button _btnStartStop;
        private System.Windows.Forms.TabControl _tab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private LiveCharts.WinForms.CartesianChart _chart2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

