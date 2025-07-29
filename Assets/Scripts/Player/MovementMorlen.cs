using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMorlen : MonoBehaviour
{
    public static MovementMorlen instance;

    public float speed = 15;

    Rigidbody rbody;
    Animator anim;

    public static bool right = true;


    // Use this for initialization
    void Start()
    {

        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement_vector = new Vector2(horizontalInput, verticalInput);

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("Input_x", movement_vector.x);
            anim.SetFloat("Input_y", movement_vector.y);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        // rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
        rbody.velocity = movement;


        if (horizontalInput > 0)
        {
            changeDirection(1);
            right = true;
        }

        else if (horizontalInput < 0)
        {
            changeDirection(-1);
            right = false;
        }

    }

     public  void changeDirection(int direction)

    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

}