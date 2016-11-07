using UnityEngine;
using System.Collections;
using RoninUtils.Helper;

public class TestComponent : RoninComponent {

    private CharacterController mCc;

    protected override void Awake () {
        base.Awake();
        mCc = GetComponent<CharacterController>();
    }

    protected override void FixedUpdate () {
        base.FixedUpdate();

        if (Input.GetKey(KeyCode.F)) {
            mCc.Move(3 * Vector3.right * Time.deltaTime + Vector3.down * Time.deltaTime );
        }

    }

    void OnTriggerEnter (Collider other) {
        Debug.Log(other.name);
    }

    // OnControllerColliderHit is called when the controller hits a collider while performing a Move.
    void OnControllerColliderHit (ControllerColliderHit hit) {
        Debug.Log(hit.collider .name + " " + Time.time);
    }
}
