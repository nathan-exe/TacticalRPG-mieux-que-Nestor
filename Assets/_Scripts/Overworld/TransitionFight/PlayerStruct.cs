/// <summary>
/// Struct de player pour save plus facilement
/// </summary>
public struct CharacterState
{
    public int HP;
    public int Mana;
    public string Name;

    public CharacterState(string name, int health, int mana)
    {
        HP = health;
        Mana = mana;
        Name = name;
    }
}
