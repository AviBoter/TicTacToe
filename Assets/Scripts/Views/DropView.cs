using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Views
{
    public class DropView : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown _dropdown = null;
        public List<TMP_Dropdown.OptionData> Options
        {
            get { return _dropdown.options; }
            set
            {
                //Resets the value to the first item
                _dropdown.value = 0;
                
                _dropdown.options = value;
            }
        }
        public string Value
        {
            get { return _dropdown.options[_dropdown.value].text; }
            set
            {
                int index = Options.FindIndex(option => option.text == value);
                _dropdown.value = index;
            }
        }

        public bool IsValid()
        {
            return Value != null;
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }
        
    }
}