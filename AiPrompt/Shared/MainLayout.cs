using System.Collections.ObjectModel;
using AiPrompt.Components;
using AiPrompt.I18n;
using AiPrompt.Model.Entity;
using AiPrompt.Model.Service;
using AiPrompt.Pages;
using MauiReactor;

namespace AiPrompt.Shared;

public class MainLayoutState : BaseState {
    public ObservableCollection<Prompt> Prompts { get; set; } = [];
}

public class Search : MauiControls.SearchHandler {
    protected override void OnQueryChanged(string oldValue, string newValue) {
        
        base.OnQueryChanged(oldValue, newValue);
    }
}

public class MainLayout : BaseComponent<MainLayoutState> {
    public override VisualNode Render() => new Shell {
            new ShellContent(L(Languages.Key.Shell.Home)) {
                new ContentPage(L(Languages.Key.Shell.Home))
            }.Route("Home"),
            State.Prompts.Select(p => new ShellContent(p.Name)
                .RenderContent(()=>new PromptListPage().Key(p.Key))
                .Route(p.Key)
            ),
            new ShellContent {
                new SettingPage()
            }.FlyoutItemIsVisible(false).Route("Setting")
        }.ItemTemplate(RenderFlyoutItemTemplate)
        .FlyoutHeader(RenderHeader())
        .FlyoutFooter(RenderFooter())
        .BackgroundColor(Colors.Transparent)
        .FlyoutBackgroundColor(Colors.Transparent)
        .FlyoutBehavior(FlyoutBehavior.Locked)
        
        .Set(MauiControls.Shell.SearchHandlerProperty, new Search());
    
    private VisualNode RenderHeader() {
        return new SourcePicker();
    }
    
    private VisualNode RenderFooter() {
        return RenderFlyoutItemTemplate(new (){ Title = L(Languages.Key.Setting) }).OnTapped(() => {
            MauiControls.Shell.Current.GoToAsync("///Settings");
        });
    }
    
    private Grid RenderFlyoutItemTemplate(MauiControls.BaseShellItem item)
        => Grid("36", "36,*", Image().GridColumn(0)
                .WidthRequest(16)
                .HeightRequest(16)
                .Source(item.FlyoutIcon)
                .VCenter()
                .HCenter(),
            Label(item.Title)
                .GridColumn(1)
                .VCenter() 
        );

    protected override void OnMounted() {
        Store.OnSourceChanged += OnSourceChanged;
        base.OnMounted();
    }

    protected override void OnWillUnmount() {
        Store.OnSourceChanged -= OnSourceChanged;
        base.OnWillUnmount();
    }

    // async void OpenSetting(){
    //     Config.SourcePath = await sourceService.GetSourcePathAsync();
    //     setting = true;
    //     StateHasChanged();
    // }
    //
    // async void SaveSetting (){
    //     await configService.Save(Config);
    //     await sourceSelectRef.Reload();
    // }
    //
    //切换咒语书时拉取分类
    private async void OnSourceChanged(Source? source) {
        if (source is null or {Path: null or ""})
            return;
        var promptService = Services.GetService<IPromptService>()!;
        var categories = await promptService.GetCategoriesAsync(source.Path);
        SetState(s => s.Prompts = new ObservableCollection<Prompt>(categories));
    }

}