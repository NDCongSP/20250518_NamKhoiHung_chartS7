using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TorqueLogReportV2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var btn = new Button { Text = "Xuất PDF Báo Cáo Torque", Dock = DockStyle.Fill };
            btn.Click += Btn_Click;
            Controls.Add(btn);
        }

        private void Btn_Click(object sender, System.EventArgs e)
        {
            var doc = CreateDocument();
            var renderer = new PdfDocumentRenderer(true) { Document = doc };
            renderer.RenderDocument();

            string outputPath = Path.Combine(Application.StartupPath, "TorqueReport.pdf");
            renderer.PdfDocument.Save(outputPath);
            Process.Start("explorer.exe", outputPath);
        }

        private Document CreateDocument()
        {
            Document doc = new Document();
            Section section = doc.AddSection();

            section.PageSetup.TopMargin = "1cm";
            section.PageSetup.BottomMargin = "1cm";
            section.PageSetup.LeftMargin = "1.5cm";
            section.PageSetup.RightMargin = "1.5cm";

            var title = section.AddParagraph("CONNECTION TORQUE LOG");
            title.Format.Font.Size = 14;
            title.Format.Font.Bold = true;
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.SpaceAfter = "0.5cm";

            Table infoTable = section.AddTable();
            infoTable.AddColumn("7cm");
            infoTable.AddColumn("7cm");

            var row = infoTable.AddRow();
            row.Cells[0].AddParagraph("OPERATOR: ZAHADI");
            row.Cells[1].AddParagraph("LOG MASTER™ II S/N: LM2-586");

            row = infoTable.AddRow();
            row.Cells[0].AddParagraph("DATE: 09 APR 25 (10:29:50)");
            row.Cells[1].AddParagraph("TORQUEMASTER™ S/N: 8025 - 5020");

            row = infoTable.AddRow();
            row.Cells[0].AddParagraph("TOOL: Series Jar");
            row.Cells[1].AddParagraph("CONSOLE S/N: 8018 - 5032");

            row = infoTable.AddRow();
            row.Cells[0].AddParagraph("JOB NO.: ASSEMBLE HQ650 JAR");
            row.Cells[1].AddParagraph("");

            row = infoTable.AddRow();
            row.Cells[0].AddParagraph("SERIES:");
            row.Cells[1].AddParagraph("");

            row = infoTable.AddRow();
            row.Cells[0].AddParagraph("TOOL S/N: OSC115748A");
            row.Cells[1].AddParagraph("");

            section.AddParagraph("\nLOGGED TORQUE VALUES").Format.Font.Bold = true;

            Table dataTable = section.AddTable();
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

            section.AddParagraph("\nwww.nov.com", "Normal").Format.Alignment = ParagraphAlignment.Left;

            return doc;
        }
    }
}