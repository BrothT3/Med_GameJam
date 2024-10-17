using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FlyingState : State
{

    GameObject flyingScreen;

    public override void End()
    {
        flyingScreen.SetActive(false);
        GameManager.Instance.TM.DestroyTubes();
        GameManager.Instance.Level++;
        flyingScreen.SetActive(false);
    }

    public override void Execute()
    {
        if (GameManager.Instance.FadeInComplete)
            GameManager.Instance.TM.MoveObstacle();
    }

    public override void Init()
    {
        GameManager.Instance.blackScreenAnim.ResetTrigger("FadeOut");

        if (GameManager.Instance.Player == null)
            GameManager.Instance.Player = Instantiate(GameManager.Instance.PlayerPrefab);
        else
        {
            GameManager.Instance.Player.transform.position = new Vector3(-6, 0, 1);
            GameManager.Instance.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;


        }
        if (flyingScreen == null)
            flyingScreen = Instantiate(GameManager.Instance.FlyingScreen);
        else
            flyingScreen.SetActive(true);

        GameManager.Instance.TM.AdjustLevel(GameManager.Instance.Level);
        GameManager.Instance.TM.GenerateTubes();
        GameManager.Instance.TM.SelectObstacle();
    }
}

