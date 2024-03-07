using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubUnitt : MonoBehaviour
{
    
    bool a=false;
    public GameObject targetObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            targetObj.GetComponent<UI>().SubUnittdamage();
            a = false;
        }
     
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            a = true;
        }

    }

}
