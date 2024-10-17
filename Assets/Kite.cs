using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kite : MonoBehaviour
{
    public GameObject rowStart;
    public GameObject TestFjer;
    void Start()
    {

    }


    void Update()
    {

    }

    public void PlaceFeathers()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject feather = Instantiate(TestFjer, transform);
                TestFjer.GetComponent<FjerMovement>().checkFeather = false;
                
                TestFjer.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.featherResults[j].GetComponent<SpriteRenderer>().sprite;

                feather.transform.position = new Vector2(rowStart.transform.position.x + (0.9f*j), rowStart.transform.position.y + (0.9f*i));
                feather.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                feather.gameObject.SetActive(true);
            }


        }
    }
}
