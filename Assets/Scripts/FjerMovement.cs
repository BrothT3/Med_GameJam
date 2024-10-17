using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FjerMovement : MonoBehaviour
{



    public Transform hand;

    public float fjerSpeed = 10f;
    public float rotationSpeed = 100f;
    public bool checkFeather = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (checkFeather)
        {
            transform.position = Vector3.MoveTowards(transform.position, hand.position, fjerSpeed * Time.deltaTime);
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

            if (transform.position == hand.position)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
