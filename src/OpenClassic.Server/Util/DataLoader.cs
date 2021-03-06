﻿using Newtonsoft.Json;
using OpenClassic.Server.Domain.Definition;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;

namespace OpenClassic.Server.Util
{
    public static class DataLoader
    {
        public static List<NpcDefinition> GetNpcDefinitions()
        {
            var file = @"C:\Users\daniel\Source\Repos\OpenClassic\src\OpenClassic.Server\GameData\Definitions\NPCDef.json";
            var fileText = File.ReadAllText(file);

            var settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;

            var results = JsonConvert.DeserializeObject<List<NpcDefinition>>(fileText, settings);

            return results;
        }

        public static List<NpcLocation> GetNpcLocations()
        {
            var file = @"./GameData/Locations/NpcLoc.json";
            var fileText = File.ReadAllText(file);

            var settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;

            var results = JsonConvert.DeserializeObject<List<NpcLocation>>(fileText, settings);

            return results;
        }

        private static void LoadFolder(string folderPath)
        {
            var filePaths = Directory.GetFiles(folderPath);

            foreach (var path in filePaths)
            {
                if (!path.EndsWith(".xml.gz", StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                var fileNameXmlExtStart = path.IndexOf(".xml.gz", StringComparison.CurrentCultureIgnoreCase);
                var fileName = path.Substring(0, fileNameXmlExtStart) + ".json";

                try
                {
                    var fileJson = LoadFileAsJson(path);

                    File.WriteAllText(fileName, fileJson);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
        }

        private static string LoadFileAsJson(string filepath)
        {
            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                throw new ArgumentException("Invalid file path");
            }

            var xml = GetXDocument(filepath);
            var json = JsonConvert.SerializeXNode(xml, Formatting.Indented);

            return json;
        }

        private static XDocument GetXDocument(string filepath)
        {
            Debug.Assert(filepath != null);

            var gzippedFile = File.ReadAllBytes(filepath);

            using (var stream = new GZipStream(new MemoryStream(gzippedFile), CompressionMode.Decompress))
            {
                var xmlDoc = XDocument.Load(stream);

                return xmlDoc;
            }
        }

        private static string GetGzippedFileAsString(string filepath)
        {
            Debug.Assert(filepath != null);

            var gzippedFile = File.ReadAllBytes(filepath);

            using (var stream = new GZipStream(new MemoryStream(gzippedFile), CompressionMode.Decompress))
            {
                var buffer = new byte[4096];

                using (var ms = new MemoryStream())
                {
                    var bytesRead = 0;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}
