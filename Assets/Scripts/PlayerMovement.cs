using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool jump = false;
    [SerializeField]private float force = 250f;

    private Vector2 move = new Vector2();
    [Range(1f, 5f)] [Tooltip("Vitesse en x")] [SerializeField] private float spdX = 4f;

    private Rigidbody2D rigid;
    private Vector3 zeroVelocity = Vector3.zero;

    [SerializeField] private float smoothing = 0.1f;
    private float movement;

    public float speed = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
     
    }
    public void OnJump(InputAction.CallbackContext context )
    {
        jump = context.performed;
        Vector2 jumpForce = new Vector2(0,force);
        Jump(jumpForce);
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        Vector3 pos = new Vector3();
        pos.x = move.x * spdX;
        rigid.velocity = Vector3.SmoothDamp(rigid.velocity, pos, ref zeroVelocity,smoothing);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        Move();        
    }

    public void Jump(Vector2 vec)
    {
        rigid.AddForce(vec * force);
    }
}
