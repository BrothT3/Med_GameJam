using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class LightCollision : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D bc;
    float featherPoints;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.tag == "LightTube")
        {
            featherPoints++;
            Debug.Log(featherPoints);
        }
    }

}
