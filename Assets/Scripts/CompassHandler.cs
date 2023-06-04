using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassHandler : MonoBehaviour
{
    [SerializeField] private Transform compassObject;
    [SerializeField] private List<MeshRenderer> renderers;
    [SerializeField] private float turnSpeed = .1f;
    [Space]
    [SerializeField] private AnimationCurve fadeCurve;

    private Quaternion goal;

    private void Start()
    {
        goal = Quaternion.Euler(new Vector3(0, 7.5f, 0));
    }

    private void Update()
    {
        compassObject.rotation = Quaternion.Slerp(compassObject.rotation, goal, turnSpeed);

        float parentRot = transform.eulerAngles.y;

        //int centerIndex = FindCenter(parentRot);

        #region TODO: LOOP
        SetOpacity(renderers[0], CalculateOpacity(parentRot, 0));//N - 0
        SetOpacity(renderers[1], CalculateOpacity(parentRot, 15));//15
        SetOpacity(renderers[2], CalculateOpacity(parentRot, 30));//30
        SetOpacity(renderers[3], CalculateOpacity(parentRot, 45));//45
        SetOpacity(renderers[4], CalculateOpacity(parentRot, 60));//60
        SetOpacity(renderers[5], CalculateOpacity(parentRot, 75));//75
        SetOpacity(renderers[6], CalculateOpacity(parentRot, 90));//E - 90
        SetOpacity(renderers[7], CalculateOpacity(parentRot, 105));//105
        SetOpacity(renderers[8], CalculateOpacity(parentRot, 120));//120
        SetOpacity(renderers[9], CalculateOpacity(parentRot, 135));//135
        SetOpacity(renderers[10], CalculateOpacity(parentRot, 150));//150
        SetOpacity(renderers[11], CalculateOpacity(parentRot, 165));//165
        SetOpacity(renderers[12], CalculateOpacity(parentRot, 180));//S - 180
        SetOpacity(renderers[13], CalculateOpacity(parentRot, 195));//195
        SetOpacity(renderers[14], CalculateOpacity(parentRot, 210));//210
        SetOpacity(renderers[15], CalculateOpacity(parentRot, 225));//225
        SetOpacity(renderers[16], CalculateOpacity(parentRot, 240));//240
        SetOpacity(renderers[17], CalculateOpacity(parentRot, 255));//255
        SetOpacity(renderers[18], CalculateOpacity(parentRot, 270));//W - 270
        SetOpacity(renderers[19], CalculateOpacity(parentRot, 285));//285
        SetOpacity(renderers[20], CalculateOpacity(parentRot, 300));//300
        SetOpacity(renderers[21], CalculateOpacity(parentRot, 315));//315
        SetOpacity(renderers[22], CalculateOpacity(parentRot, 330));//330
        SetOpacity(renderers[23], CalculateOpacity(parentRot, 345));//345 
        #endregion
    }

    //private int FindCenter(float _rot)
    //{
    //    float rawIndex = _rot / 15f;
    //    int index = Mathf.RoundToInt(rawIndex);
    //    if (index == 24) { index= 0; }
    //    return index;
    //}

    private float CalculateOpacity(float _degrees, int _target)
    {

        //float value = Mathf.Abs(_degrees - _target);
        Quaternion A = Quaternion.Euler(0, _degrees, 0);
        Quaternion B = Quaternion.Euler(0, _target, 0);
        float angle = Quaternion.Angle(A, B);
        //return 1 - Mathf.Clamp01(value / 180f);
        return fadeCurve.Evaluate(Mathf.Clamp01(angle / 360f));
    }

    private void SetOpacity(MeshRenderer _renderer, float _value)
    {
        _renderer.material.color = new Color(1, 1, 1, _value);
    }
}