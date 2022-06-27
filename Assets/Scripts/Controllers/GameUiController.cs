using UnityEngine;
using Views;

namespace Controllers
{
   public class GameUiController : MonoBehaviour
   {
      [SerializeField]
      private BackgroundView _backgroundView;
      private void Awake()
      {
         Lookup.Instance.CrossControllersEvents.OnReSkinPressedAction += AssignNewBg;
      }

      void AssignNewBg(Sprite bg)
      {
         _backgroundView.AssignNewBg(bg);
      }
   }
}
