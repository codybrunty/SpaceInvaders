using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IPointerDownHandler, IDragHandler{

    [SerializeField] private GameObject player;
    private Vector3 offset;
    private float xBounds;

    private void Start() {
        xBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f)).x;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePosition += offset;
        player.transform.position = new Vector2(Mathf.Clamp(mousePosition.x,-1f*xBounds,xBounds),player.transform.position.y);
    }

    public void OnPointerDown(PointerEventData eventData) {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        offset = player.transform.position - mousePosition;
    }

}




