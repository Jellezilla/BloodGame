using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static GameObject itemBeingDragged;
    private GameObject _infoBox;
    private float _xOffset = 0;//227 / 2 + 30; //hardcoded calculated distance to center icon on drag
    private int _currentSlotType = 0;

    Vector3 startPosition;
    Transform startParent;

    void Start() {
        //GetComponent<Button>().onClick.AddListener(() => { ShowPartData();});
        _infoBox= gameObject.transform.GetChild(0).gameObject;
        //_infoBox = transform.Find("InfoBox").gameObject;
        Debug.Log(_infoBox);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = new Vector2(eventData.position.x - _xOffset, eventData.position.y);

        _infoBox.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData) {
        //itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        /*if (transform.parent == startParent) {
            transform.position = startPosition;
        }
        else {
            
        }*/
        transform.position = startPosition;
        //int currentSlotType = transform.parent.GetComponent<UIDragSlot>().SlotType;

        Debug.Log("DRAG CALL");

        if (transform.parent.GetComponent<UIDragSlot>()) {
            _currentSlotType = transform.parent.GetComponent<UIDragSlot>().SlotType;
        }
        switch (_currentSlotType) {
            case (0):
                ShowInfo(true);
                break;
            case (1):
                ShowInfo(false);
            break;
        }

        //        itemBeingDragged.GetComponent<LayoutElement>().ignoreLayout = false;
        //        itemBeingDragged = null;
    }

    public void ShowInfo(bool visible) {
        _infoBox.SetActive(visible);
    }

    public void ShowPartData() {
        Debug.Log("SHOW ITEM");
    }
}