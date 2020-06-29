FROM ubuntu:18.04
ENV LANG ja_JP.UTF-8
ENV PATH /docfx:${PATH}
ENV DEBIAN_FRONTEND noninteractive
ENV TZ Asia/Tokyo

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

RUN wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update -y && \
    apt-get install -y --no-install-recommends \
        aspnetcore-runtime-3.1 \
        dotnet-sdk-3.1

ADD key.txt .
RUN cat key.txt | apt-key add - && \
    echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" | tee /etc/apt/sources.list.d/mono-official-stable.list && \
    apt-get update -y && \
    apt-get install -y --no-install-recommends \
        mono-complete \
        mono-runtime && \
    apt-get update -y && \
    apt-get upgrade -y && \
    rm key.txt

RUN wget https://github.com/wkhtmltopdf/wkhtmltopdf/releases/download/0.12.5/wkhtmltox_0.12.5-1.bionic_amd64.deb && \
    apt-get install -y ./wkhtmltox_0.12.5-1.bionic_amd64.deb && \
    rm ./wkhtmltox_0.12.5-1.bionic_amd64.deb

RUN npm install -g npm && \
    npm cache clean && \
    npm install n -g && \
    n stable && \
    ln -sf /usr/local/bin/node /usr/bin/node

ADD https://github.com/dotnet/docfx/releases/download/v2.49/docfx.zip /
RUN unzip /docfx.zip -d /docfx && \
    rm /docfx.zip
ADD entrypoint.sh /usr/local/bin/docfx
RUN chmod 777 /usr/local/bin/docfx && \
    chmod 777 /docfx/docfx.exe && \
    rm -rf /var/lib/apt/lists/* /tmp/*

WORKDIR /work