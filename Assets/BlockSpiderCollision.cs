using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpiderCollision : MonoBehaviour
{
    public MeshCollider spiderCollider;
    public MeshCollider spiderBlockerCollider;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(spiderCollider, spiderBlockerCollider, true);
    }

}
