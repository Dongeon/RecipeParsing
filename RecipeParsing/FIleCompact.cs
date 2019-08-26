
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Zip;
using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
namespace RecipeParsing
{
    class FIleCompact
    {

        /// <summary>
        /// Unix에서 생성한 Z 파일의 압축해제
        /// </summary>
        /// <param name="zFileName">압축해제할 Z 파일이름</param>
        /// <returns>압축 해제 성공 여부</returns>
        public static bool UnUnixZ(string zFileName)
        {
            try
            {
                FileInfo fi = new FileInfo(zFileName);
                string directoryName = fi.DirectoryName;
                string fileName = Path.GetFileNameWithoutExtension(zFileName);
                string outputFileName = $"{directoryName}\\{fileName}";

                using (Stream inStream = File.OpenRead(zFileName))
                using (FileStream outStream = File.Create(outputFileName))
                {
                    int bytesRead;
                    byte[] buffer = new byte[4096];

                    while ((bytesRead = inStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            catch (Exception e)
            {
                throw new AggregateException("Fail FileCompression.UnUnixZ", e);
            }
            return true;
        }

        /// <summary>
        /// 특정 폴더를 ZIP으로 압축
        /// </summary>
        /// <param name="targetFolderPath">압축 대상 폴더 경로</param>
        /// <param name="zipFilePath">저장할 ZIP 파일 경로</param>
        /// <param name="password">압축 암호</param>
        /// <param name="isDeleteFolder">폴더 삭제 여부</param>
        /// <returns>압축 성공 여부</returns>
        public static bool Zip(string targetFolderPath, string zipFilePath, string password, bool isDeleteFolder)
        {
            // 폴더가 존재하는 경우에만 수행
            if (!Directory.Exists(targetFolderPath))
                return false;

            // 압축 대상 폴더의 파일 목록
            List<string> fileList = GenerateFileList(targetFolderPath);

            // find number of chars to remove. from orginal file path. remove '\'
            int pathLength = (Directory.GetParent(targetFolderPath)).ToString().Length + 1;

            // ZIP 스트림 생성.
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(zipFilePath)))
            {
                // 패스워드가 있는 경우 패스워드 지정
                if (password != null && password != string.Empty)
                    zipOutputStream.Password = password;

                // 암호화 레벨.(최대 압축)
                zipOutputStream.SetLevel(9);

                ZipEntry zipEntry;
                foreach (string fileName in fileList)
                {
                    zipEntry = new ZipEntry(fileName.Remove(0, pathLength));
                    zipOutputStream.PutNextEntry(zipEntry);

                    try

                    {
                        // 파일인 경우
                        if (!fileName.EndsWith(@"/"))
                        {
                            using (FileStream fileStream = File.OpenRead(fileName))
                            {
                                byte[] buffer = new byte[fileStream.Length];
                                fileStream.Read(buffer, 0, buffer.Length);
                                zipOutputStream.Write(buffer, 0, buffer.Length);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // 오류가 난 경우 생성 했던 파일을 삭제.
                        if (File.Exists(zipFilePath))
                            File.Delete(zipFilePath);

                        throw new AggregateException("FileCompression.Zip : Fail to ZipOutputStream", e);
                    }
                    finally
                    {
                        zipOutputStream.Finish(); // 압축 종료
                        zipOutputStream.Close();
                    }
                }
            }

            if (isDeleteFolder) // 폴더 삭제를 원할 경우 폴더 삭제
            {
                try
                {
                    Directory.Delete(targetFolderPath, true);
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.Zip : Fail to delete folder", e);
                }
            }

            return true;
        }

        /// <summary>
        /// 특정 폴더를 ZIP으로 압축
        /// </summary>
        /// <param name="targetFolderPath">압축 대상 폴더 경로</param>
        /// <param name="zipFilePath">저장할 ZIP 파일 경로</param>
        /// <param name="isDeleteFolder">폴더 삭제 여부</param>
        /// <returns>압축 성공 여부</returns>
        public static bool Zip(string targetFolderPath, string zipFilePath, bool isDeleteFolder)
        {
            // 폴더가 존재하는 경우에만 수행
            if (!Directory.Exists(targetFolderPath))
                return false;

            // 압축 대상 폴더의 파일 목록
            List<string> fileList = GenerateFileList(targetFolderPath);

            // find number of chars to remove. from orginal file path. remove '\'
            int pathLength = (Directory.GetParent(targetFolderPath)).ToString().Length + 1;

            // ZIP 스트림 생성.
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(zipFilePath)))
            {
                // 암호화 레벨.(최대 압축)
                zipOutputStream.SetLevel(9);

                ZipEntry zipEntry;
                foreach (string fileName in fileList)
                {
                    zipEntry = new ZipEntry(fileName.Remove(0, pathLength));
                    zipOutputStream.PutNextEntry(zipEntry);

                    try
                    {
                        // 파일인 경우
                        if (!fileName.EndsWith(@"/"))
                        {
                            using (FileStream fileStream = File.OpenRead(fileName))
                            {
                                byte[] buffer = new byte[fileStream.Length];
                                fileStream.Read(buffer, 0, buffer.Length);
                                zipOutputStream.Write(buffer, 0, buffer.Length);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // 오류가 난 경우 생성 했던 파일을 삭제.
                        if (File.Exists(zipFilePath))
                            File.Delete(zipFilePath);

                        throw new AggregateException("FileCompression.Zip : Fail to ZipOutputStream", e);
                    }
                    finally
                    {
                        zipOutputStream.Finish(); // 압축 종료
                        zipOutputStream.Close();
                    }
                }
            }

            if (isDeleteFolder) // 폴더 삭제를 원할 경우 폴더 삭제
            {
                try
                {
                    Directory.Delete(targetFolderPath, true);
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.Zip : Fail to delete folder", e);
                }
            }

            return true;
        }

        public static void Tgz()
        {

        }

        public static void Tar()
        {

        }

        /// <summary>
        /// 파일, 폴더 목록 생성
        /// </summary>
        /// <param name="directory">폴더 경로</param>
        /// <returns>폴더, 파일 목록(ArrayList)</returns>
        private static List<string> GenerateFileList(string directory)
        {
            var fileList = new List<string>();

            bool isEmpty = true;

            try
            {
                foreach (string fileName in Directory.GetFiles(directory)) // 폴더 내의 파일 추가
                {
                    fileList.Add(fileName);
                    isEmpty = false;
                }
            }
            catch (Exception e)
            {
                throw new AggregateException("FileCompression.GenetateFileList : Fail to add files", e);
            }

            if (isEmpty)
            {
                try
                {
                    if (Directory.GetDirectories(directory).Length == 0) // 파일이 없고, 폴더도 없는 경우 자신의 폴더 추가
                        fileList.Add(directory + @"/");
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.GenetateFileList : Fail to add self", e);
                }
            }

            try
            {
                foreach (string directoryName in Directory.GetDirectories(directory)) // 폴더 내 폴더 목록
                {
                    foreach (string fileName in GenerateFileList(directoryName)) // 해당 폴더로 다시 GenerateFileList 재귀 호출
                    {
                        fileList.Add(fileName); // 해당 폴더 내의 파일, 폴더 추가
                    }
                }
            }
            catch (Exception e)
            {
                throw new AggregateException("FileCompression.GenetateFileList : Fail to add directories", e);
            }

            return fileList;
        }

        /// <summary>
        /// ZIP 압축 파일 풀기
        /// </summary>
        /// <param name="zipFilePath">ZIP파일 경로</param>
        /// <param name="unZipTargetFolderPath">압축 풀 폴더 경로</param>
        /// <param name="password">해지 암호</param>
        /// <param name="isDeleteZipFile">zip파일 삭제 여부</param>
        /// <returns>압축 풀기 성공 여부 </returns>
        public static bool Unzip(string zipFilePath, string unZipTargetFolderPath, string password, bool isDeleteZipFile)
        {
            if (!File.Exists(zipFilePath)) // ZIP 파일이 있는 경우만 수행
                return false;

            using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath))) // ZIP 스트림 생성
            {
                if (password != null && password != string.Empty) // 패스워드가 있는 경우 패스워드 지정
                    zipInputStream.Password = password;

                try
                {
                    ZipEntry theEntry;

                    while ((theEntry = zipInputStream.GetNextEntry()) != null) // 반복하며 파일을 가져옴
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name); // 폴더 명칭
                        string fileName = Path.GetFileName(theEntry.Name); // 파일 명칭

                        Directory.CreateDirectory(unZipTargetFolderPath + directoryName); // 폴더 생성

                        if (fileName == string.Empty) // 파일 이름이 없으면 Pass
                            continue;

                        using (FileStream streamWriter = File.Create((unZipTargetFolderPath + theEntry.Name))) // 파일 스트림 생성.(파일생성)
                        {
                            byte[] data = new byte[2048];

                            while (true) // 파일 복사
                            {
                                int size = zipInputStream.Read(data, 0, data.Length);

                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }

                            streamWriter.Close(); // 파일스트림 종료
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.Unzip : Fail to ZipInputStream", e);
                }
                finally
                {
                    zipInputStream.Close(); // ZIP 파일 스트림 종료
                }
            }

            if (isDeleteZipFile) // ZIP파일 삭제를 원할 경우 파일 삭제
            {
                try
                {
                    File.Delete(zipFilePath);
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.Unzip : Fail to delete zip file", e);
                }
            }

            return true;
        }

        /// <summary>
        /// ZIP 압축 파일 풀기
        /// </summary>
        /// <param name="zipFilePath">ZIP파일 경로</param>
        /// <param name="unZipTargetFolderPath">압축 풀 폴더 경로</param>
        /// <param name="isDeleteZipFile">zip파일 삭제 여부</param>
        /// <returns>압축 풀기 성공 여부 </returns>
        public static bool Unzip(string zipFilePath, string unZipTargetFolderPath, bool isDeleteZipFile)
        {
            if (!File.Exists(zipFilePath)) // ZIP 파일이 있는 경우만 수행
                return false;

            using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath))) // ZIP 스트림 생성
            {
                try
                {
                    ZipEntry theEntry;

                    while ((theEntry = zipInputStream.GetNextEntry()) != null) // 반복하며 파일을 가져옴
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name); // 폴더 명칭
                        string fileName = Path.GetFileName(theEntry.Name); // 파일 명칭

                        Directory.CreateDirectory(unZipTargetFolderPath + directoryName); // 폴더 생성

                        if (fileName == string.Empty) // 파일 이름이 없으면 Pass
                            continue;

                        using (FileStream streamWriter = File.Create((unZipTargetFolderPath + theEntry.Name))) // 파일 스트림 생성.(파일생성)
                        {
                            byte[] data = new byte[2048];

                            while (true) // 파일 복사
                            {
                                int size = zipInputStream.Read(data, 0, data.Length);

                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }

                            streamWriter.Close(); // 파일스트림 종료
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.Unzip : Fail to ZipInputStream", e);
                }
                finally
                {
                    zipInputStream.Close(); // ZIP 파일 스트림 종료
                }
            }

