using UnityEngine;
using UnityEngine.UI;


namespace Views
{
    public class BackgroundView : MonoBehaviour
    {
        public void AssignNewBg(Sprite bg)
        {
            Image image = GetComponent<Image>();
            if (image != null)
            {
                image.sprite = bg;
            }
            else
            {
                Debug.LogError("BackgroundView error");
            }
        }
    }
}
