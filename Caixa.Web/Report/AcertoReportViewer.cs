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
    public class AcertoReportViewer : ReportViewer
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public AcertoReportViewer()
        {
            Paisagem = false;
        }

        public override void MontaCorpoDados()
        {
            base.MontaCorpoDados();

            #region Cabeçalho do Relatório
            PdfPTable table = new PdfPTable(6);
            BaseColor preto = new BaseColor(0, 0, 0);
            BaseColor fundo = new BaseColor(200, 200, 200);
            Font font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
            Font titulo = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);

            float[] colsW = { 10, 10, 10, 10, 10, 10, };
            table.SetWidths(colsW);
            table.HeaderRows = 1;
            table.WidthPercentage = 100f;

            table.DefaultCell.Border = PdfPCell.BOX;
            table.DefaultCell.BorderColor = preto;
            table.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);

            table.AddCell(getNewCell("N° Acerto", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Emissão", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Estabelecimento", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Máquina", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Entrada", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Saída", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            #endregion

            var acerto = db.Acerto.Include("Estabelecimento").Include("Maquina").ToList();

            foreach (var d in acerto)
            {
                table.AddCell(getNewCell(d.Id.ToString(), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
                table.AddCell(getNewCell(d.Data.ToString("dd/MM/yyyy"), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
                table.AddCell(getNewCell(d.Estabelecimento.Nome, font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
                table.AddCell(getNewCell(d.Maquina.Codigo.ToString(), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
                table.AddCell(getNewCell(string.Format("{0:0.00}", d.Entrada), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
                table.AddCell(getNewCell(string.Format("{0:0.00}", d.Saida), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            }

            var totalEntrada = acerto.Sum(x => x.Entrada);
            table.AddCell(getNewCell("Totais: ", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell(string.Format("{0:0.00}", totalEntrada), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell(string.Format("{0:0.00}", totalEntrada), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            doc.Add(table);
        }
    }
}
