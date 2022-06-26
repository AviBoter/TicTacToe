using System.Collections.Generic;

namespace Models
{
    public class PlayerMove
    {
        // location on the matrix
        internal KeyValuePair<int,int> Location { get; set; }
        
        // X or O
        public PlayerMove(KeyValuePair<int, int> location, PlayerType type)
        {
            Location = location;
        }

    }
}