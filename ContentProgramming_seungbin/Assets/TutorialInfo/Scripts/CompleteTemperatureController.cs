using UnityEngine;

public class CompleteTemperatureController : MonoBehaviour
{
    [Header("온도 설정")]
    public float temperature = 25.0f;     // 온도
    public float maxHeight = 3.0f;        // 최대 높이
    
    [Header("디버깅")]
    public bool showDebugInfo = true;     // 디버그 정보 표시
    
    private Renderer objectRenderer;       // Renderer 컴포넌트
    private float nextDebugTime = 0f;      // 다음 디버그 출력 시간
    
    void Start()
    {
        // Renderer 컴포넌트 가져오기
        objectRenderer = GetComponent<Renderer>();
        
        if (objectRenderer == null)
        {
            Debug.LogError("이 GameObject에는 Renderer가 없습니다!");
        }
        
        Debug.Log("온도계 시작! 초기 온도: " + temperature + "도");
    }
    
    void Update()
    {
        float height = temperature / 50.0f * maxHeight;
        if (height < 0.1f) height = 0.1f;

        transform.localScale = new Vector3(1, height, 1);

        // 바닥 기준을 -2.5로 고정
        float baseY = -2.5f;
        transform.position = new Vector3(
            transform.position.x,
            baseY + height / 2f,
            transform.position.z
        );

        // 2. 색상 제어 (온도 구간별)
        if (objectRenderer != null)
        {
            if (temperature < 15.0f)
            {
                objectRenderer.material.color = Color.blue;   // 추움
            }
            else if (temperature >= 15.0f && temperature < 30.0f)
            {
                objectRenderer.material.color = Color.green;  // 적당
            }
            else
            {
                objectRenderer.material.color = Color.red;    // 더움
            }
        }
        
        // 3. 디버그 정보 (1초마다)
        if (showDebugInfo && Time.time >= nextDebugTime)
        {
            Debug.Log("[" + gameObject.name + "] 온도: " + temperature + "도, 높이: " + height.ToString("F2"));
            nextDebugTime = Time.time + 1.0f;
        }
    }
}
