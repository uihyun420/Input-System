using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;  
public class FilteredReferences : MonoBehaviour
{  
    public AssetReferenceGameObject leftObject;
    public AssetReferenceGameObject rightObject;

    public AssetReferenceT<Material> spawnMaterial;
    public AssetReferenceT<Material> midMaterial;
    public AssetReferenceT<Material> lateMaterial;

    public Vector3 leftPosition;
    public Vector3 rightPosition;

    private MeshRenderer m_LeftMeshRender;
    private MeshRenderer m_RightMeshRender;

    private void Start()
    {
        leftObject.LoadAssetAsync().ToUniTask().Forget();
        rightObject.LoadAssetAsync().ToUniTask().Forget();
        spawnMaterial.LoadAssetAsync().ToUniTask().Forget();
        midMaterial.LoadAssetAsync().ToUniTask().Forget();
        lateMaterial.LoadAssetAsync().ToUniTask().Forget();
    }

    private void OnDisable()
    {
        leftObject.ReleaseAsset();
        rightObject.ReleaseAsset();
        spawnMaterial.ReleaseAsset();
        midMaterial.ReleaseAsset();
        lateMaterial.ReleaseAsset();
    }


    int m_FrameCounter = 0;
    //Note that we never actually wait for the loads to complete.  We just check if they are done (if the asset exists)
    //before proceeding.  This is often not going to be the best practice, but has some benefits in certain scenarios.
    void FixedUpdate()
    {
        m_FrameCounter++;
        if (m_FrameCounter == 20)
        {
            if (leftObject.Asset != null)
            {
                var leftGo = Instantiate(leftObject.Asset, leftPosition, Quaternion.identity) as GameObject;
                m_LeftMeshRender = leftGo.GetComponent<MeshRenderer>();
            }

            if (rightObject.Asset != null)
            {
                var rightGo = Instantiate(rightObject.Asset, rightPosition, Quaternion.identity) as GameObject;
                m_RightMeshRender = rightGo.GetComponent<MeshRenderer>();
            }

            if (spawnMaterial.Asset != null && m_LeftMeshRender != null && m_RightMeshRender != null)
            {
                m_LeftMeshRender.material = spawnMaterial.Asset as Material;
                m_RightMeshRender.material = spawnMaterial.Asset as Material;
            }
        }

        if (m_FrameCounter == 40)
        {
            if (midMaterial.Asset != null && m_LeftMeshRender != null && m_RightMeshRender != null)
            {
                m_LeftMeshRender.material = midMaterial.Asset as Material;
                m_RightMeshRender.material = midMaterial.Asset as Material;
            }
        }

        if (m_FrameCounter == 60)
        {
            m_FrameCounter = 0;
            if (lateMaterial.Asset != null && m_LeftMeshRender != null && m_RightMeshRender != null)
            {
                m_LeftMeshRender.material = lateMaterial.Asset as Material;
                m_RightMeshRender.material = lateMaterial.Asset as Material;
            }
        }
    }
}

