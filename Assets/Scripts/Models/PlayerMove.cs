using System.Collections.Generic;

namespace Models
{
    public class PlayerMove
    {

        public PlayerMove(KeyValuePair<int, int> location, PLayerType type)
        {
            _location = location;
            _state = (TargetState)type;
        }
        private KeyValuePair<int,int> _location { get; set; }
        private TargetState _state { get; set; }
    }
}