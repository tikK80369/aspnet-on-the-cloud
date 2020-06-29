# ASP.NET とASP.NET Core

ASP.NET とASP.NET Core それぞれの概要、違い、開発環境等を調査してまとめる。

## ASP.NET 概要

ASP.NET は、HTML、CSS、および JavaScript を使用して優れた web サイトや web アプリケーションを構築するための無料の web フレームワークです。
Web API を作成し、Web ソケットなどのリアルタイム テクノロジを使用できます。

## ASP.NET Core 概要

ASP.NET Core は、インターネットに接続された最新のクラウド対応アプリを構築するための、クロス プラットフォームで高パフォーマンスのオープン ソース フレームワークです。
ASP.NET は、Windows以外のOSでは開発することができないという問題点がありました。その点を解決すべく、マルチプラットフォームに対応したASP.NET Core が登場しました。
ASP.NET CoreはASP.NET 4.xを再設計したものです。

# それぞれの違い

| | ASP.NET 4.x | ASP.NET Core |
| -- | -- | -- |
| 利用できるOS | Windowsのみ | Windows、macOS、Linux |
| 利用できる言語 | C#、VB、F# | C#、F# |
| 利用できる統合開発環境 | Visual Studio | Visual Studio<br>Visual Studio for Mac<br>Visual Studio Code |
| 対応している機能 | Webフォーム、MVC、SignalR、Web API | MVC、SignalR、Web API |
| サーバーアプリケーション<br>フレームワーク | .NET Framework | .NET Core |

> [!NOTE]
> ASP.NET CoreはWebフォームに対応していません。Webフォームでは、IISというWindows用のWebサーバーを使用しているためです。


> [!WARNING]
> ASP.NET 4.xのすべてのアプリがASP.NET Coreで動くというわけではないので注意が必要です。

# サーバーアプリケーション フレームワーク

## .NET Core を選択する場合
1. クロスプラット フォームが必要である。
2. マイクロサービスが対象である。
3. Docker コンテナーを使用している。
4. 高パフォーマンスでスケーラブルなシステムが必要である。
5. 1 つのアプリケーションに複数の .NET バージョンが必要である。

## .NET Framework を選択する場合

1. 現在、アプリで .NET Framework を使用している。
2. アプリが .NET Core で使用できないサードパーティ製の .NET ライブラリや NuGet パッケージを使用している。
3. アプリで、.NET Core で使用できない .NET テクノロジを使用している。
4. アプリで、.NET Core をサポートしていないプラットフォームを使用している。

# 参考文献

- [ASP.NET 概要](https://docs.microsoft.com/ja-jp/aspnet/overview)
- [ASP.NET Core の概要](https://docs.microsoft.com/ja-jp/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-3.1)
- [ASP.NET 4.x と ASP.NET Core の選択](https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/choose-aspnet-framework?view=aspnetcore-3.1)
- [サーバー アプリでの .NET Core または .NET Framework の使用](https://docs.microsoft.com/ja-jp/dotnet/standard/choosing-core-framework-server?toc=%2Faspnet%2Fcore%2Ftoc.json&bc=%2Faspnet%2Fcore%2Fbreadcrumb%2Ftoc.json&view=aspnetcore-3.1)
- [ASP.NET Core を使ってできることとは？](https://www.fenet.jp/dotnet/column/environment/1344/)