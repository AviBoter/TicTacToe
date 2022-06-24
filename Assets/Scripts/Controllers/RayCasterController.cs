using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Controllers;
using GameEvents;
using Models.GameModels;
using Views;

public class RayCasterController : MonoBehaviour
{
    GraphicRaycaster _raycaster;
    PointerEventData _pointerEventData;
    EventSystem _eventSystem;

    private GameModel _gameModel => Lookup.Instance.GameModel;
    private CrossControllersEvents _ControllersEvents => Lookup.Instance.CrossControllersEvents;

    private List<TargetView> _targetViews;
    void Start()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
        _eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            
            List<RaycastResult> results = new List<RaycastResult>();
            _raycaster.Raycast(_pointerEventData, results);
            
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Target"))
                {
                    HandleResultView(result);
                }
            }
        }
    }

    private void HandleResultView(RaycastResult result)
    {
        TargetView targetView = result.gameObject.GetComponent<TargetView>();
        if (_gameModel is PvpGameModel && !targetView.IsPressed())
        {
            _ControllersEvents.OnPlayerPressTarget.Invoke(targetView.GetX(),targetView.GetY() , true);
            targetView.TargetPressed(true);
        }
        else if (_gameModel is PvCGameModel && !targetView.IsPressed() && _gameModel._isPlayer1)
        {
            _ControllersEvents.OnPlayerPressTarget.Invoke(targetView.GetX(),targetView.GetY(), true);
            targetView.TargetPressed(true);
        }
    }
}