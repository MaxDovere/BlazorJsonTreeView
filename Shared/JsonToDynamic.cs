﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BlazorJsonTreeView.Shared
{
    public class JsonToDynamic
    {
        public class Item
        {
            public string Text { get; set; }
            public IEnumerable<Item> Childs { get; set; } = new List<Item>();
            public bool Disabled { get; set; }

        }

        public IEnumerable<Item> SetObjectAsJson<T>(T obj)
        {

            JToken t = JsonConvert.DeserializeObject<JToken>(JsonConvert.SerializeObject(obj));

            IEnumerable<Item> result = FillItem(t.Value<string>());
            IEnumerable<Item> myEnumerable = new[]
            {
                new Item { Text = "Json Tree", Disabled = false, Childs = result }
            };

            return myEnumerable;
        }
        private IEnumerable<Item> FillItem(string json)
        {
            Item FirstNode = new Item() { Text = "ROOT" };
            List<Item> Children = new List<Item>();

            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var item in array)
            {
                string Type = "";
                try
                {
                    if (item.Last != null)
                    {
                        if (item.Last.Type == null)
                            Type = ((JObject)item.Last).Type.ToString();
                        else
                            Type = item.Last.Type.ToString();

                        switch (Type)
                        {
                            case "Integer":
                            case "Double":
                            case "Float":
                            case "Boolean":
                            case "String":
                            case "JValue":
                                Item NewItem = new Item() { Text = $"{item.Name} : {item.Value} <{item.Last.Type}>", Disabled = false };
                                Children.Add(NewItem);
                                break;
                            case "Array":
                            case "JArray":
                                List<Item> InternChildren = new List<Item>();
                                foreach (var itemArray in item.Value)
                                {
                                    string namelocal = itemArray.Name;
                                    List<Item> LocalChilds = new List<Item>();
                                    dynamic arrayValues = JsonConvert.DeserializeObject<JToken>(JsonConvert.SerializeObject(itemArray));
                                    foreach (var itemIn in arrayValues)
                                    {
                                        Item NewItemArray = new Item() { Text = $"{itemIn.Last.Name} : {itemIn.Value} <{itemIn.Last.Type}>", Disabled = false };
                                        LocalChilds.Add(NewItemArray);
                                    }
                                    Item NewItemlocal = new Item() { Text = namelocal, Disabled = false, Childs = LocalChilds.ToArray() };
                                    InternChildren.Add(NewItemlocal);
                                }
                                Item NewItemroot = new Item() { Text = item.Name, Disabled = false };
                                NewItemroot.Childs = InternChildren.ToArray();
                                Children.Add(NewItemroot);
                                break;
                            case "Object":
                            case "JObject":
                                string nameO = item.Name;
                                List<Item> InternOChildren = new List<Item>();
                                dynamic arrayObject = JsonConvert.DeserializeObject<JToken>(JsonConvert.SerializeObject(item.Last));
                                foreach (var itemObject in arrayObject)
                                {
                                    if (itemObject.Last.Type == null)
                                    {
                                        Type = ((JObject)itemObject.Last).Type.ToString();
                                        string Value = ((JObject)itemObject.Value).ToString();
                                        IEnumerable<Item> Items = FillItem(Value);
                                        InternOChildren.InsertRange(0, Items);
                                    }
                                    else
                                    {
                                        Item NewItemObject = new Item() { Text = $"{itemObject.Name} : {itemObject.Value} <{itemObject.Last.Type}>", Disabled = false };
                                        InternOChildren.Add(NewItemObject);
                                    }
                                }
                                Item NewItemO = new Item() { Text = nameO, Disabled = false, Childs = InternOChildren.ToArray() };
                                Children.Add(NewItemO);
                                break;
                            default:
                                Console.WriteLine("{0} {1} {2}", item.Last.Type, item.Last.Name, item.Last.Value);
                                break;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            FirstNode.Childs = Children.ToArray();

            return FirstNode.Childs;
        }
    }
}
