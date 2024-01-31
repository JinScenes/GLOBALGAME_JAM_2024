using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario64ColliderHandle : MonoBehaviour
{
    public GameObject enemyPlayer;
    public GameObject mario64TPObj;
    // Start is called before the first frame update
    void Start()
    {
        mario64TPObj = GameObject.Find("Mario64TPObj");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.gameObject == enemyPlayer && enabled)
        {
            print("Touching enemy");
            enemyPlayer.transform.position = mario64TPObj.transform.position;
            AudioManager.instance.PlayAudios("Mario 64 Painting");
            AudioManager.instance.PlayAudios("Mario WAHHHH");
        }
    }
}
