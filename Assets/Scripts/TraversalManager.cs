using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TraversalManager : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public int obInd = 0;
    public GameObject TubeObj;
    public int Tubes;
    public float tubeSpeed;
    public float SplineDifference;

    void Start()
    {
        GenerateTubes();
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
        if (obj == null)
        {

        }

        float yPos = Random.Range(Screen.height / 4, (Screen.height / 4 + Screen.height / 2));
        var obPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + 500, yPos));
        //Debug.Log(yPos);
        obj.transform.position = new Vector2(obPos.x, 0);
        obj.gameObject.SetActive(true);

    }

    void MoveObstacle()
    {
        if (objects.Count <= 0)
            return;

        Vector3 obPos = new Vector3(objects[obInd].transform.position.x - tubeSpeed, objects[obInd].transform.position.y, 0);
        objects[obInd].transform.position = obPos;
        if (Camera.main.WorldToScreenPoint(obPos).x < -800 && obInd + 1 < objects.Count)
        {
            obInd++;
            SelectObstacle();
        }
    }
    void GenerateTubes()
    {
        for (int i = 0; i < Tubes; i++)
        {
            GameObject to = Instantiate(TubeObj);

            SpriteShapeController ssc = to.GetComponent<SpriteShapeController>();
            float ypos = ssc.spline.GetPosition(1).y;

        

            for (int j = 0; j < 5; j++)
            {
                ypos += Random.Range(-2f, 2f);
                //    if (Camera.main.WorldToScreenPoint(new Vector3(0, ypos, 0)).y > Screen.height/2 + 50f)
                //        ypos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - 150f, 0)).y;
                //    if (Camera.main.WorldToScreenPoint(new Vector3(0, ypos, 0)).y < -(Screen.height/2))
                //        ypos = Camera.main.ScreenToWorldPoint(new Vector3(0, 50, 0)).y;

                //    ssc.spline.SetPosition(j, new Vector3(ssc.spline.GetPosition(j).x, ypos));
                //    Debug.Log(j + ":"+Camera.main.WorldToScreenPoint(new Vector3(0, ypos, 0)).y);

                float topBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - Screen.height / 4 - 65f, 0)).y;
                float bottomBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 4+65, 0)).y;

                ypos = Mathf.Clamp(ypos, bottomBoundary, topBoundary);

                ssc.spline.SetPosition(j, new Vector3(ssc.spline.GetPosition(j).x, ypos));
            }
            to.gameObject.SetActive(false);
            objects.Add(to);
            ypos += Random.Range(-SplineDifference, SplineDifference);


        }
    }
}
