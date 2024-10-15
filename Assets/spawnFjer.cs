using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFjer : MonoBehaviour
{
    public GameObject fjer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawn()
    {
        Instantiate(fjer, transform.position, transform.rotation);
    }
}
