using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class LightCollision : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D bc;
    public int featherPoints;
    private bool tubeContact;
    float pointsToAdd = 0;
    public Animator anim;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AddFeatherScore();
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "LightTube")
        {
            tubeContact = true;
            anim.SetBool("TouchingTube", true);
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "LightTube")
        {
            tubeContact = false;
            featherPoints += (int)pointsToAdd;
            //Debug.Log(featherPoints);
            pointsToAdd = 0;
            anim.SetBool("TouchingTube", false);
        }
    }

    public void AddFeatherScore()
    {
        if (tubeContact)
            pointsToAdd += Time.deltaTime * 10f;       
    }

}
