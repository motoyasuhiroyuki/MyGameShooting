using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Image titleUI;
    public Image frameUI;
    public Image startUI;
    public Image ruleUI;
    public Image operatorUI;
    
    int textFlag_ = 0;
    bool animeFlag_ = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

       if(textFlag_ > 2) { 
        textFlag_ = 2;
        }
       if(textFlag_ < 0) { 
        textFlag_ = 0;
        }

        Debug.Log(textFlag_);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            textFlag_ -= 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            textFlag_ += 1;
        }

        //ゲームを始める
        if (textFlag_ == 0)
        {
            transform.position = new Vector3(startUI.transform.position.x, startUI.transform.position.y, startUI.transform.position.z);
           if(Input.GetKeyDown(KeyCode.Space))
            {
                titleUI.enabled = false;
                startUI.enabled = false;
                ruleUI.enabled = false;
                operatorUI.enabled = false;
                frameUI.enabled = false;
            }
        }
        //ルール説明
        if (textFlag_ == 1)
        {
            transform.position = new Vector3(ruleUI.transform.position.x, ruleUI.transform.position.y, ruleUI.transform.position.z);
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
        //操作説明
        if (textFlag_ == 2)
        {
            transform.position = new Vector3(operatorUI.transform.position.x, operatorUI.transform.position.y, operatorUI.transform.position.z);
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
                Application.Quit();//ゲームプレイ終了
#endif
        }
    }

}
