using DG.Tweening;
using Environment;
using UnityEngine;

namespace Ability.Abilities
{
    [CreateAssetMenu]
    public class GrapplingAbility : Ability
    {
        public float reachedDistance = 2f;
        public float hookSpeed = 10f;
        public LineRenderer line;
        public override void Activate(GameObject parent)
        {
            if (line ==  null)
            {
                line = parent.GetComponent<LineRenderer>();
            }
            base.Activate(parent);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
            {
                if (hit.collider.gameObject.GetComponent<HookableObject>())
                {
                    Vector3 hookTargetPosition = hit.point;
                    hookTargetPosition += Vector3.up * 1f;
                    line.SetPosition(1,hookTargetPosition);
                    line.SetPosition(0,parent.transform.position);
                    float distance = Vector3.Distance(parent.transform.position, hookTargetPosition);
                   
                    parent.transform.DOMove(hookTargetPosition, distance / hookSpeed)
                        .SetEase(Ease.OutBounce).OnComplete(() =>
                        {
                            line.SetPosition(0,parent.transform.position);
                            line.SetPosition(1,parent.transform.position);
                            
                        });



                }
            }
        }

        public override void BeginCooldown(GameObject parent)
        {
          
        }
    }
}