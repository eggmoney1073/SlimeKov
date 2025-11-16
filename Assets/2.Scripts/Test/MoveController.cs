using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    private Vector2 moveInput;
    private float moveSpeed = 5.0f;
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Player Input의 Move 이벤트 연결
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        float x = moveInput.x;
        float y = moveInput.y;

        Vector3 direction = new Vector3(x, 0.0f, y);

        Vector3 velocity = direction * moveSpeed;

        rigid.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
}
