using System.Text.Json;
using SQLite;

namespace AiPrompt.Model.Data;

/// <summary>
///  缓存
/// </summary>
/// <typeparam name="TValue"></typeparam>
public record Cache<TValue> {
    protected Cache() {
    }

    protected Cache(string cacheKey, TValue cacheValue) {
        CacheKey = cacheKey;
        CacheValue = cacheValue;
    }

    [PrimaryKey] public string CacheKey { get; set; }

    private string? _cacheValue;

    public TValue CacheValue {
        get => GetValue<TValue>();
        set => SetValue(value);
    }

    /// <summary>
    /// 缓存值转换成string类型
    /// </summary>
    /// <returns></returns>
    public Cache<string> ToCacheString() {
        return new Cache<string>(CacheKey, _cacheValue);
    }

    /// <summary>
    /// 缓存值转换成泛型
    /// </summary>
    /// <typeparam name="TOther">其他类型</typeparam>
    /// <returns></returns>
    public Cache<TOther> ToCache<TOther>() {
        var value = GetValue<TOther>();
        return new Cache<TOther>(CacheKey, value);
    }

    /// <summary>
    /// string泛型转
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T GetValue<T>() => typeof(T).Name switch {
        nameof(String) => (T)Convert.ChangeType(_cacheValue, typeof(T)),
        nameof(Int32) => (T)Convert.ChangeType(int.Parse(_cacheValue), typeof(T)),
        nameof(Double) => (T)Convert.ChangeType(double.Parse(_cacheValue), typeof(T)),
        _ => JsonSerializer.Deserialize<T>(_cacheValue)
    };

    /// <summary>
    /// 泛型转string
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    private void SetValue<T>(T value) {
        var setValue = value ?? throw new ArgumentException("cache value can not be null");
        try {
            _cacheValue = typeof(T).Name switch {
                nameof(String) => setValue.ToString(),
                nameof(Int32) => setValue.ToString(),
                nameof(Double) => setValue.ToString(),
                _ => JsonSerializer.Serialize<T>(setValue)
            };
        }
        catch (Exception e) {
            _cacheValue = setValue.ToString();
        }
    }
}