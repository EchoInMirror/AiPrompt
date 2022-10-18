using AiPrompt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiPrompt.Service;

public class StateContainer
{
    private string? query;

    public string Query
    {
        get => query ?? string.Empty;
        set
        {
            query = value;
            OnQueryChanged?.Invoke();
        }
    }
    public event Action? OnQueryChanged;


    private Source? source;
    public Source Source
    {
        get => source;
        set
        {
            source = value;
            OnSourceChanged?.Invoke();
        }
    }
    public event Action? OnSourceChanged;

}