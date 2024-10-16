using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FlyingState : State
{
    public override void End()
    {
        GameManager.Instance.Level++;
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
            
        Instantiate(GameManager.Instance.FlyingScreen);
        GameManager.Instance.TM.AdjustLevel(GameManager.Instance.Level);
        GameManager.Instance.TM.GenerateTubes();
        GameManager.Instance.TM.SelectObstacle();
    }
}

