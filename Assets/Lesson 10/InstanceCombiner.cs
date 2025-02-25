using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[System.Serializable]
public class InstanceCombiner : MonoBehaviour
{
    
    [SerializeField] private List<MeshFilter> listMeshFilter = new List<MeshFilter>();

    [SerializeField] private MeshFilter targetMesh;

    [ContextMenu("Combine Meshes")]
    private void CombineMesh()
    {
        if (listMeshFilter.Count == 0 || targetMesh == null)
        {
            Debug.LogWarning("Need to add MeshFilters to list and add TargetMesh!");
            return;
        }

        CombineInstance[] combine = new CombineInstance[listMeshFilter.Count];

        for (int i = 0; i < listMeshFilter.Count; i++)
        {
            if (listMeshFilter[i] == null) continue;

            combine[i].mesh = listMeshFilter[i].sharedMesh;
            combine[i].transform = listMeshFilter[i].transform.localToWorldMatrix;
            listMeshFilter[i].gameObject.SetActive(false);
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        targetMesh.sharedMesh = mesh;
        targetMesh.gameObject.SetActive(true);
    }
}
