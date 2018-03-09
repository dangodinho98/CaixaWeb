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

            base.PageTitle = acerto.Estabelecimento.Nome;
            base.PageSubTitle = "Acerto n°: " + acerto.Id;           

            float[] colsW = { 15, 15 , 15};
            table.SetWidths(colsW);
            table.HeaderRows = 1;
            table.WidthPercentage = 100f;

            table.DefaultCell.Border = PdfPCell.BOX;
            table.DefaultCell.BorderColor = preto;
            table.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);
            table.DefaultCell.Padding = 10;

            table.AddCell(getNewCell("Entrada Anterior", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Saída Anterior", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Lacre Anterior", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Maquina.DI), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Maquina.DS), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Entrada Atual", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Saída Atual", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Lacre Atual", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Entrada), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Saida), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Total Entrada", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Bonificação Pago", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("LB", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Subtotal), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Quebra", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Comssão Loja", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Colar Lacre", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Quebra), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Comissao), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Líquido", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("", titulo, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));
            table.AddCell(getNewCell("PJ", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell(string.Format("{0:0.00}", acerto.Total), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.NO_BORDER));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("OBS:", titulo, Element.ALIGN_CENTER, 10, PdfPCell.PARAGRAPH));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.PARAGRAPH));

            #endregion

            doc.Add(table);
        }
    }
}
