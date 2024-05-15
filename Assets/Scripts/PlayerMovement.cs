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
        if (canJump)
        {
            canJump = false;
            GetComponent<Rigidbody>().AddForce(this.gameObject.transform.up * jumpForce);
        }
    }

    void OnCollisionEnter(Collision collidingObject)
    {
        if (collidingObject.gameObject.layer == 8)
        {
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