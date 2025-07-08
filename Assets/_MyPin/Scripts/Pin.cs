using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Pin 클래스는 핀 오브젝트의 이동, 충돌, 발사 상태를 관리합니다.
public class Pin : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f; // 핀이 위로 이동하는 속도

    private bool IsPinned = false;      // 핀이 타겟에 고정되었는지 여부
    private bool IsLaunchered = false;  // 핀이 발사되었는지 여부

    public GameObject effectPrefab; // 핀 파괴 시 생성할 이펙트 프리팹

    // FixedUpdate는 일정한 시간 간격으로 호출됩니다. (물리 연산에 적합)
    private void FixedUpdate()
    {
        // 핀이 아직 고정되지 않았고, 발사된 상태라면 위로 이동
        if (IsPinned == false && IsLaunchered == true)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    // 다른 오브젝트와 충돌 시 호출
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetCircle")
        {
            IsPinned = true; // 타겟 원과 충돌 시에만 고정
            IsLaunchered = false; // 타겟에 고정되면 더 이상 발사 상태가 아님
            // 타겟 원과 충돌 시: 자식 오브젝트(Square) 활성화, 부모를 타겟 원으로 설정, 목표 감소
            // 자식 오브젝트(Square)를 찾아서 SpriteRenderer를 활성화(핀의 사각형 부분 표시)
            GameObject childObject = transform.Find("Square").gameObject;
            childObject.GetComponent<SpriteRenderer>().enabled = true;
            // 핀을 타겟 원의 자식으로 설정 (함께 회전하도록)
            transform.SetParent(collision.gameObject.transform);
            // 목표 핀 개수 감소
            GameManager.instance.DecreaseGoal();
        }
        else if (collision.gameObject.tag == "Pin")
        {
            Debug.Log($"[Pin] 충돌: this.IsLaunchered={IsLaunchered}, other.IsLaunchered={collision.gameObject.GetComponent<Pin>().IsLaunchered}");
            // 발사된 핀(자신)만 이펙트 및 파괴 처리, 기존 핀의 IsLaunchered가 false일 때만
            if (IsLaunchered && !collision.gameObject.GetComponent<Pin>().IsLaunchered)
            {
                Debug.Log("[Pin] 이팩트 생성 및 게임오버 처리");
                GameObject effect = Instantiate(effectPrefab, collision.transform.position, Quaternion.identity);
                Destroy(effect, 1f);
                Destroy(gameObject);
                GameManager.instance.SetGameOver(false);
            }
        }
    }

    // 핀을 발사 상태로 전환하는 함수 (PinLauncher에서 호출)
    public void Launch()
    {
        IsLaunchered = true;
    }
}
