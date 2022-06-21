using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Models
{
    public class GameInitiator : MonoBehaviour, IGameInitiator
    {
        void Awake()
        {
            Lookup.Instance.GameInitiator = this;
        }
        
        public async void StartGame(TournamentDefinition tournamentDefinition)
        {
            await LoadGameScene();
            ActivateDirector(tournamentDefinition);
        }

        private void ActivateDirector(TournamentDefinition tournamentDefinition)
        {
            //Lookup.Instance.GameDirector.ActivateDirector(tournamentDefinition);
        }

        private async Task LoadGameScene()
        {
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync("GameScene");
            while (!sceneLoad.isDone)
            {
                await Task.Delay(200);
                Debug.Log("Loading scene");
            }
        }
    }
}