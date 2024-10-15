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
        var obPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + 500, yPos));
        Debug.Log(yPos);
        obj.transform.position = obPos;
        
    }

    void MoveObstacle()
    {
        Vector3 obPos = new Vector3(objects[obInd].transform.position.x-0.2f, objects[obInd].transform.position.y, 0);
        objects[obInd].transform.position = obPos;
        if (Camera.main.WorldToScreenPoint(obPos).x < -500 && obInd+1 < objects.Count)
        {
            obInd++;
            SelectObstacle();
        }
    }
}
