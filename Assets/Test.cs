using UnityEngine;
using System.Collections;
using RoninUtils.Helper;
using RoninUtils.RoninFramework;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

    private ResourceRequest mRequest;


    public GameObject testPrefab;

    public bool test;


    // Use this for initialization
    void Start () {
        PoolService poolService = GameFramework.GetService<PoolService>();
        var pool = poolService.CreatePool("Test");
        poolService.PreloadPrefab(pool, testPrefab, 3, 5);
        poolService.Spawn(pool, testPrefab, Vector3.zero, Quaternion.identity, this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (test && Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene("testLoad");
    }
}