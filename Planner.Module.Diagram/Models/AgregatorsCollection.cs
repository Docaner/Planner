using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Planner.Module.Diagram.Models
{
    public class AgregatorsCollection: ObservableCollection<Agregator>
    {
        public AgregatorsCollection(CanvasSettings settings) {
            IDialogService dialogService = new DefaultDialogService();
            if (dialogService.OpenFileDialog())
            {
                ObservableCollection<JsonAgregator> jsonAgregators = ReadJson(dialogService.FilePath);

                foreach (JsonAgregator jsonAgregator in jsonAgregators)
                {
                    Agregator agregator = new Agregator(jsonAgregator.name, new ObservableCollection<Melting>());
                    foreach (JsonMelting jsonMelting in jsonAgregator.meltings)
                    {
                        agregator.Meltings.Add(new Melting(
                            jsonMelting.heatNum,
                            jsonMelting.start,
                            jsonMelting.end,
                            settings));
                    }
                    Add(agregator);
                }

                //var normAgregators = NormalizeAgregators(jsonAgregators);
                //SerializeJson("norm.json", normAgregators);
            }
        }

        private ObservableCollection<JsonAgregator> ReadJson(string fileName)
        {
            string json = File.ReadAllText(fileName);
            ObservableCollection<JsonAgregator> jsonAgregators = JsonConvert.DeserializeObject<ObservableCollection<JsonAgregator>>(json);
            return jsonAgregators;
        }


        private ObservableCollection<JsonAgregator> NormalizeAgregators(ObservableCollection<JsonAgregator> from)
        {
            ObservableCollection<JsonAgregator> to = new ObservableCollection<JsonAgregator>(from);
            foreach(JsonAgregator jsonAgregator in to)
            {
                for(int i = 0; i < jsonAgregator.meltings.Count; i++)
                {
                    jsonAgregator.meltings[i].heatNum = i;
                }
            }
            return to;
        }

        private void SerializeJson(string fileName, ObservableCollection<JsonAgregator> agrs)
        {
            var serialize = JsonConvert.SerializeObject(agrs);
            File.WriteAllText(fileName, serialize);
        }

        private class JsonAgregator
        {
            public string name { get; set; }
            public ObservableCollection<JsonMelting> meltings { get; set; }
        }
        private class JsonMelting
        {
            public int heatNum { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
        }
    }
}
