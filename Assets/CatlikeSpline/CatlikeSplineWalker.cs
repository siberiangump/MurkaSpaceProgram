using UnityEngine;

[AddComponentMenu("Catlike/CatlikeSplineWalker")]
public class CatlikeSplineWalker : MonoBehaviour
{
    public CatlikeBezierSpline spline;

    public float duration;

    public bool lookForward;

    public CatlikeSplineWalkerMode mode;
    
    private float progress;
    private bool goingForward = true;

    public bool InEditor = true;
    [Range(0f,1f)]
    public float Progress;
    private void Update()
    {
        if (goingForward)
        {
            progress += Time.deltaTime/duration;

            if (progress > 1f)
            {
                if (mode == CatlikeSplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == CatlikeSplineWalkerMode.Loop)
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

            progress -= Time.deltaTime/duration;


            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }
        SetPossition(progress);
    }

    public void SetPossition(float progress)
    {
        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }

    void OnValidate()
    {
        if (InEditor)
        {
            SetPossition(Progress);
        }
    }
}