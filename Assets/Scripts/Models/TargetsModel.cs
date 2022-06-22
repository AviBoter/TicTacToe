using System;
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

    public class TargetsModel
    {
        private int[][] _targetsMatrix;
        private LinkedList<PlayerMove> _playersMoves;

        public Action<PlayerMove> OnDeleteLastMoveFromListAction;
        public TargetsModel()
        {
            _targetsMatrix = new int[3][];
            _targetsMatrix[0] = new int[3];
            _targetsMatrix[1] = new int[3];
            _targetsMatrix[2] = new int[3];
            Reset();
            
            _playersMoves = new LinkedList<PlayerMove>();
           
        }
        
        public void AddMoveToList(KeyValuePair<int, int> location, PLayerType player)
        {
            _playersMoves.AddFirst(new PlayerMove(location, player));
        }
        
        public void DeleteLastMoveFromList()
        {
            PlayerMove deleted = new PlayerMove(_playersMoves.First.Value);
            _playersMoves.RemoveFirst();
            OnDeleteLastMoveFromListAction?.Invoke(deleted);
        }

        public void Reset()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _targetsMatrix[i][j] = (int)TargetState.Non;
                }
            }
        }
    }
}