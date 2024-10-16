using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class LightCollision : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D bc;
    public int featherPoints;
    private bool tubeContact;
    float pointsToAdd = 0;
    private Animator anim;
    [SerializeField] private RectTransform barFillTemporary;
    [SerializeField] private RectTransform barFillPermanent;
    [SerializeField] private int checkpoint;
    [SerializeField] private GameObject[] feathers;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Color[] featherColors;
    [SerializeField] private GameObject popUpTextPrefab;

    private void Start(){
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        foreach (GameObject feather in feathers){
            feather.SetActive(false);
        }
        feathers[0].SetActive(true);
        barFillPermanent.GetComponent<Image>().color = featherColors[0];
    }

    private void Update(){
        if (tubeContact){
            AddFeatherScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D c){
        if (c.gameObject.tag == "LightTube"){
            tubeContact = true;
            anim.SetBool("TouchingTube", true);
        }
    }

    private void OnTriggerExit2D(Collider2D c){
        if (c.gameObject.tag == "LightTube"){
            tubeContact = false;
            featherPoints += (int)pointsToAdd;
            if (pointsToAdd > 0.49f){
                GameObject popUpText = Instantiate(popUpTextPrefab, transform.position + new Vector3(2, 0, 0), transform.rotation);
                popUpText.GetComponentInChildren<TextMeshPro>().text = pointsToAdd.ToString("0");
            }
            pointsToAdd = 0;
            anim.SetBool("TouchingTube", false);
            barFillPermanent.sizeDelta = new Vector2(featherPoints * 0.49f, 24);
        }
    }

    public void AddFeatherScore(){
        pointsToAdd += Time.deltaTime * 10f;
        scoreText.text = (pointsToAdd + featherPoints).ToString("0") + "/200";
        barFillTemporary.sizeDelta = new Vector2((pointsToAdd + featherPoints) * 0.49f, 24);
        if (checkpoint == 0 && pointsToAdd + featherPoints > 50){
            checkpoint = 1;
            feathers[0].SetActive(false);
            feathers[1].SetActive(true);
            barFillPermanent.GetComponent<Image>().color = featherColors[1];
        } else if (checkpoint == 1 && pointsToAdd + featherPoints > 100){
            checkpoint = 2;
            feathers[1].SetActive(false);
            feathers[2].SetActive(true);
            barFillPermanent.GetComponent<Image>().color = featherColors[2];
        } else if (checkpoint == 2 && pointsToAdd + featherPoints > 150){
            checkpoint = 3;
            feathers[2].SetActive(false);
            feathers[3].SetActive(true);
            barFillPermanent.GetComponent<Image>().color = featherColors[3];
        }
    }
}
