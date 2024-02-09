using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Util
{
    public class ExportToCSV : Singleton<ExportToCSV>
    {
        private bool headersAdded = false;
        private bool recorded = false;
        public StringBuilder sb = new System.Text.StringBuilder();
        public Dictionary<string, int> enemyToKillCount = new Dictionary<string, int>();

   
        public void AddHeaders()
        {
            if (!headersAdded)
            {
                foreach (string enemyType in enemyToKillCount.Keys)
                {
                    sb.Append(enemyType + ",");
                }

                sb.AppendLine();
                //sb.AppendLine("Column1,Column2,Column3,Column4"); // Replace with actual column names
                headersAdded = true;
            }
        }

        // Record data to the CSV file
        public void Record()
        {
            AddHeaders();
            foreach (string enemyType in enemyToKillCount.Keys)
            {
                sb.Append(enemyToKillCount[enemyType] + ",");
            }
            sb.AppendLine();
            SaveToFile(sb.ToString());
        }

        public void RecordKill(string enemyTypeName)
        {
            Debug.Log("Recording Kill for " + enemyTypeName);
            if (!enemyToKillCount.ContainsKey(enemyTypeName))
            {
                enemyToKillCount.Add(enemyTypeName,0);
            }

            enemyToKillCount[enemyTypeName] += 1;
        }

        // Save the CSV file
        public void SaveToFile(string content)
        {
            var folder = Path.Combine(Application.dataPath,"Output");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = Path.Combine(folder, "export.csv");
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(content);
            }
        }

       


    }
}