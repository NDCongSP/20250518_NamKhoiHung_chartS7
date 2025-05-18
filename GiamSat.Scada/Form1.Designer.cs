
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
            this._chart2 = new LiveCharts.WinForms.CartesianChart();
            this._btnUpdate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._btnExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.easyTextBox1 = new EasyScada.Winforms.Controls.EasyTextBox();
            this.easyTextBox2 = new EasyScada.Winforms.Controls.EasyTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.easyLabel1 = new EasyScada.Winforms.Controls.EasyLabel();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyTextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.easyLabel1)).BeginInit();
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
            // _chart2
            // 
            this._chart2.Location = new System.Drawing.Point(12, 204);
            this._chart2.Name = "_chart2";
            this._chart2.Size = new System.Drawing.Size(1240, 532);
            this._chart2.TabIndex = 11;
            this._chart2.Text = "cartesianChart1";
            // 
            // _btnUpdate
            // 
            this._btnUpdate.Location = new System.Drawing.Point(1114, 89);
            this._btnUpdate.Name = "_btnUpdate";
            this._btnUpdate.Size = new System.Drawing.Size(118, 23);
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
            this._btnExport.Location = new System.Drawing.Point(1114, 118);
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(118, 23);
            this._btnExport.TabIndex = 13;
            this._btnExport.Text = "Export PDF";
            this._btnExport.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(18, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Max";
            // 
            // easyTextBox1
            // 
            this.easyTextBox1.DropDownBackColor = System.Drawing.SystemColors.Control;
            this.easyTextBox1.DropDownBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.easyTextBox1.DropDownDirection = EasyScada.Winforms.Controls.DropDownDirection.None;
            this.easyTextBox1.DropDownFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyTextBox1.DropDownForeColor = System.Drawing.SystemColors.ControlText;
            this.easyTextBox1.HightLightStatusTime = 3;
            this.easyTextBox1.Location = new System.Drawing.Point(17, 103);
            this.easyTextBox1.Name = "easyTextBox1";
            this.easyTextBox1.Role = null;
            this.easyTextBox1.Size = new System.Drawing.Size(100, 20);
            this.easyTextBox1.StringFormat = null;
            this.easyTextBox1.TabIndex = 15;
            this.easyTextBox1.TagPath = "Local Station/Channel1/Device1/Max";
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
            this.easyTextBox2.Location = new System.Drawing.Point(237, 103);
            this.easyTextBox2.Name = "easyTextBox2";
            this.easyTextBox2.Role = null;
            this.easyTextBox2.Size = new System.Drawing.Size(100, 20);
            this.easyTextBox2.StringFormat = null;
            this.easyTextBox2.TabIndex = 17;
            this.easyTextBox2.TagPath = "Local Station/Channel1/Device1/Target";
            this.easyTextBox2.Text = "easyTextBox2";
            this.easyTextBox2.WriteDelay = 200;
            this.easyTextBox2.WriteTrigger = EasyScada.Core.WriteTrigger.OnEnter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(234, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Target";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(509, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Torque Value";
            // 
            // easyLabel1
            // 
            this.easyLabel1.DisplayMode = EasyScada.Winforms.Controls.DisplayMode.Value;
            this.easyLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.easyLabel1.Location = new System.Drawing.Point(512, 103);
            this.easyLabel1.Name = "easyLabel1";
            this.easyLabel1.Size = new System.Drawing.Size(100, 23);
            this.easyLabel1.StringFormat = null;
            this.easyLabel1.TabIndex = 19;
            this.easyLabel1.TagPath = "Local Station/Channel1/Device1/Value";
            this.easyLabel1.Text = "easyLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 808);
            this.Controls.Add(this.easyLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.easyTextBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.easyTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._btnExport);
            this.Controls.Add(this._btnUpdate);
            this.Controls.Add(this._chart2);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _labSriverStatus;
        private System.Windows.Forms.Label _labTime;
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.CartesianChart _chart2;
        private System.Windows.Forms.Button _btnUpdate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Label label2;
        private EasyScada.Winforms.Controls.EasyTextBox easyTextBox1;
        private EasyScada.Winforms.Controls.EasyTextBox easyTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private EasyScada.Winforms.Controls.EasyLabel easyLabel1;
    }
}

