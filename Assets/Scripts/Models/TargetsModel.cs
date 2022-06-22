using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetState
{
    Non = 0, X = 1, O = 2
}

public enum PLayerType
{
    X = 1, O = 2
}

namespace Models
{

    public class TargetsModel : MonoBehaviour
    {
        private int[][] _targetsMatrix;
        private LinkedList<PlayerMove> _playersMoves;
        
        void Awake()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _targetsMatrix[i][j] = (int)TargetState.Non;
                }
            }
        }
        
        private void AddMoveToList(KeyValuePair<int, int> location, PLayerType player)
        {
            _playersMoves.AddFirst(new PlayerMove(location, player));
        }
        
        private void DeleteLastMoveFromList()
        {
            _playersMoves.RemoveFirst();
        }
        
        
    }
}