using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollRectNoDrag : ScrollRect
{
    /* Override Drag Functions To Do Nothing */
    public override void OnBeginDrag (PointerEventData eventData) { }
    public override void OnDrag (PointerEventData eventData) { }
    public override void OnEndDrag (PointerEventData eventData) { }
}
