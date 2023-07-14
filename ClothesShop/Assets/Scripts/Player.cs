using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody2D;
    private Vector2 movement;
    private bool canMove;
    public static Player player;

    [FoldoutGroup("Controls")]
    public KeyCode interactKey, escapeKey;

    void Awake()
    {
        if(player == null)
        {
            player = this;
        }
    }
    void Start()
    {
        ToggleMovement(true);
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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rigidbody2D.MovePosition(rigidbody2D.position + movement * speed * Time.deltaTime);
    }

    public bool Interact()
    {
        return Input.GetKeyDown(interactKey);
    }

    public bool Escape()
    {
        return Input.GetKeyDown(escapeKey);
    }

    public void ToggleMovement(bool value)
    {
        canMove = value;
    }
}
