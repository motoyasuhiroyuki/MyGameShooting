using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

    public bool AnimeCountFlag ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 2"))
        {
            AnimeCountFlag = true;

        }


        if (AnimeCountFlag)
        {
            //SceneBをロード。現在のシーンは自動的に削除されて、シーンBだけになる
            SceneManager.LoadScene("Title");
        }

    }
}
