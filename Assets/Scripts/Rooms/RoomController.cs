using Rooms;
using Rules;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    [Header("Refs")]
    public RuleManager ruleManager;
    public RulesUI rulesUI;
    public Room[] rooms;              
    public Transform player;          
   public string outroSceneName = "OutroScene";


    [Header("Run Settings")]
    public int startingRevives = 3;
    public float transitionLock = 0.25f;

    // runtime
    private EnemySpawner _enemySpawner;
    private DefianceManager _defiance;  
    private Room currentRoom;
    private int currentIndex = -1;
    private int alive = 0;
    private readonly List<GameObject> spawned = new();
    private readonly Dictionary<int, int> roomSeeds = new();  
    private readonly HashSet<int> clearedRooms = new();       
    private bool transitioning = false;

    void Awake()
    {
        if (!_defiance) _defiance = FindFirstObjectByType<DefianceManager>();
    }

    void OnEnable()
    {
        if (_defiance)
        {
            _defiance.onPlayerRevive.AddListener(OnPlayerRevive);
            _defiance.onOutOfRevives.AddListener(OnOutOfRevives);
        }
    }

    void OnDisable()
    {
        if (_defiance)
        {
            _defiance.onPlayerRevive.RemoveListener(OnPlayerRevive);
            _defiance.onOutOfRevives.RemoveListener(OnOutOfRevives);
        }
    }

    void Start()
    {
        if (_defiance) _defiance.revivesLeft = startingRevives;
        GoToRoom(0);
    }

    public void GoNextRoom() => GoToRoom(currentIndex + 1);

    private void GoToRoom(int targetIndex)
    {
        if (transitioning) return;
        transitioning = true;

       
        CleanupSpawned();
        ruleManager?.ClearActive();
        if (currentRoom) currentRoom.gameObject.SetActive(false);

       
        if (targetIndex >= rooms.Length)
        {
            SceneManager.LoadScene(outroSceneName);
            return;
        }
        if (targetIndex < 0) targetIndex = 0;

        
        currentIndex = targetIndex;
        currentRoom = rooms[currentIndex];
        currentRoom.gameObject.SetActive(true);

        
        if (player && currentRoom.playerSpawn)
            player.position = currentRoom.playerSpawn.position;

        
        if (currentRoom.exitTrigger)
        {
            var door = currentRoom.exitTrigger.GetComponent<ExitDoor>();
            if (!door) door = currentRoom.exitTrigger.gameObject.AddComponent<ExitDoor>();
            door.controller = this;
        }

       
        
        
        if (currentRoom.kind == RoomKind.Combat || currentRoom.kind == RoomKind.Boss)
        {
            bool cleared = clearedRooms.Contains(currentIndex);
            if (currentRoom.exitTrigger) currentRoom.exitTrigger.gameObject.SetActive(cleared); // open if cleared
            if (cleared)
            {
                rulesUI?.Refresh();
            }
            else
            {
                StartCombatRoom();
            }
        }
        else
        {
            
            rulesUI?.Refresh();
            if (currentRoom.exitTrigger) currentRoom.exitTrigger.gameObject.SetActive(true);
        }

        Invoke(nameof(UnlockTransition), transitionLock);
    }

    private void StartCombatRoom()
    {
        
        if (!roomSeeds.TryGetValue(currentIndex, out var seed))
        {
            seed = Random.Range(int.MinValue, int.MaxValue);
            roomSeeds[currentIndex] = seed;
        }
        ruleManager?.ApplyRulesForRoom(seed);
        rulesUI?.Refresh();

        spawned.Clear();
        alive = 0;
        _enemySpawner = currentRoom.GetComponentInChildren<EnemySpawner>(true);
        if (_enemySpawner != null)
        {
            foreach (var e in _enemySpawner.SpawnAll())
            {
                if (!e) continue;
                spawned.Add(e);
                alive++;
                var hp = e.GetComponent<Health>();
                if (hp) hp.onDeath.AddListener(OnEnemyDeath);
            }
        }

        
        if (alive == 0 && currentRoom.exitTrigger)
            currentRoom.exitTrigger.gameObject.SetActive(true);
    }

    private void OnEnemyDeath()
    {
        alive--;
        if (alive <= 0)
        {
            clearedRooms.Add(currentIndex);
            if (currentRoom.exitTrigger)
                currentRoom.exitTrigger.gameObject.SetActive(true);
            
        }
    }

    
    private void OnPlayerRevive()
    {
        
        GoToRoom(Mathf.Max(0, currentIndex - 1));
    }

    private void OnOutOfRevives()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CleanupSpawned()
    {
        for (int i = 0; i < spawned.Count; i++)
            if (spawned[i]) Destroy(spawned[i]);
        spawned.Clear();
        alive = 0;
    }

    private void UnlockTransition() => transitioning = false;
}
