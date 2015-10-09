using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraDistance;
    public float cameraAngle = 0f;

    private Vector3 dest;
    private Vector3 cameraOffset;

    void Awake()
    {
    }

    void Start()
    {
        target = PlayerEntity.instance.transform;
        dest = target.position;
        cameraDistance = Vector3.Distance(transform.position, target.transform.position);
        cameraOffset.y = -Mathf.Sin(Mathf.Deg2Rad * cameraAngle) * cameraDistance;
        cameraOffset.z = -Mathf.Cos(Mathf.Deg2Rad * cameraAngle) * cameraDistance;
        UpdateAngle();
    }

    void Update()
    {
        Vector2 myPos = transform.position;
        Vector3 targetPos = target.transform.position;

        float distance2D = Vector2.Distance(myPos, targetPos);

        dest = Vector3.Lerp(
            dest,
            targetPos,
            1f / (distance2D + 1f / Constants.CAMERA_SPEED) + Constants.CAMERA_SPEED
        );

        transform.position = dest + cameraOffset;
        UpdateAngle();
    }

    public void UpdateAngle()
    {
        cameraOffset.y = -Mathf.Sin(Mathf.Deg2Rad * cameraAngle) * cameraDistance;
        cameraOffset.z = -Mathf.Cos(Mathf.Deg2Rad * cameraAngle) * cameraDistance;
        transform.rotation = Quaternion.Euler(-cameraAngle, 0f, 0f);

        Entity[] entities = GameObject.FindObjectsOfType<Entity>();
        foreach (Entity entity in entities)
        {
            entity.transform.rotation = Quaternion.Euler(-cameraAngle, 0f, 0f);
        }
    }
}
