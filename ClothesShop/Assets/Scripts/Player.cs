using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody2D;
    public Animator animator;
    private Vector2 movement;
    [ReadOnly]
    public bool canMove;
    private Vector3 originalScale;
    public static Player instance;

    [FoldoutGroup("Controls")]
    public KeyCode inventoryKey;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        ToggleMovement(true);
        originalScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (canMove)
        {
            Movement();
        }
    }

    private void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.y = Input.GetAxisRaw("Vertical") * speed;
        rigidbody2D.velocity = new Vector2(movement.x, movement.y);
        animator.SetFloat("speed", rigidbody2D.velocity.sqrMagnitude);
        if(movement.x < 0)
        {
            Flip(true);
        }
        else if(movement.x > 0)
        {
            Flip(false);
        }
    }

    private void Flip(bool value)
    {
        if (value)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    public bool Inventory()
    {
        return Input.GetKeyDown(inventoryKey);
    }

    public void ToggleMovement(bool value)
    {
        canMove = value;
    }
}
