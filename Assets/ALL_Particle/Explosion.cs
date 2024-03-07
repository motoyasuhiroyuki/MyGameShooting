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

        //時間経過で消滅
        time -= Time.deltaTime;

        if (time < 0f)
        {
            Destroy(gameObject);
        }




    }
}
