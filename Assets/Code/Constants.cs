public class Constants
{
    // ...
}


public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public enum BubbleType
{
    None,
    White,
    Red
}

public enum Axis
{
    None, X, Y, Both
}

public delegate void SharedDelegate();
public delegate void ItemEvent(Item item);
public delegate void BooleanDelegate(bool value);
public delegate void BubbleQueryDelegate(BubbleType requiredBubble, BubblePoint targetPoint);
