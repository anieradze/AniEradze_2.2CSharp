using ClosedXML.Excel;
using System.ComponentModel;
using System.Reflection;

namespace practice_5.Controllers
{
    public class WorkWithExcel<T>
    {
            public IXLWorksheet AddHeader(XLWorkbook wb, List<T> objs)
            {

                T obj = objs.FirstOrDefault();
                var ws = wb.Worksheets.Add(obj.GetType().Name).SetTabColor(XLColor.Green);
                PropertyInfo[] properties = obj.GetType().GetProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    var displayNameAttribute = properties[i].GetCustomAttributes(typeof(DisplayNameAttribute), false);
                    string displayName = (displayNameAttribute.Count() != 0) ?
                                         (displayNameAttribute[0] as DisplayNameAttribute).DisplayName : properties[0].Name;
                    ws.Cell(1, i + 1).Value = displayName;
                    ws.Cell(1, i + 1).Style.Font.Bold = true;
                }

                return ws;

            }
            public IXLWorksheet AddBody(IXLWorksheet ws, List<T> objs)
            {
                T obj = objs.FirstOrDefault();
                int properties = obj.GetType().GetProperties().Length;
                string previousValue = string.Empty;

                for (int i = 0; i < objs.Count(); i++)
                {
                    Type myType = objs[i].GetType();
                    PropertyInfo[] props = myType.GetProperties();

                    for (int j = 0; j < properties; j++)
                    {
                        PropertyInfo prop = props[j];
                        string propValue = prop.GetValue(objs[i], null).ToString();
                        ws.Cell(2 + i, j + 1).Value = propValue;
                    }
                }
                return ws;
            }
            public XLWorkbook Generate(List<T> objs)
            {
               
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = AddHeader(wb, objs);
                    ws = AddBody(ws, objs);
                    return wb;
                }
            }
        }
    }
