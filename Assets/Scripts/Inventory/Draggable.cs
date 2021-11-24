using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragging; //whether this object is currently being dragged
    public Transform originalParent; //original parent of this object that it will snap back to after drag has ended
    public Canvas canvas; //canvas that parent is attached to

    public Jar jar;

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


    //if the object has ended dragging, set it back to its original position and if dragged onto another jar, move around jars
    public void OnEndDrag(PointerEventData eventData)
    {
        //snap back to the parent
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;

        //check if there is there a jar underneath the object
        Jar jarFound = null;
        EventSystem.current.RaycastAll(eventData, hits);
        foreach (RaycastResult hit in hits)
        {
            Jar j = hit.gameObject.GetComponent<Jar>();
            if (j)
            {
                jarFound = j;
            }
        }

        //if there is a jar under the object, swap its contents with that of the jar at the original position
        if (jarFound)
        {
            Swap(jarFound);
        }

        //set dragging back to false
        dragging = false;
    }

    //sbstract function that swaps the contents of two jars
    protected abstract void Swap(Jar newParent);
}
