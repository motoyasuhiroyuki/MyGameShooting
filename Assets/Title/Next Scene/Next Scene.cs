using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public float AnimeCount = 130.0f;
    float AnimeCountTimer = 0.0f;
    bool AnimeCountFlag = false;
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
            AnimeCountTimer += 0.1f;
        }

        Vector3 Pos = transform.position;


        if (Pos.z>=300)
        {
            //SceneBをロード。現在のシーンは自動的に削除されて、シーンBだけになる
            SceneManager.LoadScene("GameScene");
        }

        

    }

    
}
