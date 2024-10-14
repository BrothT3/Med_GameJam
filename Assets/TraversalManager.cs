using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversalManager : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    int obInd = 0;
    
    void Start()
    {
        SelectObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();

    }

    void SelectObstacle()
    {
        GameObject obj = objects[obInd];
        if (obj == null )
        {

        }
        float yPos = Random.Range(Screen.height/4, (Screen.height/4 + Screen.height/2));
        obj.transform.position = new Vector2 ( Screen.width + 250, yPos);
        
    }

    void MoveObstacle()
    {
        objects[obInd].transform.position = new Vector2(transform.position.x - 1, 0);
    }
}
