using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SetBullet : MonoBehaviour
{


   
    public Transform setPlayer; // ���@�̃��[���h���W
    public GameObject myBullet;

    public Vector3 setPos;

    public float interval = 0.1f; // ���b�Ԋu�Ō���
    private float timer = 0.0f; // ���ԃJ�E���g�p�̃^�C�}�[

    
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {



        transform.position = new Vector3 (setPlayer.position.x + setPos.x, 
            setPlayer.position.y + setPos.y, setPlayer.position.z + setPos.z);
        

        if (Input.GetKey(KeyCode.Z) && timer <= 0.0f && Time.timeScale == 1.0f)// Z ����������
        {
           
            //�e�𐶐����@���W���v���C���[�ɍ��킹��
            Instantiate(myBullet, new Vector3(transform.position.x,
                transform.position.y, transform.position.z+2), Quaternion.identity);

            timer = interval; // �Ԋu���Z�b�g
        }
        //�@R�{�^�����T�@L�{�^�����S
        if ( timer <= 0.0f && Input.GetKey("joystick button 5"))// Z ����������
        {

            //�e�𐶐����@���W���v���C���[�ɍ��킹��
            Instantiate(myBullet, new Vector3(transform.position.x,
                transform.position.y, transform.position.z + 2), Quaternion.identity);

            timer = interval; // �Ԋu���Z�b�g
        }


        // �^�C�}�[�̒l�����炷
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }

    }
   
}
