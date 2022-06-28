
using System;
using Controllers;
using Models.GameModels;
using Models.Interfaces;
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
            if (Lookup.Instance.GameModel is PvpGameModel == false)
            {
                Lookup.Instance.GameModel = new PvpGameModel();
            }
        }
    }
}
