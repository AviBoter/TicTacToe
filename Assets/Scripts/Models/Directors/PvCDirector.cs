
using Controllers;
using Models.GameModels;
using UnityEngine;

namespace  Models.Directors
{
    public class PvCDirector : Director
    {
        [SerializeField] private GameObject _hintButton;
        [SerializeField] private GameObject _undoButton;
        private void Awake()
        {
            _hintButton.SetActive(true);
            _undoButton.SetActive(true);
            StartGame(TournamentDefinition.PvP);
        }
        
        public override void StartGame(TournamentDefinition tournamentDefinition)
        {
            Lookup.Instance.GameModel = new PvCGameModel();
        }
    }
}
