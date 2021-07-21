using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonToDinamyc;

namespace BlazorJsonTreeView.Client.Components
{
    public partial class TreeBlazorReal
    {
        private sealed class IndexContainer
        {
            private int _n;
            public int Inc() => _n++;
        }
        //public class Item
        //{
        //    public string Text { get; set; }
        //    public IEnumerable<Item> Childs { get; set; } = new List<Item>();
        //    public bool Disabled { get; set; }
            
        //}
        //public class Node
        //{
        //    public string Text { get; set; }
        //    public List<Node> Children{ get; set; } = new List<Node>();
        //    public bool Disabled { get; set; }

        //}
        IEnumerable<Converter.Item> NewItems { get; set; } = new List<Converter.Item>();

        //    IEnumerable<Item> Items = new[]
        //    {
        //    new Item { Text = "Item 1" },
        //    new Item { Text = "Item 2", Childs = new []
        //{
        //        new Item { Text = "Item 2.1" },
        //        new Item { Text = "Item 2.2", Disabled = true, Childs = new []
        //    {
        //            new Item { Text = "Item 2.2.1" },
        //            new Item { Text = "Item 2.2.2", Disabled = true },
        //            new Item { Text = "Item 2.2.3" },
        //            new Item { Text = "Item 2.2.4" }

        //        } },
        //        new Item { Text = "Item 2.3" },
        //        new Item { Text = "Item 2.4" }

        //    } },
        //    new Item { Text = "Item 3" },
        //    new Item { Text = "Item 4", Childs = new []
        //{
        //        new Item { Text = "Item 4.1" },
        //        new Item { Text = "Item 4.2", Childs = new []
        //    {
        //            new Item { Text = "Item 4.2.1" },
        //            new Item { Text = "Item 4.2.2" },
        //            new Item { Text = "Item 4.2.3", Disabled = true },
        //            new Item { Text = "Item 4.2.4" }
        //        } },
        //        new Item { Text = "Item 4.3" },
        //        new Item { Text = "Item 4.4" }
        //    } },
        //    new Item { Text = "Item 5" },
        //    new Item { Text = "Item 6" },
        //};

        IList<Converter.Item> ExpandedNodes = new List<Converter.Item>();
        Converter.Item selectedNode;

        

        public void SetObjectAsJson<T>(T obj)
        {
            var convert = new JsonToDinamyc.Converter();
            NewItems = (IEnumerable<Converter.Item>)convert.SetObjectAsJson(obj);

            //NewItems = new[]
            //    {
            //        new Item { Text = "Item 2.0", Disabled = true },
            //        new Item
            //        {
            //            Text = "Item 2.1",
            //            Childs = new[]
            //            {
            //                new Item { Text = "Item 2.2", Disabled = true },
            //                new Item { Text = "Item 2.3", Disabled = true }
            //            }
            //        }
            //    };

        }
    }
}
