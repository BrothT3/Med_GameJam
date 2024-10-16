using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FlyingState : State
{
    public override void End()
    {

    }

    public override void Execute()
    {
        GameManager.Instance.TM.MoveObstacle();
    }

    public override void Init()
    {
        Instantiate(GameManager.Instance.Player);
        GameManager.Instance.TM.GenerateTubes();
        GameManager.Instance.TM.SelectObstacle();
    }
}

