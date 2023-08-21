using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    private bool dragging, placed;
    private Vector2 offset, original_pos;
    public Camera Canvas_camera;
    AudioManager audiomanager;
    private PuzzleSlot _slot;
    ScoreManager star;

    private void Awake()
    {
        original_pos = transform.localPosition;
        Debug.Log(original_pos);
    }

    public void Init(PuzzleSlot slot)
    {
        _renderer.sprite = slot.Renderer.sprite;
        _slot = slot;
    }
    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        Canvas_camera = GameObject.FindWithTag("canvas_camera").GetComponent<Camera>();
        star = ScoreManager.Instance;

    }
    void Update()
    {
        if (!dragging) return;
        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
    }
    void OnMouseDown()
    {
        if (!placed)
        {
            dragging = true;
            offset = GetMousePos() - (Vector2)transform.position;
        }
    }
    void OnMouseUp()

    {
        if (_slot != null && !placed)
        {
            if (Vector2.Distance(transform.position, _slot.transform.position) < 2)
            {
                star.progress();
                transform.position = _slot.transform.position;
                audiomanager.Play("puzzleplaced");
                placed = true;
                audiomanager.Play(_slot.name);

            }

            else
            {

                transform.localPosition = original_pos;
                Debug.Log(transform.localPosition);
                audiomanager.Play("drop");
            }
            dragging = false;
        }



    }
    Vector2 GetMousePos()
    {
        return Canvas_camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
