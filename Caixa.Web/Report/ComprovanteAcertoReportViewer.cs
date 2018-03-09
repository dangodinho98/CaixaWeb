using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Caixa.Web.Models;
using Microsoft.AspNet.Identity;

namespace Caixa.Web.Report
{
    public class ComprovanteAcertoReportViewer : ReportViewer
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Acerto acerto { get; set; }
        public Estabelecimentos estabelecimento { get; set; }
        public Maquinas maquina { get; set; }

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
            Font font = FontFactory.GetFont("Verdana", 10, Font.NORMAL, preto);
            Font titulo = FontFactory.GetFont("Verdana", 12, Font.BOLD, preto);

            doc.AddTitle(estabelecimento.Nome + " - " + DateTime.Now.ToString("dd-mm-yyyy hh:mm:ss"));

            float[] colsW = { 15, 15, 15 };
            float[] colsH = { 30 };
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

            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", maquina.DI), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", maquina.DS), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Entrada Atual", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Saída Atual", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Lacre Atual", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", acerto.Entrada), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", acerto.Saida), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Total Entrada", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Bonificação Pago", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("LB", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", acerto.Subtotal.GetValueOrDefault(0)), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Quebra", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Comssão Loja", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell("Colar Lacre", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", acerto.Quebra.GetValueOrDefault(0)), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", acerto.Comissao.GetValueOrDefault(0)), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell("", font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("Líquido", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));
            table.AddCell(getNewCell(string.Empty, titulo, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));
            table.AddCell(getNewCell("PJ", titulo, Element.ALIGN_CENTER, 10, PdfPCell.BOX, preto, fundo));

            table.AddCell(getNewCell("R$" + string.Format("{0:0.00}", acerto.Total.GetValueOrDefault(0)), font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.NO_BORDER));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.BOX));

            table.AddCell(getNewCell("OBS:", titulo, Element.ALIGN_LEFT, 5, PdfPCell.TOP_BORDER));
            table.AddCell(getNewCell(string.Empty, titulo, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));
            table.AddCell(getNewCell(string.Empty, titulo, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));

            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.TOP_BORDER));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.TOP_BORDER));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.TOP_BORDER));

            table.AddCell(getNewCell("Ass.:", titulo, Element.ALIGN_LEFT, 5, PdfPCell.TOP_BORDER));
            table.AddCell(getNewCell(string.Empty, titulo, Element.ALIGN_CENTER, 10, PdfPCell.NO_BORDER));
            table.AddCell(getNewCell("Ass.:", titulo, Element.ALIGN_LEFT, 5, PdfPCell.TOP_BORDER));

            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.TOP_BORDER));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.NO_BORDER));
            table.AddCell(getNewCell(string.Empty, font, Element.ALIGN_CENTER, 5, PdfPCell.TOP_BORDER));



            #endregion

            doc.Add(table);
        }
    }
}
