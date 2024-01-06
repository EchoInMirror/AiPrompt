using AiPrompt.I18n;
using AiPrompt.Model.Data;
using AiPrompt.Model.Utils;
using AiPrompt.Util;
using MauiReactor;
using Microsoft.Maui.ApplicationModel;

namespace AiPrompt.Components;
/// <summary>
/// 基础状态
/// </summary>
public class BaseState {
    public AppTheme Theme { get; set; } = AppInfo.Current.RequestedTheme;
    public string? Language { get; set; }

    public string? Search { get; set; }
}
/// <summary>
/// 分页状态
/// </summary>
public class PageBaseState : BaseState {
    public int Offset { get; set; }
    public int Total { get; set; }
    public int Limit { get; set; }
    public bool Finish => Total <= Offset * Limit;
    public bool Loading { get; set; }
    public void Load(Func<Task>? load) {
        if (Loading || Finish) {
            return;
        }

        Loading = true;
        load?.Invoke();
        Loading = false;
    }
    public void SetPage(int offset,int total ,int limit) {
        Offset = offset;
        Total = total;
        Limit = limit;
    }

}
/// <summary>
/// 基础属性
/// </summary>
public class BaseProps{}

/// <summary>
/// 基础组件
/// </summary>
public abstract class BaseComponent : BaseComponent<BaseState> { }

/// <summary>
/// 带状态组件
/// </summary>
/// <typeparam name="TState"></typeparam>
public abstract class BaseComponent<TState> : BaseComponent<TState, BaseProps> where TState : BaseState, new() { }

/// <summary>
/// 带状态属性组件
/// </summary>
/// <typeparam name="TState"></typeparam>
/// <typeparam name="TProps"></typeparam>
public abstract class BaseComponent<TState,TProps> : Component<TState,TProps> 
    where TState: BaseState ,new() where TProps : BaseProps, new() {

    
    protected MauiControls.Page CurrentPage => ReactorApplication.Current.MainPage;

    protected Store Store => Ioc.Resolve<Store>();
    
    /// <summary>
    /// 主题切换事件
    /// </summary>
    /// <param name="o"></param>
    /// <param name="args"></param>
    void AppThemeChanged(object? o,MauiControls.AppThemeChangedEventArgs args) {
        SetState(s => s.Theme = args.RequestedTheme);
    }
    /// <summary>
    /// 语言切换事件
    /// </summary>
    /// <param name="lang"></param>
    void AppLanguageChanged(string lang) {
        SetState(s=>s.Language = lang);
    }

    protected override void OnMounted() {
        ReactorApplication.Current.RequestedThemeChanged += AppThemeChanged;
        Ioc.Resolve<I18nService>().LanguageChanged += AppLanguageChanged;
        base.OnMounted();
    }

    protected override void OnWillUnmount() {
        ReactorApplication.Current.RequestedThemeChanged -= AppThemeChanged;
        Ioc.Resolve<I18nService>().LanguageChanged -= AppLanguageChanged;
        base.OnWillUnmount();
    }

    protected string L(string key) => Ioc.Resolve<I18nService>()[key];
    protected Database Database => Ioc.Resolve<Database>();
    protected T ThemeDarkLight<T>(T dark, T light) {
        return State.Theme == AppTheme.Dark ? dark : light;
    }
    /// <summary>
    /// 主题色
    /// </summary>
    protected Color ThemeColor => MauiControls.Application.AccentColor ?? Colors.AliceBlue;
    /// <summary>
    /// 默认黑白色
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    protected Color DefaultBlackWhiteColor(float alpha = 1) => ThemeDarkLight(Color.FromRgb(43, 43, 43).WithAlpha(alpha), Color.FromRgb(255, 255, 255).WithAlpha(alpha));
    /// <summary>
    /// 高对比黑白色
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    protected Color DeepBlackWhiteColor(float alpha = 1) => ThemeDarkLight(Color.FromRgb(32, 32, 32).WithAlpha(alpha), Color.FromRgb(243, 243, 243).WithAlpha(alpha));

}

