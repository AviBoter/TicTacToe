using UnityEngine;
using UnityEngine.UI;


namespace Views
{
    public class BackgroundView : MonoBehaviour
    {
        public void AssignNewBg(Sprite bg)
        {
            try
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
            catch
            {
                Debug.LogError("you should create asset bundle before u try to re-skin!");
            }
        }
    }
}
