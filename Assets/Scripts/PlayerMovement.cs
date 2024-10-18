using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float currentSpeed; // the speed the plane is moving at currently
    public float maxSpeed = 10; // the max speed the plane should be able to accel
    public float defaultForce = 550; // how much "power" the engine of the plane has to push it forwards, this value will need tweaked to fit your game
    Vector3 forceToAdd = Vector3.zero;
    bool spaceReleased = true;
    public float maxTiltAngle;
    public float tiltSpeed;
    public Animator anim;
    public bool MovingPermitted;
    [SerializeField] private AudioSource jumpSound;
    // Start is called before the first frame update

    private void Update()
    {
        Playermovement();
        PlayerTiltLerp();
    }

    void Playermovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spaceReleased)
        {
            if (GameManager.Instance.inCheckpoint) return;

            spaceReleased = false;
            jumpSound.pitch = Random.Range(0.89f, 1.11f);
            jumpSound.Play();

            PlayerAddForce();
        }
        spaceReleased = true;
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y + 45f > Screen.height)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, -3f, 0);
        }
        if ((screenPos.y + 100f) < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 3.5f, 0);
        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
    }

    void PlayerAddForce()
    {
        {
            forceToAdd = new Vector3(0, defaultForce, 0);
            //Debug.Log(forceToAdd.y);


            GetComponent<Rigidbody2D>().AddForce(forceToAdd);
            anim.SetTrigger("WingFlap");
        }
    }
    void PlayerTiltLerp()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Get the current velocity on the Y axis
        float verticalVelocity = rb.velocity.y;

        // Calculate the target tilt angle based on the vertical velocity
        // Tilts upwards if moving up, downwards if moving down
        float targetTiltAngle = Mathf.Clamp(verticalVelocity * maxTiltAngle / maxSpeed, -maxTiltAngle, maxTiltAngle);

        // Get the current rotation (z-axis) and smoothly interpolate to the target angle
        float currentTiltAngle = transform.eulerAngles.z;

        // Adjust angle from 0-360 to -180 to 180 for proper Lerp behavior
        if (currentTiltAngle > 180)
        {
            currentTiltAngle -= 360;
        }

        // Use Mathf.Lerp to smoothly transition between current and target tilt
        float newTiltAngle = Mathf.Lerp(currentTiltAngle, targetTiltAngle, Time.deltaTime * tiltSpeed);

        // Apply the new rotation to the player object (on the z-axis for 2D)
        transform.rotation = Quaternion.Euler(0, 0, newTiltAngle);
    }


}
