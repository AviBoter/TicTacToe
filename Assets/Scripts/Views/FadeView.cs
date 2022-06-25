using System.Threading.Tasks;
using Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class FadeView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private static Color _opaqueBlack = new Color(0, 0, 0, 0);

        private async void Awake()
        {
            Lookup.Instance.FadeView = this;
            await AnimateFadeOut(2);
        }

        public async Task AnimateFadeIn(float time = 1)
        {
            _image.gameObject.SetActive(true);
            _image.color = _opaqueBlack;
            bool finished = false;
            _image.DOColor(Color.black, time).OnComplete(() =>
            {
                finished = true;
            });
            while (!finished) await Task.Delay(200);
        }

        private async Task AnimateFadeOut(float time = 1)
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
