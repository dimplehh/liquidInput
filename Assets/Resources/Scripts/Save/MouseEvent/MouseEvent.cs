using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text selectText; //현재 텍스트
    private void Start()
    {
        selectText = GetComponentInChildren<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectText.fontSize = 60;
        selectText.color = Color.white;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        selectText.fontSize = 50;
        selectText.color = Color.black;
    }
}
