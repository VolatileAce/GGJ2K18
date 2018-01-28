using UnityEngine;

public class SplineWalkerControls : MonoBehaviour {

	public BezierSpline spline;

	public bool lookForward;

	public SplineWalkerMode mode;
    public GasType GasType;

	public float currentValue;
    public float desiredValue;
	private bool goingForward = true;

	private void Update () {

        desiredValue = AtmosphereManager.GetGas(GasType).Percentage;
        currentValue = Mathf.Lerp(currentValue, desiredValue, 0.004f);
        UpdatePosition(currentValue);

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
                    value = 2f - value;
                    goingForward = false;
                }
            }
        }
        else
        {
            if (value < 0f)
            {
                value = -value;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(value);
        transform.position = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(value));
        }
    }
}