using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed;
    public float jumpForce;

    [Header("Rotation")]
    public float sensitivity = 250f;
    public float MouseX;
    public Vector3 RotationVelocityLeft;
    public Vector3 RotationVelocityRight;

    [Header("Points")]
    public int coins;

    [HideInInspector]
    public Rigidbody rb;

    Vector3 x, z;

    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        RotationVelocityLeft = new Vector3(0, sensitivity, 0);
        RotationVelocityRight = new Vector3(0, -sensitivity, 0);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        x = transform.right * Input.GetAxisRaw("Horizontal");
        z = transform.forward * Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && jumping == false)
        {
            jump();
        }
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    void Movement()
    {
        Vector3 movement = (x + z).normalized * playerSpeed;
        rb.AddForce(movement);
    }

    void jump()
    {
        jumping = true;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Rotation()
    {
        //rotate left
        Quaternion leftDeltaRotation = Quaternion.Euler(RotationVelocityLeft * Time.deltaTime);
        //rotate right
        Quaternion rightDeltaRotation = Quaternion.Euler(RotationVelocityRight * Time.deltaTime);

        if (Input.GetAxis("Mouse X") > 0)
        {
            MouseX += 0.01f;
        }

        else if (Input.GetAxis("Mouse X") < 0)
        {
            MouseX -= 0.01f;
        }

        else
        {
            MouseX = 0;
        }

        MouseX = Mathf.Clamp(MouseX, -1, 1);

        //rotate left
        if (MouseX > 0)
        {
            rb.MoveRotation(rb.rotation * leftDeltaRotation);
        }

        //rotate right
        if (MouseX < 0)
        {
            rb.MoveRotation(rb.rotation * rightDeltaRotation);
        }
    }
        }
    }

}
