using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{


    Vector3 target_dir = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void Update()
    {

        ////カメラ視点移動 （角度）
        //if (Input.GetKeyDown(KeyCode.W))// W を押したとき
        //{
        //    target_dir = new Vector3(0, 0, 0);
        //}

        //if (Input.GetKeyDown(KeyCode.S))// S を押したとき
        //{
        //    target_dir = new Vector3(0, 0, -1);
        //}

        //if (Input.GetKeyDown(KeyCode.A))// A を押したとき
        //{
        //    target_dir = new Vector3(-1, 0, 0);   
        //}

        //if (Input.GetKeyDown(KeyCode.D))// D を押したとき
        //{
        //    target_dir = new Vector3(1, 0, 0);  
        //}
        
        //Quaternion rotation = Quaternion.LookRotation(target_dir);//　X Y Z の値を持ってくる
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);

    }
}
