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
    public Config Config { get; set; } = new();
}

public class MainLayout : BaseComponent<MainLayoutState> {
    public override VisualNode Render() => new Shell {
            new ShellContent(L(Languages.Key.Shell.Home)) {
                new ContentPage(L(Languages.Key.Shell.Home))
            }.Route("Home"),
            State.Prompts.Select(p => new ShellContent(p.Name) {
                    new PromptListPage(p.Key)
                }.Route(p.Key)
            )
        }.ItemTemplate(RenderFlyoutItemTemplate)
        .FlyoutHeader(RenderHeaderTemplate())
        .BackgroundColor(Colors.Transparent)
        .FlyoutBackgroundColor(Colors.Transparent)
        .FlyoutBehavior(FlyoutBehavior.Locked)
        .Set(MauiControls.Shell.NavBarIsVisibleProperty, false);

    private VisualNode RenderHeaderTemplate() {
        return Picker();
    }
    private VisualNode RenderFlyoutItemTemplate(Microsoft.Maui.Controls.BaseShellItem item)
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

    protected override async void OnMounted() {
        var sourceService = Services.GetService<ISourceService>()!;
        var sourcePath = await sourceService.GetSourcePathAsync();
        SetState(s => s.Config.SourcePath = sourcePath);
        OnSourceChanged();
        base.OnMounted();
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
    private async void OnSourceChanged() {
        var promptService = Services.GetService<IPromptService>()!;
        var categories = await promptService.GetCategoriesAsync("");
        SetState(s => s.Prompts = new ObservableCollection<Prompt>(categories));
        // if (categories.Count>0) {
        //     Navigation.PushAsync()
        //     NavigateTo(promps[0].Key);
        //     selected = 0;
        // }
    }
    //
    // protected override async Task OnInitializedAsync()
    // {
    //     Config.SourcePath = await sourceService.GetSourcePathAsync();
    // }
}