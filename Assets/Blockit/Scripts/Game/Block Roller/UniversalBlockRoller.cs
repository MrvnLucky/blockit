using UnityEngine;
using System.Collections;
using Blockit.Persistent;

namespace Blockit.Level
{
  // Rolling of any cuboid of size KxMxN with support for falling, where K, M, N > 0.

  public class UniversalBlockRoller : MonoBehaviour
  {
    private float buttonSensitivity = 0.2f;
    private float rollDuration = 0.25f;
    private bool canStartRolling = true;

    private Rigidbody rb;
    private bool recovering;

    protected void Awake() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
      TryRecoverFlipping();      // try to restore roll functionality

      if (canStartRolling)
        FlipCube(ResolveDirection());
    }

    private void FlipCube(Vector3[] dir)
    {
      if (Input.GetAxis("Horizontal") > buttonSensitivity) StartCoroutine(Roll(dir[0]));
      if (Input.GetAxis("Horizontal") < -buttonSensitivity) StartCoroutine(Roll(dir[1]));
      if (Input.GetAxis("Vertical") > buttonSensitivity) StartCoroutine(Roll(dir[2]));
      if (Input.GetAxis("Vertical") < -buttonSensitivity) StartCoroutine(Roll(dir[3]));
    }

    protected virtual Vector3[] ResolveDirection() => new Vector3[4] { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };  // kierunki turlania

    private IEnumerator Roll(Vector3 direction)
    {
      canStartRolling = false;
      rb.useGravity = false;

      Vector3 endPosition = transform.position;
      Vector3 pointAround = Vector3.zero;

      float xScale = transform.localScale.x / 2;
      float yScale = transform.localScale.y / 2;
      float zScale = transform.localScale.z / 2;

      void ComputeRollParameters(Vector3 myAxis, Vector3 axis, float scaleA, float scaleB, float scaleC, float scaleD)
      {
        if (Vector3.Distance(myAxis, axis) < 0.1f || Vector3.Distance(myAxis, -axis) < 0.1f)
        {
          pointAround = transform.position + direction * scaleA + Vector3.down * scaleC;
          endPosition = transform.position + direction * (scaleA + scaleC) + Vector3.down * (scaleC - scaleA);
        }
        else
        {
          pointAround = transform.position + direction * scaleB + Vector3.down * scaleD;
          endPosition = transform.position + direction * (scaleB + scaleD) + Vector3.down * (scaleD - scaleB);
        }
      }

      if (Vector3.Distance(transform.up, Vector3.up) < 0.1f || Vector3.Distance(transform.up, -Vector3.up) < 0.1f) // at the beginning
      {
        if (direction == Vector3.forward || direction == -Vector3.forward) ComputeRollParameters(transform.forward, Vector3.right, xScale, zScale, yScale, yScale);
        else if (direction == Vector3.right || direction == -Vector3.right) ComputeRollParameters(transform.forward, Vector3.right, zScale, xScale, yScale, yScale);
      }
      else if (Vector3.Distance(transform.up, Vector3.forward) < 0.1f || Vector3.Distance(transform.up, -Vector3.forward) < 0.1f)
      {
        if (direction == Vector3.forward || direction == -Vector3.forward) ComputeRollParameters(transform.forward, Vector3.right, yScale, yScale, xScale, zScale);
        else if (direction == Vector3.right || direction == -Vector3.right) ComputeRollParameters(transform.forward, Vector3.right, zScale, xScale, xScale, zScale);
      }
      else if (Vector3.Distance(transform.up, Vector3.right) < 0.1f || Vector3.Distance(transform.up, -Vector3.right) < 0.1f)
      {
        if (direction == Vector3.right || direction == -Vector3.right) ComputeRollParameters(transform.right, Vector3.up, yScale, yScale, xScale, zScale);
        else if (direction == Vector3.forward || direction == -Vector3.forward) ComputeRollParameters(transform.right, Vector3.up, zScale, xScale, xScale, zScale);
      }

      float rollDecimal = 0, oldAngle = 0;

      Vector3 rollAxis = Vector3.Cross(Vector3.up, direction);    // the axis of rotation as the result of the vector product

      if (endPosition != transform.position)      // for Rigidbody
      {
        while (rollDecimal < rollDuration)
        {
          yield return new WaitForEndOfFrame();
          rollDecimal += Time.deltaTime;
          float newAngle = (rollDecimal / rollDuration) * 90;
          float rotateThrough = newAngle - oldAngle;
          oldAngle = newAngle;
          transform.RotateAround(pointAround, rollAxis, rotateThrough);
        }
      }

      int xMul = Mathf.RoundToInt(transform.rotation.eulerAngles.x / 90);
      int yMul = Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90);
      int zMul = Mathf.RoundToInt(transform.rotation.eulerAngles.z / 90);

      transform.rotation = Quaternion.Euler(new Vector3(xMul * 90, yMul * 90, zMul * 90));
      transform.position = endPosition;

      yield return new WaitForEndOfFrame();
      rb.useGravity = true;
      yield return new WaitForSeconds(0.025f);

      if (rb.velocity.y < -0.1f)       // if we fall
      {
        recovering = true;
        yield break;
      }

      canStartRolling = true;

      // TODO: SoundManager.GetSoundEffect(0, 0.1f);
    }

    private void TryRecoverFlipping()           // try to restore roll functionality
    {
      if (rb.velocity.y == 0 && recovering)   // when the lump stopped falling
      {
        recovering = false;
        canStartRolling = true;
      }
    }
  }
}