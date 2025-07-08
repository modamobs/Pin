using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PinLauncher 클래스는 핀 오브젝트를 발사하는 기능을 담당합니다.
// 마우스 클릭으로 핀을 발사하고, 새로운 핀을 준비하는 역할을 합니다.
public class PinLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject pinObject; // 발사할 핀의 프리팹

    private Pin currPin; // 현재 준비된 핀 오브젝트

    // Start는 게임 시작 시 한 번 호출됩니다.
    void Start()
    {
        PreparePin(); // 첫 번째 핀 준비
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭, 현재 핀이 존재, 게임 오버가 아닐 때 핀 발사
        if (Input.GetMouseButtonDown(0) && currPin != null && GameManager.instance.isGameOver == false)
        //if (Input.GetMouseButtonDown(0) && currPin != null)
        {
            currPin.Launch(); // 현재 핀 발사
            currPin = null;   // 현재 핀 참조 제거
            Invoke("PreparePin", 0.1f); // 0.1초 후 새로운 핀 준비
        }
    }

    // 새로운 핀을 생성하고 준비하는 함수
    void PreparePin()
    {
        // 핀 프리팹을 현재 위치에 생성
        GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
        // 생성된 핀의 Pin 컴포넌트를 가져와서 현재 핀으로 설정
        currPin = pin.GetComponent<Pin>();
    }
}
