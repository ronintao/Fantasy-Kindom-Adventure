using UnityEngine;
using System.Collections;
using RoninUtils.Helper;

public class TestSwitch : MonoBehaviour {

    public Animator defaultAnimator;

    public Animator switchAnimator;



    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            SwitchAnimator();
        }
    }


    private void SwitchAnimator() {
        Animator newAnim = Instantiate(switchAnimator, defaultAnimator.transform.position, defaultAnimator.transform.localRotation) as Animator;
        defaultAnimator.CopyTo(newAnim);
        DestroyObject(defaultAnimator.gameObject);
    }


}
