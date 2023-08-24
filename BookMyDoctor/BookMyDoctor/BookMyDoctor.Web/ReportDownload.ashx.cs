using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using BookMyDoctor.Business;
using BookMyDoctor.Utils.Models;
using BookMyDoctor.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace BookMyDoctor.Web
{

    public class ReportDownload : IHttpHandler, IRequiresSessionState
    {
        readonly Font BoldTextFont = new Font(Font.FontFamily.HELVETICA, 14f, Font.BOLD, BaseColor.BLACK);

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "application/pdf";
                string type = context.Request.QueryString["type"];
                using (MemoryStream ms = new MemoryStream())
                {
                    Document doc = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                    doc.Open();

                    if (type == "Appointment")
                    {
                        GenerateAppointmentPDF(context, doc);
                    }
                    else
                    {
                        GenerateReportPDF(context, doc, type);
                    }

                    doc.Close();

                    byte[] pdfBytes = ms.ToArray();
                    context.Response.BinaryWrite(pdfBytes);
                }

            }
            catch (Exception ex)
            {
                Utils.Utilities.LogError(ex);
            }
        }

        public void GenerateSummaryTable(Document doc, string reportMonth)
        {
            PdfPTable table = new PdfPTable(4);
            Phrase ph = new Phrase("Summary Report Month " + reportMonth.Remove(reportMonth.Length - 3), new Font(Font.FontFamily.HELVETICA, 14f, Font.BOLD, BaseColor.BLACK));
            PdfPCell cell = new PdfPCell(ph);
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Padding = 10;

            table.AddCell(cell);
            table.AddCell(new PdfPCell(new Phrase("Date", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase("Total", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase("Closed", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase("Cancelled", BoldTextFont)));

            var reports = BusinessLogic.GetReportList("Summary", DateTime.Parse(reportMonth));

            foreach (var report in reports)
            {
                table.AddCell(new PdfPCell(new Phrase(report.Date)));
                table.AddCell(new PdfPCell(new Phrase(report.TotalAppointments.ToString())));
                table.AddCell(new PdfPCell(new Phrase(report.ClosedAppointments.ToString())));
                table.AddCell(new PdfPCell(new Phrase(report.CancelledAppointments.ToString())));
            }

            doc.Add(table);
        }

        public void GenerateDetailedTable(Document doc, string reportMonth)
        {
            PdfPTable table = new PdfPTable(3);

            Phrase ph = new Phrase("Detailed Report Month " + reportMonth.Remove(reportMonth.Length - 3), new Font(Font.FontFamily.HELVETICA, 14f, Font.BOLD, BaseColor.BLACK));
            PdfPCell cell = new PdfPCell(ph);
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Padding = 10;

            table.AddCell(cell);

            table.AddCell(new PdfPCell(new Phrase("Date", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase("Patient Name", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase("Status", BoldTextFont)));

            List<ReportDetailedViewModel> reports = BusinessLogic.GetReportList("Detailed", DateTime.Parse(reportMonth));

            foreach (var report in reports)
            {

                PdfPCell date = new PdfPCell(new Phrase(report.Date));
                date.Rowspan = report.Appointments.Count;
                date.Padding = 10;
                table.AddCell(date);
                report.Appointments.ForEach((item) =>
                {
                    table.AddCell(new PdfPCell(new Phrase(item.PatientName)));
                    table.AddCell(new PdfPCell(new Phrase(item.AppointmentStatusUI)));
                });
            }

            doc.Add(table);
        }

        public void GenerateAppointmentPDF(HttpContext context, Document doc)
        {
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=Appointment-Receipt.pdf");
            var appointment = BusinessLogic.GetAppointment(Int32.Parse(context.Request.QueryString["appointmentId"]));

            doc.Add(new Paragraph("Date : " + DateTime.Now.ToString()));
            doc.Add(new Chunk(new LineSeparator()));
            PdfPTable table = new PdfPTable(2);
            Phrase ph = new Phrase("Appointment Confirmed!", new Font(Font.FontFamily.HELVETICA, 14f, Font.BOLD, BaseColor.BLACK));
            PdfPCell cell = new PdfPCell(ph);
            cell.Colspan = 2;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Padding = 10;

            table.AddCell(cell);
            table.AddCell(new PdfPCell(new Phrase("Appointment Id", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase(appointment.AppointmentId.ToString())));
            table.AddCell(new PdfPCell(new Phrase("Doctor Name", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase("Dr." + BusinessLogic.GetDoctor(appointment.DoctorId).DoctorName)));
            table.AddCell(new PdfPCell(new Phrase("Patient Name", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase(appointment.PatientName)));
            table.AddCell(new PdfPCell(new Phrase("Date", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase(appointment.AppointmentDateUI)));
            table.AddCell(new PdfPCell(new Phrase("Time", BoldTextFont)));
            table.AddCell(new PdfPCell(new Phrase(appointment.AppointmentTimeUI)));
            doc.Add(table);

        }

        public void GenerateReportPDF(HttpContext context, Document doc, string type)
        {
            if (Utils.Utilities.IsAuthorized())
            {
                string reportMonth = context.Request.QueryString["reportMonth"];
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + reportMonth.Remove(reportMonth.Length - 3) + "-" + type + "-Report.pdf");

                var doctorName = BusinessLogic.GetDoctor(Utils.Utilities.GetSessionId()).DoctorName;

                doc.Add(new Paragraph("Date : " + DateTime.Now.ToString()));
                doc.Add(new Paragraph("Name : Dr." + doctorName));
                doc.Add(new Chunk(new LineSeparator()));

                if (type == "Summary")
                {
                    GenerateSummaryTable(doc, reportMonth);
                }
                else if (type == "Detailed")
                {
                    GenerateDetailedTable(doc, reportMonth);
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    } 
}