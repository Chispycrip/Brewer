using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragging; //whether this object is currently being dragged
    public Transform originalParent; //original parent of this object that it will snap back to after drag has ended
    public Canvas canvas; //canvas that parent is attached to

    public Slot slot;

    List<RaycastResult> hits = new List<RaycastResult>();


    //when object is starting to be dragged, set its dragging state to true and render it in front of all other objects
    public void OnBeginDrag(PointerEventData eventData)
    {
        //make sure original parent and canvas are set before moving the object
        if (originalParent == null)
        {
            originalParent = transform.parent;
        }
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        //swap parent to the canvas and set object to be drawn at front
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();

        //set dragging state to true
        dragging = true;
    }


    //update position to the mouse position
    public void OnDrag(PointerEventData eventData)
    {
        //if this object is being dragged, update its position to the mouse
        if (dragging)
        {
            transform.position = eventData.position;
        }
    }


    //if the object has ended dragging, set it back to its original position and if dragged onto another slot, move around slots
    public void OnEndDrag(PointerEventData eventData)
    {
        //snap back to the parent
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;

        //check if there is there a slot or cauldron underneath the object
        Slot slotFound = null;
        CauldronUI caulFound = null;
        EventSystem.current.RaycastAll(eventData, hits);
        foreach (RaycastResult hit in hits)
        {
            Slot j = hit.gameObject.GetComponent<Slot>();
            CauldronUI c = hit.gameObject.GetComponent<CauldronUI>();
            if (j)
            {
                slotFound = j;
            }
            else if (c)
            {
                caulFound = c;
            }
        }

        //if there is a slot under the object, swap its contents with that of the slot at the original position
        if (slotFound)
        {
            Swap(slotFound);
        }
        //if there is a cauldron under the object and the object is empty, take the contents from the cauldron
        else if (caulFound)
        {
            CauldronFound(caulFound);
        }

        //set dragging back to false
        dragging = false;
    }

    //abstract function that swaps the contents of two slots
    protected abstract void Swap(Slot newParent);


    //abstract function that swaps items with the cauldron
    protected abstract void CauldronFound(CauldronUI cauldron);
}
