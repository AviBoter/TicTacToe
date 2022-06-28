using System.Linq;
using StaticClasses;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CustomEditor : EditorWindow
    {
        private const string CreateAssetBundle = "Create Asset Bundle";
        private const string BuildAssetBundle = "Build asset bundle";
        private ReSkin _reskin = new ReSkin();
    
        [MenuItem("Tools/Custom Editor")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow window = GetWindow<CustomEditor>();
            window.titleContent = new GUIContent("Custom Editor");
        }

        private void OnGUI()
        {
            var loaded =AssetBundle.GetAllLoadedAssetBundles();
            Debug.Log(loaded.Count());
            AssetBundle.UnloadAllAssetBundles(true);
            SetWindow();
        
            if (GUILayout.Button(BuildAssetBundle))
            {
                AssignAssetBundleName(_reskin);
                CreateAssetBundles.BuildAllAssetBundles();
            }
        }

        private void SetWindow()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(CreateAssetBundle, EditorStyles.boldLabel);
            EditorGUILayout.Space();
            _reskin.SetX((Sprite)EditorGUILayout.ObjectField("X Sprite", _reskin.X(), typeof(Sprite)));
            EditorGUILayout.Space();
            _reskin.SetO((Sprite)EditorGUILayout.ObjectField("O Sprite", _reskin.O(), typeof(Sprite)));
            EditorGUILayout.Space();
            _reskin.SetBg((Sprite)EditorGUILayout.ObjectField("Background Sprite", _reskin.Bg(), typeof(Sprite)));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Asset Bundle Name", EditorStyles.boldLabel);
            _reskin.AssetBundleName(EditorGUILayout.TextField("Bundle Id",_reskin.GetAssetBundleName()));
            EditorGUILayout.Space(2);
            EditorGUILayout.Space();
        }

        private void AssignAssetBundleName(ReSkin reSkin)
        {
            string path = reSkin.GetXPath();
            AssetImporter.GetAtPath(path).SetAssetBundleNameAndVariant(GlobalValues.BundleId, "");
            path = reSkin.GetOPath();
            AssetImporter.GetAtPath(path).SetAssetBundleNameAndVariant(GlobalValues.BundleId, "");
            path = reSkin.GetBgPath();
            AssetImporter.GetAtPath(path).SetAssetBundleNameAndVariant(GlobalValues.BundleId, "");
        }
    }
}

