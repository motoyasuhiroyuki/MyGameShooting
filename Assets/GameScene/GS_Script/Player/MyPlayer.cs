using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading;

public class MyPlayer : MonoBehaviour
{
    //�J�����I�u�W�F�N�g
    public GameObject mainCamera;

    // ���𒲐� (Adjust) ���̐��Ȃ�v���C���[�̑O�� (��l�̎��_)�A
    // ���̐��Ȃ�v���C���[�̌��ɔz�u����         (�O�l�̎��_)�A
    public float xAdjust =  0.0f;
    public float yAdjust =  1.0f;
    public float zAdjust = -8.0f;

    /*public*/ float maxAngleX =  0.0f; // �ő��]�p�xX
    /*public*/ float minAngleX =  0.0f; // �ŏ���]�p�xX
    /*public*/ float speedAngleX = 0.0f; // ��]�X�s�[�hX

    /*public*/ float maxAngleZ =  10.0f;   // �ő��]�p�xZ
    /*public*/ float minAngleZ = -10.0f;   // �ŏ���]�p�xZ
    /*public*/ float speedAngleZ =   0.6f; // ��]�X�s�[�hZ


    int   rotaKeep = 0;       // �t���O���o�����邽�߂̕ϐ�
    bool  rotaFlag = false;   // ��]�t���O
    float rotaTimer = 0.0f;   // ��]�^�C�}�[
    float rotaSpeed = 360.0f; // ��]���x
    float moveSpeed = 40.0f;   // �ړ����x
   
    //x�������̓��͂�ۑ�
    private float _input_x;
    //z�������̓��͂�ۑ�
    private float _input_z;


    private NormalBullet NB;

    int  accelerationFlag = 0;
    float accelerationTimer = 0.0f;

    public static bool isPause = false;
    public float moveSpeedZ = 100f;


    //�_�Ŋ֘A
    [SerializeField] 
    private Renderer playerhide1; //�_�ŗp
    [SerializeField]
    private Renderer playerhide2; //�_�ŗp
    [SerializeField]
    private Renderer playerhide3; //�_�ŗp
    [SerializeField]
    private Renderer playerhide4; //�_�ŗp
    [SerializeField]
    private Renderer playerhide5; //�_�ŗp
    [SerializeField]
    private Renderer playerhide6; //�_�ŗp
    [SerializeField]
    private Renderer playerhide7; //�_�ŗp

    float intervalhideTime = 0.0f;
    float hideTime = 0.2f;
    float hideCount = 0;
    bool hideFlag = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

  


    // Update is called once per frame
    void Update()
    {

        

        if (hideFlag)
        {
            intervalhideTime += Time.deltaTime;
            if(intervalhideTime > 0.1f) { //�_�ŊJ�n
                hideTime += Time.deltaTime;
                if(hideTime >= 0.1)
                {
                    hideTime = 0.0f;
                    hideCount+=1;
                    playerhide1.enabled = !playerhide1.enabled;
                    playerhide2.enabled = !playerhide2.enabled;
                    playerhide3.enabled = !playerhide3.enabled;
                    playerhide4.enabled = !playerhide4.enabled;
                    playerhide5.enabled = !playerhide5.enabled;
                    playerhide6.enabled = !playerhide6.enabled;
                    playerhide7.enabled = !playerhide7.enabled;
                }
                if (hideCount == 8f)
                {
                    hideFlag = false;
                    intervalhideTime = 0;
                    hideTime = 0.0f;
                    hideCount = 0f;
                }
                
            }


        }


        Rota(); // ���[�e�[�V���� �֐�

        Move(); // �|�W�V���� �֐�

    }
   
