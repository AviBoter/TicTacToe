using System;
using UnityEngine;
using UnityEngine.Events;

namespace Views
{
    public class TargetView : MonoBehaviour
    {
        [SerializeField] private int _xLocation;
        [SerializeField] private int _yLocation;

        private bool Pressed = false;

        public int GetX()
        {
            return _xLocation;
        }
        
        public int GetY()
        {
            return _yLocation;
        }
        
        public bool IsPressed()
        {
            return Pressed;
        }
    }
}
