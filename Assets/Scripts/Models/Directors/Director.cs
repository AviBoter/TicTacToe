using Models;
using Models.Interfaces;
using UnityEngine;

namespace Models.Directors
{
    public abstract class Director : MonoBehaviour
    {
        public virtual void StartGame(TournamentDefinition tournamentDefinition){}
    }
}