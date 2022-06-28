using System;
using System.IO;
using StaticClasses;
using UnityEngine;

namespace Views
{
    public class ReplaceGameSkinView : MonoBehaviour
    {
        private Sprite _xSprite;
        private Sprite _oSprite;
        private Sprite _bgSprite;

        private const string ExTarget = "ExTarget";
        private const string CircleTarget = "CircleTarget";
        private const string Background = "Background";
        
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        public void OnReplaceSkinPressed(string reSkinAssetBundleName)
        {
            var myLoadedAssetBundle 
                = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, reSkinAssetBundleName));
            if (myLoadedAssetBundle == null) {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }
            
            SaveSprites(myLoadedAssetBundle);
        }

        private void SaveSprites(AssetBundle myLoadedAssetBundle)
        {
            var sprites = myLoadedAssetBundle.LoadAllAssets<Sprite>();
            foreach (var sprite in sprites)
            {
                switch (sprite.name)
                {
                    case ExTarget:
                        _xSprite = sprite;
                        break;
                    case CircleTarget:
                        _oSprite = sprite;
                        break;
                    case Background:
                        _bgSprite = sprite;
                        break;
                }
            }
        }

        public Sprite GetXSprite()
        {
            return _xSprite;
        }

        public Sprite GetOSprite()
        {
            return _oSprite;
        }
        
        public Sprite GetBgSprite()
        {
            return _bgSprite;
        }
    }
}
