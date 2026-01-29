using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Vector2 mousePosition;
    public float Speed = 1f;
    private Rigidbody2D rb;
    private Vector2 position = new Vector2(0f, 0f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, Speed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}
