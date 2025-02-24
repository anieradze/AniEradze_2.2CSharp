using ClosedXML.Excel;
using System.ComponentModel;
using System.Reflection;

public class WorkWithExcel<T>
{
    public IXLWorksheet AddHeader(XLWorkbook wb, List<T> objs)
    {
        var ws = wb.Worksheets.Add(typeof(T).Name).SetTabColor(XLColor.Green);
        PropertyInfo[] properties = typeof(T).GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            var displayNameAttribute = properties[i].GetCustomAttribute<DisplayNameAttribute>();
            string displayName = displayNameAttribute != null ? displayNameAttribute.DisplayName : properties[i].Name;

            ws.Cell(1, i + 1).Value = displayName;
            ws.Cell(1, i + 1).Style.Font.Bold = true;
        }

        return ws;
    }

    public IXLWorksheet AddBody(IXLWorksheet ws, List<T> objs)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            PropertyInfo[] props = typeof(T).GetProperties();

            for (int j = 0; j < props.Length; j++)
            {
                var value = props[j].GetValue(objs[i], null)?.ToString() ?? "";
                ws.Cell(i + 2, j + 1).Value = value;
            }
        }
        return ws;
    }

    public XLWorkbook Generate(List<T> objs)
    {
        var wb = new XLWorkbook();
        var ws = AddHeader(wb, objs);
        ws = AddBody(ws, objs);
        return wb;
    }
}
