/// <summary>
/// Struct de player pour save plus facilement
/// </summary>
public struct CharacterState
{
    public int HP;
    public int Mana;
    public string Name;

    public CharacterState(int health, int mana, string nom)
    {
        HP = health;
        Mana = mana;
        Name = nom;
    }
}
