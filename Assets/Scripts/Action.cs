using System.Collections;
using UnityEngine;

public class RobotAnimator : MonoBehaviour
{
    [SerializeField] private Transform headPoint;
    [SerializeField] private Transform torsoPoint;
    [SerializeField] private Transform legsPoint;

    [SerializeField] private float offset = 0.3f;
    [SerializeField] private float duration = 0.3f;

    private bool _isPlaying;

    public void PlayAction()
    {
        if (_isPlaying) return;
        StartCoroutine(ActionRoutine());
    }

    private IEnumerator ActionRoutine()
    {
        _isPlaying = true;

        Vector3 headOrigin = headPoint.localPosition;
        Vector3 torsoOrigin = torsoPoint.localPosition;
        Vector3 legsOrigin = legsPoint.localPosition;

        
        yield return MoveAll(
            headPoint,  headOrigin  + Vector3.left    * offset,
            torsoPoint, torsoOrigin + Vector3.right   * offset,
            legsPoint,  legsOrigin  + Vector3.forward * offset);
        
        yield return MoveAll(
            headPoint,  headOrigin,
            torsoPoint, torsoOrigin,
            legsPoint,  legsOrigin);

        _isPlaying = false;
    }

    private IEnumerator MoveAll(
        Transform a, Vector3 targetA,
        Transform b, Vector3 targetB,
        Transform c, Vector3 targetC)
    {
        float elapsed = 0f;

        Vector3 startA = a.localPosition;
        Vector3 startB = b.localPosition;
        Vector3 startC = c.localPosition;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            a.localPosition = Vector3.Lerp(startA, targetA, t);
            b.localPosition = Vector3.Lerp(startB, targetB, t);
            c.localPosition = Vector3.Lerp(startC, targetC, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        a.localPosition = targetA;
        b.localPosition = targetB;
        c.localPosition = targetC;
    }
}