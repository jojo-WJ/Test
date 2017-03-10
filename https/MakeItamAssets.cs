﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MakeItamAssets : MonoBehaviour
{

    [MenuItem("MyMenu/Build Assetbundle")]
    static private void BuildAssetBundle()
    {
        string dir = Application.dataPath + "/StreamingAssets";

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Atlas");
        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
        {
            List<Sprite> assets = new List<Sprite>();
            string path = dir + "/" + dirInfo.Name + ".assetbundle";
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
            {
                string allPath = pngFile.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                assets.Add(AssetDatabase.LoadAssetAtPath<Sprite>(assetPath));
            }
            if (BuildPipeline.BuildAssetBundle(null, assets.ToArray(), path, BuildAssetBundleOptions.UncompressedAssetBundle |BuildAssetBundleOptions.CollectDependencies, GetBuildTarget()))
            {
            }
        }
    }
    static private BuildTarget GetBuildTarget()
    {
        BuildTarget target = BuildTarget.WebPlayer;
#if UNITY_STANDALONE
        target = BuildTarget.StandaloneWindows;
#elif UNITY_IPHONE
			target = BuildTarget.iPhone;
#elif UNITY_ANDROID
			target = BuildTarget.Android;
#endif
        return target;
    }
}
