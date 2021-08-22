using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private Rigidbody ballRigidBody;

    public void ShootBall()
    {
        GameObject sceneObject = Instantiate<GameObject>(ballPrefab);
        sceneObject.transform.position = transform.position;
        ballRigidBody = sceneObject.GetComponent<Rigidbody>();
        ballRigidBody.AddForce(transform.forward * 5000); //5000 some constant value to multiply force

        /* Also, Vector3.forward is a global direction(Vector3 is used to represent Position and Direction in WorldSpace). It's a direction that all GameObjects can agree on, despite their orientation or position in the hierarchy. It ALWAYS points in that direction. It's a constant value.
        Transform.forward as an individual GameObjects forward direction, relative to its orientation. If you turn the GameObject, then the Transform.forward returned is DIFFERENT from Vector3.forward.
        That's why the source code multiplies the rotation (orientation) by Vector3.forward. Of course that value is going to be different!
        */

    }

 
}
