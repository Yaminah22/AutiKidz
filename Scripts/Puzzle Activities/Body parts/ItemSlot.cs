using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] WithoutFoxStarScript star;
    [SerializeField] DragDrop item;
    GameObject slot;
    private Vector2 original_pos;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        slot = gameObject;
        audioManager.Play("question");
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (slot.CompareTag(item.tag))
        {
            if (Vector2.Distance(slot.transform.position, item.transform.position) < 2)

            {
                item.transform.position = slot.transform.position;
                audioManager.Play(slot.tag);
                item.enabled = false;
                Destroy(slot);
                star.progress();
            }
        }
    }
}
