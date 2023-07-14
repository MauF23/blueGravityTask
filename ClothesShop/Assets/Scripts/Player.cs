using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody2D;
    private Vector2 movement;
    public bool canMove;
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

    public bool Inventory()
    {
        return Input.GetKeyDown(inventoryKey);
    }

    public void ToggleMovement(bool value)
    {
        canMove = value;
    }
}