            if (isDeleteZipFile) // ZIP파일 삭제를 원할 경우 파일 삭제
            {
                try
                {
                    File.Delete(zipFilePath);
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.Unzip : Fail to delete zip file", e);
                }
            }

            return true;
        }

        /// <summary>
        /// tgz 파일 압축 풀기
        /// </summary>
        /// <param name="fileName">tgz 파일명</param>
        /// <param name="savePath">저장할 파일 위치</param>
        /// <returns>압축 풀기 성공 여부 </returns>
        public static bool UnTgz(string fileName, string savePath)
        {
            if (!File.Exists(fileName))
                return false;

            using (SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(fileName))
            {
                try
                {
                    sevenZipExtractor.EventSynchronization = EventSynchronizationStrategy.AlwaysSynchronous;
                    savePath = savePath.Replace(@"\\", @"\");
                    sevenZipExtractor.ExtractionFinished +=
                        (sender, e) =>
                        {
                            try
                            {
                                List<string> fileList = GenerateFileList(savePath);
                                foreach (string file in fileList)
                                {
                                    var fi = new FileInfo(file);
                                    if (fi.Extension.ToUpper() == ".TAR")
                                    {
                                        UnTar(file);
                                        fi.Delete();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new AggregateException("FileCompression.ExtractionFinishedHandler : Fail sevenZipExtractor", ex);
                            }
                        };

                    sevenZipExtractor.ExtractArchive(savePath);
                }
                catch (Exception e)
                {
                    throw new AggregateException("FileCompression.UnTgz : Fail sevenZipExtractor", e);
                }
            }

            return true;
        }

        /// <summary>
        /// tar 파일 압축 해제
        /// </summary>
        /// <param name="TarName">tar 파일명</param>
        public static void UnTar(string TarName)
        {
            try
            {
                FileInfo fi = new FileInfo(TarName);

                using (TarInputStream s = new TarInputStream(File.OpenRead(TarName)))
                {
                    TarEntry theEntry;

                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string FullName = string.Format("{0}\\{1}", fi.DirectoryName, theEntry.Name);
                        string DirName = Path.GetDirectoryName(FullName);
                        string FileName = Path.GetFileName(FullName);

                        if (!Directory.Exists(DirName)) Directory.CreateDirectory(DirName);

                        if (FileName != string.Empty)
                        {
                            FileStream SW = File.Create(FullName);

                            int Size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                Size = s.Read(data, 0, data.Length);
                                if (Size > 0) SW.Write(data, 0, Size);
                                else break;
                            }
                            SW.Close();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new AggregateException("FileCompression.UnTar : Fail untar", e);
            }
        }

        public static bool UnCabExpData(
            string eqName,
            string[] disk,
            string uDirPath,
            string[] ppidSplit,
            string ppid
            )
        {
            try
            {
                string rcpName = ppidSplit[0].Remove(0, 5);

                System.Diagnostics.ProcessStartInfo proInfo = new System.Diagnostics.ProcessStartInfo();

                System.Diagnostics.Process pro = new System.Diagnostics.Process();

                proInfo.FileName = @"cmd.exe";

                proInfo.CreateNoWindow = false;
                proInfo.UseShellExecute = false;

                proInfo.RedirectStandardOutput = true;
                proInfo.RedirectStandardInput = true;
                proInfo.RedirectStandardError = true;

                pro.StartInfo = proInfo;

                pro.Start();
                pro.StandardInput.Write(@"" + disk[0] + ":" + Environment.NewLine);
                pro.StandardInput.Write(@"cd " + uDirPath + "" + Environment.NewLine);
                pro.StandardInput.Write(@"md " + rcpName + Environment.NewLine);
                pro.StandardInput.Write(@"expand -R " + ppid + " " + rcpName + "\\" + Environment.NewLine);
                pro.StandardInput.Close();

                string resultValue = pro.StandardOutput.ReadToEnd();
                pro.WaitForExit();
                pro.Close();


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool UnCabExpData(
            string eqName,
            string file,
            string expPath
            )
        {
            System.Diagnostics.ProcessStartInfo proInfo = null;
            System.Diagnostics.Process pro = null;

            try
            {
                proInfo = new System.Diagnostics.ProcessStartInfo();
                pro = new System.Diagnostics.Process();

                // --

                proInfo.FileName = @"cmd.exe";

                // --

                proInfo.CreateNoWindow = false;
                proInfo.UseShellExecute = false;
                // --
                proInfo.RedirectStandardOutput = true;
                proInfo.RedirectStandardInput = true;
                proInfo.RedirectStandardError = true;

                // --

                pro.StartInfo = proInfo;

                // --

                pro.Start();
                pro.StandardInput.Write(@"" + Path.GetPathRoot(file) + ":" + Environment.NewLine);
                pro.StandardInput.Write(@"cd " + Path.GetDirectoryName(file) + "" + Environment.NewLine);
                pro.StandardInput.Write(@"md " + expPath + Environment.NewLine);
                pro.StandardInput.Write(@"expand -R " + Path.GetFileName(file) + " " + expPath + "\\" + Environment.NewLine);
                pro.StandardInput.Close();

                string resultValue = pro.StandardOutput.ReadToEnd();
                pro.WaitForExit();
                pro.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
