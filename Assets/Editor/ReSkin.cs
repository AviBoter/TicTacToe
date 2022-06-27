using StaticClasses;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ReSkin
    {
        private static Sprite _x;
        private static Sprite _o;
        private static Sprite _background;
        private static string _assetBundleName;

        public ReSkin()
        {
            _x = null;
            _o = null;
            _background = null;
            _assetBundleName = null;
        }
        public Sprite X()
        {
            return _x;
        }
        public Sprite O()
        {
            return _o;
        }
        public Sprite Bg()
        {
            return _background;
        }
        public string AssetBundleName()
        {
            return _assetBundleName;
        }
        
        public void SetX(Sprite x)
        {
            _x = x;
        }
        public void SetO(Sprite o)
        {
            _o = o;
        }
        public void SetBg(Sprite bg)
        {
            _background = bg;
        }
        public void AssetBundleName(string str)
        {
            _assetBundleName = str;
            GlobalValues.BundleId = str;
        }

        public string GetXPath()
        {
            return AssetDatabase.GetAssetPath(_x);
        }
    
        public string GetOPath()
        {
            return AssetDatabase.GetAssetPath(_o);
        }
    
        public string GetBgPath()
        {
            return AssetDatabase.GetAssetPath(_background);
        }
    
        public string GetAssetBundleName()
        {
            return _assetBundleName;
        }
    }
}