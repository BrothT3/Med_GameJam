using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{

    [SerializeField] private float speed = 1f;

    private void Start(){
        
    }

    private void FixedUpdate(){
        transform.position = new Vector2(transform.position.x - speed/500, transform.position.y);
    }
}
