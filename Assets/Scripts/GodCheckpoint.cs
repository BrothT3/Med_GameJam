using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GodCheckpoint : MonoBehaviour
{

    public GameObject fjer;
    public Sprite lvl1Feather;
    public Sprite lvl2Feather;
    public Sprite lvl3Feather;
    public Sprite lvl4Feather;
    public GameObject LightHand;
    public GameObject DarkHand;

    GameObject SelectFeather()
    {
        int RoundScore = GameManager.Instance.Player.GetComponent<LightCollision>().featherPoints;
        GameManager.Instance.Player.GetComponent<LightCollision>().featherPoints = 0;
        GameObject feather = fjer;
        feather.GetComponent<SpriteRenderer>().sprite = lvl1Feather;
        GameManager.Instance.scoreText.text = "0/200";
        GameManager.Instance.barFillTemporary.sizeDelta = new Vector2(0, 24);
        foreach (GameObject featherIcon in GameManager.Instance.feathers){
            featherIcon.SetActive(false);
        }
        GameManager.Instance.feathers[0].SetActive(true);
        GameManager.Instance.barFillPermanent.GetComponent<Image>().color = GameManager.Instance.featherColors[0];

        switch (RoundScore)
        {
            case int n when n <50:
                feather.GetComponent<SpriteRenderer>().sprite = lvl1Feather;
                feather.GetComponent<FjerMovement>().hand = DarkHand.transform;
                break;
            case int n when n < 100:
                feather.GetComponent<SpriteRenderer>().sprite = lvl2Feather;
                feather.GetComponent<FjerMovement>().hand = DarkHand.transform;
                break;
            case int n when n < 150:
                feather.GetComponent<SpriteRenderer>().sprite = lvl3Feather;
                feather.GetComponent<FjerMovement>().hand = LightHand.transform;
                break;
            case int n when n > 151:
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
