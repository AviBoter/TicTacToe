using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class FadeView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _loadingPanel;

        private static Color _opaqueBlack = new Color(0, 0, 0, 0);

        public async Task AnimateFadeIn(float time = 1 , bool loadingPanel = true )
        {
            _image.gameObject.SetActive(true);
            _image.color = _opaqueBlack;
            bool finished = false;
            _image.DOColor(Color.black, time).OnComplete(() =>
            {
                finished = true;
                if (loadingPanel)
                {
                    _loadingPanel.SetActive(true);
                }
            });
            while (!finished) await Task.Delay(200);
        }

        public async Task AnimateFadeOut(float time = 1)
        {
            bool finished = false;
            _image.DOColor(new Color(0, 0, 0, 0.1f), time).OnStepComplete(() =>
            {
                    _image.gameObject.SetActive(false);
                    finished = true;
            });
            
            while (!finished) await Task.Delay(100);
        }
    }
}
