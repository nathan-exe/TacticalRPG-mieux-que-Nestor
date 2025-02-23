
# Data
- ## Player Representation
    - Server side:
    ```cs
    int ID
    string[] tags
    object PlayerData
    ``` 
    - struct PlayerData (client side)
    ```cs 
    string Name
    ```
- ## 
# Network Events
    
- ## Network Manager Events
    Broadcast by the network manager to other player's network managers, in the same lobby,while playing

    ```cs
    OnAgentInstantiated > string ID , string PrefabPath
    OnAgentDestroyed > string ID
    ```

- ## NetworkAgent Events (implicit agentID)
    Broadcast by local GameObjects to their proxys on other players sides, in the same lobby,while playing

    -  MovableNetworkAgent :

        ```cs
        OnPositionUpdated > Vector3
        ```
