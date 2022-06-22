using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Controllers
{
    public class DirectorsController : MonoBehaviour
    {
        [SerializeField] private GameObject _pvpDirector;
        [SerializeField] private GameObject _pvcDirector;
        [SerializeField] private GameObject _cvcDirector;

        private void Awake()
        {
            Lookup.Instance.CrossCrossControllersEvents.OnTournamentDefinitionPLayAction += ActivateDirector;
        }

        public void ActivateDirector(TournamentDefinition tournamentDefinition)
        {
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
    }
}