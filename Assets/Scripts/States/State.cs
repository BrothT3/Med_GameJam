using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class State : MonoBehaviour
{
    public abstract void Init();

    public abstract void Execute();

    public abstract void End();

}

