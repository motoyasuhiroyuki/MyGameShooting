using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAnime : MonoBehaviour
{
    // �A�j���[�^�[
    private Animator m_Animator = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����A�X�y�[�X�L�[�������ꂽ��Ȃ�
        if (Input.GetKey(KeyCode.Space)|| Input.GetKey("joystick button 2"))
        {
            m_Animator.SetTrigger("CannonFlag");
        }

    }
}
