using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Special
{
    public abstract void Excute();
}

public class RocketSpecial : Special
{
    public override void Excute()
    {
        //init 2 rocket
        Debug.Log("RocketSpecial");
    }
}

public class BombSpecial : Special
{
    public override void Excute()
    {
        Debug.Log("BombSpecial");
        BoardManager.Instance.RemoveRandomCouple();
    }
}

public class LightningSpecial : Special
{
    public override void Excute()
    {
        Debug.Log("LightningSpecial");
        for (int i = 0; i < 2; i++)
        {
            BoardManager.Instance.RemoveRandomCouple();
        }
    }
}
