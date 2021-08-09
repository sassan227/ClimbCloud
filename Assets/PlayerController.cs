using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene���g�����߂ɕK�v�I�I

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2d;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 90.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //�W�����v����
        if (Input.GetMouseButtonDown(0) &&
            this.rigid2d.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2d.AddForce(transform.up * this.jumpForce);
        }

        //���E�ړ�
        int key = 0;
        if (Input.acceleration.x > this.threshold) key = 1;
        if (Input.acceleration.x < -this.threshold) key = -1;

        //�v���C���̑��x
        float speedx = Mathf.Abs(this.rigid2d.velocity.x);

        //�X�s�[�h����
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2d.AddForce(transform.right * this.walkForce * key);
        }

        // ���������ɉ����Ĕ��]
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //�v���C���̑��x�ɉ����ăA�j���[�V�������x��ς���
        if(this.rigid2d.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        // ��ʊO�ɏo���ꍇ�͍ŏ�����
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }


    // �S�[���ɓ��B
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�S�[��");
        SceneManager.LoadScene("ClearScene");
    }
}
