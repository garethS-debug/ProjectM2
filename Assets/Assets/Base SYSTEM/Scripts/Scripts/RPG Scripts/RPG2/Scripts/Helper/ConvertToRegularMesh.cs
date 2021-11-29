using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToRegularMesh : MonoBehaviour {

    [ContextMenu ("Convert to Regular Mesh")] // adds item to drop down menu in inspector
void Convert ()
    {

        SkinnedMeshRenderer skinnedMeshRender = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter= gameObject.AddComponent<MeshFilter>();

        meshFilter.sharedMesh = skinnedMeshRender.sharedMesh;
        meshRenderer.sharedMaterials = skinnedMeshRender.sharedMaterials;

        //replacing the skiined mesh render with the mesh render and the mesh filter

        DestroyImmediate(skinnedMeshRender);
        DestroyImmediate(this); // destroy script
    }
}
