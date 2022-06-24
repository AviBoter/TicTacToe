using UnityEngine;

namespace Timer
{
    public class BaseSharedBehaviour<T> : MonoBehaviour where T : BaseSharedBehaviour<T>
    {
        private static T _sharedInstance;

        public static T SharedInstance
        {
            get
            {
                if (_sharedInstance != null) return _sharedInstance;
                var go = new GameObject();
                _sharedInstance = go.AddComponent<T>();
                return _sharedInstance;
            }
        }

        protected virtual void Awake()
        {
            _sharedInstance = (T)this;
        }
    }
}