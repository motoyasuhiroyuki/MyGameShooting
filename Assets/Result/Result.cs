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
        if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 3"))
        {
            AnimeCountFlag = true;

        }

        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif


        }


        if (AnimeCountFlag)
        {
            //SceneB�����[�h�B���݂̃V�[���͎����I�ɍ폜����āA�V�[��B�����ɂȂ�
            SceneManager.LoadScene("Title");
        }

    }
}
