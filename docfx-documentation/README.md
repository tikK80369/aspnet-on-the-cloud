# DocFXを使用したドキュメントの作成
## 環境構築
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
  - dotnet CLIを使用するために必要
- [Build Tools for Visual Studio 2019](https://visualstudio.microsoft.com/ja/downloads/)
  - DocFXのビルドに必要
  - 起動後に以下のコンポーネントをインストール
    - .NET Core 3.1 LTS ランタイム
    -.NET Core SDK
- [DocFX](https://dotnet.github.io/docfx/)
  - ドキュメントの作成に必要
  - windowsならchocoでインストールできる
    - PowerShellを管理者権限で起動して以下のコマンドでインストール
      ```
      choco install docfx -y
      ```
- [NuGet](https://www.nuget.org/downloads)
  - DocFXのプラグインをインストールするために必要
- [GraphViz](http://www.graphviz.org/download/)
  - DocFXのプラグインでPlant-umlの図を描画するために必要
  - windowsならchocoでインストールできる
    - PowerShellを管理者権限で起動して以下のコマンドでインストール
      ```
      choco install graphviz -y
      ```
- [Node.js](https://nodejs.org/ja/download/)
  - DocFXのビルドに必要
- [wkhtmltopdf](https://wkhtmltopdf.org/downloads.html)
  - ドキュメントのPDF化に必要
  - windowsならchocoでインストールできる
    - PowerShellを管理者権限で起動して以下のコマンドでインストール
      ```
      choco install wkhtmltopdf -y
      ```

## プログラム概要
1. 以下のコマンドで実行する。
  ```
  dotnet run
  ```
2. ターミナルに以下が表示される。
  ```
  Press <Enter> only to exit; otherwise, enter a string and press <Enter>:

  Please enter two numbers.
  ```
3. 数字を二つ入力すると、四則演算の結果がそれぞれ表示される。
  ```
  Press <Enter> only to exit; otherwise, enter a string and press <Enter>:

    Please enter two numbers.
    6
    2
    6 + 2 = 8
    6 - 2 = 4
    6 * 2 = 12
    6 / 2 = 3
    Please enter two numbers.
  ```
4. 何も入力せずにEnterを押すとプログラムが終了する。

## ドキュメントの作成
1. ディレクトリの移動
  ```
  cd docfx
  ```
2. ソースコードコメントのyml化
  ```
  docfx metdata
  ```
3. プラグインのインストール
  ```
  nuget install DocFx.Plugins.PlantUml -ExcludeVersion -OutputDirectory .
  ```
4. ビルド
  ```
  docfx build
  ```
  _siteフォルダににhtml等が展開される
5. ウェブページのホスティング
  ```
  docfx serve _site
  ```
  ブラウザでlocalhost:8080にアクセスしてドキュメントを確認できる<br>
  4と5は以下のコマンドでまとめて実施できる
  ```
  docfx --serve
  ```
6. ドキュメントのPDF化
  ```
  docfx pdf
  ```
  _site_pdfフォルダにPDFが作成される。

### 課題
- PDFのフォントが汚い
  - docfxはテンプレートを作成できるのでどうにかなりそう。
- PDFに画像が貼れない
  - PDFにすると画像が消える。

## DocFXのファイルの説明
- documents/image
  - 画像が保存されているフォルダ
- documents/toc.md
  - ドキュメントメニューの構成を定義している
- documents/**.md
  - ドキュメントメニューの各ページ
- pdf/toc.yml
  - PDF化した時の構成を定義している
- restapi/toc.md
  - REST API ドキュメントメニューの構成を定義している
- restapi/**.json
  - REST APIの仕様をSwaggerのJSON形式で定義している
- docfx.json
  - DocFXで使用するファイルや設定を定義している
- index.md
  - 初期画面
- toc.yml
  - WEBページの構成を定義する

### 参考
[DocFX Getting Started](https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html)