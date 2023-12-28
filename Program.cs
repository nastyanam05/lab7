using System.Reflection;
using System.Xml.Linq;
internal class Task
{
    private static void Main()
    {
        var mylib = Assembly.LoadFrom("/Users/mira/RiderProjects/lab7/bin/Debug/net7.0/lab7.dll"); //Assembly.LoadFrom загружает сборку из указанного файла.
        var types = mylib.GetExportedTypes(); //В этой строке извлекается массив типов (classes, interfaces, structs, enumerations), которые экспортированы из загруженной сборки.
        XDocument xdoc = new XDocument();
        uint num;
        XElement Classes = new XElement("classes");
        xdoc.Add(Classes);
        foreach (var type in types) {
            XElement el = new XElement("class");
            XAttribute el_attr = new XAttribute("name", type.FullName);
            el.Add(el_attr);
            XElement Properties = new XElement("properties");
            el.Add(Properties);
            XElement Methods = new XElement("methods");
            el.Add(Methods);
            num = 1;
            foreach (var method in type.GetMethods()) { Methods.Add(new XElement($"method{num}",method)); ++num; }
            num = 1;
            foreach (var prop in type.GetProperties()) { Properties.Add(new XElement($"property{num}", prop)); ++num; }
            Classes.Add(el);
        }
        
        xdoc.Save("classes.xml");
    }
}