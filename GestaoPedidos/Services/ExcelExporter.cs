using GestaoPedidos.DTO;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace GestaoPedidos.Services
{
    public class ExcelExporter
    {
        public byte[] ExportOrderSummaryToExcel(OrderSummaryDTO orderSummary)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Order Summary");

                worksheet.Cells["A1"].Value = "Order Summary";
                worksheet.Cells["A1:E1"].Merge = true;
                worksheet.Cells["A1:E1"].Style.Font.Bold = true;
                worksheet.Cells["A1:E1"].Style.Font.Size = 14;
                worksheet.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells["A3"].Value = "Cliente:";
                worksheet.Cells["B3"].Value = orderSummary.ClientName;

                worksheet.Cells["A4"].Value = "Email:";
                worksheet.Cells["B4"].Value = orderSummary.ClientEmail;

                worksheet.Cells["A5"].Value = "Endereço de Cobrança:";
                worksheet.Cells["B5"].Value = orderSummary.BillingAddress;

                worksheet.Cells["A6"].Value = "Endereço de Entrega:";
                worksheet.Cells["B6"].Value = orderSummary.DeliveryAddress;

                worksheet.Cells["A7"].Value = "Data do Pedido:";
                worksheet.Cells["B7"].Value = orderSummary.OrderDate.ToString("dd/MM/yyyy HH:mm");

                worksheet.Cells["A8"].Value = "Valor Total:";
                worksheet.Cells["B8"].Value = $"R$ {orderSummary.TotalOrderValue:F2}";

                worksheet.Cells["A10"].Value = "Produto";
                worksheet.Cells["B10"].Value = "Quantidade";
                worksheet.Cells["C10"].Value = "Valor Unitário";
                worksheet.Cells["D10"].Value = "Valor Total";
                worksheet.Cells["A10:D10"].Style.Font.Bold = true;
                worksheet.Cells["A10:D10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A10:D10"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                int row = 11;
                foreach (var item in orderSummary.OrderDetails)
                {
                    worksheet.Cells[$"A{row}"].Value = item.ProductName;
                    worksheet.Cells[$"B{row}"].Value = item.Quantity;
                    worksheet.Cells[$"C{row}"].Value = item.UnitPrice;
                    worksheet.Cells[$"D{row}"].Value = item.TotalPrice;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}
