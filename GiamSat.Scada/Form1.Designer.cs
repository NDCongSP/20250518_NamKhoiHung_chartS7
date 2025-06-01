
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
            this.label2 = new System.Windows.Forms.Label();
            this.easyTextBox1 = new EasyScada.Winforms.Controls.EasyTextBox();
            this.easyTextBox2 = new EasyScada.Winforms.Controls.EasyTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.easyLabel1 = new EasyScada.Winforms.Controls.EasyLabel();
            this.easyLabel2 = new EasyScada.Winforms.Controls.EasyLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.easyTextBox3 = new EasyScada.Winforms.Controls.EasyTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.easyTextBox4 = new EasyScada.Winforms.Controls.EasyTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this._btnStartStop = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._chart2 = new LiveCharts.WinForms.CartesianChart();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox4)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 774);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "TT kết nối Driver:";
            // 
            // _labSriverStatus
            // 
            this._labSriverStatus.AutoSize = true;
            this._labSriverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labSriverStatus.Location = new System.Drawing.Point(132, 774);
            this._labSriverStatus.Name = "_labSriverStatus";
            this._labSriverStatus.Size = new System.Drawing.Size(98, 20);
            this._labSriverStatus.TabIndex = 5;
            this._labSriverStatus.Text = "Driver status";
            // 
            // _labTime
            // 
            this._labTime.AutoSize = true;
            this._labTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labTime.Location = new System.Drawing.Point(1069, 774);
            this._labTime.Name = "_labTime";
            this._labTime.Size = new System.Drawing.Size(183, 20);
            this._labTime.TabIndex = 2;
            this._labTime.Text = "dd/MM/YYYY HH:mm:ss";
            this._labTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(-1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1267, 64);
            this.label1.TabIndex = 7;
            this.label1.Text = "CONNECTION TORQUE LOG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _chart1
            // 
            this._chart1.Location = new System.Drawing.Point(6, 6);
            this._chart1.Name = "_chart1";
            this._chart1.Size = new System.Drawing.Size(1198, 555);
            this._chart1.TabIndex = 11;
            this._chart1.Text = "cartesianChart1";
            // 
            // _btnUpdate
            // 
            this._btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this._btnUpdate.Location = new System.Drawing.Point(1015, 77);
            this._btnUpdate.Name = "_btnUpdate";
            this._btnUpdate.Size = new System.Drawing.Size(224, 37);
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
            this._btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this._btnExport.Location = new System.Drawing.Point(1015, 120);
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(224, 37);
            this._btnExport.TabIndex = 13;
            this._btnExport.Text = "Export PDF";
            this._btnExport.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(18, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Max 1";
            // 
            // easyTextBox1
            // 
            this.easyTextBox1.DropDownBackColor = System.Drawing.SystemColors.Control;
            this.easyTextBox1.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.easyTextBox1.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this.easyTextBox1.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyTextBox1.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this.easyTextBox1.HightLightStatusTime = 3;
            this.easyTextBox1.Location = new System.Drawing.Point(66, 90);
            this.easyTextBox1.Name = "easyTextBox1";
            this.easyTextBox1.Role = null;
            this.easyTextBox1.Size = new System.Drawing.Size(100, 20);
            this.easyTextBox1.StringFormat = null;
            this.easyTextBox1.TabIndex = 15;
            this.easyTextBox1.TagPath = "Local Station/Channel1/Device1/Max1";
            this.easyTextBox1.Text = "easyTextBox1";
            this.easyTextBox1.WriteDelay = 200;
            this.easyTextBox1.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // easyTextBox2
            // 
            this.easyTextBox2.DropDownBackColor = System.Drawing.SystemColors.Control;
            this.easyTextBox2.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.easyTextBox2.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this.easyTextBox2.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyTextBox2.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this.easyTextBox2.HightLightStatusTime = 3;
            this.easyTextBox2.Location = new System.Drawing.Point(320, 90);
            this.easyTextBox2.Name = "easyTextBox2";
            this.easyTextBox2.Role = null;
            this.easyTextBox2.Size = new System.Drawing.Size(100, 20);
            this.easyTextBox2.StringFormat = null;
            this.easyTextBox2.TabIndex = 17;
            this.easyTextBox2.TagPath = "Local Station/Channel1/Device1/Target1";
            this.easyTextBox2.Text = "easyTextBox2";
            this.easyTextBox2.WriteDelay = 200;
            this.easyTextBox2.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(243, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Target 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(485, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Torque Value 1";
            // 
            // easyLabel1
            // 
            this.easyLabel1.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyLabel1.Location = new System.Drawing.Point(597, 92);
            this.easyLabel1.Name = "easyLabel1";
            this.easyLabel1.Size = new System.Drawing.Size(100, 23);
            this.easyLabel1.StringFormat = null;
            this.easyLabel1.TabIndex = 19;
            this.easyLabel1.TagPath = "Local Station/Channel1/Device1/Value1";
            this.easyLabel1.Text = "easyLabel1";
            // 
            // easyLabel2
            // 
            this.easyLabel2.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyLabel2.Location = new System.Drawing.Point(597, 135);
            this.easyLabel2.Name = "easyLabel2";
            this.easyLabel2.Size = new System.Drawing.Size(100, 23);
            this.easyLabel2.StringFormat = null;
            this.easyLabel2.TabIndex = 25;
            this.easyLabel2.TagPath = "Local Station/Channel1/Device1/Value2";
            this.easyLabel2.Text = "easyLabel2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(485, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 17);
            this.label6.TabIndex = 24;
            this.label6.Text = "Torque Value 2";
            // 
            // easyTextBox3
            // 
            this.easyTextBox3.DropDownBackColor = System.Drawing.SystemColors.Control;
            this.easyTextBox3.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.easyTextBox3.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this.easyTextBox3.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyTextBox3.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this.easyTextBox3.HightLightStatusTime = 3;
            this.easyTextBox3.Location = new System.Drawing.Point(320, 133);
            this.easyTextBox3.Name = "easyTextBox3";
            this.easyTextBox3.Role = null;
            this.easyTextBox3.Size = new System.Drawing.Size(100, 20);
            this.easyTextBox3.StringFormat = null;
            this.easyTextBox3.TabIndex = 23;
            this.easyTextBox3.TagPath = "Local Station/Channel1/Device1/Target2";
            this.easyTextBox3.Text = "easyTextBox3";
            this.easyTextBox3.WriteDelay = 200;
            this.easyTextBox3.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(243, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "Target 2";
            // 
            // easyTextBox4
            // 
            this.easyTextBox4.DropDownBackColor = System.Drawing.SystemColors.Control;
            this.easyTextBox4.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.easyTextBox4.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this.easyTextBox4.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyTextBox4.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this.easyTextBox4.HightLightStatusTime = 3;
            this.easyTextBox4.Location = new System.Drawing.Point(66, 133);
            this.easyTextBox4.Name = "easyTextBox4";
            this.easyTextBox4.Role = null;
            this.easyTextBox4.Size = new System.Drawing.Size(100, 20);
            this.easyTextBox4.StringFormat = null;
            this.easyTextBox4.TabIndex = 21;
            this.easyTextBox4.TagPath = "Local Station/Channel1/Device1/Max2";
            this.easyTextBox4.Text = "easyTextBox4";
            this.easyTextBox4.WriteDelay = 200;
            this.easyTextBox4.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(18, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Max 2";
            // 
            // _btnStartStop
            // 
            this._btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this._btnStartStop.Location = new System.Drawing.Point(740, 77);
            this._btnStartStop.Name = "_btnStartStop";
            this._btnStartStop.Size = new System.Drawing.Size(246, 80);
            this._btnStartStop.TabIndex = 26;
            this._btnStartStop.Text = "Start/Stop";
            this._btnStartStop.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(21, 178);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1218, 593);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._chart1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1210, 567);
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
            this.tabPage2.Size = new System.Drawing.Size(1210, 567);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TORQUE 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _chart2
            // 
            this._chart2.Location = new System.Drawing.Point(6, 6);
            this._chart2.Name = "_chart2";
            this._chart2.Size = new System.Drawing.Size(1198, 555);
            this._chart2.TabIndex = 12;
            this._chart2.Text = "cartesianChart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 808);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._btnStartStop);
            this.Controls.Add(this.easyLabel2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.easyTextBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.easyTextBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.easyLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.easyTextBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.easyTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._btnExport);
            this.Controls.Add(this._btnUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._labSriverStatus);
            this.Controls.Add(this._labTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHART DRAWING";
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox4)).EndInit();
            this.tabControl1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label2;
        private EasyScada.Winforms.Controls.EasyTextBox easyTextBox1;
        private EasyScada.Winforms.Controls.EasyTextBox easyTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel1;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel2;
        private System.Windows.Forms.Label label6;
        private EasyScada.Winforms.Controls.EasyTextBox easyTextBox3;
        private System.Windows.Forms.Label label7;
        private EasyScada.Winforms.Controls.EasyTextBox easyTextBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button _btnStartStop;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private LiveCharts.WinForms.CartesianChart _chart2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

