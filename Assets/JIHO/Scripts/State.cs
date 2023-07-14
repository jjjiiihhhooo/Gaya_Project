using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T>
{
    public virtual void StateEnter(T Controller)
    {

    }

    public virtual void StateChange(T Controller)
    {

    }

    public virtual void StateExit(T Controller)
    {

    }

    public virtual void StateUpdate(T Controller)
    {

    }
}
