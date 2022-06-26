using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ManuController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tournamentType;
    
        public void OnStartGame()
        {
            Lookup.Instance.CrossControllersEvents.OnTournamentStartAction.Invoke(_tournamentType.text);
            Lookup.Instance.FadeView.AnimateFadeIn();
        }
    }
}
