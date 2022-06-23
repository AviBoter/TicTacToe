using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using Models;
using Models.GameModels;
using UnityEngine;

namespace Controllers
{
    public class DirectorsController : MonoBehaviour
    {
        [SerializeField] private GameObject _pvpDirector;
        [SerializeField] private GameObject _pvcDirector;
        [SerializeField] private GameObject _cvcDirector;

        private CrossControllersEvents _controllersEvents => Lookup.Instance.CrossControllersEvents;
        private void Awake()
        {
            _controllersEvents.OnTournamentDefinitionPLayAction += ActivateDirector;
            _controllersEvents.GameOverAction += GameOver;
        }

        private void ActivateDirector(TournamentDefinition tournamentDefinition)
        {
            Lookup.Instance.CrossControllersEvents.OnTournamentDefinitionPLayAction -= ActivateDirector;
            switch (tournamentDefinition)
            {
                case TournamentDefinition.PvP:
                    _pvpDirector.SetActive(true);
                    _pvcDirector.SetActive(false);
                    _cvcDirector.SetActive(false);
                    break;
                case TournamentDefinition.PvC:
                    _pvpDirector.SetActive(false);
                    _pvcDirector.SetActive(true);
                    _cvcDirector.SetActive(false);
                    break;
                case TournamentDefinition.CvC:
                    _pvpDirector.SetActive(false);
                    _pvcDirector.SetActive(false);
                    _cvcDirector.SetActive(true);
                    break;
            }
        }

        private void GameOver(GameState state)
        {
            _pvpDirector.SetActive(false);
            _pvcDirector.SetActive(false);
            _cvcDirector.SetActive(false);
            _controllersEvents.GameOverAction -= GameOver;
        }
    }
}