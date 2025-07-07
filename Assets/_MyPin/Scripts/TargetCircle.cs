using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1.0f;



    public float minSpeed = 50f;   // 최소 회전 속도
    public float maxSpeed = 200f;  // 최대 회전 속도

    public float minChangeTime = 1f;  // 회전 방향/속도 변경 최소 시간
    public float maxChangeTime = 3f;  // 회전 방향/속도 변경 최대 시간

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

        

        // 시간 카운트
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomRotation();
        }

    }

    void SetRandomRotation()
    {
        // 랜덤 회전 방향 (3D 기준, 2D라면 Z축만 사용 가능)
        rotationAxis = Random.onUnitSphere;  // 2D의 경우: new Vector3(0, 0, 1)

        // 랜덤 속도 (음수 포함 가능)
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        if (Random.value < 0.5f)
            currentSpeed *= -1f;

        // 다음 변경까지 기다릴 시간
        timer = Random.Range(minChangeTime, maxChangeTime);
    }
}



