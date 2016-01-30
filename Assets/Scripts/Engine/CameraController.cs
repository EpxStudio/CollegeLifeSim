using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraDistance;
    public float cameraAngle = 0f;

	public bool isShaking = false;
	public float shakingIntensity = 1f;

	public bool isCutscene = false;
	public Vector3 cutsceneDestOffset = Vector3.zero;
	public Vector3 cutsceneCurOffset = Vector3.zero;
	public float cutsceneTimer = 0f;
	public float cutsceneAnimTime = 0f;

	public bool isFading = false;
	public GameObject fadeGameObj;
	public Color fadeDestColor = Color.white;
	public float fadeAlpha = 0f;
	public float fadeTimer = 0f;
	public float fadeAnimTime = 0f;

	private Vector3 dest = Vector3.zero;
	private Vector3 cameraOffset = Vector3.zero;

    void Awake()
    {
		fadeGameObj = transform.Find("Fade").gameObject;

		//StartCutsceneOffset(Vector3.down * 1f, 1f); 
		//StartFade(new Color(1f, 0.1f, 0.1f, 0.7f), 3f);
		//StartShake(0.08f);
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

	public void StartFade(Color fadeColor, float fadeAnimTime)
	{
		isFading = true;
		fadeGameObj.SetActive(true);
		fadeGameObj.GetComponent<SpriteRenderer>().color = fadeColor;
		fadeDestColor = fadeColor;
		fadeAlpha = 0f;
		this.fadeAnimTime = fadeAnimTime;
	}

	public void StartShake(float intensity)
	{
		isShaking = true;
		shakingIntensity = intensity;
	}

	public void StartCutsceneOffset(Vector3 newDest, float cutsceneAnimTime)
	{
		isCutscene = true;
		cutsceneDestOffset = newDest;
		cutsceneCurOffset = new Vector3(0f, 0f, 0f);
		this.cutsceneAnimTime = cutsceneAnimTime;
	}

	public void StopFade() { isFading = false; fadeGameObj.SetActive(false); }
	public void StopShake() { isShaking = false; }
	public void StopCutsceneOffset() { isCutscene = false; }

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

		if (isFading)
		{
			fadeAlpha = Mathf.Lerp(0f, fadeDestColor.a, fadeTimer / fadeAnimTime);
			Color newColor = fadeGameObj.GetComponent<SpriteRenderer>().color;
			newColor.a = fadeAlpha;
			fadeGameObj.GetComponent<SpriteRenderer>().color = newColor;
			fadeTimer += Time.deltaTime;
		}

		Vector3 newPos = dest + cameraOffset;

		if (isShaking)
			newPos += Auxs.RandomVector3() * shakingIntensity;

		if (isCutscene)
		{
			cutsceneCurOffset = Vector3.Lerp(Vector3.zero, cutsceneDestOffset, cutsceneTimer / cutsceneAnimTime);
			// match camera angle
			cutsceneCurOffset.y *= Mathf.Cos(Mathf.Deg2Rad * cameraAngle);
			newPos += cutsceneCurOffset;

			cutsceneTimer += Time.deltaTime;
		}

		transform.position = newPos;

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
