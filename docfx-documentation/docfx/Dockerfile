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