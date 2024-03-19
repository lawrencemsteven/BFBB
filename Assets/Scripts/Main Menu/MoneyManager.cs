using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static int m_money = 0;

    public static int getMoney()
    {
        return m_money;
    }

    public static void addMoney(int amount)
    {
        m_money += amount;
        m_money = Math.Max(m_money, 0);
    }
}
