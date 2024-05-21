using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody rb;
    [SerializeField] float speed = 1.0f;//Speed of the player
    private bool paused;
    [SerializeField] GameObject pauseMenu;

    bool canJump = true;
    public float jumpForce = 50;
    private int jumpCount = 0; 
    private int maxJumps = 2; //Max number of available jumps

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Moves player
        rb.velocity = transform.TransformDirection(new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }

    void jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity for consistent jump height
            rb.AddForce(this.gameObject.transform.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    void OnCollisionEnter(Collision collidingObject)
    {
        if (collidingObject.gameObject.layer == 8)
        {
            jumpCount = 0; //Reset jump count after landing
            canJump = true;
        }
    }



    void OnMove(InputValue input)//When a movement key is pressed update the move input
    {
        //Debug.Log("Move");
        moveInput = input.Get<Vector2>();
    }

    void OnPause(InputValue input)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (paused)
        {
            //Unpause
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            paused = false;
        }
        else
        {
            //Pause
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            paused = true;
        }
    }
}