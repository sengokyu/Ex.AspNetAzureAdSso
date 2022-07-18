using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// ログに機微情報を出力したくても、以下のコードは有効にならない。
// appsettings.Development.jsonの中で設定する。
// IdentityModelEventSource.ShowPII = true;

// 認証を有効化 AzureAD単一組織を使用
builder.Services.AddAuthentication()
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

// 認可ポリシー 
var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
    // OpenIdをデフォルトにする
    .AddAuthenticationSchemes(OpenIdConnectDefaults.AuthenticationScheme)
    // 認証済みユーザ必須
    .RequireAuthenticatedUser()
    .Build();

// 認可を有効化
builder.Services.AddAuthorization(options =>
{
    // 最終的に有効になるデフォルトのポリシー    
    options.FallbackPolicy = requireAuthenticatedUserPolicy;
});

// Razorを有効化
builder.Services.AddRazorPages();

var app = builder.Build();

// 静的ファイルを使用
app.UseStaticFiles();
// 認証を使用
app.UseAuthentication();
// 認可を使用
app.UseAuthorization();
// パスとRazorを紐付け
app.MapRazorPages();

app.Run();
