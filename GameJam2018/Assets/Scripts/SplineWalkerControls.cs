using UnityEngine;

public class SplineWalkerControls : MonoBehaviour {

	public BezierSpline spline;

	public bool lookForward;

	public SplineWalkerMode mode;
    public GasType GasType;

	public float progress;
	private bool goingForward = true;

	private void Update () {

        UpdatePosition(AtmosphereManager.GetGas(GasType).Percentage);

	}

    public void UpdatePosition(float value)
    {
        if (goingForward)
        {
            //progress
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }
}