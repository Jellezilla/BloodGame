using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static GameObject itemBeingDragged;
    public string PartTypeName;
    public bool IsThruster = false;

    private GameObject _infoBox;
    private float _xOffset = 0;//227 / 2 + 30; //hardcoded calculated distance to center icon on drag
    private int _currentSlotType = 0;
    private Player _player;
    private GameObject _partType;

    Vector3 startPosition;
    Transform startParent;

    void Start() {
        _player = GameController.Instance.Player.GetComponent<Player>();
        _infoBox = gameObject.transform.GetChild(0).gameObject;

        switch (PartTypeName) {
            case ("hook"):
                _partType = _player.Chasis.hooklauncherPrefab;
                break;
            case ("rifle"):
                _partType = _player.Chasis.laserRiflePrefab;
                break;
            case ("rocket"):
                _partType = _player.Chasis.rocketLauncherPrefab;
                break;
            case ("mine"):
                _partType = _player.Chasis.mineLauncherPrefab;
            break;
            case ("main-thruster"):
                _partType = _player.Chasis.mainThrusterPrefab;
            break;
            case ("side-thruster"):
                _partType = _player.Chasis.lateralThrusterPrefab;
                break;
            default:
                _partType = _player.Chasis.mainThrusterPrefab;
                break;
        }
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

        if (_currentSlotType == 0) {
            ShowInfo(true);
        }
        else {
            ShowInfo(false);
            Debug.Log("Launcher attached!");
            _player.Chasis.RemovePart(_currentSlotType - 1);
            _player.Chasis.AttachPart(_partType, _currentSlotType-1, IsThruster);
            _player.UpdateParts();
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