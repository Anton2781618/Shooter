using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHendsUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);

            eventData.pointerDrag.GetComponent<ItemUI>().SetToHends();
        }
    }
}
