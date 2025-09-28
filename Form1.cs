using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using Octokit;

namespace BBModLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Tuple<string, string>> mods = new List<Tuple<string, string>>();

        DirectoryInfo modsDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Mods"));
        FileInfo? gameExe = null;

        string currentVersion = "1.0.0";

        private void launchInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Patches and launches the mod; doesn't affect the original game.", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void patchInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Patches the game; affects the original game.", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static async Task CheckForUpdates(string currentVersionString)
        {
            var client = new GitHubClient(new ProductHeaderValue("BBModLoader"));

            try
            {
                IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("2x4096", "BBModLoader");
                if (releases.Count == 0)
                    return;

                Release latestRelease = releases[0];

                if (latestRelease.Assets == null || latestRelease.Assets.Count == 0)
                    return;

                string latestTagName = latestRelease.TagName.TrimStart('v');
                Version latestVersion = new Version(latestTagName);
                Version currentVersion = new Version(currentVersionString);

                if (currentVersion.CompareTo(latestVersion) >= 0)
                    return;

                if (MessageBox.Show($"Your current version is outdated. Would you like to update?", "BBModLoader", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    return;

                Random random = new Random();

                string tempFileName = $"bbml-{random.Next()}.zip";
                FileInfo tempFile = new FileInfo(Path.Combine(Path.GetTempPath(), tempFileName));

                while (tempFile.Exists)
                {
                    tempFileName = $"bbml-{random.Next()}.zip";
                    tempFile = new FileInfo(Path.Combine(Path.GetTempPath(), tempFileName));
                }

                using (HttpClient httpClient = new HttpClient())
                using (var response = await httpClient.GetAsync(latestRelease.Assets[0].BrowserDownloadUrl))
                {
                    response.EnsureSuccessStatusCode();
                    await File.WriteAllBytesAsync(tempFile.FullName, await response.Content.ReadAsByteArrayAsync());
                }

                DirectoryInfo extractDir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), latestTagName));
                if (!extractDir.Exists)
                    extractDir.Create();

                ZipFile.ExtractToDirectory(tempFile.FullName, extractDir.FullName, true);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"An exception was caught: {exception.Message}. If this issue persists, please report the issue on the GitHub repository.", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (!modsDirectory.Exists)
            {
                modsDirectory.Create();
            }

            modBox.Items.Clear();

            foreach (DirectoryInfo modDir in modsDirectory.GetDirectories())
            {
                mods.Add(Tuple.Create(modDir.Name, modDir.FullName));
                modBox.Items.Add(modDir.Name);
            }

            await CheckForUpdates(currentVersion);
        }

        private bool RecursiveCopy(string sourceDir, string destinationDir, bool overwrite)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
            {
                MessageBox.Show("Failed to copy; the directory doesn't exist!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(tempPath, overwrite);
            }

            foreach (DirectoryInfo subDir in dirs)
            {
                string tempPath = Path.Combine(destinationDir, subDir.Name);
                RecursiveCopy(subDir.FullName, tempPath, overwrite);
            }

            return true;
        }

        private bool Patch(DirectoryInfo gameDir)
        {
            string? selectedMod = (string?)modBox.SelectedItem;
            if (selectedMod == null)
            {
                MessageBox.Show("Failed to patch; no mod is selected!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DirectoryInfo? modDir = null;
            foreach (Tuple<string, string> mod in mods)
            {
                if (mod.Item1 == selectedMod)
                {
                    modDir = new DirectoryInfo(mod.Item2);
                }
            }

            if (modDir == null || !modDir.Exists)
            {
                MessageBox.Show("Failed to patch; couldn't find selected mod!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (FileInfo file in modDir.GetFiles())
            {
                string newFilePath = Path.Combine(gameDir.FullName, file.Name);
                file.CopyTo(newFilePath, true);
            }

            foreach (DirectoryInfo dir in modDir.GetDirectories())
            {
                string? dirName = dir.Name;
                if (dirName == null)
                    continue;

                RecursiveCopy(dir.FullName, Path.Combine(gameDir.FullName, dirName), true);
            }

            return true;
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = 3;

            progressBar.Value = 0;
            progressLabel.Text = "Checking...";

            if (gameExe == null || !gameExe.Exists)
            {
                MessageBox.Show("Game path is invalid!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Value = 0;
                progressLabel.Text = "Waiting...";
                return;
            }

            progressBar.Value = 1;
            progressLabel.Text = "Patching...";

            Random random = new Random();
            string patchedDirName = $"bbmod-{random.Next()}";

            DirectoryInfo patchedDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), patchedDirName));
            while (patchedDir.Exists)
            {
                patchedDirName = $"bbmod-{random.Next()}";
                patchedDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), patchedDirName));
            }

            patchedDir.Create();

            DirectoryInfo? gameDir = gameExe.Directory;
            if (gameDir == null || !gameDir.Exists)
            {
                MessageBox.Show("Failed to get the game directory!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Value = 0;
                progressLabel.Text = "Waiting...";
                return;
            }

            foreach (FileInfo file in gameDir.GetFiles())
            {
                file.CopyTo(Path.Combine(patchedDir.FullName, file.Name), true);
            }

            foreach (DirectoryInfo directory in gameDir.GetDirectories())
            {
                string? dirName = directory.Name;
                if (dirName == null)
                    continue;

                RecursiveCopy(directory.FullName, Path.Combine(patchedDir.FullName, dirName), true);
            }

            if (!Patch(patchedDir))
            {
                progressBar.Value = 0;
                progressLabel.Text = "Waiting...";
                return;
            }

            progressBar.Value = 2;
            progressLabel.Text = "Launching...";

            string exePath = Path.Combine(patchedDir.FullName, gameExe.Name);

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = exePath;

            Process.Start(processStartInfo);

            progressBar.Value = 3;
            progressLabel.Text = "Launched!";
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = 2;

            progressBar.Value = 0;
            progressLabel.Text = "Checking...";

            if (gameExe == null || !gameExe.Exists)
            {
                MessageBox.Show("Game executable is invalid!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressLabel.Text = "Waiting...";
                return;
            }

            progressBar.Value = 1;
            progressLabel.Text = "Patching...";

            DirectoryInfo? gameDir = gameExe.Directory;
            if (gameDir == null || !gameDir.Exists)
            {
                MessageBox.Show("Failed to get the game directory!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressLabel.Text = "Waiting...";
                return;
            }

            if (!Patch(gameDir))
            {
                progressBar.Value = 0;
                progressLabel.Text = "Waiting...";
                return;
            }

            progressBar.Value = 2;
            progressLabel.Text = "Patched!";

            MessageBox.Show("Patched!", "BBModLoader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void browseDirButton_Click(object sender, EventArgs e)
        {
            if (browseGameDialog.ShowDialog() == DialogResult.OK)
            {
                gameExe = new FileInfo(browseGameDialog.FileName);
            }
        }
    }
}
