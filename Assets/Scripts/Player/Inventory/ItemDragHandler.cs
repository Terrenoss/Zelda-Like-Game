using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = eventData.position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out Vector2 localPoint
        );

        transform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        
        Slot dropSlot = null;
        if (eventData.pointerEnter != null)
        {
            dropSlot = eventData.pointerEnter.GetComponent<Slot>() 
                       ?? eventData.pointerEnter.GetComponentInParent<Slot>();
        }
        
        if (originalParent == null)
        {
            transform.SetParent(transform.root);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            return;
        }

        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null && dropSlot != originalSlot)
        {
            GameObject itemInDropSlot = dropSlot.currentItem;

            dropSlot.currentItem = gameObject;
            transform.SetParent(dropSlot.transform);

            if (itemInDropSlot != null)
            {
                itemInDropSlot.transform.SetParent(originalParent);
                itemInDropSlot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                originalSlot.currentItem = itemInDropSlot;
            }
            else
            {
                originalSlot.currentItem = null;
            }
        }
        else
        {
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }


}
