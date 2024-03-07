using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy3way : MonoBehaviour
{
    float timer = 0;

    public Transform player;
    GameObject[] target; //�@�W�I�̍��W���擾
    public float moveSpeed = 60.0f;
    float enemyHP_ = 10.0f;
    [SerializeField, Min(0)]
    float time = 10;
    [SerializeField]
    float lifeTime = 10;// �e�����ԂŊǗ�
    bool attackFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        target = GameObject.FindGameObjectsWithTag("Player"); // �^�O���� transform ���擾


        foreach (GameObject player in target)
        {

            if (player.transform.position.z + 1500 > transform.position.z)
            {
                attackFlag = true;
            }
            // �v���C���[�ɋ߂Â�����ǔ����~
            if (player.transform.position.z + 300 > transform.position.z)
            {
                attackFlag = false;
            }

            if (player.transform.position.z < gameObject.transform.position.z)
            {
                if (attackFlag)
                {
                    transform.LookAt(player.transform);
                    GetComponent<Rigidbody>().velocity = transform.forward.normalized * moveSpeed;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //���Ԍo�߂ŏ���
        time -= Time.deltaTime;

        if (time < 0f)
        {
            Destroy(gameObject);
            return;
        }   
    }
}