using System.Collections.ObjectModel;
using AiPrompt.Model.Entity;
using AiPrompt.Model.Service;
using MauiReactor;

namespace AiPrompt.Components;

public class SourcePickerState : BaseState {
    public ObservableCollection<Source> Sources { get; set; } = [];

    public int SelectIndex { get; set; }
}
public class SourcePicker :BaseComponent<SourcePickerState> {
    
    public override Picker Render() => new Picker()
        .ItemsSource(State.Sources.Select(x=>x.Name).ToList())
        .SelectedIndex(State.SelectIndex)
        .OnSelectedIndexChanged(OnSelectedIndexChanged);

    private void OnSelectedIndexChanged(int index) {
        SetState(s=>s.SelectIndex = index);
        Store.Source = State.Sources[index];
    }
    protected override async void OnMounted() {
        var sourceService = Services.GetService<ISourceService>()!;
        var allSource = await sourceService.AllSourceAsync(); 
        SetState(s=> {
            s.Sources = new ObservableCollection<Source>(allSource);
        });
        if (Store.Source is null && allSource is {Count:>0}) {
            Store.Source = allSource[0];
        }
        if (Store.Source is not null && allSource is {Count:>0}) {
            var findIndex = allSource.FindIndex(x=>x.Path == Store.Source.Path);
            SetState(s=>s.SelectIndex = findIndex);
        }
        base.OnMounted();
    }

}

