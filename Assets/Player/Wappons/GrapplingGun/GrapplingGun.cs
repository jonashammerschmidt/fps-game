using UnityEngine;

public class GrapplingGun : MonoBehaviour
{

    public GameObject tipInGun;
    public GameObject tipPrefab;
    public Transform cameraTransform;

    private GameObject tipInWorld;
    private bool mouseDown = false;

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

    private void MouseDown()
    {
        tipInGun.SetActive(false);

        RaycastHit hit;
        bool isHitted = Physics.Raycast(cameraTransform.position, cameraTransform.rotation * Vector3.forward, out hit, 1000);
        if (isHitted)
        {
            tipInWorld = GameObject.Instantiate(tipPrefab);
            tipInWorld.transform.position = hit.point;
            tipInWorld.transform.rotation = cameraTransform.rotation;
        }
    }

    private void MouseUp()
    {
        tipInGun.SetActive(true);
        GameObject.Destroy(tipInWorld);
    }
}
