using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPerspectiveController : MonoBehaviour
{
    public bool trolleyRightSideCamera, trolleyLeftSideCamera;

    [SerializeField] float rotationSpeed = 5.0f;

    private float xOffset = -1.0f;
    private float zOffset = -2.5f;
    private float rotOffsetY = 10.0f;

    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineTransposer transposer;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraTrolleySidePerspective();
    }

    public void CameraTrolleySidePerspective()
    {
        if (trolleyRightSideCamera)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -rotOffsetY, transform.localRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Mathf.SmoothStep(0.0f, 1.0f, rotationSpeed * Time.deltaTime));

            Vector3 targetPosition = new Vector3(-xOffset, transposer.m_FollowOffset.y, zOffset);
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, rotationSpeed * Time.deltaTime));
        }
        else if(trolleyLeftSideCamera)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, rotOffsetY, transform.localRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Mathf.SmoothStep(0.0f, 1.0f, rotationSpeed * Time.deltaTime));

            Vector3 targetPosition = new Vector3(xOffset, transposer.m_FollowOffset.y, zOffset);
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, rotationSpeed * Time.deltaTime));
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0.0f, transform.localRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Mathf.SmoothStep(0.0f, 1.0f, rotationSpeed * Time.deltaTime));

            Vector3 targetPosition = new Vector3(0.0f, 6.0f, -3.0f);
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, rotationSpeed * Time.deltaTime));
        }
    }
}
