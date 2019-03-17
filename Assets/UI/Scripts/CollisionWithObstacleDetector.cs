using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObstacleDetector : MonoBehaviour {

    public ParticleSystem collisionEffect;

    [Range(0,1)]
    public float headDamages;

    [Range(0, 1)]
    public float bodyDamages;

    [Range(0, 1)]
    public float memberDamages;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="CharacterHead")
        {
            RunAndJumpGameManager.Instance.Health -= headDamages;
        }

        else if (collision.gameObject.tag == "CharacterBody")
        {
            RunAndJumpGameManager.Instance.Health -= bodyDamages;
        }

        else if (collision.gameObject.tag == "CharacterMember")
        {
            RunAndJumpGameManager.Instance.Health -= memberDamages;
        }

        else
        {
            return;
        }

        //CollisionEffect
        collisionEffect.transform.position = collision.contacts[0].point;
        collisionEffect.Play();
    }
}
