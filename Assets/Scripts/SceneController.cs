using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ARPlaneManager))]
public class SceneController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _togglePlanesAction;

    [SerializeField]
    private InputActionReference _leftActivateAction;

    [SerializeField]
    private InputActionReference _rightActivateAction;

    [SerializeField]
    private XRRayInteractor _leftRayInteractor;

    [SerializeField]
    private GameObject _grabbableCube;

    [SerializeField]
    private GameObject _prefab;

    private ARPlaneManager _planeManager;
    private ARAnchorManager _anchorManager;
    private bool _isVisible = true;
    private int _numPlanesAddedOccured = 0;

    private List<ARAnchor> _anchors = new();

    void Start()
    {
        Debug.Log("-> SceneController::Start()");

        _planeManager = GetComponent<ARPlaneManager>();

        if (_planeManager is null) {
            Debug.LogError("-> Can't find 'ARPlaneManager' :( ");
        }

        _anchorManager = GetComponent<ARAnchorManager>();

        if (_anchorManager is null)
        {
            Debug.LogError("-> Can't find 'ARAnchorManager' :(");
        }

        _togglePlanesAction.action.performed += OnTogglePlanesAction;
        _planeManager.planesChanged += OnPlanesChanged;
        _anchorManager.anchorsChanged += OnAnchorsChanged;
        _leftActivateAction.action.performed += OnLeftActivateAction;
        _rightActivateAction.action.performed += OnRightActivateAction;
    }

    private void OnAnchorsChanged(ARAnchorsChangedEventArgs obj)
    {
        foreach (var removedAnchor in obj.removed)
        {
            _anchors.Remove(removedAnchor);
            Destroy(removedAnchor.gameObject);
        }
    }

    private void OnLeftActivateAction(InputAction.CallbackContext obj)
    {
        CheckIfRayHitsCollider();
    }

    private void CheckIfRayHitsCollider()
    {
        if (_leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Debug.Log("-> Hit detected! :) - name: " + hit.transform.name);
            Quaternion rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
            GameObject instance = Instantiate(_prefab, hit.point, rotation);

            if (instance.GetComponent<ARAnchor>() == null)
            {
                ARAnchor anchor = instance.AddComponent<ARAnchor>();

                if (anchor != null)
                {
                    Debug.Log("-> CreateAnchoredObject() - anchor added! :(");
                    _anchors.Add(anchor);
                }
                else
                {
                    Debug.LogError("-> CreateAnchoredObject() - anchor is null! :(");
                }
            }
        }
        else
        {
            Debug.Log("-> No hit detected! :(");
        }
    }

    private void OnRightActivateAction(InputAction.CallbackContext obj)
    {
        SpawnGrabbableCube();
    }

    private void SpawnGrabbableCube()
    {
        Debug.Log("-> SceneController::SpawnGrabbableCube()");

        Vector3 spawnPosition;

        foreach (var plane in _planeManager.trackables) 
        {
            if (plane.classification == PlaneClassification.Table)
            {
                spawnPosition = plane.transform.position;
                spawnPosition.y += 0.3f;
                Instantiate(_grabbableCube, spawnPosition, Quaternion.identity);
            }
        }
    }

    private void OnTogglePlanesAction(InputAction.CallbackContext obj)
    {
        _isVisible = !_isVisible;
        float fillAlpha = _isVisible ? 0.3f : 0f;
        float lineAlpha = _isVisible ? 1.0f : 0f;

        Debug.Log("-> OnTogglePlanesAction() - trackables.count: " + _planeManager.trackables.count);

        foreach (var plane in _planeManager.trackables)
        {
            SetPlaneAlpha(plane, fillAlpha, lineAlpha);
        }
    }

    private void SetPlaneAlpha(ARPlane plane, float fillAlpha, float lineAlpha)
    {
        var meshRenderer = plane.GetComponentInChildren<MeshRenderer>();
        var lineRenderer = plane.GetComponentInChildren<LineRenderer>();

        if (meshRenderer != null)
        {
            Color color = meshRenderer.material.color;
            color.a = fillAlpha;
            meshRenderer.material.color = color;
        }

        if (lineRenderer != null)
        {
            Color startColor = lineRenderer.startColor;
            Color endColor = lineRenderer.endColor;

            startColor.a = lineAlpha;
            endColor.a = lineAlpha; 

            lineRenderer.startColor = startColor;
            lineRenderer.endColor = endColor;
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args) 
    {
        if (args.added.Count > 0)
        {
            _numPlanesAddedOccured++;

            foreach (var plane in _planeManager.trackables)
            {
                PrintPlaneLabel(plane);
            }

            Debug.Log("-> Number of planes: " + _planeManager.trackables.count);
            Debug.Log("-> num Planes Added Occurred: " + _numPlanesAddedOccured);
        }
    }

    private void PrintPlaneLabel(ARPlane plane)
    {
        string label = plane.classification.ToString();
        string log = $"Plane ID: {plane.trackableId}, Label: {label}";
        Debug.Log(log);
    }

    void OnDestroy()
    {
        Debug.Log("-> SceneController::OnDestroy()");
        _togglePlanesAction.action.performed -= OnTogglePlanesAction;
        _planeManager.planesChanged -= OnPlanesChanged;
        _anchorManager.anchorsChanged -= OnAnchorsChanged;
        _leftActivateAction.action.performed -= OnLeftActivateAction;
        _rightActivateAction.action.performed -= OnRightActivateAction;
    }
}
