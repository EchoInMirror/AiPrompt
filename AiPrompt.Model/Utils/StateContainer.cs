using AiPrompt.Model.Entity;

namespace AiPrompt.Util;

public class StateContainer
{
    private string _query;

    public string Query
    {
        get => _query ?? string.Empty;
        set
        {
            _query = value;
            OnQueryChanged?.Invoke();
        }
    }
    public event Action? OnQueryChanged;


    private Source _source;
    public Source Source
    {
        get => _source;
        set
        {
            _source = value;
            OnSourceChanged?.Invoke();
        }
    }
    public event Action? OnSourceChanged;

}