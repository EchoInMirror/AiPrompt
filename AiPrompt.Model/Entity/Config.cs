using System.Numerics;
using System.Text.Json;
using SQLite;

namespace AiPrompt.Model.Entity;

[Table("config")]
public record Config<TValue> {
    public Config(){}
    public Config(string key, TValue value) {
        Key = key;
        Value = value;
    }

    [PrimaryKey]public string? Key { get; set; }

    private string? _value;

    public TValue Value {
        get => GetValue<TValue>();
        set => SetValue(value);
    }

    /// <summary>
    /// 缓存值转换成string类型
    /// </summary>
    /// <returns></returns>
    public Config<string> ToStr() {
        return new Config<string>(Key, _value);
    }

    /// <summary>
    /// 缓存值转换成泛型
    /// </summary>
    /// <typeparam name="TOther">其他类型</typeparam>
    /// <returns></returns>
    public Config<TOther> To<TOther>() {
        var value = GetValue<TOther>();
        return new Config<TOther>(Key, value);
    }

    /// <summary>
    /// string泛型转
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T GetValue<T>() => typeof(T).Name switch {
        nameof(String) => (T)Convert.ChangeType(_value, typeof(T)),
        nameof(Int32) => (T)Convert.ChangeType(int.Parse(_value), typeof(T)),
        nameof(Double) => (T)Convert.ChangeType(double.Parse(_value), typeof(T)),
        _ => JsonSerializer.Deserialize<T>(_value)
    };

    /// <summary>
    /// 泛型转string
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    private void SetValue<T>(T value) {
        var setValue = value ?? throw new ArgumentException("Config value can not be null");
        try {
            _value = typeof(T).Name switch {
                nameof(String) => setValue.ToString(),
                nameof(Int32) => setValue.ToString(),
                nameof(Double) => setValue.ToString(),
                _ => JsonSerializer.Serialize<T>(setValue)
            };
        }
        catch{
            _value = setValue.ToString();
        }
    }
}