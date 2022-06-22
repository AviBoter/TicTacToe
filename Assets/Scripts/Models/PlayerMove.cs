using System.Collections.Generic;

namespace Models
{
    public class PlayerMove
    {
        // location on the matrix
        internal KeyValuePair<int,int> Location { get; set; }
        
        // X or O
        internal TargetState State { get; set; }

        public PlayerMove(PlayerMove other)
        {
            Location = other.Location;
            State = other.State;
        }
        public PlayerMove(KeyValuePair<int, int> location, PLayerType type)
        {
            Location = location;
            State = (TargetState)type;
        }

    }
}