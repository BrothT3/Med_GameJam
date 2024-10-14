using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float currentSpeed; // the speed the plane is moving at currently
    float maxSpeed = 500; // the max speed the plane should be able to accel
    Vector3 defaultForce = new Vector3(0, 250, 0); // how much "power" the engine of the plane has to push it forwards, this value will need tweaked to fit your game
    Vector3 forceToAdd = Vector3.zero;
    bool spaceReleased = true;
    
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Playermovement();
    }

    void Playermovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spaceReleased)
        {
            spaceReleased = false;
            
            PlayerAddForce();

        }
        spaceReleased = true;
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y + 45f > Screen.height)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, -3f, 0);
        }
        if ((screenPos.y - 45f) < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 5f, 0);
        }
    }

    void PlayerAddForce()
    {
        {
            
            currentSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;
            if (GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            Debug.Log($"{currentSpeed}," + (maxSpeed - (maxSpeed / 4)));
            if (currentSpeed > maxSpeed - (maxSpeed / 4))
            {
                Debug.Log("test");
                float forceMultiplier = defaultForce.y * maxSpeed - (currentSpeed / maxSpeed);
                forceToAdd.y = forceMultiplier; 
            }
            else 
            {
                forceToAdd = defaultForce;
                
            }
            Debug.Log(forceToAdd.y);
            
            GetComponent<Rigidbody2D>().AddForce(forceToAdd);
        }
    }

}
