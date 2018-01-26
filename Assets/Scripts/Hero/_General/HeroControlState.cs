public class HeroControlState
{
    public bool jump { get; set; }
    public bool movement { get; set; }
    public bool ability1 { get; set; }
    public bool ability2 { get; set; }
    public bool ultimate { get; set; }

    public void SetAll(bool state)
    {
        jump = state;
        movement = state;
        ability1 = state;
        ability2 = state;
        ultimate = state;
    }
}