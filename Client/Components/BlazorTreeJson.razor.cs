using BlazorJsonTreeView.Shared;
using System.Collections.Generic;


namespace BlazorJsonTreeView.Client.Components
{
    public partial class BlazorTreeJson
    {
        IEnumerable<JsonToDynamic.Item> Items { get; set; } = new List<JsonToDynamic.Item>();

        IList<JsonToDynamic.Item> ExpandedNodes = new List<JsonToDynamic.Item>();
        JsonToDynamic.Item selectedNode;

        public void SetObjectAsJson<T>(T obj)
        {
            var convert = new JsonToDynamic();
            Items = (IEnumerable<JsonToDynamic.Item>)convert.SetObjectAsJson(obj);
        }
    }
}
