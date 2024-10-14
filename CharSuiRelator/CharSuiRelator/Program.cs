/*
 * Author: CharSui
 * Created On: 2024.10.14
 * Description: Logic Entry
 * 由于大部分的ASR服务或者TTS服务都有对内容长度进行限制。主逻辑提供基于FFMPEG的视频拆分
 * 核心逻辑如下：
 * 1、获得视频(本地读取/提供链接进行下载)
 * 2、ffmpeg提取音频
 * 3、选择对应的配置：由服务提供商约束对内容裁切的参数，对音频进行裁切(但好像一般都支持)
 * 4、ASR服务：对视频内容进行文本识别
 * 5、Translate服务：对文本内容进行目标语言翻译
 * 6、TTS服务：将翻译后的文本进行音频输出
 * 76、合成到视频中，产出视频
 */
// See https://aka.ms/new-console-template for more information

using System.Reflection;

Console.WriteLine($"Hello, here is the CharSuiRelator{Assembly.GetExecutingAssembly().GetName().Version}, a tool chain for translate video to the localizatio TTS video");
Console.ReadLine();