using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Item itemUI;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform conent;

    private void Awake() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        conent = transform.parent;
    }

    public void Initialize(Item item)
    {
        itemUI = item;
        transform.GetComponent<Image>().sprite = itemUI.image;
    }

    public Item GetItem()
    {
        return itemUI;
    }

    public void SetToHends()
    {
        Player.playerSingleton.TakeInHends(Inventory.singleton.GetItem(itemUI.nameID));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            // Player.playerSingleton.TakeInHends(Inventory.singleton.GetItem(itemUI.nameID));
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Inventory.singleton.RemoveItem(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
}
