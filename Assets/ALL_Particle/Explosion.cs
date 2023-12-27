using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Min(0)]
    float time = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //éûä‘åoâﬂÇ≈è¡ñ≈
        time -= Time.deltaTime;

        if (time < 0f)
        {
            Destroy(gameObject);
        }




    }
}
