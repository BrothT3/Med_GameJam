using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class EnterCheckPointState : State
{
    public GameObject checkScreen;
    public override void End()
    {
        checkScreen.SetActive(false);
    }

    public override void Execute()
    {
        Vector3 Pos = GameManager.Instance.Player.transform.position;
        if (GameManager.Instance.FadeOutComplete)
        {

            Pos = new Vector3(Pos.x + 0.09f, Pos.y, Pos.z);
            GameManager.Instance.Player.transform.position = Pos;
        }
        if (Camera.main.WorldToScreenPoint(Pos).x > Screen.width + 100)
        {
            GameManager.Instance.blackScreenAnim.SetTrigger("FadeOut");
            if (GameManager.Instance.FadeOutComplete )
            {
                if (GameManager.Instance.Level < 2)
                    GameManager.Instance.ChangeState(GameManager.Instance.flyingState);
                else
                    GameManager.Instance.ChangeState(GameManager.Instance.FinaleState);
            }
        }
    }

    public override void Init()
    {
        if (checkScreen == null)
            checkScreen = Instantiate(GameManager.Instance.CheckPointScreen);
        else
            checkScreen.SetActive(true);
        GameManager.Instance.Player.transform.position = new Vector3(-10.50f, 0, 0);
        GameManager.Instance.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.blackScreenAnim.ResetTrigger("FadeOut");
        GameManager.Instance.blackScreenAnim.SetTrigger("FadeIn");
    }
}

