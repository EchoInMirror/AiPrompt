using System.Collections.ObjectModel;
using AiPrompt.Components;
using AiPrompt.I18n;
using AiPrompt.Model.Entity;
using AiPrompt.Model.Service;
using MauiReactor;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace AiPrompt.Pages;

public class PromptItem(Prompt prompt) {
    public Prompt Prompt { get; set; } = prompt;
    public bool IsHover { get; set; }
}

public class PromptListState : PageBaseState {
    public ObservableCollection<PromptItem> Prompts { get; set; } = [];

    public IItemsLayout ItemsLayout { get; set; } = new VerticalGridItemsLayout(4);
}

public class PromptListPage() : BaseComponent<PromptListState> {
    private const int ItemWidth = 240;
    private string _key;

    public new PromptListPage Key(string key) {
        _key = key;
        return this;
    }
    public override VisualNode Render()
        => ContentPage(CollectionView()
            .ItemsLayout(State.ItemsLayout)
            .ItemsSource(State.Prompts, ItemTemplate)
            .RemainingItemsThreshold(5)
            .ItemsUpdatingScrollMode(MauiControls.ItemsUpdatingScrollMode.KeepScrollOffset)
            .OnRemainingItemsThresholdReached(OnRemainingItemsThresholdReached)
            .GridRow(1)
            .OnSizeChanged(x => {
                    SetState(s => s.ItemsLayout = new VerticalGridItemsLayout((int)(x.Width / ItemWidth)));
                }
            )
        );

    private VisualNode ItemTemplate(PromptItem item) {
        var option = Frame(Grid(Label(item.Prompt.Key),
                Image().Opacity(0.3f)
                .Source(item.Prompt.Image ?? ThemeDarkLight("dotnet_bot.png", "dotnet_bot.png"))
            ).WidthRequest(160).HeightRequest(160)
        ).BackgroundColor(Colors.Transparent);

        var content = Label(item.Prompt.Name).Center();

        var body = Frame(Grid("Auto", "Auto,*", option, content.GridRow(1))
                .Rows("Auto,*").RowSpacing(10)
            ).CornerRadius(10)
            .WidthRequest(ItemWidth)
            .BackgroundColor(item.IsHover ? Colors.Gray.WithAlpha(0.1f) : Colors.Transparent)
            .OnPointerExited(_ => { SetState(x => item.IsHover = false); })
            .OnPointerMoved(_ => { SetState(x => item.IsHover = true); })
            .OnTapped(() => {
                Clipboard.SetTextAsync(item.Prompt.Key);
                CurrentPage.DisplayAlert(L(Languages.Key.Copy),L(Languages.Key.CopySuccess),L(Languages.Key.Ok));
            });
        return body;
    }

    private void OnRemainingItemsThresholdReached() {
        SetState(s => s.Load(Load));
    }

    private async Task Load() {
        if (Store.Source == null) return;
        var promptService = Services.GetService<IPromptService>()!;
        var prompts = await promptService.GetPromptsAsync(Store.Source.Path, _key, query: null);
        var observableCollection = new ObservableCollection<PromptItem>(prompts.Select(x => new PromptItem(x)));
        SetState(s => s.Prompts = observableCollection);
    }


    private async void OnSourceChanged(Source obj) {
        await Load();
    }
    
    protected override async void OnMounted() {
        Store.OnSourceChanged += OnSourceChanged;
        await Load();
        base.OnMounted();
    }

    // protected override async Task OnParametersSetAsync()
    // {
    //     Prompts = await promptService.PromptsAsync(Key);
    // }
    //

    protected override void OnWillUnmount() {
        Store.OnSourceChanged -= OnSourceChanged;
        base.OnWillUnmount();
    }
}