using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Views;


namespace Parana.Common.UI
{
    public class SupportPopupPanel : MonoBehaviour
    {
        [SerializeField] DropView _dropDown = null;
        private readonly List<string> _categoriesOptions = new List<string>()
        {
            "PvP", "PvC", "CvC"
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