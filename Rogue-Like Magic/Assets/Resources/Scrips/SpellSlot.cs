using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragAble dragAble = dropped.GetComponent<DragAble>();
            if (dragAble.DragState == DragAble.Drag.Spell)
            {
                dragAble.parentAfterDrag = transform;
            }
        }
    }
}
