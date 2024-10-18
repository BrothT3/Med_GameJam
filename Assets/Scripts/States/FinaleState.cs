using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinaleState : State
{
    GameObject finaleScreen;
    private bool loadScene;

    public override void End()
    {

    }

    public override void Execute()
    {
        Vector3 Pos = GameManager.Instance.Player.transform.position;
        if (finaleScreen.GetComponent<FinalePoint>().Tree.transform.position.x - Pos.x < 1f)
        {
            Debug.Log(Pos.x);
            GameManager.Instance.Player.GetComponent<LightCollision>().anim.SetTrigger("SitOnTree");
            GameManager.Instance.Player.transform.position = new Vector3(Pos.x, -4.49f, 0);
            if (!loadScene){
                loadScene = true;
                if (!PlayerPrefs.HasKey("Highscore")){
                    PlayerPrefs.SetInt("Highscore", GameManager.Instance.combinedScore);
                } else if (GameManager.Instance.combinedScore > PlayerPrefs.GetInt("Highscore")){
                    PlayerPrefs.SetInt("Highscore", GameManager.Instance.combinedScore);
                }
                StartCoroutine(LoadCutscene());
            }
        }
        else if (GameManager.Instance.FadeOutComplete)
        {
            var posMovement = 4f * Time.deltaTime;
            Pos = new Vector3(Pos.x + posMovement, Pos.y, Pos.z);
            GameManager.Instance.Player.transform.position = Pos;
        }
    }

    public override void Init()
    {
        if (finaleScreen == null)
            finaleScreen = Instantiate(GameManager.Instance.FinaleScreen);
        else
            finaleScreen.SetActive(true);
        GameManager.Instance.Player.transform.position = new Vector3(-10.50f, -2.1f, 0);
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.blackScreenAnim.ResetTrigger("FadeOut");
        GameManager.Instance.blackScreenAnim.SetTrigger("FadeIn");
        GameObject k = Instantiate(GameManager.Instance.kite);
        k.transform.position = new Vector2(5, -3.5f);
        k.GetComponent<Kite>().PlaceFeathers();
    }

    private IEnumerator LoadCutscene(){
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("EndCutscenes");
    }
}

