
using System;
using Controllers;
using Models.GameModels;
using UnityEngine;

namespace  Models.Directors
{
    public class PvPDirector : Director
    {
        [SerializeField] private GameObject _hintButton;
        [SerializeField] private GameObject _undoButton;
        private void Awake()
        {
            _hintButton.SetActive(false);
            _undoButton.SetActive(false);
            StartGame(TournamentDefinition.PvP);
        }

        public override void StartGame(TournamentDefinition tournamentDefinition)
        {
            Lookup.Instance.GameModel = new PvpGameModel();
        }
    }
}