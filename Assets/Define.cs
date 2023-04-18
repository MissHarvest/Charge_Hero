using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene { Home, Game, Unknown }

    public enum UIEvent
    {
        Click,
        Drag,
        Press,
    }
    public enum E_Status
    {
        HP,
        ATK,
        SHIELD,
    }
    public enum Effects
    {
        None,
        Avitation,
        Invincibility,
    }
}
