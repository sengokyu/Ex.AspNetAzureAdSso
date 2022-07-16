var builder = WebApplication.CreateBuilder(args);

// Razorを使用するため
builder.Services.AddRazorPages();

var app = builder.Build();

// 静的ファイルを使用
app.UseStaticFiles();
// パスとRazorを紐付け
app.MapRazorPages();

app.Run();
