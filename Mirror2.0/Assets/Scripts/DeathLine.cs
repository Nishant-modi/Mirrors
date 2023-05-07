using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject player1;
    public GameObject player2;
    public GameObject cam;
    public float shakeDuration;
    public float shakeMagnitude;

    public Animator upAnim;
    public Animator downAnim;
    Vector3 sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = startPoint.transform.position;
    }

    public void Death()
    {
        StartCoroutine(DeathSequence());
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));

        //Debug.Log("lalalalala");
    }

    IEnumerator DeathSequence()
    {
        upAnim.SetBool("IsDead", true);
        downAnim.SetBool("IsDead", true);
        player1.GetComponent<Player>().enabled = false;
        player2.GetComponent<Player>().enabled = false;
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        yield return new WaitForSeconds(0.5f);
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        player1.transform.position = new Vector3(sp.x, 2f, 0f);
        player2.transform.position = new Vector3(sp.x, -2f, 0f);
        player1.GetComponent<Player>().enabled = true;
        player2.GetComponent<Player>().enabled = true;
        upAnim.SetBool("IsDead", false);
        downAnim.SetBool("IsDead", false);
        player2.gameObject.SetActive(true);
        player1.gameObject.SetActive(true);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = cam.transform.position;

        float elapsed = 0f;

        while(elapsed<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.position = new Vector3(x + originalPos.x, originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        } 
        
        cam.transform.position = originalPos;
    }
}
