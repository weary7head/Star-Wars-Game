using UnityEngine;

public class RayShooter
{
    private Camera _camera;
    private float _distance;

    public RayShooter(Camera camera, float distance)
    {
        _camera = camera;
        _distance = distance;
    }

    public Vector3 GetTargetPoint()
    {
        Vector3 cameraPoint = new Vector3(_camera.pixelWidth / 2f, _camera.pixelHeight / 2f);
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(cameraPoint);
        if (Physics.Raycast(ray, out hit, _distance))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
