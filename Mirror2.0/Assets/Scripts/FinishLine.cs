using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    int countPlayer = 0;
    public void Finish()
    {
        countPlayer++;
        //Destroy(other.gameObject);
        //other.gameObject.SetActive(false);
        if (countPlayer == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
