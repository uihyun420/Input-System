using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
public class BasicReference : MonoBehaviour
{
    public AssetReference baseCube; // 직접 참조 처럼 연결할 수 있다. 그치만 레퍼런스이기 때문에 정보만 들고있음.
    [SerializeField] private Button instantiateButton;
    private void Start()
    {
        instantiateButton.onClick.AddListener(SpawnThing);
    }

    public void SpawnThing()
    {
        baseCube.InstantiateAsync().ToUniTask().Forget(); // InstantiateAsync 생성을 하는데 메모리에 할당이 안되어있으면 로드부터 비동기로 동작
    }

    
}
