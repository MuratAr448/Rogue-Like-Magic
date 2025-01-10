using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DragAble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum Drag
    {
        Spell,
        Item
    }
    public Drag DragState;
    Spells spell;
    TurnSystem TurnSystem;
    GameObject theSpell;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (DragState == Drag.Spell)
        {
            TurnSystem = FindObjectOfType<TurnSystem>();
            image = GetComponent<Image>();
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
            theSpell = eventData.pointerDrag;
            spell = theSpell.GetComponent<Spells>();
        }
        else if (DragState == Drag.Item)
        {
            image = GetComponent<Image>();
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
