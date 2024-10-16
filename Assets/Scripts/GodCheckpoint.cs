using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GodCheckpoint : MonoBehaviour
{

    public GameObject fjer;
    public Sprite lvl1Feather;
    public Sprite lvl2Feather;
    public Sprite lvl3Feather;
    public Sprite lvl4Feather;
    public GameObject LightHand;
    public GameObject DarkHand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    GameObject SelectFeather()
    {
        int RoundScore = GameManager.Instance.Player.GetComponent<LightCollision>().featherPoints;
        GameManager.Instance.Player.GetComponent<LightCollision>().featherPoints = 0;
        GameObject feather = fjer;
        feather.GetComponent<SpriteRenderer>().sprite = lvl1Feather;
        switch (RoundScore)
        {
            case int n when n <25:
                feather.GetComponent<SpriteRenderer>().sprite = lvl1Feather;
                feather.GetComponent<FjerMovement>().hand = DarkHand.transform;
                break;
            case int n when n < 50:
                feather.GetComponent<SpriteRenderer>().sprite = lvl2Feather;
                feather.GetComponent<FjerMovement>().hand = DarkHand.transform;
                break;
            case int n when n < 75:
                feather.GetComponent<SpriteRenderer>().sprite = lvl3Feather;
                feather.GetComponent<FjerMovement>().hand = LightHand.transform;
                break;
            case int n when n < 100:
                feather.GetComponent<SpriteRenderer>().sprite = lvl4Feather;
                feather.GetComponent<FjerMovement>().hand = LightHand.transform;
                break;
        }

        return feather;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Instantiate(SelectFeather(), transform.position, transform.rotation);
        }
    }
}
