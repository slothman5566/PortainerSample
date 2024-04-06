# PortainerSample

## 流程:
1. 建立Docker File及Repo
這邊先直接用Vs產生的,上傳到Github上
需要多建立docker-compose.yml 給portainer使用
```
version: '3.8'

services:
  portainer-sample:
    image: ghcr.io/slothman5566/portainer-sample:latest
    restart: always
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
```

2. 建立Github workfolw
[Creating and managing GitHub Actions workflows](https://docs.github.com/en/actions/using-workflows)

```
name: Docker Image CI

on:
  push:
    branches: [ "master" ]
  pull_request:
    branches: [ "master" ]

jobs:
  build:
    runs-on: ubuntu-latest
    environment: Development
    steps:
      - uses: actions/checkout@v3
      - name: Build the Docker image
        run: |
          echo "${{ secrets.GITHUB_TOKEN }}" | docker login ghcr.io -u ${{ github.actor }} --password-stdin  
          docker build . --file ./PortainerSample/Dockerfile --tag portainer-sample:$(date +%s) --tag ghcr.io/slothman5566/portainer-sample:latest
          docker push ghcr.io/slothman5566/portainer-sample:latest
```

workflow作用是檢查master是否有commit,
將commit後的結果打包成image上傳至ghcr

[image 位置](https://github.com/slothman5566/PortainerSample/pkgs/container/portainer-sample)

3. 設定portainer
使用docker安裝portainer
```
docker run -d -p 9000:9000 --name=portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce`
```
跳過帳號設定直接到建立Stacks上
![image](https://github.com/slothman5566/PortainerSample/blob/e892946430a8dd6b3a1fec9b745429ccb25f5903/2024-04-06%20222044.png)
* Repository URL:要使用的Github Repo
* Repository reference: 要偵測的分支
* Compose path: Repo上部屬用
* GitOps updates:選擇要用哪種方式更新,這邊直接用Polling

設定完成後簡易的CI/CD功能就完成