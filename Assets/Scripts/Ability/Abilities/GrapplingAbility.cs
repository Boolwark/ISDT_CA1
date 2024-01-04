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
        public float radius;
        public LayerMask whatIsGrappable;
        public LineRenderer line;
        public override void Activate(GameObject parent)
        {
            if (line ==  null)
            {
                line = parent.GetComponent<LineRenderer>();
            }
            Debug.Log("Activating grapple ability");
            base.Activate(parent);
            Collider[] possibleGrapples =
                Physics.OverlapSphere(Camera.main.transform.position, radius, whatIsGrappable);
            float minAngle = float.PositiveInfinity;
            HookableObject target = null;
            foreach (var possible in possibleGrapples)
            {
                Debug.Log("Possible:"+possible.transform.name);
                if (possible.TryGetComponent(out HookableObject hookableObject))
                {
                    var angle = Vector3.Angle(Camera.main.transform.position,possible.transform.position);
                    if (angle < minAngle)
                    {
                        minAngle = angle;
                        target = hookableObject;

                    } 
                }
            }


            if (target == null)
            {
                Debug.Log("No targets to grapple.");
                return;
            }
            Vector3 hookTargetPosition = target.transform.position;
                    float distance = Vector3.Distance(parent.transform.position, hookTargetPosition);
                   
                    hookTargetPosition += Vector3.up * 1f;
                    line.SetPosition(1,hookTargetPosition);
                    line.SetPosition(0,parent.transform.position);

                    if (!target.MovesTowardsPlayer)
                    { 
                        // The player moves towards the object
                       
                       
                     
                        parent.transform.DOMove(hookTargetPosition, distance / hookSpeed)
                            .SetEase(Ease.OutBounce).OnComplete(() =>
                            {
                                line.SetPosition(0,parent.transform.position);
                                line.SetPosition(1,parent.transform.position);
                         
                            
                            });
                    }
                    else
                    {
                        target.transform.DOMove(parent.transform.position + reachedDistance * parent.transform.forward, distance / hookSpeed)
                            .SetEase(Ease.OutBounce).OnComplete(() =>
                            {
                                line.SetPosition(0,parent.transform.position);
                                line.SetPosition(1,parent.transform.position);
                            });
                    }
               



                
        
            
        }

        public override void BeginCooldown(GameObject parent)
        {
          
        }
    }
}