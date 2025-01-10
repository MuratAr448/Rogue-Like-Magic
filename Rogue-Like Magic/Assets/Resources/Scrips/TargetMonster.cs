using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TargetMonster : MonoBehaviour
{
    public GameObject target;
    public GameObject targetImage;
    private GameObject temp;
    [SerializeField] private UnityEngine.Transform targetTransform;
    public void SetTarget()
    {
        if (target != null)
        {
            targetTransform = target.GetComponentInChildren<RectTransform>();
            temp = Instantiate(targetImage, target.transform.position, Quaternion.identity, targetTransform);
        }
    }
    private void Update()
    {
        if (target == null)
        {
            Destroy(temp);
        }
    }
}
