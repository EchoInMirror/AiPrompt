using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AiPrompt.Model.Utils;


public static class Ioc {
    private static IServiceProvider? _serviceProvider;

    public static void SetProvider(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    public static T Resolve<T>(Type type) where T : notnull {
        EnsureProviderInitialized(_serviceProvider);
        return (T)_serviceProvider.GetRequiredService(type);
    }
    
    public static T Resolve<T>() where T : notnull {
        EnsureProviderInitialized(_serviceProvider);
        return _serviceProvider.GetRequiredService<T>();
    }
    
    public static IEnumerable<T> Resolves<T>() where T : notnull {
        EnsureProviderInitialized(_serviceProvider);
        return _serviceProvider.GetServices<T>();
    }
    private static void EnsureProviderInitialized([NotNull] IServiceProvider? serviceProvider) {
        if (serviceProvider is null) {
            throw new InvalidOperationException(
                "Service provider hasn't been initialized yet. Please call the SetProvider method prior to using this"
            );
        }
    }
}