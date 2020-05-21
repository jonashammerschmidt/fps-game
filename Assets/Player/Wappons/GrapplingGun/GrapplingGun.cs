using UnityEngine;

public class GrapplingGun : MonoBehaviour
{

    public Rigidbody playerRigidbody;

    public GameObject tipInGun;
    public GameObject tipPrefab;
    public GameObject linePrefab;
    public Transform cameraTransform;

    public float gunPullForce = 50f;
    public float breakingDistance = 2f;

    private GameObject tipInWorld;
    private GameObject lineInstance;
    private LineRenderer lineRenderer;
    private bool mouseDown = false;
    private Vector3 backTipPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !mouseDown)
        {
            MouseDown();
            mouseDown = true;
        }

        if (Input.GetMouseButtonUp(0) && mouseDown)
        {
            MouseUp();
            mouseDown = false;
        }
    }

    void FixedUpdate()
    {
        if (mouseDown && lineRenderer != null)
        {
            if ((backTipPosition - tipInGun.transform.position).magnitude < breakingDistance)
            {
                MouseUp();
                return;
            }
            var forceDirection = (backTipPosition - tipInGun.transform.position);
            if (forceDirection.y > 0)
            {
                playerRigidbody.AddForce(forceDirection.normalized * gunPullForce * 3 / 4);
                playerRigidbody.AddForce(Vector3.up * gunPullForce / 4);
            }
            else
            {
                playerRigidbody.AddForce(forceDirection.normalized * gunPullForce);
            }
        }
    }

    void LateUpdate()
    {
        if (mouseDown && lineRenderer != null)
        {
            lineRenderer.SetPosition(0, tipInGun.transform.position);
        }
    }

    private void MouseDown()
    {
        RaycastHit hit;
        bool isHitted = Physics.Raycast(cameraTransform.position, cameraTransform.rotation * Vector3.forward, out hit, 1000);
        if (isHitted)
        {
            tipInWorld = Instantiate(tipPrefab);
            tipInWorld.transform.position = hit.point;
            tipInWorld.transform.rotation = cameraTransform.rotation;

            backTipPosition = tipInWorld.transform.Find("BackTip").transform.position;

            lineInstance = Instantiate(linePrefab);
            lineRenderer = lineInstance.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, tipInGun.transform.position);
            lineRenderer.SetPosition(1, backTipPosition);
        }

        tipInGun.SetActive(false);
    }

    private void MouseUp()
    {
        Destroy(lineInstance);
        Destroy(tipInWorld);
        lineRenderer = null;
        lineInstance = null;
        tipInWorld = null;

        tipInGun.SetActive(true);
    }

    void OnDestroy()
    {
        MouseUp();
    }
}
