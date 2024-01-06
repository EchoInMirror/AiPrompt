using AiPrompt.Model.Entity;

namespace AiPrompt.Util;

public class Store {
    private Source? _source;
    public Source? Source {
        get => _source;
        set {
            _source = value;
            if (value != null) OnSourceChanged?.Invoke(value);
        }
    }
    public event Action<Source>? OnSourceChanged;
}