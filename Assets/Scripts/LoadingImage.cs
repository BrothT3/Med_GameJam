using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingImage : MonoBehaviour{
    
    [SerializeField, Range(1f, 10f)] private float spinSpeed = 5f;
    private float rotationZ;

    private void FixedUpdate(){
        rotationZ -= spinSpeed;
        if (rotationZ < 0){
            rotationZ = 360;
        }

        transform.localRotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
