using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 6;
    public int exp, expmax, level;

    private void Start()
    {
        exp = 0;
        expmax = 10;
        level = 1;
    }


}
