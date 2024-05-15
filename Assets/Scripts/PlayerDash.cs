using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f; // How long dash lasts
    public float dashCooldown = 1f; // Cooldown duration in seconds

    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime; // The time when the last dash ended

    private PlayerInput playerInput;
    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Dash"].performed += OnDash;
    }

    private void OnDisable()
    {

        playerInput.actions["Dash"].performed -= OnDash;
    }

    private void Update()
    {
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
                lastDashTime = Time.time; // Record the time when the dash ended
            }
            else
            {
                transform.position += dashDirection * dashSpeed * Time.deltaTime;
            }
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        // Check if currently not dashing and if the cooldown has passed
        if (!isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashDirection = GetDashDirection();
        }
    }

    private Vector3 GetDashDirection()
    {
        return transform.forward;
    }
}