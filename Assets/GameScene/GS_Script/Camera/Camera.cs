using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject player;

    Vector3 target_dir = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void Update()
    {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y+8f, player.transform.position.z-18f);

      
    }
}
