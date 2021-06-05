using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class PlayerCharakter : NetworkBehaviour
{
    #region Networking

    [Header("SyncVariablen")]
    [SyncVar(hook = nameof(PlayerNameChanged))] public string _playername;
    [SyncVar(hook = nameof(PlayerParentChanged))] public Transform _parent;
    #endregion

    #region Hook
    public void PlayerNameChanged(string _, string newPlayerName)
    {
        PlayerChar.instance._name = newPlayerName;
        this.gameObject.name = newPlayerName;
        this.ChangeName(newPlayerName);
    }
    public void PlayerParentChanged(Transform _, Transform newTransform)
    {
        this.transform.SetParent(newTransform);
    }
    #endregion

    #region Commands
    [Command]
    private void ChangeMySyncName(string a) {
        _playername = a;
    }
    [Command]
    private void ChangeMySyncTransform(Transform parent)
    {
        _parent = parent;
    }
    #endregion

    [Header("etc")]
    [SerializeField] GameObject inventar;
    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        PlayerManager.instance.OnPlayerNameChanged += ChangeName;
        playerEvents.onPlayerLevelUp += LevelUp;

        PlayerChar.instance._name = "user1";
        PlayerChar.instance._maxhp = 100;
        PlayerChar.instance._hp = PlayerChar.instance._maxhp;
        PlayerChar.instance._atk = 10;
        PlayerChar.instance._lvl = 1;
        PlayerChar.instance._lvlPoints = 0;

        ChangeName(PlayerChar.instance._name);
        ChangeTransform();
    }
    private void ChangeName(string name)
    {
        ChangeMySyncName(name); // new synvcar
    }
    private void ChangeTransform()
    {
        ChangeMySyncTransform(GameObject.Find("home").transform);
    }
    private void LevelUp()
    {
        switch (PlayerChar.instance._lvl)
        {
            case 1:
                Debug.Log(1);
                break;
            case 2:
                GameObject _inventar = Instantiate(inventar,this.transform);
                _inventar.name = "inventar";
                PlayerChar.instance.inventar = _inventar;
                break;
            case 3:
                Debug.Log(3);
                break;
            case 4:
                Debug.Log(4);
                break;
            case 5:
                Debug.Log(5);
                break;
        }
    }
}
public class playerEvents : MonoBehaviour
{
    public static event Action onPlayerLevelUp;
    public static void PlayerLevelUp()
    {
        onPlayerLevelUp?.Invoke();
    }
}
public class inventarEvents : MonoBehaviour
{
    public static event Action onInventarUpdate;
    public static void InventarUpdate()
    {
        onInventarUpdate?.Invoke();
    }
}
