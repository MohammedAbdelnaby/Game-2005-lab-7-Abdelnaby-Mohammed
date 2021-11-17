using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab7PhysicsSystem : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    public List<Lab7PhysicsObjects> lab7Physics = new List<Lab7PhysicsObjects>();

    public List<PhysicsCollider> ColliderShapes;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < lab7Physics.Count; i++)
        {
            lab7Physics[i].velocity += gravity * Time.fixedDeltaTime;
        }

        CollisionUpdate();
    }
    void CollisionUpdate()
    {
        for (int i = 0; i < lab7Physics.Count; i++)
        {
            for (int j = i + 1; j < lab7Physics.Count; j++)
            {
                Lab7PhysicsObjects ObjectA = lab7Physics[i];
                Lab7PhysicsObjects ObjectB = lab7Physics[j];

                Vector3 ObjectAPosition = ObjectA.transform.position;
                Vector3 ObjectBPosition = ObjectB.transform.position;

                if (ObjectA.shape == null || ObjectB.shape == null)
                {
                    continue;
                }

                if (ObjectA.shape.GetCollistionShape() == CollistionShape.Sphere
                    && ObjectB.shape.GetCollistionShape() == CollistionShape.Sphere)
                {
                    float distance = Mathf.Sqrt(Mathf.Pow(ObjectAPosition.x - ObjectBPosition.x, 2) +
                                                Mathf.Pow(ObjectAPosition.y - ObjectBPosition.y, 2) +
                                                Mathf.Pow(ObjectAPosition.z - ObjectBPosition.z, 2));
                    if ((distance) <= (((PhysicsColliderSphere)ColliderShapes[i]).getRaduis() + ((PhysicsColliderSphere)ColliderShapes[j]).getRaduis()))
                    {
                        ObjectA.velocity = Vector3.zero;
                        ObjectB.velocity = Vector3.zero;
                        Debug.Log(ObjectA.name + " and " + ObjectB.name + " collided");
                    }
                }
            }
        }
    }
}
