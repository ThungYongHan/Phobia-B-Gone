using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpiderCollision : MonoBehaviour
{
    /*public MeshCollider spiderCollider;
    public MeshCollider spiderBlockerCollider;*/
    public CapsuleCollider spiderCollider;
    public CapsuleCollider spiderBlockerCollider;
    // Start is called before the first frame update
    void Awake()
    {
        Physics.IgnoreCollision(spiderCollider, spiderBlockerCollider, true);
    }

}
