using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player1;

    private void Start()
    {
        GameObject player = Instantiate(player1);
        player.SetActive(true);
        player.transform.position = this.transform.position;
    }
}
