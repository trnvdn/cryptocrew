using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestTask
{
    public class RootObject
    {
        public List<Currency> data { get; set; }
        public void SaveData()
        {
            File.WriteAllText("storage.json", JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}
