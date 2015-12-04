using UnityEngine;
using System.Collections;

public class moveTrail : MonoBehaviour {

    public int bulletSpeed = 230;

	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        Destroy(gameObject, 1);
	}
}
