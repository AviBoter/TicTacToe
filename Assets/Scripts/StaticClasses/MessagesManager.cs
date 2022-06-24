using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class MessagesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObjectShowMessage = null;
    public static MessagesManager Instance { get; private set; } = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Attempting to create second MessagesManager on " + _gameObjectShowMessage.name);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

         public void ShowMessage(string text, float time)
        {
            StartCoroutine(Popup(text,time));
        }

         IEnumerator Popup(string text,float time)
        {
            _gameObjectShowMessage.transform.DOKill();
            _gameObjectShowMessage.SetActive(true);
            _gameObjectShowMessage.GetComponent<TMP_Text>().text = text;
            yield return new WaitForSeconds(time);
            _gameObjectShowMessage.GetComponent<TMP_Text>().color = Color.white;
            _gameObjectShowMessage.SetActive(false);
        }

}