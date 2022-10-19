using AiPrompt.Data;

namespace AiPrompt.Util;

public class StateContainer
{
    private string query;

    public string Query
    {
        get => query ?? string.Empty;
        set
        {
            query = value;
            OnQueryChanged?.Invoke();
        }
    }
    public event Action OnQueryChanged;


    private Source source;
    public Source Source
    {
        get => source;
        set
        {
            source = value;
            OnSourceChanged?.Invoke();
        }
    }
    public event Action OnSourceChanged;

}