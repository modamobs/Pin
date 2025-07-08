using UnityEngine;

// Effect 클래스는 오브젝트가 커졌다가 작아지는 이펙트를 구현합니다.
public class Effect : MonoBehaviour
{
    [SerializeField]
    private float effectDuration = 0.5f; // 이펙트 전체 지속 시간
    [SerializeField]
    private float maxScale = 1.5f;       // 최대 스케일 배율
    private float timer = 0f;
    private Vector3 originalScale;
    private bool isPlaying = false;

    void Start()
    {
        originalScale = transform.localScale;
        PlayEffect(); // 시작하자마자 이펙트 실행 (원하면 제거 가능)
    }

    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            float halfDuration = effectDuration / 2f;
            if (timer < halfDuration)
            {
                // 커지는 구간
                float t = timer / halfDuration;
                transform.localScale = Vector3.Lerp(originalScale, originalScale * maxScale, t);
            }
            else if (timer < effectDuration)
            {
                // 작아지는 구간
                float t = (timer - halfDuration) / halfDuration;
                transform.localScale = Vector3.Lerp(originalScale * maxScale, originalScale, t);
            }
            else
            {
                // 이펙트 종료
                transform.localScale = originalScale;
                isPlaying = false;
            }
        }
    }

    // 외부에서 호출해도 이펙트 재생 가능
    public void PlayEffect()
    {
        timer = 0f;
        isPlaying = true;
    }
}
