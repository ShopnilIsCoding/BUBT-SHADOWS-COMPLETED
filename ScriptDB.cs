using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;

public class ScriptDb : MonoBehaviour
{
    private string dbName = "URI=file:Player.db";
    public GameObject player;
    public GameObject lift; // Add this line
    private SimpleShootingGun shootingGun;
    public GameObject shootingGunObject;
    public GameObject wheel;
    private scrollwheel scrollwheel;
    public playerController playerController;
    public FuseBox fuseBox;

    void Start()
    {
        shootingGun = shootingGunObject.GetComponent<SimpleShootingGun>();
        scrollwheel = wheel.GetComponent<scrollwheel>();

        if (shootingGun == null)
        {
            Debug.LogError("SimpleShootingGun component not found on the specified object.");
            return;
        }

        CreateDB();

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isNewGame)
            {
                StartNewGame();
            }
            else
            {
                LoadGame();
            }
        }
        else
        {
            Debug.LogError("GameManager instance is not found.");
        }
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS player_position (x FLOAT, y FLOAT, z FLOAT);";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS player_info (health FLOAT, torch_intensity FLOAT, battery_count INT DEFAULT 0, soul_count INT DEFAULT 0, stored_ammo INT DEFAULT 0, magazine_ammo INT DEFAULT 5, is_sword_active INT DEFAULT 0, is_raygun_active INT DEFAULT 0, is_plug_picked INT DEFAULT 0, fusebox INT DEFAULT 0);";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS lift_position (x FLOAT, y FLOAT, z FLOAT);";
                command.ExecuteNonQuery();
            }
        }
    }

    private void StartNewGame()
    {
        player.transform.position = new Vector3(-2f, 1.1f, 16f);
        player.GetComponent<playerController>().startingHealth = 100f;
        player.GetComponent<PlayerController>().torch.intensity = 7f;
        FindObjectOfType<BatteryManager>().batteryCount = 0;
        FindObjectOfType<Inventory>().soul = 0;
        shootingGun.storedAmmo = 0;
        shootingGun.magazineCapacity = 5;
        scrollwheel.isSwordActive = false;
        scrollwheel.isRayGunActive = false;
        playerController.picked = false;
        fuseBox.allInserted = false;

        if (lift != null)
        {
            lift.transform.position = new Vector3(-41.95f, 0f, -45.98f); // Initial lift position
        }
    }

    private void LoadGame()
    {
        LoadPlayerPosition();
        LoadPlayerHealth();
        LoadTorchIntensity();
        LoadBatteryCount();
        LoadSoulCount();
        LoadAmmoCounts();
        LoadWeaponStates();
        LoadPlugState();
        LoadfuseboxState();

        if (lift != null)
        {
            Vector3 liftPosition = LoadLiftPosition();
            if (liftPosition != Vector3.zero) // Ensure a valid position is loaded
            {
                lift.transform.position = liftPosition;
            }
        }
    }

    public void SavePlayerPosition()
    {
        Vector3 playerPosition = player.transform.position;
        float x = playerPosition.x;
        float y = playerPosition.y;
        float z = playerPosition.z;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM player_position;";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO player_position (x, y, z) VALUES ({x}, {y}, {z});";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void SavePlayerHealth()
    {
        float health = player.GetComponent<playerController>().health;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM player_info;";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO player_info (health, torch_intensity, battery_count, soul_count) VALUES (@health, @torchIntensity, @batteryCount, @soulCount);";
                command.Parameters.AddWithValue("@health", health);
                command.Parameters.AddWithValue("@torchIntensity", player.GetComponent<PlayerController>().torch.intensity);
                command.Parameters.AddWithValue("@batteryCount", FindObjectOfType<BatteryManager>().batteryCount);
                command.Parameters.AddWithValue("@soulCount", FindObjectOfType<Inventory>().soul);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void SaveTorchIntensity()
    {
        float torchIntensity = player.GetComponent<PlayerController>().torch.intensity;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE player_info SET torch_intensity = @torchIntensity;";
                command.Parameters.AddWithValue("@torchIntensity", torchIntensity);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void SaveIntToDatabase(string columnName, int value)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE player_info SET {columnName} = @value;";
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void SaveBatteryCount(int batteryCount)
    {
        SaveIntToDatabase("battery_count", batteryCount);
    }

    public void SaveSoulCount(int soulCount)
    {
        SaveIntToDatabase("soul_count", soulCount);
    }

    public void SaveAmmoCounts()
    {
        SaveIntToDatabase("stored_ammo", shootingGun.storedAmmo);
        SaveIntToDatabase("magazine_ammo", shootingGun.magazineCapacity);
    }

    public int LoadIntFromDatabase(string columnName, int defaultValue)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT {columnName} FROM player_info;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(reader.GetOrdinal(columnName));
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
            }
        }
    }

    public void LoadPlayerPosition()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM player_position;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        float x = reader.GetFloat(reader.GetOrdinal("x"));
                        float y = reader.GetFloat(reader.GetOrdinal("y"));
                        float z = reader.GetFloat(reader.GetOrdinal("z"));
                        player.transform.position = new Vector3(x, y, z);
                    }
                }
            }
        }
    }

    public float LoadFloatFromDatabase(string columnName, float defaultValue)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT {columnName} FROM player_info;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetFloat(reader.GetOrdinal(columnName));
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
            }
        }
    }

    public void LoadPlayerHealth()
    {
        float health = LoadFloatFromDatabase("health", 100f);
        player.GetComponent<playerController>().startingHealth = health;
    }

    public void LoadTorchIntensity()
    {
        float torchIntensity = LoadFloatFromDatabase("torch_intensity", 14);
        player.GetComponent<PlayerController>().torch.intensity = torchIntensity;
    }

    public int LoadBatteryCount()
    {
        return LoadIntFromDatabase("battery_count", 0);
    }

    public int LoadSoulCount()
    {
        return LoadIntFromDatabase("soul_count", 0);
    }

    public void LoadAmmoCounts()
    {
        shootingGun.storedAmmo = LoadIntFromDatabase("stored_ammo", 0);
        shootingGun.magazineCapacity = LoadIntFromDatabase("magazine_ammo", 5);
    }

    public void SaveWeaponStates(bool isSwordActive, bool isRayGunActive)
    {
        SaveIntToDatabase("is_sword_active", isSwordActive ? 1 : 0);
        SaveIntToDatabase("is_raygun_active", isRayGunActive ? 1 : 0);
    }

    public void SavePlugStates(bool picked)
    {
        SaveIntToDatabase("is_plug_picked", picked ? 1 : 0);
    }

    public void SavefuseboxStates(bool inserted)
    {
        SaveIntToDatabase("fusebox", inserted ? 1 : 0);
    }

    public void LoadWeaponStates()
    {
        scrollwheel.isSwordActive = LoadIntFromDatabase("is_sword_active", 0) == 1;
        scrollwheel.isRayGunActive = LoadIntFromDatabase("is_raygun_active", 0) == 1;
    }

    public void LoadPlugState()
    {
        playerController.picked = LoadIntFromDatabase("is_plug_picked", 0) == 1;
    }

    public void LoadfuseboxState()
    {
        fuseBox.allInserted = LoadIntFromDatabase("fusebox", 0) == 1;

        // Set the initial state of each plug based on allInserted
        if (fuseBox.allInserted)
        {
            for (int i = 0; i < fuseBox.plugs.Length; i++)
            {
                fuseBox.plugParents[i].triggered = true;
                fuseBox.plugs[i].SetActive(true);
            }
        }
    }

    // New methods for lift position
    public void SaveLiftPosition(Vector3 liftPosition)
    {
        float x = liftPosition.x;
        float y = liftPosition.y;
        float z = liftPosition.z;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM lift_position;";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO lift_position (x, y, z) VALUES ({x}, {y}, {z});";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public Vector3 LoadLiftPosition()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM lift_position;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        float x = reader.GetFloat(reader.GetOrdinal("x"));
                        float y = reader.GetFloat(reader.GetOrdinal("y"));
                        float z = reader.GetFloat(reader.GetOrdinal("z"));
                        return new Vector3(x, y, z);
                    }
                    else
                    {
                        return Vector3.zero; // Default position if not found
                    }
                }
            }
        }
    }

    void Update()
    {
        // ...
    }

    public void OnSaveButtonClicked()
    {
        SavePlayerPosition();
        SavePlayerHealth();
        SaveTorchIntensity();
        SaveBatteryCount(FindObjectOfType<BatteryManager>().batteryCount);
        SaveSoulCount(FindObjectOfType<Inventory>().soul);
        SaveAmmoCounts();
        FindObjectOfType<scrollwheel>().SaveWeaponStates();
        SavePlugStates(playerController.picked);
        SavefuseboxStates(fuseBox.allInserted);

        // Save lift position
        if (lift != null)
        {
            SaveLiftPosition(lift.transform.position);
        }
    }
}
