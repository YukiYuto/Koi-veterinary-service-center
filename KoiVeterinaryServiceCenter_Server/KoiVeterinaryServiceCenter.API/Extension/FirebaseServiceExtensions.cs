using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;

namespace KoiVeterinaryServiceCenter.API.Extension;

public static class FirebaseServiceExtensions
{
    public static IServiceCollection AddFirebaseService(this IServiceCollection services)
    {
        var credentialPath = Path.Combine(Directory.GetCurrentDirectory(),
            "koiveterinaryservicecent-33eaa-firebase-adminsdk-yf6vy-7ce064ef9d.json");
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(credentialPath)
        });
        services.AddSingleton(StorageClient.Create(GoogleCredential.FromFile(credentialPath)));
        services.AddScoped<IFirebaseService, FirebaseService>();
        return services;
    }
}