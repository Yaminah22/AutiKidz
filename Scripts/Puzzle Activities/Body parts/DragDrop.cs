using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 original_pos;
    AudioManager audioManager;
    private Vector3 newTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        audioManager = FindObjectOfType<AudioManager>();
        original_pos = transform.localPosition;
    }
    public void SoundBtn()
    {
        audioManager.Play("question");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        audioManager.Play("notplaced");
        newTransform = new Vector3(original_pos.x, original_pos.y, 0.0f);
        if (transform.localPosition != newTransform)
        {
            transform.localPosition = newTransform;
        }
        canvasGroup.blocksRaycasts = true;
    }
/*
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }*/
    
}
