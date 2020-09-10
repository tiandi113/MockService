# 项目介绍
在前后端分离的项目过程中，前端开发经常需要后台提供Mock数据，当然前端也有mockjs等一些模拟技术，本项目主要基于.netcore后端进行前端数据的模拟。

# 使用说明
1、appsetting.json配置文件中增加mock数据的配置

"mock": [
    {
      "api": "/",           //接口路径
      "mockjs": "default", //Mock文件中对应的json文件名
      "method": "GET"      //接口类型（主要为了应对restful）
    },
    {
      "api": "/test",
      "mockjs": "mock.json",
      "method": "GET"
    }
  ]
  
  2、Mock文件夹中添加对应的json文件，将api对应的json数据写入文件
  
  3、前端接口调用时需要前端在header中添加一个Mock 为true 的信息
  
