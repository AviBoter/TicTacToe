using System.Threading.Tasks;
using Controllers;
using GameEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Models
{
    public class GameInitiator : MonoBehaviour, IGameInitiator
    {
        void Awake()
        {
            Lookup.Instance.GameInitiator = this;
            Lookup.Instance.CrossControllersEvents.OnTournamentStartAction += StartGame;
        }
        
        public async void StartGame(string tournamentType)
        {
            await LoadGameScene();
            Lookup.Instance.CrossControllersEvents.OnTournamentStartAction -= StartGame;
            switch (tournamentType)
            {
                case "PvP":
                    ActivateDirector(TournamentDefinition.PvP);
                    break;
                case "PvC":
                    ActivateDirector(TournamentDefinition.PvC);
                    break;
                case "CvC":
                    ActivateDirector(TournamentDefinition.CvC);
                    break;
            }
        }

        private void ActivateDirector(TournamentDefinition tournamentDefinition)
        {
            Lookup.Instance.CrossControllersEvents.OnTournamentDefinitionPLayAction?.Invoke(tournamentDefinition);
        }

        private async Task LoadGameScene()
        {
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync("GameScene");
            while (!sceneLoad.isDone)
            {
                await Task.Delay(200);
            }
        }
    }
    
}