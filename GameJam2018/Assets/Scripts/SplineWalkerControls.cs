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
            if (value > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    value = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    value -= 1f;
                }
                else
                {
                    value = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            if (value < 0f)
            {
                value = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(value));
        }
    }
}