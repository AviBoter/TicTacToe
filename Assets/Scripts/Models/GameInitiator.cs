using System.Threading.Tasks;
using Controllers;
using GameEvents;
using Models.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Models
{
    public class GameInitiator : MonoBehaviour, IGameInitiator
    {

        private CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
        void Awake()
        {
            Lookup.Instance.GameInitiator = this;
            _controllersEvents.OnTournamentStartAction += StartGame;
        }
        
        public async void StartGame(string tournamentType)
        {
            await LoadGameScene();
            _controllersEvents.OnTournamentStartAction -= StartGame;
            switch (tournamentType)
            {
                case "Player Vs Player":
                    ActivateDirector(TournamentDefinition.PvP);
                    break;
                case "Player Vs Computer":
                    ActivateDirector(TournamentDefinition.PvC);
                    break;
                case "Computer Vs Computer":
                    ActivateDirector(TournamentDefinition.CvC);
                    break;
            }
        }

        private void ActivateDirector(TournamentDefinition tournamentDefinition)
        {
            _controllersEvents.OnTournamentDefinitionPLayAction?.Invoke(tournamentDefinition);
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