# DocFXを使用したドキュメントの作成
DocFXを使用したドキュメントの作成方法について記載する。

# 環境構築

## Windows PC
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
  - dotnet CLIを使用するために必要

- [Build Tools for Visual Studio 2019](https://visualstudio.microsoft.com/ja/downloads/)
  - DocFXのビルドに必要
  - 起動後に以下のコンポーネントをインストール
    - .NET Core 3.1 LTS ランタイム
    - .NET Core SDK

- [DocFX](https://dotnet.github.io/docfx/)
  - ドキュメントの作成に必要
  - windowsならchocoでインストールできる
    - PowerShellを管理者権限で起動して以下のコマンドでインストール
      
      ```bash
      choco install docfx -y
      ```

- [NuGet](https://www.nuget.org/downloads)
  - DocFXのプラグインをインストールするために必要

- [GraphViz](http://www.graphviz.org/download/)
  - DocFXのプラグインでPlant-umlの図を描画するために必要
  - windowsならchocoでインストールできる
    - PowerShellを管理者権限で起動して以下のコマンドでインストール

      ```bash
      choco install graphviz -y
      ```

- [Node.js](https://nodejs.org/ja/download/)
  - DocFXのビルドに必要

- [wkhtmltopdf](https://wkhtmltopdf.org/downloads.html)
  - ドキュメントのPDF化に必要
  - windowsならchocoでインストールできる
    - PowerShellを管理者権限で起動して以下のコマンドでインストール

      ```bash
      choco install wkhtmltopdf -y
      ```

## Dockerで環境構築

- 下記のDockerfileをビルドする。

  ```Dockerfile:Dockerfile
  FROM ubuntu:18.04
  ENV LANG ja_JP.UTF-8
  ENV PATH /docfx:${PATH}
  ENV DEBIAN_FRONTEND noninteractive
  ENV TZ Asia/Tokyo
  ADD entrypoint.sh /usr/local/bin/docfx

  # aptパッケージインストール
  RUN apt-get update -y && \
      apt-get install -y --no-install-recommends \
          apt-transport-https \
          ca-certificates \
          dirmngr \
          fonts-ipafont \
          gnupg \
          libssl1.0-dev \
          nodejs \
          npm \
          nuget \
          wget \
          unzip

  # ASP.NET環境構築
  RUN wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
      dpkg -i packages-microsoft-prod.deb && \
      apt-get update -y && \
      apt-get install -y --no-install-recommends \
          aspnetcore-runtime-3.1 \
          dotnet-sdk-3.1

  # monoインストール
  # ----- noproxy環境の場合 -----
  # RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF && \

  # ------ proxy環境の場合 ------
  ADD key.txt .
  RUN cat key.txt | apt-key add - && \
  # ----------------------------
      echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" | tee /etc/apt/sources.list.d/mono-official-stable.list && \
      apt-get update -y && \
      apt-get install -y --no-install-recommends \
          mono-complete \
          mono-runtime && \
      apt-get update -y && \
      apt-get upgrade -y && \
      rm key.txt

  # wkhtmltopdfインストール
  RUN wget https://github.com/wkhtmltopdf/wkhtmltopdf/releases/download/0.12.5/wkhtmltox_0.12.5-1.bionic_amd64.deb && \
      apt-get install -y ./wkhtmltox_0.12.5-1.bionic_amd64.deb && \
      rm ./wkhtmltox_0.12.5-1.bionic_amd64.deb

  # nodejsインストール
  RUN npm install -g npm && \
      npm cache clean && \
      npm install n -g && \
      n stable && \
      ln -sf /usr/local/bin/node /usr/bin/node

  # DocFXインストール（v2.5以上の場合、metadataオプションが動作しない）
  ADD https://github.com/dotnet/docfx/releases/download/v2.49/docfx.zip /
  RUN unzip /docfx.zip -d /docfx && \
      rm /docfx.zip
  RUN chmod 777 /usr/local/bin/docfx && \
      chmod 777 /docfx/docfx.exe && \
      rm -rf /var/lib/apt/lists/* /tmp/*

  WORKDIR /work
  ```
  ```bash:entrypoint.sh
  #!/bin/bash

  mono "/docfx/docfx.exe" "$@"
  ```
  プロキシ環境でapt-keyコマンドが動作しなかったので[こちら](https://keyserver.ubuntu.com/pks/lookup?search=0x3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF&fingerprint=on&op=index)から公開鍵をダウンロードした。
  ```txt:key.txt
  -----BEGIN PGP PUBLIC KEY BLOCK-----

  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
  kHFYv2XsdQ==
  =45Mw
  -----END PGP PUBLIC KEY BLOCK-----
  ```

- 作成した環境にファイルをマウントして利用する。

  ```bash
  docker run --rm -it -v /`pwd`:/work <image名> bash
  ```

# ドキュメントの作成

## DocFXのファイルの説明
- docfx.json
  - DocFXで使用するファイルや設定を定義している
- index.md
  - 初期画面
- toc.yml
  - WEBページの構成を定義する
- pdf/toc.yml
  - PDFの構成を定義している
- pdf/cover.yml
  - PDFのカバーページ
- **/.md
  - 各ページ
- template/*
  - デザインなどを定義している資材が格納されている
- **/*.json
  - REST APIの仕様をSwaggerのJSON形式で定義している

## ドキュメントの作成手順
1. ディレクトリの移動

  ```bash
  cd <docfx.jsonが存在するディレクトリ>
  ```

2. ソースコードコメントのyml化
  ```bash
  docfx metadata
  ```

3. プラグインのインストール<br>
  Plantumlの描画が可能になる
  ```bash
  nuget install DocFx.Plugins.PlantUml -ExcludeVersion -OutputDirectory .
  ```

4. ドキュメントのPDF化
  ```bash
  docfx pdf
  ```
  _site_pdfフォルダにPDFが作成される。

5. ビルド
  ```bash
  docfx build
  ```
  _siteフォルダににhtml等が展開される

6. ウェブページのホスティング
  ```bash
  docfx serve _site -n "*" -p 8080
  ```
  ブラウザでlocalhost:8080にアクセスしてドキュメントを確認できる<br>
  Windowsの場合、4と5は以下のコマンドでまとめて実施できる
  ```bash
  docfx --serve
  ```

# 課題
- WindowsだとPDFのフォントが汚い
  - フォントをインストールすれば、どうにかなりそう。

- WindowsだとPDFに画像が貼れない
  - PDFにすると画像が消える。

- PDFにするとnoteやwarningなどが崩れる
  - ボックスが崩れる

- Dockerfileのサイズが大きい

# CIへの組み込み
GitHub Actionsで環境を構築するのはインストールするものが多くて難しいので、GitHub Actions上でDockerを使用する必要がある。<br>
しかし、上に記載したDockerfileはビルドに約10分ほどかかり、毎回ビルドするのは避けたい。<br>
そこで、事前にGitHub Package RegistryにビルドしたイメージをPushしておき、GitHub Actions上でそのイメージをpullして利用する。

# 参考文献
- [DocFX Getting Started](https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html)
- [mono Download](https://www.mono-project.com/download/stable/#download-lin)
- [Ubuntu に .NET Core SDK または .NET Core ランタイムをインストールする](https://docs.microsoft.com/ja-jp/dotnet/core/install/linux-ubuntu)