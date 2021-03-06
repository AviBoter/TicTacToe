using GameEvents;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Controllers
{
    public class ManuController : MonoBehaviour
    {
        CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
        
        [SerializeField] private TMP_Text _tournamentType;
        [SerializeField] private TMP_Text _reSkinAssetBundleName;
    
        public void OnStartGame()
        {
            _controllersEvents.OnTournamentStartAction.Invoke(_tournamentType.text);
            Lookup.Instance.FadeView.AnimateFadeIn();
        }
        
        public void OnReSkinPressed()
        {
            GameObject myObject = Instantiate(new GameObject());
            myObject.AddComponent<ReplaceGameSkinView>();
            myObject.GetComponent<ReplaceGameSkinView>().OnReplaceSkinPressed(_reSkinAssetBundleName.text);
        }
    }
}
