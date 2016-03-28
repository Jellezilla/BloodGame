using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIDragSlot : MonoBehaviour, IDropHandler {
    [Tooltip("Slot types indicated which slot it belongs to. 0 = menu")]
    public int SlotType;
    public GameObject itemBeingDragged {
        get {
            if (transform.childCount > 0) {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("SLOT CALL");
        if (!itemBeingDragged) {
            UIDragHandler.itemBeingDragged.transform.SetParent(transform);
        }
    }


}