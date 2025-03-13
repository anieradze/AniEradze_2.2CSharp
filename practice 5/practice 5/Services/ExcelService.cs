using ClosedXML.Excel;

public class ExcelService
{
    public byte[] GenerateExcel(List<Personali> personalebi)
    {
        using (var wb = new XLWorkbook())
        {
            var ws = wb.AddWorksheet("Personalebi");

            // Add Header
            ws.Cell(1, 1).Value = "Gvari";
            ws.Cell(1, 2).Value = "Saxeli";
            ws.Cell(1, 3).Value = "Ganyofileba";
            ws.Cell(1, 4).Value = "Qalaqi";
            ws.Cell(1, 5).Value = "Xelfasi";
            ws.Cell(1, 6).Value = "Asaki";
            ws.Cell(1, 7).Value = "Staji";
            ws.Cell(1, 8).Value = "Tarigi Dabadebis";
            ws.Cell(1, 9).Value = "Sqesi";
            ws.Cell(1, 10).Value = "Email";
            ws.Cell(1, 11).Value = "Ierarqia";

            // Add Data
            for (int i = 0; i < personalebi.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = personalebi[i].Gvari;
                ws.Cell(i + 2, 2).Value = personalebi[i].Saxeli;
                ws.Cell(i + 2, 3).Value = personalebi[i].Ganyofileba;
                ws.Cell(i + 2, 4).Value = personalebi[i].Qalaqi;
                ws.Cell(i + 2, 5).Value = personalebi[i].Xelfasi;
                ws.Cell(i + 2, 6).Value = personalebi[i].Asaki;
                ws.Cell(i + 2, 7).Value = personalebi[i].Staji;
                ws.Cell(i + 2, 8).Value = personalebi[i].Tarigi_Dabadebis.ToString("yyyy-MM-dd");
                ws.Cell(i + 2, 9).Value = personalebi[i].Sqesi;
                ws.Cell(i + 2, 10).Value = personalebi[i].Email;
                ws.Cell(i + 2, 11).Value = personalebi[i].Ierarqia;
            }

            using (var ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                return ms.ToArray();
            }
        }
    }
}
