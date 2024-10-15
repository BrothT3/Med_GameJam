using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FjerMovement : MonoBehaviour
{
    
   
    
    public Transform hand;
    
    public float fjerSpeed = 10f;
    public float rotationSpeed = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.FindGameObjectWithTag("GoodHandGrab").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hand.position, fjerSpeed * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        if (transform.position == hand.position)
        {
            Destroy(gameObject);
        }
    }
}