    void Move()// �ړ�����
    {

        //L Stick
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");

        //R Stick
        float rsh = Input.GetAxis("R_Stick_H");
        float rsv = Input.GetAxis("R_Stick_V");





        if (Input.GetKey(KeyCode.UpArrow) || lsv > 0)
        {
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || lsv < 0)
        {
            transform.position += new Vector3(0,-moveSpeed, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || lsh > 0 )// ��
        {
            transform.position += new Vector3(-moveSpeed , 0,0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || lsh < 0 )// �E
        {
            transform.position += new Vector3(moveSpeed , 0,0) * Time.deltaTime;
        }

        if (accelerationFlag == 0)
        {
            moveSpeedZ = 100f;
            transform.position += new Vector3(0, 0, moveSpeedZ) * Time.deltaTime;
        }
        //�����t���O

        if (accelerationFlag == 1)
        {

            if (moveSpeedZ <= 160.0f)
            {
                moveSpeedZ += 0.5f;//����
                transform.position += new Vector3(0, 0, moveSpeedZ) * Time.deltaTime;
            }
            else
            {
                accelerationFlag = 2;
            }
        }

        if (accelerationFlag == 2)
        {
           
            if (moveSpeedZ >= 80.0f)
            {
                moveSpeedZ -= 0.5f;
                transform.position += new Vector3(0, 0, moveSpeedZ) * Time.deltaTime;
            }
            if (moveSpeedZ <= 80f)
            {
                accelerationFlag = 0;
            }

        }


        //�ړ��͈͐���

        if (transform.position.y >= 250)//��
        {
            transform.position += new Vector3(0, -moveSpeed, 0) * Time.deltaTime;
        }
        if (transform.position.y <= -100)//��
        {
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }
        

        //���W��������S�[��

        if (transform.position.z >= 1800)
        {
            SceneManager.LoadScene("Result");
        }



        mainCamera.transform.position = new Vector3(transform.position.x+xAdjust,transform.position.y+yAdjust,transform.position.z+zAdjust);




    }

    void Rota() // ��]����
    {

        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown("joystick button 2"))// �X�y�[�X�L�[���������Ƃ�
        {
            rotaKeep += 1;// �l������

        }

        if (rotaKeep > 0)// ���̒l�ȏ�ɂȂ�����
        {
            rotaFlag = true;// �t���O������
        }

        if (rotaFlag)// �t���O����������
        {
            rotaTimer += Time.deltaTime;// �b���𐔂���


            if (rotaTimer < 0.5)// �����b�܂ŉ�]������
            {
                transform.Rotate(new Vector3(0, 0, rotaSpeed*2) * Time.deltaTime);
                moveSpeed = 120f;
            }
            else
            {
                // �����b�𒴂����珉��������
                moveSpeed = 40f;
                rotaKeep = 0;
                rotaTimer = 0;
                rotaFlag = false;
            }
        }

       
        if (!rotaFlag)// �t���O�������Ă��Ȃ����
        {
        //�[�[�[�[�[�[�[�㉺�X���[�[�[�[�[�[�[�[�[�[�[
        // ���͏��
        float turnX = Input.GetAxis("Vertical");
        // ���݂̉�]�p�x��0�`360����-180�`180�ɕϊ�
        float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        // ���݂̉�]�p�x�ɓ���(turn)������������]�p�x��Mathf.Clamp()���g��minAngle����MaxAngle���Ɏ��܂�悤�ɂ���
        float angleX = Mathf.Clamp(rotateX + -turnX * speedAngleX, minAngleX, maxAngleX);

        // ��]�p�x��-180�`180����0�`360�ɕϊ�
        angleX = (angleX < 0) ? angleX + 360 : angleX;


        //�[�[�[�[�[�[�[���E�X���[�[�[�[�[�[�[�[�[�[�[
        // ���͏��
        float turnZ = Input.GetAxis("Horizontal");
        // ���݂̉�]�p�x��0�`360����-180�`180�ɕϊ�
        float rotateZ = (transform.eulerAngles.z > 180) ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
        // ���݂̉�]�p�x�ɓ���(turn)������������]�p�x��Mathf.Clamp()���g��minAngle����MaxAngle���Ɏ��܂�悤�ɂ���
        float angleZ = Mathf.Clamp(rotateZ + -turnZ * speedAngleZ, minAngleZ, maxAngleZ);

        // ��]�p�x��-180�`180����0�`360�ɕϊ�
        angleZ = (angleZ < 0) ? angleZ + 360 : angleZ;
            //// ��]�p�x���I�u�W�F�N�g�ɓK�p
            //transform.rotation = Quaternion.Euler(0, 0, angleZ);


            // �p�x�������l�ɖ߂�

            // 0.2�͗V�ђl
            if (rotateX <= -0.2f)
            {
            transform.eulerAngles += new Vector3(80.0f, 0.0f, 0.0f) * Time.deltaTime;
            }
            else if (rotateX >= 0.2f)
            {
            transform.eulerAngles -= new Vector3(80.0f, 0.0f, 0.0f) * Time.deltaTime;
            }

            // 0.2�͗V�ђl
            if (rotateZ <= -0.2f)
            {
                transform.eulerAngles += new Vector3(0.0f, 0.0f, 80.0f) * Time.deltaTime;
            }
            else if (rotateZ >= 0.2f)
            {
                transform.eulerAngles -= new Vector3(0.0f, 0.0f, 80.0f) * Time.deltaTime;
            }
        }


       
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "acceleItem")
        {
            accelerationFlag = 1;
        }


        if (other.gameObject.tag == "acceleItem" || other.gameObject.tag == "Item"|| 
            other.gameObject.tag == "Bullet")
        {
        }
        else if(!hideFlag)
        {
            hideFlag = true;
        }

    }

    

}


