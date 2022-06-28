using System.IO;
using UnityEditor;

namespace Editor
{
    public static class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        public static void BuildAllAssetBundles()
        {
            string assetBundleDirectory = "Assets/StreamingAssets";
            if(!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
#if UNITY_IOS
            //for my people (BuildTarget.iOS)
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
                BuildAssetBundleOptions.None, 
                BuildTarget.iOS);
#else
            //for the haters (BuildTarget.StandaloneWindows)
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
                BuildAssetBundleOptions.None, 
                 BuildTarget.StandaloneWindows);
#endif
        }
    }
}