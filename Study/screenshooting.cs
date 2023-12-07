using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CapturaTelasEVideo
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputFolder = "C:\\Caminho\\Para\\Salvar\\AsCapturas\\";
            Directory.CreateDirectory(outputFolder);

            AbrirPastasCapturarTelas(outputFolder);
            CriarVideoDasTelas(outputFolder);
        }

        static void AbrirPastasCapturarTelas(string outputFolder)
        {
            try
            {
                // Abrir a pasta Downloads
                Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads");
                System.Threading.Thread.Sleep(2000);
                CaptureScreen(outputFolder + "downloads.png");

                // Fechar a janela da pasta Downloads
                FecharJanelaProcesso("explorer", "Downloads");

                // Abrir a Lixeira
                Process.Start("explorer.exe", "shell:RecycleBinFolder");
                System.Threading.Thread.Sleep(2000);
                CaptureScreen(outputFolder + "lixeira.png");
                FecharJanelaProcesso("explorer", "RecycleBinFolder");

                // Abrir a pasta Documentos
                Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                System.Threading.Thread.Sleep(2000);
                CaptureScreen(outputFolder + "documentos.png");
                FecharJanelaProcesso("explorer", "Documents");

                // Abrir a pasta Imagens
                Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
                System.Threading.Thread.Sleep(2000);
                CaptureScreen(outputFolder + "imagens.png");
                FecharJanelaProcesso("explorer", "Pictures");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        static void CaptureScreen(string savePath)
        {
            try
            {
                Rectangle bounds = Screen.PrimaryScreen.Bounds;
                Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
                Graphics gfxScreenshot = Graphics.FromImage(screenshot);
                gfxScreenshot.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
                screenshot.Save(savePath, ImageFormat.Png);
                Console.WriteLine("Captura de tela salva em " + savePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao capturar a tela: " + ex.Message);
            }
        }

       static void FecharJanelaProcesso(string processo, string nomeJanela)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(processo))
                {
                    if (proc.MainWindowTitle.Contains(nomeJanela))
                    {
                        proc.Kill();
                        Console.WriteLine(nomeJanela + " fechada.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao fechar a janela: " + ex.Message);
            }
        }
    }
}
