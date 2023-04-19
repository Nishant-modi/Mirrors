using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    TrailRenderer trailRenderer;
    public Gradient jumpGradient;
    public Gradient groundGradient;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            trailRenderer.colorGradient = jumpGradient;
        }
        else
        {
            trailRenderer.colorGradient = groundGradient;
        }
    }
}
