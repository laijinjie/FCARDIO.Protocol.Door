# DoNetDrive.Protocol.Fingerprint

## 介绍

用于定义 符合 人脸机/指纹机 UDP/TCP协议文档的设备命令，含命令协议文档中的所有章节


## 软件架构
基于 netstandard2.0 ；



## 使用说明

~~~ c#
var mAllocator = ConnectorAllocator.GetAllocator();



var cmdDtl = CommandDetailFactory.CreateDetail(CommandDetailFactory.ConnectType.UDPClient, "192.168.1.56", 8101,
                CommandDetailFactory.ControllerType.Door88, "0000000000000000", "FFFFFFFF");
ReadSN cmd = new ReadSN(cmdDtl);
try
{
    await mAllocator.AddCommandAsync(cmd);
    var snResult = cmd.getResult() as SN_Result;
    Console.WriteLine(snResult.SNBuf);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

//释放
mAllocator.Dispose();
~~~



## 版本记录



### Ver 2.02
增加命令：人脸机消防开关，云筑网开关及重新拉取，点名机相关命令，人脸活体识别阈值

### Ver 2.03
新增加 ReadTransactionDatabaseByAll 命令，可读取历史记录。



### ver 2.05.0

修改命令ReadTransactionAndImageDatabase，修改此命令的参数 ImageDownloadCheckCallblack，回调时增加当前记录详情，另外修改读取记录照片的检测逻辑，增加检测命令详情中的photo字段。此字段为0则不读取照片。
