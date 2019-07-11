﻿using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNETVueJSTemplate
{
    /// <summary>
    /// For the purpose of cache busting, js and css files will have hash values in their file names
    /// This helper will retrieve those file names so that razor views can correctly reference js and css files
    /// </summary>
    public static class WebpackHelper
    {
        public static IDictionary<string, WebpackAsset> GetAssets()
        {
            return GetAssets("webpack-assets", "main");
        }

        private static IDictionary<string, WebpackAsset> GetAssets(string resource, params string[] names)
        {
            var webpackAssets = new Dictionary<string, WebpackAsset>();

            var assembly = typeof(WebpackHelper).Assembly;

            // webpack-assets.json file is generated by webpack
            using (var stream = assembly.GetManifestResourceStream($"Hcc.Portal.{resource}.json"))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            var json = (JObject)JToken.ReadFrom(jsonReader);

                            foreach (var name in names)
                            {
                                webpackAssets.AddAsset(name, json);
                            }
                        }
                    }
                }
            }

            return webpackAssets;
        }

        private static void AddAsset(this IDictionary<string, WebpackAsset> assets, string name, JObject json)
        {
            var asset = new WebpackAsset();

            var js = json[name]["js"];
            if (js != null)
            {
                asset.Script = $"~{js}";
            }

            var css = json[name]["css"];
            if (css != null)
            {
                asset.Style = $"~{css}";
            }

            assets.Add(name, asset);
        }
    }

    public class WebpackAsset
    {
        public string Script { get; set; }
        public string Style { get; set; }
    }
}