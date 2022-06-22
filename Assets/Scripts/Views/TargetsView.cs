using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Views
{
    public class TargetsView : MonoBehaviour
    {
        [SerializeField]
        private Sprite X;
        [SerializeField]
        private Sprite O;
        
        [SerializeField]
        private Image _zeroXZero;
        [SerializeField]
        private Image _zeroXOne;
        [SerializeField]
        private Image _zeroXTwo;
    
        [SerializeField]
        private Image _oneXZero;
        [SerializeField]
        private Image _oneXOne;
        [SerializeField]
        private Image _oneXTwo;
    
        [SerializeField]
        private Image _twoXZero;
        [SerializeField]
        private Image _twoXOne;
        [SerializeField]
        private Image _twoXTwo;
        
        // Update is called once per frame
        private void AddTargetAtLocation(KeyValuePair<int,int> location,PLayerType type)
        {
            Image target = GetTargetImage(location);
            
            if (type == PLayerType.O)
            {
                target.sprite = O;
                target.DOColor(new Color(0, 0, 0, 1f), 0.5f);
            }
            else
            {
                target.sprite = X;
                target.DOColor(new Color(0, 0, 0, 1f), 0.5f);
            }
        }

        private Image GetTargetImage(KeyValuePair<int, int> keyValuePair)
        {
            switch (keyValuePair)
            {
                case (0,0):
                    return _zeroXZero;
                case (0,1):
                    return _zeroXOne;
                case (0,2):
                    return _zeroXTwo;
                case (1,0):
                    return _oneXZero;
                case (1,1):
                    return _oneXOne;
                case (1,2):
                    return _oneXTwo;
                case (2,0):
                    return _twoXZero;
                case (2,1):
                    return _twoXOne;
                case (2,2):
                    return _twoXTwo;
            }
            return null;
        }
        
        private void RemoveTargetAtLocation(KeyValuePair<int,int> location)
        {
            Image target = GetTargetImage(location);

            target.sprite = null;
            target.DOColor(new Color(0, 0, 0, 0f), 0.5f);
        }
        
        private void ResetView()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    RemoveTargetAtLocation(new KeyValuePair<int, int>(i, j));
                }
            }
        }
        
    }
}
