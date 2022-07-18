# ASP.NET Azure Active Directory Single Sign-On example

## 事前に必要なもの

1. [.NET SDK 6](https://dotnet.microsoft.com/en-us/download)
2. [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
3. Azure AD アプリケーション（登録方法は[後述](#azure-ad-アプリケーションの登録方法)）。

## はじめかた

1. ExAzureAdSso.Web/Properties/launchSettings.sample.json ファイルをコピーして、launchSettings.json ファイルを作成します。
2. launchSettings.json ファイルを編集します（詳細は[後述](#launchsettingsjson-の編集方法)）。
3. `dotnet run --project ExAzureAdSso.Web` コマンドを実行します。
4. ブラウザでプラベートウィンドウ（Chrome の場合はシークレットウィンドウ）を開き、 http://localhost:5000/ へアクセスします。

## Azure AD アプリケーションの登録方法

Azure CLI は、事前にログインしておいてください。

1. アプリケーションを登録します。アプリ名は、適当に決めてください。

```console
az ad app create --display-name アプリ名
```

出力される ID と、Application ID を控えておきます。Application ID は、プログラムを設定するのに必要です。

2. リダイレクト URL を登録します。

```console
az ad app update --id 控えておいたID --web-redirect-uris http://localhost:5000/signin-oidc
```

## launchSettings.json の編集方法

以下の環境変数を変更します。

| 環境変数                       | 内容                            |
| :----------------------------- | :------------------------------ |
| ASPNETCORE_AzureAd\_\_Domain   | AzurAD のドメイン名             |
| ASPNETCORE_AzureAd\_\_TenantId | AzureAD の Tenant ID            |
| ASPNETCORE_AzureAd\_\_ClientId | 登録したアプリの Application ID |

**Note:** プロダクション環境に展開するときは、`launchSettings.json`ではなく、実際の環境変数を設定します。
