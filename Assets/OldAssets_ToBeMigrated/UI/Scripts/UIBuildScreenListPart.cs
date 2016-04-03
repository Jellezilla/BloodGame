using UnityEngine;
using System.Collections;

public class UIBuildScreenListPart : MonoBehaviour {
    private GameObject _infoBox;
    private Vector2 _startCoords;
    private float _xOffset = 227 / 2 + 30; //hardcoded calculated distance to center icon on drag

    void Start() {
        _infoBox = transform.Find("InfoBox").gameObject;
        _startCoords = new Vector2(transform.position.x, transform.position.y);
    }

    public void OnDrag() {
        transform.position = new Vector3(Input.mousePosition.x - _xOffset, Input.mousePosition.y, Input.mousePosition.z);
        _infoBox.SetActive(false);
    }

    public void EndDrag() {
        transform.position = _startCoords;
        _infoBox.SetActive(true);
    }
}