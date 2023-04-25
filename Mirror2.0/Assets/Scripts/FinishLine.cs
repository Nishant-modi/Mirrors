using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    int countPlayer = 0;
    public void Finish(GameObject hit)
    {
        countPlayer++;
        //Destroy(hit.collider.gameObject);
        //hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Win player count " + countPlayer);
        if (countPlayer >= 6)
        {
            //Debug.Log("Win player count " + countPlayer);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
