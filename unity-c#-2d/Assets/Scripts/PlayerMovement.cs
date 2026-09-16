using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer fireSprite;

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float turnSpeed = 2f;

    float moveAxis;
    float turnAxis;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveAxis = value.Get<float>() * moveSpeed;
        fireSprite.color = new Color(fireSprite.color.r, fireSprite.color.g, fireSprite.color.b, value.Get<float>());
    }
    void OnTurn(InputValue value)
    {
        turnAxis = value.Get<float>() * turnSpeed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * moveAxis);
        rb.AddTorque(turnAxis);
    }

    private void OnDisable()
    {
        moveAxis = 0;
        turnAxis = 0;
        rb.linearVelocity = Vector2.zero;
        fireSprite.color = new Color(fireSprite.color.r, fireSprite.color.g, fireSprite.color.b, 0);
    }

}//class
