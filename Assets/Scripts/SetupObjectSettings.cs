using UnityEngine;

public class SetupObjectSettings : MonoBehaviour
{
    public static SetupObjectSettings _instance;
    public static SetupObjectSettings Instance { get; }

    public void Awake()
    {
        _instance = this;
    }

    public void setBoxColliders(GameObject gameObject)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mesh in meshes)
        {
            mesh.gameObject.AddComponent<MeshCollider>();
            print($" mesh {mesh}");
        }

    }


}
