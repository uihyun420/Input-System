using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;  
using System.Collections.Generic;

public class ListOfReference : MonoBehaviour
{
    public List<AssetReference> shapes;

    private bool isReady = false;

    private int currentIndex = 0;

    private async UniTaskVoid Start()
    {
        await LoadAllShapesAsync();
    }

    private void OnDestroy()
    {
        foreach(var shape in shapes)
        {
            shape.ReleaseAsset();   
        }
    }

    private async UniTask LoadAllShapesAsync()
    {
        var loadTasks = new List<UniTask<GameObject>>(); 

        foreach (var shape in shapes)
        {
            var task = shape.LoadAssetAsync<GameObject>().ToUniTask();
            loadTasks.Add(task);
        }

        await UniTask.WhenAll(loadTasks);
        isReady = true;
    }

    public void SpawnThing()
    {
        if (isReady && shapes[currentIndex].Asset != null)
        {
            Instantiate(shapes[currentIndex].Asset);
            currentIndex = (currentIndex + 1) % shapes.Count; // 0 1 0 1 순환되도록 한 것임
        }
    }
}
