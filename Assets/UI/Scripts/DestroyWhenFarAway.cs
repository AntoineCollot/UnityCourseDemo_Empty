using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenFarAway : MonoBehaviour {

    public float destroyDistanceFromPlayer;
    Transform characterT;

    private void Start()
    {
        characterT = RunAndJumpGameManager.Instance.character.transform;

        StartCoroutine(CheckIfShouldDestroy());
    }

    IEnumerator CheckIfShouldDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (Vector3.Distance(characterT.position, transform.position) > destroyDistanceFromPlayer)
                Destroy(gameObject);
        }
    }
}
