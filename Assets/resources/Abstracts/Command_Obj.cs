

public abstract class Command_Obj
{
    public string name;
    public string command;
    public string manual;
    public Command_Obj[] minor;
    public abstract string OnActivate();
}
