#if ANDROID
using Android.Content.Res;
#endif
#if IOS
using Foundation;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PDFViewer.Interfaces;

namespace PDFViewer.Services;

public class ListResAssets : IListAssets
{
    public List<string> ListAssets(string path)
    {
        var fileslist = new List<string>();
        
        #if ANDROID
        AssetManager? assets = Platform.AppContext?.Assets;
        if (assets != null)
        {
            var files = assets.List(path);
            if (files != null && files.Length > 0)
                fileslist.AddRange(files.Where(f => !string.IsNullOrEmpty(f)));
        }
        #elif IOS
        NSBundle mainBundle = NSBundle.MainBundle;
        string? resourcesPath = mainBundle.ResourcePath;
        if (!string.IsNullOrEmpty(resourcesPath))
        {
            string subfolderPath = Path.Combine(resourcesPath, path);

            if (Directory.Exists(subfolderPath))
            {
                var files = Directory.GetFiles(subfolderPath);
                if (files != null && files.Length > 0)
                    fileslist = files
                        .Select(Path.GetFileName)
                        .Where(s => !string.IsNullOrEmpty(s))
                        .Select(s => s!)
                        .ToList();
            }
        }
        #endif
        return fileslist;
    }
}