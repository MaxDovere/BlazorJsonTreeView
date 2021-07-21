using JsonToDynamic;
using System.Collections.Generic;


namespace BlazorJsonTreeView.Client.Components
{
    public partial class BlazorTreeJson
    {
       
        IEnumerable<Converter.Item> Items { get; set; } = new List<Converter.Item>();

        IList<Converter.Item> ExpandedNodes = new List<Converter.Item>();
        Converter.Item selectedNode;

        public void SetObjectAsJson<T>(T obj)
        {
            var convert = new Converter();
            Items = (IEnumerable<Converter.Item>)convert.SetObjectAsJson(obj);
        }
    }
}
