using JsonEncryptor.Application.Abstractions;
using JsonEncryptor.WinForm.Constants;
using JsonEncryptor.WinForm.Controls.Extensions;
using JsonEncryptor.WinForm.Models;
using Newtonsoft.Json;
using Serilog;

namespace JsonEncryptor.WinForm.Forms;

public partial class MainForm : Form
{
    private const int AesKeySize = 256;
    private readonly ILogger _logger = Log.ForContext<MainForm>();
    private readonly IJsonEncryption _jsonEncryptor;
    private readonly IJsonService _jsonService;
    private readonly IAesEncryptionService _aesEncryptionService;

    public MainForm(
        IAesEncryptionService aesEncryptionService,
        IJsonEncryption jsonEncryptor,
        IJsonService jsonService)
    {
        InitializeComponent();

        _jsonEncryptor = jsonEncryptor;
        _jsonService = jsonService;
        _aesEncryptionService = aesEncryptionService;
    }

    #region Form Events
    private void MainForm_Shown(object sender, EventArgs e)
    {
        if (File.Exists(Paths.UiStateFilePath))
        {
            LoadUiState();
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Log.CloseAndFlush();
    }
    #endregion

    #region UI Button Click Events
    private void buttonBrowseFile_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JSON Files (*.json)|*.json";
        openFileDialog.RestoreDirectory = true;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxFilePath.UpdateText(openFileDialog.FileName);

            textBoxBefore.Text = _jsonService.PrettifyJson(File.ReadAllText(openFileDialog.FileName));
        }
    }

    private void buttonGenerateAesKey_Click(object sender, EventArgs e)
    {
        textBoxAesKey.UpdateText(_aesEncryptionService.GenerateKey(AesKeySize));
    }

    private void buttonGenerateAesIV_Click(object sender, EventArgs e)
    {
        textBoxAesIv.UpdateText(_aesEncryptionService.GenerateIv());
    }

    private void buttonEncrypt_Click(object sender, EventArgs e)
    {
        textBoxBefore.Clear();
        textBoxAfter.Clear();

        textBoxBefore.Text = _jsonService.PrettifyJson(File.ReadAllText(textBoxFilePath.Text));

        if (MessageBox.Show(
                text: "Are you sure you want to encrypt the file? The changes will be saved to the file directly.",
                caption: "Confirmation",
                MessageBoxButtons.YesNo) == DialogResult.No)
        {
            return;
        }

        var output = ProcessFile(textBoxFilePath.Text, textBoxJsonKeys.Text, textBoxAesKey.Text, textBoxAesIv.Text, UserAction.Encrypt);

        if (string.IsNullOrEmpty(output))
        {
            return;
        }

        textBoxAfter.Text = output;
    }

    private void buttonDecrypt_Click(object sender, EventArgs e)
    {
        textBoxBefore.Clear();
        textBoxAfter.Clear();

        textBoxBefore.Text = _jsonService.PrettifyJson(File.ReadAllText(textBoxFilePath.Text));

        if (MessageBox.Show(
                text: "Are you sure you want to decrypt the file? The changes will be saved to the file directly.",
                caption: "Confirmation",
                MessageBoxButtons.YesNo) == DialogResult.No)
        {
            return;
        }

        var output = ProcessFile(textBoxFilePath.Text, textBoxJsonKeys.Text, textBoxAesKey.Text, textBoxAesIv.Text, UserAction.Decrypt);

        if (string.IsNullOrEmpty(output))
        {
            return;
        }

        textBoxAfter.Text = output;
    }
    #endregion

    #region ToolStripItems Click Events
    private void saveUiStateToolStripMenuItem_Click(object sender, EventArgs e) => SaveUiState();
    private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e) => textBoxLog.ClearLogs();
    private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();
    #endregion

    #region Private Methods
    private string ProcessFile(string filePath, string jsonKeys, string aesKey, string aesIv, UserAction action)
    {
        string input = string.Empty;

        if (string.IsNullOrEmpty(filePath))
        {
            _logger.Warning("Please specify a path to a json file.");
            return string.Empty;
        }
        else
        {
            input = _jsonService.PrettifyJson(File.ReadAllText(filePath));

            if (string.IsNullOrEmpty(input))
            {
                _logger.Warning("Unable to encrypt and/or decrypt a empty file.");
                return string.Empty;
            }
        }

        if (!File.Exists(filePath))
        {
            _logger.Warning("The file you have specified does not exist.");
            return string.Empty;
        }

        if (string.IsNullOrEmpty(jsonKeys))
        {
            _logger.Warning("Please specify the json keys you would like to encrypt and/or decrypt.");
            return string.Empty;
        }

        if (string.IsNullOrEmpty(aesKey))
        {
            _logger.Warning("Please enter an AES Key (256 Bits). Click 🔄 if you do not have any.");
            return string.Empty;
        }

        if (string.IsNullOrEmpty(aesIv))
        {
            _logger.Warning("Please enter an Initalization Vector (IV) . Click 🔄 if you do not have any.");
            return string.Empty;
        }

        List<string> jsonKeysList = jsonKeys.Split(';').ToList();

        if (jsonKeysList.Count <= 0)
        {
            _logger.Warning("Please specify the json keys you would like to encrypt and/or decrypt.");
            return string.Empty;
        }

        try
        {
            string output = action switch
            {
                UserAction.Encrypt => _jsonEncryptor.Encrypt(input, jsonKeysList, aesKey, AesKeySize, aesIv),
                UserAction.Decrypt => _jsonEncryptor.Decrypt(input, jsonKeysList, aesKey, AesKeySize, aesIv),
                _ => throw new InvalidOperationException("Invalid user action")
            };

            output = _jsonService.PrettifyJson(output);

            File.WriteAllText(filePath, output);

            return output;
        }
        catch (System.Security.Cryptography.CryptographicException)
        {
            _logger.Error("Unable to decrypt content due to incorrect AES Key and/or Iv.");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
        }

        return string.Empty;
    }

    private void SaveUiState()
    {
        try
        {
            var uiState = new UIState
            {
                FilePath = textBoxFilePath.Text,
                JsonKeys = textBoxJsonKeys.Text,
                AesKey = textBoxAesKey.Text,
                AesIv = textBoxAesIv.Text
            };

            Directory.CreateDirectory(Paths.ConfigurationDirectory);
            File.WriteAllText(Paths.UiStateFilePath, JsonConvert.SerializeObject(uiState, Formatting.Indented));

            MessageBox.Show("User interface state saved successfully.");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message, ex);
        }

    }

    private void LoadUiState()
    {
        var uiState = JsonConvert.DeserializeObject<UIState>(File.ReadAllText(Paths.UiStateFilePath));

        if (uiState == null)
        {
            return;
        }

        textBoxFilePath.UpdateTextIfNotNullOrEmpty(uiState.FilePath);
        textBoxJsonKeys.UpdateTextIfNotNullOrEmpty(uiState.JsonKeys);
        textBoxAesKey.UpdateTextIfNotNullOrEmpty(uiState.AesKey);
        textBoxAesIv.UpdateTextIfNotNullOrEmpty(uiState.AesIv);
        groupBoxControls.Focus();

        if (string.IsNullOrEmpty(textBoxFilePath.Text))
        {
            return;
        }

        if (!File.Exists(textBoxFilePath.Text))
        {
            _logger.Error($"File does not exist: {textBoxFilePath.Text}");
            return;
        }

        textBoxBefore.Text = File.ReadAllText(textBoxFilePath.Text);
    }
    #endregion
}
