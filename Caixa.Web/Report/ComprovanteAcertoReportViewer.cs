using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Caixa.Web.Models;

namespace Caixa.Web.Report
{
    public class ComprovanteAcertoReportViewer : ReportViewer
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ComprovanteAcertoReportViewer()
        {
            Paisagem = false;
        }

        public override void MontaCorpoDados()
        {
            base.MontaCorpoDados();

            #region Cabeçalho do Relatório
            PdfPTable table = new PdfPTable(3);
            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            Font font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
            Font titulo = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            var acerto = db.Acerto.Include("Estabelecimento").Include("Maquina").FirstOrDefault();

            doc.AddTitle(acerto.Estabelecimento.Nome + " - " + DateTime.Now.ToString("dd-mm-yyyy hh:mm:ss"));

            float[] colsW = { 15, 15 , 15};
            table.SetWidths(colsW);
            table.HeaderRows = 1;
            table.WidthPercentage = 100f;

            table.DefaultCell.Border = PdfPCell.BOX;
            table.DefaultCell.BorderColor = preto;
            table.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            table.DefaultCell.Padding = 10;

            table.AddCell(getNewCell("Estabelecimento", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Emissão", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Código Máquina", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell(acerto.Estabelecimento.Nome, font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(acerto.Data.ToString("dd/MM/yyyy"), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(acerto.Maquina.Codigo.ToString(), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Código Máquina", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("DI (Entrada)", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("DS (Saída)", titulo, Element.ALIGN_LEFT, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell(acerto.Maquina.Codigo.ToString(), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Maquina.DI), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Maquina.DS), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            #endregion

            //table.AddCell(getNewCell(acerto.Estabelecimento.Nome, font, Element.ALIGN_LEFT, 5, PdfPCell.BOTTOM_BORDER));
                //table.AddCell(getNewCell(acerto.Data.ToString("dd/MM/yyyy"), font, Element.ALIGN_LEFT, 5, PdfPCell.BOTTOM_BORDER));

            doc.Add(table);
        }
    }
}
