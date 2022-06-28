using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Views
{
    public class SupportPopupPanel : MonoBehaviour
    {
        [SerializeField] private DropView _dropDown = null;
        private readonly List<string> _categoriesOptions = new List<string>()
        {
            "Player Vs Player", "Player Vs Computer"
        };

        private void Awake()
        {
            AssignDropDownOptions();
        }

        void AssignDropDownOptions()
        {
            _dropDown.Options = _categoriesOptions.Select(option => new TMP_Dropdown.OptionData(option)).ToList();
        }
        
    }
}