using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Stock
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = JObject.Parse(File.ReadAllText("input.json"));
            var data = input["data"];
            bool finished = false;
            List<JToken> levels = new List<JToken>();
            int sum = 0;
            JToken currentLevel = data["items"];
            do
            {
                if (currentLevel is JArray)
                {
                    bool upOneLevel = false;
                    bool thereWasItem = false;
                    bool finishedSection = true;
                    foreach (var item in currentLevel)
                    {
                        if (item["finished"] == null)
                        {
                            finishedSection = false;
                            if (item["value"] != null)
                            {
                                sum += int.Parse(item["value"].ToString().Split(' ')[0]);
                                item["value"]["finished"] = true;
                                upOneLevel = true;
                            }
                            else
                            {
                                thereWasItem = true;
                            }
                        }
                    }
                    if (finishedSection)
                    {
                        upOneLevel = true;
                        currentLevel["finished"] = true;
                    }
                    if (upOneLevel)
                    {
                        levels.RemoveAt(levels.Count - 1);
                        if (levels.Count > 0)
                        {
                            currentLevel = levels[levels.Count - 1];
                        }
                        else
                        {
                            finished = true;
                        }
                    }
                    if (thereWasItem)
                    {
                        levels.Add(currentLevel);
                        currentLevel = currentLevel[0]["items"];
                    }
                }
            } while (!finished);
        }
    }
}
