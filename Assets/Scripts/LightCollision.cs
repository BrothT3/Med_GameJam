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
    public Animator anim;
    [SerializeField] private int checkpoint;
    [SerializeField] private GameObject popUpTextPrefab;

    private void Start(){
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        foreach (GameObject feather in GameManager.Instance.feathers){
            feather.SetActive(false);
        }
        GameManager.Instance.feathers[0].SetActive(true);
        GameManager.Instance.barFillPermanent.GetComponent<Image>().color = GameManager.Instance.featherColors[0];
    }

    private void Update(){
        if (tubeContact){
            AddFeatherScore();
        }

        if (GameManager.Instance.barFillPermanent.sizeDelta.x != featherPoints * 0.49f){
            GameManager.Instance.barFillPermanent.sizeDelta = new Vector2(Mathf.Lerp(GameManager.Instance.barFillPermanent.sizeDelta.x, featherPoints * 0.49f, 5f * Time.deltaTime), 24);
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
                popUpText.GetComponentInChildren<TextMeshPro>().text = "+" + pointsToAdd.ToString("0");
            }
            pointsToAdd = 0;
            anim.SetBool("TouchingTube", false);
        }
    }

    public void AddFeatherScore(){
        pointsToAdd += Time.deltaTime * 10f;
        GameManager.Instance.scoreText.text = (pointsToAdd + featherPoints).ToString("0") + "/200";
        GameManager.Instance.barFillTemporary.sizeDelta = new Vector2((pointsToAdd + featherPoints) * 0.49f, 24);
        if (checkpoint == 0 && pointsToAdd + featherPoints >= 50){
            checkpoint = 1;
            GameManager.Instance.feathers[0].SetActive(false);
            GameManager.Instance.feathers[1].SetActive(true);
            GameManager.Instance.barFillPermanent.GetComponent<Image>().color = GameManager.Instance.featherColors[1];
            GameManager.Instance.TriggerShake();
        } else if (checkpoint == 1 && pointsToAdd + featherPoints >= 100){
            checkpoint = 2;
            GameManager.Instance.feathers[1].SetActive(false);
            GameManager.Instance.feathers[2].SetActive(true);
            GameManager.Instance.barFillPermanent.GetComponent<Image>().color = GameManager.Instance.featherColors[2];
            GameManager.Instance.TriggerShake();
        } else if (checkpoint == 2 && pointsToAdd + featherPoints >= 150){
            checkpoint = 3;
            GameManager.Instance.feathers[2].SetActive(false);
            GameManager.Instance.feathers[3].SetActive(true);
            GameManager.Instance.barFillPermanent.GetComponent<Image>().color = GameManager.Instance.featherColors[3];
            GameManager.Instance.TriggerShake();
        }
    }
}
