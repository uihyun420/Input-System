using UnityEngine;
using UnityEngine.AddressableAssets;
public class SelfDestruct : MonoBehaviour
{
    public float lifetime = 2f;

    private void Start()
    {        
        Destroy(gameObject, lifetime); 
    }

    private void OnDestroy()
    {
        Addressables.ReleaseInstance(gameObject); // 어드레서블 인스턴스 해제 쌍으로 맞춰서 사용해야함
    }
}
