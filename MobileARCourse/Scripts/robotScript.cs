using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotScript : MonoBehaviour
{

    [SerializeField] Transform theHead;
    [SerializeField] float popDistance;
    [SerializeField] float popResetTime;
    [SerializeField] AudioClip headPopSound;
    [SerializeField] AudioClip chatterSound;
    [SerializeField] AudioSource headAudio;

    Rigidbody botRB;
    private void OnEnable()
    {
        moveLoc = this.transform.position;
        botRB = gameObject.GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
    }

    IEnumerator delay3(float x)
    {
        yield return new WaitForSeconds(x);
        isHighlighted = false;
    }

    IEnumerator delay2(float x)
    {
        yield return new WaitForSeconds(x);
        headIsPopped = false;
    }

    IEnumerator delay(float x)
    {
        yield return new WaitForSeconds(x);
        headVector.y = headBase;
        theHead.localPosition = headVector;
        StartCoroutine(delay2(0.5f));
    }

    bool headIsPopped = false;
    Vector3 headVector = new Vector3(0, 0.5125f, 0);
    float headBase = 0.5125f;
    public void PopHead()
    {
        if (headIsPopped) return;
        headIsPopped = true;

        headVector.y = headBase + popDistance;
        theHead.localPosition = headVector;
        headAudio.PlayOneShot(headPopSound);

        StartCoroutine(delay(2.0f));
    }

    bool isHighlighted = false;
    public void Highlight()
    {
        if (isHighlighted) return;
        isHighlighted = true;
        headAudio.PlayOneShot(chatterSound);
        StartCoroutine(delay3(3.0f));
    }

    public bool isPopped()
    {
        return headIsPopped;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<BallPrefab>() != null) PopHead();
        if (c.gameObject.name == "DeathPlane")
        {
            moveTo(moveLoc);
        }
    }

    Vector3 moveLoc;

    public void moveTo(Vector3 v)
    {
        moveLoc = v;
        botRB.isKinematic = true;
        botRB.velocity = Vector3.zero;
        botRB.angularVelocity = Vector3.zero;
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        gameObject.transform.position = v;
        gameObject.transform.rotation = Quaternion.identity;
        botRB.isKinematic = false;
    }
}
