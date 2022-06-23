using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Models.GameModels;
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

        public event Action<PlayerMove> OnDeleteLastMoveFromListAction;
        public event Action OnGameStateChanged;
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
            _targetsMatrix[location.Key][location.Value] = (int)(TargetState)player;
            if (GetGameState() != GameState.OnGoing)
            {
                OnGameStateChanged?.Invoke();
            }
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
                    if (_playersMoves?.Count > 0)
                    {
                        DeleteLastMoveFromList();
                    }
                }
            }
        }

        private bool IsTargetAvailable(int x,int y)
        {
            return _targetsMatrix[x][y] == (int)TargetState.Non;
        }
        
        public bool PlayerPressTargetButton(KeyValuePair<int, int> location)
        {
            if (IsTargetAvailable(location.Key, location.Value))
            {
                AddMoveToList(location, Lookup.Instance.GameModel._isPlayer1 ? PLayerType.X : PLayerType.O);
                return true;
            }
            return false;
        }
        
        public GameState GetGameState()
        {
            GameState state = CheckRowsVictory();
            if (state != 0)
            {
                return state;
            }
            
            state = CheckColumnsVictory();
            if (state != 0)
            {
                return state;
            }
            
            state = CheckDiagonalsVictory();
            if (state != 0)
            {
                return state;
            }

            state = CheckGameTie();
            if (state != 0)
            {
                return state;
            }
            
            return GameState.OnGoing;
        }

        private GameState CheckGameTie()
        {
            GameState state = GameState.OnGoing;
            for (int i = 0; i < _targetsMatrix.Length; i++)
            {
                for (int j = 0; j < _targetsMatrix[i].Length; j++)
                {
                    if (_targetsMatrix[i][j] == 0)
                    {
                        state = GameState.Tie;
                    }
                }
            }

            return state;
        }

        private GameState CheckDiagonalsVictory()
        {
            if (_targetsMatrix[0][0] == _targetsMatrix[1][1] && _targetsMatrix[1][1] == _targetsMatrix[2][2])
            {
              return (GameState)_targetsMatrix[0][0];
            }
 
            if (_targetsMatrix[0][2] == _targetsMatrix[1][1] && _targetsMatrix[1][1] == _targetsMatrix[2][0])
            {
                return (GameState)_targetsMatrix[0][2];
            }

            return GameState.OnGoing;
        }

        private GameState CheckRowsVictory()
        {
            for (int row = 0; row < 3; row++)
            {
                if (_targetsMatrix[row][0] == _targetsMatrix[row][1] &&
                    _targetsMatrix[row][1] == _targetsMatrix[row][2])
                {
                    if (_targetsMatrix[row][0] == (int)PLayerType.X)
                        return GameState.XWin;
                    if(_targetsMatrix[row][0] == (int)PLayerType.O)
                        return GameState.OWin;
                }
            }
            return GameState.OnGoing;
        }
        
        private GameState CheckColumnsVictory()
        {
            for (int col = 0; col < 3; col++)
            {
                if (_targetsMatrix[0][col] == _targetsMatrix[1][col] &&
                    _targetsMatrix[1][col] == _targetsMatrix[2][col])
                {
                    if (_targetsMatrix[0][col] == (int)PLayerType.X)
                        return GameState.XWin;
 
                    else if (_targetsMatrix[0][col] == (int)PLayerType.O)
                        return GameState.OWin;
                }
            }

            return GameState.OnGoing;
        }
    }
}