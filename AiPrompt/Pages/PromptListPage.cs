using System.Collections.ObjectModel;
using AiPrompt.Components;
using AiPrompt.Model.Entity;
using AiPrompt.Model.Service;
using MauiReactor;

namespace AiPrompt.Pages;

public class PromptItem(Prompt prompt) {
    public Prompt Prompt { get; set; } = prompt;
    public bool IsHover { get; set; }
}


public class PromptListState : PageBaseState {
    public ObservableCollection<PromptItem> Prompts { get; set; } = [];

    public IItemsLayout ItemsLayout { get; set; } = new VerticalGridItemsLayout(4);
}

public class PromptListPage(string key) : BaseComponent<PromptListState> {


    private readonly int _itemWidth = 240;

    public override VisualNode Render()
        => ContentPage(CollectionView()
            .ItemsLayout(State.ItemsLayout)
            .ItemsSource(State.Prompts, ItemTemplate)
            .RemainingItemsThreshold(5)
            .ItemsUpdatingScrollMode(MauiControls.ItemsUpdatingScrollMode.KeepScrollOffset)
            .OnRemainingItemsThresholdReached(OnRemainingItemsThresholdReached)
            .GridRow(1)
            .OnSizeChanged(x => {
                    SetState(s => s.ItemsLayout = new VerticalGridItemsLayout((int)(x.Width / _itemWidth)));
                }
            )
        );

    private VisualNode ItemTemplate(PromptItem item) {
        var option = Frame(Grid(Image().WidthRequest(160).HeightRequest(160)
                .Source(item.Prompt.Image ?? ThemeDarkLight("music_library_dark.png", "music_library_light.png"))
            )
        ).BackgroundColor(Colors.Transparent);

        var content = Label(item.Prompt.Name).Center();

        var body = Frame(Grid("Auto", "Auto,*", option, content.GridRow(1))
                .Rows("Auto,*").RowSpacing(10)
            ).CornerRadius(10)
            .WidthRequest(_itemWidth)
            .BackgroundColor(item.IsHover ? Colors.Gray.WithAlpha(0.1f) : Colors.Transparent)
            .OnPointerExited(_ => { SetState(x => item.IsHover = false); })
            .OnPointerMoved(_ => { SetState(x => item.IsHover = true); })
            .OnTapped(() => { });
        return body;
    }

    private async void OnRemainingItemsThresholdReached() {
        SetState(s => s.Load(Load));
    }

    private async Task Load() {
        var promptService = Services.GetService<IPromptService>()!;
        var prompts = await promptService.GetPromptsAsync(key);
        var observableCollection = new ObservableCollection<PromptItem>(prompts.Select(x => new PromptItem(x)));
        SetState(s => s.Prompts = observableCollection);
    }


    protected override async void OnMounted() {
        await Load();
        base.OnMounted();
    }
    // protected override async Task OnParametersSetAsync()
    // {
    //     Prompts = await promptService.PromptsAsync(Key);
    // }
    //

    protected override void OnWillUnmount() {
        base.OnWillUnmount();
    }
}