using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patricle_Start_Stop : MonoBehaviour
{

    public GameObject Main;
    public GameObject Main2;
    public GameObject Sub;
    public GameObject Sub2;

    // アニメーター
    public Animator m_Animator = null;

    // Start is called before the first frame update
    void Start()
    {
        Main = GameObject.Find("Main");
        Main.SetActive(false);

        Main2 = GameObject.Find("Main2");
        Main2.SetActive(false);

        Sub = GameObject.Find("Sub");
        Sub.SetActive(false);

        Sub2 = GameObject.Find("Sub2");
        Sub2.SetActive(false);

        m_Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //もし、スペースキーが押されたらなら
        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 2") )
        {
            m_Animator.SetTrigger("StartFlag");
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 2")) 
        {
            Main.SetActive(true);
            Main2.SetActive(true);
            Sub.SetActive(true);
            Sub2.SetActive(true);
        }
    }

    
}
