using UnityEngine;


public class DraggableObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialPosition;
    [SerializeField] Camera Canvas_camera;
    [SerializeField] WithoutFoxStarScript star;
    private Collider2D dropAreaCollider; // Collider of the drop area
    private DropStrategy dropStrategy; // Drop strategy for the object
    private bool placed= false;
    AudioManager audiomanager;

    private void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        audiomanager.Play("question");
    }
    public void SoundBtn()
    {
        audiomanager.Play("question");
    }
    private void OnMouseDown()
    {
        isDragging = true;
        initialPosition = transform.position;
    }

    private void OnMouseUp()
    {
        isDragging = false;
      
        // Check if the object is dropped within the drop area and collides with the correct container
        if (ValidateCollision())
        {
            audiomanager.Play("placed");
            // Object dropped within the drop area and collides with the correct container
            dropStrategy.HandleDrop(gameObject, dropAreaCollider);
            star.progress();
            placed = true;
            audiomanager.Play(this.gameObject.name);
            SetDropAreaCollider(null);

        }
        else if (!placed && !ValidateCollision())
        {
            // Object dropped outside of the drop area or collides with the incorrect container
            // Return it to its initial position
           transform.position = initialPosition;
            audiomanager.Play("drop");
        }
    }
    


    private void Update()
    {
        if (isDragging && !placed)
        {
            // Update the object's position based on the mouse movement
            Vector3 mousePosition = GetMousePos();
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the object entered a drop area collider
        if (other.gameObject.CompareTag("DropArea"))
        {
            dropAreaCollider = other.gameObject.GetComponent<Collider2D>();
           
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exited a drop area collider
        if (other.CompareTag("DropArea"))
        {
            
            dropAreaCollider = null;
        }
    }

    public void SetDropAreaCollider(Collider2D collider)
    {
        dropAreaCollider = collider;
    }

    public void SetDropStrategy(DropStrategy strategy)
    {
        dropStrategy = strategy;
    }

    public string getdrop() {
        return dropStrategy.GetType().Name;
    }
    private bool ValidateCollision()
    {

        // Validate the collision based on the drop strategy and the object's tag
        if (dropStrategy != null && dropAreaCollider!=null&& gameObject.CompareTag(dropStrategy.GetType().Name))
        {
            return dropStrategy.ValidateDrop(dropAreaCollider, gameObject.tag);
        }
        else
        {
            return false;
        }
    }


Vector3 GetMousePos()
    {
        return Canvas_camera.ScreenToWorldPoint(Input.mousePosition);
    }

}
