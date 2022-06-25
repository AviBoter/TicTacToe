using System.Collections;
using System.Collections.Generic;
using Controllers;
using Models;
using TMPro;
using UnityEngine;

public class ManuController : MonoBehaviour
{
    [SerializeField] private TMP_Text _tournamentType;
    
    public void OnStartGame()
    {
        Lookup.Instance.CrossControllersEvents.OnTournamentStartAction.Invoke(_tournamentType.text);
        Lookup.Instance.FadeView.AnimateFadeIn();
    }
}
