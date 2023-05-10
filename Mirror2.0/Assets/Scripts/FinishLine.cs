using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    public int countPlayer = 0;
    public void Finish(GameObject hit)
    {
        countPlayer++;
        //Destroy(hit.collider.gameObject);
        //hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<AudioManager>().Play("PlayerWin");

        Debug.Log("Win player count " + countPlayer);
        if (countPlayer >= 6)
        {
            //Debug.Log("Win player count " + countPlayer);
            StartCoroutine(FinishSequence());
        }
    }

    IEnumerator FinishSequence()
    {
        int count = SceneManager.sceneCountInBuildSettings;
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
    }
}
