using Assets.Scripts.Player;
using Assets.Scripts.Systems.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject _player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        _player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    public void ToggleCursor(CursorLockMode mode)
    {
        MouseLook.Instance.ChangeLockState(mode);
    }
}
