using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TraversalManager : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    int obInd = 0;
    public GameObject TubeObj;
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
    void GenerateTubes()
    {
        GameObject to = Instantiate(TubeObj);
        
        SpriteShapeController ssc  = to.GetComponent<SpriteShapeController>();
        
        ssc.spline.SetPosition(1, new Vector3(ssc.spline.GetPosition(1).x, Random.Range(-2f, 2f)));
        ssc.spline.SetPosition(2, new Vector3(ssc.spline.GetPosition(2).x, Random.Range(-2f, 2f)));
        ssc.spline.SetPosition(3, new Vector3(ssc.spline.GetPosition(3).x, Random.Range(-2f, 2f)));
        ssc.spline.SetPosition(4, new Vector3(ssc.spline.GetPosition(4).x, Random.Range(-2f, 2f)));

    }
}
