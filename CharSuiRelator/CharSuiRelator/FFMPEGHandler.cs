/*
 * Copyright (c) PeroPeroGames Co., Ltd.
 * Author: CharSui
 * Created On: 2024.10.11
 * Description: 用于提供对FFMPEG的调用
 */

using System.Diagnostics;

namespace CharSuiTalker;

public class FFMPEGHandler
{
    public void CheckTool()
    {
        
    }
    

    public void Invoke()
	{
		    string toolPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tool", "ffmpeg.exe");
            string inputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input");
            string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");


            // 确保Output目录存在

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }


            // 检查Input目录是否存在
            if (!Directory.Exists(inputDir))
            {
                Console.WriteLine("Input目录不存在！");
                return;
            }


            // 获取输入视频文件
            var videoExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv", ".flv" }; // 添加你需要支持的视频格式

            var videoFiles = Directory.GetFiles(inputDir)
                                      .Where(file => videoExtensions.Contains(Path.GetExtension(file).ToLower()))
                                      .ToList();


            if (videoFiles.Count == 0)
            {
                Console.WriteLine("Input目录中没有找到任何视频文件！");
                return;
            }

            if (videoFiles.Count > 1)
            {
                Console.WriteLine("Input目录中有多个视频文件，请确保只有一个视频文件存在！");
                return;
            }

            string inputFile = videoFiles.First();
            // 设置输出文件路径
            string outputVideo = Path.Combine(outputDir, "output_video.mp4");
            string outputAudio = Path.Combine(outputDir, "output_audio.mp3");


            // 构建ffmpeg命令来拆分视频和音频

            string arguments = $"-i \"{inputFile}\" -an \"{outputVideo}\" -vn \"{outputAudio}\"";


            // 创建并启动进程
            Process process = new Process();
            process.StartInfo.FileName = toolPath;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

            Console.WriteLine("正在拆分视频和音频...");


            try
            {
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

            }
            catch (Exception ex)
            {

                Console.WriteLine("发生错误: " + ex.Message);
            }


            if (process.ExitCode == 0)
            {
                Console.WriteLine("处理完成！");
            }
            else
            {
                Console.WriteLine("视频处理失败，查看错误信息进行调试。");
            }
	}
}