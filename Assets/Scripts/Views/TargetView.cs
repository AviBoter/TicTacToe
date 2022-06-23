using System;
using UnityEngine;
using UnityEngine.Events;

namespace Views
{
    public class TargetView : MonoBehaviour
    {
        [SerializeField] private int _xLocation;
        [SerializeField] private int _yLocation;

        public event Action<int, int> OnPlayerPressTarget;
    
        private bool Pressed { set; get; }
        
        // called when target's button pressed
        public void PlayerPressTheTarget()
        {
            if (!Pressed)
            {
                OnPlayerPressTarget?.Invoke(_xLocation,_yLocation);
                Pressed = !Pressed;
                Debug.Log("Player Press the Target! location: "+ _xLocation +" "+ _yLocation );
            }
            else
            {
                Debug.Log("Player Pressed the wrong! Target! location: "+ _xLocation +" "+ _yLocation);
            }
        }
    }
}
