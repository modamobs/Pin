using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1.0f;



    public float minSpeed = 50f;   // �ּ� ȸ�� �ӵ�
    public float maxSpeed = 200f;  // �ִ� ȸ�� �ӵ�

    public float minChangeTime = 1f;  // ȸ�� ����/�ӵ� ���� �ּ� �ð�
    public float maxChangeTime = 3f;  // ȸ�� ����/�ӵ� ���� �ִ� �ð�

    private float currentSpeed;
    private Vector3 rotationAxis;
    private float timer;

    // Update is called once per frame


    void Start()
    {
        SetRandomRotation();
    }
    void Update()
    {
        if (GameManager.instance.isGameOver == false)
        {
            transform.Rotate(0, 0, currentSpeed * Time.deltaTime);
        }

        

        // �ð� ī��Ʈ
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomRotation();
        }

    }

    void SetRandomRotation()
    {
        // ���� ȸ�� ���� (3D ����, 2D��� Z�ุ ��� ����)
        rotationAxis = Random.onUnitSphere;  // 2D�� ���: new Vector3(0, 0, 1)

        // ���� �ӵ� (���� ���� ����)
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        if (Random.value < 0.5f)
            currentSpeed *= -1f;

        // ���� ������� ��ٸ� �ð�
        timer = Random.Range(minChangeTime, maxChangeTime);
    }
}



