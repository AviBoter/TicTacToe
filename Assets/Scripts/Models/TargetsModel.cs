using System;
using System.Collections.Generic;
using Controllers;
using Models.GameModels;
using Random = UnityEngine.Random;

public enum TargetState
{
    Non = 0, X = 1, O = 2
}

public enum PlayerType
{
    X = 1, O = 2
}

namespace Models
{

    public class TargetsModel
    {
        private int[][] _targetsMatrix;
        private LinkedList<PlayerMove> _playersMoves;

        public event Action<KeyValuePair<int,int>> OnDeleteLastMoveFromListAction;
        public event Action<GameState> OnGameStateChanged;
        public TargetsModel()
        {
            _targetsMatrix = new int[3][];
            _targetsMatrix[0] = new int[3];
            _targetsMatrix[1] = new int[3];
            _targetsMatrix[2] = new int[3];
            Reset();
            
            _playersMoves = new LinkedList<PlayerMove>();
           
        }
        
        public void AddMoveToList(KeyValuePair<int, int> location, PlayerType player)
        {
            _playersMoves.AddFirst(new PlayerMove(location, player));
            _targetsMatrix[location.Key][location.Value] = (int)(TargetState)player;
            GameState state = GetGameState();
            if (state != GameState.OnGoing)
            {
                OnGameStateChanged?.Invoke(state);
            }
        }
        
        public void DeleteLastMoveFromList()
        {
            if(_playersMoves.Count % 2 == 0 && _playersMoves.Count>1)
            {
                KeyValuePair<int, int> location = _playersMoves.First.Value.Location;
                OnDeleteLastMoveFromListAction?.Invoke(location);
                _targetsMatrix[location.Key][location.Value] = (int)TargetState.Non;
                _playersMoves.RemoveFirst();
                location = _playersMoves.First.Value.Location;
                OnDeleteLastMoveFromListAction?.Invoke(location);
                _playersMoves.RemoveFirst();
                _targetsMatrix[location.Key][location.Value] = (int)TargetState.Non;
            }
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

        public bool IsTargetAvailable(int x,int y)
        {
            return _targetsMatrix[x][y] == (int)TargetState.Non;
        }
        
        public KeyValuePair<int,int> FindAvailableTarget()
        {
            bool randomTargetFound = false;
            while (!randomTargetFound)
            {
                int xRandom = Random.Range(0, 3);
                int yRandom = Random.Range(0, 3);
                KeyValuePair<int, int> location = new KeyValuePair<int, int>(xRandom, yRandom);
                bool result = IsTargetAvailable(location.Key, location.Value);
                if (result)
                {
                    return location;
                }
            }
            return new KeyValuePair<int, int>(-1, -1);
        }
        
        public bool PlayerPressTargetButton(KeyValuePair<int, int> location)
        {
            if (IsTargetAvailable(location.Key, location.Value))
            {
                AddMoveToList(location, Lookup.Instance.GameModel._isPlayer1 ? PlayerType.X : PlayerType.O);
                return true;
            }
            return false;
        }
        
        private GameState GetGameState()
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
            GameState state = GameState.Tie;
            for (int i = 0; i < _targetsMatrix.Length; i++)
            {
                for (int j = 0; j < _targetsMatrix[i].Length; j++)
                {
                    if (_targetsMatrix[i][j] == 0)
                    {
                        state = GameState.OnGoing;
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
                    if (_targetsMatrix[row][0] == (int)PlayerType.X)
                        return GameState.XWin;
                    if(_targetsMatrix[row][0] == (int)PlayerType.O)
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
                    if (_targetsMatrix[0][col] == (int)PlayerType.X)
                        return GameState.XWin;
 
                    else if (_targetsMatrix[0][col] == (int)PlayerType.O)
                        return GameState.OWin;
                }
            }

            return GameState.OnGoing;
        }
    }
}