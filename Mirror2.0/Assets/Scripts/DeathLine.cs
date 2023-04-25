using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject player1;
    public GameObject player2;
    Vector3 sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = startPoint.transform.position;
    }

    public void Death()
    {
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        player1.transform.position = new Vector3(sp.x, 2f, 0f);  
        player2.transform.position = new Vector3(sp.x, -2f, 0f);

        player2.gameObject.SetActive(true);
        player1.gameObject.SetActive(true);
    }
}
