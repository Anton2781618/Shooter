using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//класс для работы с UI элементом инвентаря
public class ItemUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Item itemUI;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform conent;

    public Unit unitt;
    public Inventory inventory;

    private void Awake() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        conent = transform.parent;
    }

    public void Initialize(Item item, Inventory invent)
    {
        itemUI = item;
        transform.GetComponent<Image>().sprite = itemUI.image;

        inventory = invent;
        unitt = inventory.transform.GetComponent<Unit>();
    }

    public Item GetItem()
    {
        return itemUI;
    }

    public void SetToHends()
    {
        if(!unitt.photon.IsMine)return; 
        inventory.SetCoseItem(this);
        unitt.TakeInHends(inventory.GetItem(itemUI.nameID));        
    }

    public void SetToInvent()
    {
        if(inventory.GetcoseItem() == null)
        {
            inventory.DisaibleWeapon();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {

        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventory.PutItem(this);
        transform.SetParent(transform.parent.parent);
        canvasGroup.blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!eventData.pointerEnter)
        {
            SetToInvent();
            inventory.RemoveItem(this);
        }
        canvasGroup.blocksRaycasts = true;
    }
}
