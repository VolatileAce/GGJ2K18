using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEntry : MonoBehaviour
{
    public static AnimatedEntry Instance;

    [SpaceAttribute(10)]
    //[HeaderAttribute]
    public Vector3 StartScale;
    public Vector3 StartPosition;

    public Vector3 EndScale;
    public Vector3 EndPosition;

    public AnimationCurve ScaleCurve;
    public AnimationCurve PositionCurve;
    
    public float EffectTime = 0.75f;

	// Use this for initialization
	void Start ()
    {
        Instance = this;

        SetupVariables();
    }

    public void AnimateOn()
    {
        StartCoroutine(AnimationOn());
    }

    public void AnimateOff()
    {
        StartCoroutine(AnimationOff());
    }

    void SetupVariables()
    {
        EndScale = transform.localScale;
        EndPosition = transform.localPosition;
    }

    IEnumerator AnimationOn()
    {
        transform.localPosition = StartPosition;
        transform.localScale = StartScale;

        float time = 0;
        float perc = 0;
        float lastTime = Time.realtimeSinceStartup;

        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;

            perc = Mathf.Clamp01(time / EffectTime);
            Vector3 tempScale = Vector3.Lerp(StartScale, EndScale, ScaleCurve.Evaluate(perc));
            Vector3 TempPos = Vector3.Lerp(StartPosition, EndPosition, PositionCurve.Evaluate(perc));
            transform.localScale = tempScale;
            transform.localPosition = TempPos;
            yield return null;
        } while (perc < 1);

        transform.localPosition = EndPosition;
        transform.localScale = EndScale;

    }

    IEnumerator AnimationOff()
    {
        transform.localPosition = StartPosition;
        transform.localScale = StartScale;

        float time = 0;
        float perc = 0;
        float lastTime = Time.realtimeSinceStartup;

        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;

            perc = Mathf.Clamp01(time / EffectTime);
            Vector3 tempScale = Vector3.Lerp(StartScale, EndScale, ScaleCurve.Evaluate(perc));
            Vector3 TempPos = Vector3.Lerp(StartPosition, EndPosition, PositionCurve.Evaluate(perc));
            transform.localScale = tempScale;
            transform.localPosition = TempPos;
            yield return null;
        } while (perc < 1);

        transform.localPosition = EndPosition;
        transform.localScale = EndScale;

    }
}
