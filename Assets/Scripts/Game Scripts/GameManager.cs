using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPoint;

    private GameObject _player;

    private void Start() {
        _player = Instantiate(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation);    
    }    
}