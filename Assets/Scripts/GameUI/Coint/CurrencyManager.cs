using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    int coint;

    public void AddCoint (int cointAdd)
    {
        coint += cointAdd;
    }

    public void SubtractCoint(int cointSubtract)
    {
        if (cointSubtract > coint)
        {
            //TO DO
        }
        else
        {
            coint -= cointSubtract;
        }
    }
}
