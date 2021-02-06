using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _snakeTilePrefab;
    [SerializeField]
    private UiScore _uiScore;
    [SerializeField]
    private UiGameOver _uiGameover;
    private PlayerSound _playerSound;
    private PlayerInput _playerInput;
    private PlayerCollisions _playerCollisions;
    private List<Transform> _tailTiles = new List<Transform>();
    private Vector2 _movementVector;
    private Vector2 _lastPosition;
    private float _movementTick;
    [SerializeField]
    private float _movementTickStartValue;
    private bool _collidedWithFood;
    private bool _gameIsRunning;
    

    private int _score
    {
        get
        {
            return _tailTiles.Count;
        }
    }


    private void Awake()
    {
        _playerSound = GetComponentInChildren<PlayerSound>();
        _playerInput = GetComponent<PlayerInput>();
        _playerCollisions = GetComponent<PlayerCollisions>();
        _playerCollisions.OnPlayerEnteredTrigger.AddListener(PlayerEnteredTrigger);
    }

    private void Start()
    {
        _movementTick = _movementTickStartValue;
        _movementVector = Vector2.right;
        _gameIsRunning = true;
    }

    private void Update()
    {
        if (!_gameIsRunning)
        {
            return;
        }
        _movementTick -= Time.deltaTime;
     
        if (_movementTick < 0f)
        {
            UpdateMovementVector();
            _movementTick = _movementTickStartValue;
            _lastPosition = transform.position;
            transform.Translate(_movementVector);
            _playerSound.PlayMovementAudio();

            if (_collidedWithFood)
            {
                Transform newSnakeTile = GameObject.Instantiate(_snakeTilePrefab).transform;
                newSnakeTile.transform.position = _lastPosition;
                _tailTiles.Insert(0, newSnakeTile.transform);
                _collidedWithFood = false;
                _uiScore.SetScore(_score);
            }
            else if (_tailTiles.Count > 0)
            {
                Transform lastTailTile = _tailTiles[_tailTiles.Count - 1];
                lastTailTile.position = _lastPosition;
                _tailTiles.Insert(0, lastTailTile);
                _tailTiles.RemoveAt(_tailTiles.Count - 1);
            }
        }
    }

    private void UpdateMovementVector()
    {
        if (_playerInput.HorizontalInput == 0 && _playerInput.VerticalInput != 0 && _movementVector.y == 0)
        {
            _movementVector = Vector2.up * _playerInput.VerticalInput;
        }
        else if (_playerInput.HorizontalInput != 0 && _playerInput.VerticalInput == 0 && _movementVector.x == 0)
        {
            _movementVector = Vector2.right * _playerInput.HorizontalInput;
        }
    }

    private IEnumerator RestartGameWithDelay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    private void PlayerEnteredTrigger(Transform collidedTransform)
    {
        if (collidedTransform.CompareTag("Food"))
        {
            OnPlayerAteFood(collidedTransform);
        }
        else if (collidedTransform.CompareTag("Player") || collidedTransform.root.CompareTag("Border"))
        {
            Debug.Log("Gameover!");
            _playerSound.PlayDeathAudio();
            _gameIsRunning = false;
            _uiGameover.gameObject.SetActive(true);
            _uiGameover.SetScore(_score);
            StartCoroutine(RestartGameWithDelay());
        }
    }

    private void OnPlayerAteFood(Transform collidedTransform)
    {
        _collidedWithFood = true;
        _playerSound.PlayAteFoodAudio();
        Food food = collidedTransform.GetComponent<Food>();

        if (food != null)
        {
            food.ChangePositionRandomly();
        }
    }

    private void OnPlayerDeathCollision(Transform collidedTransform)
    {
        Debug.Log("Gameover!");
        _playerSound.PlayDeathAudio();
        _gameIsRunning = false;
        _uiGameover.gameObject.SetActive(true);
        _uiGameover.SetScore(_score);
        StartCoroutine(RestartGameWithDelay());
    }
}
