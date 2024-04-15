using Application.Abstractions;
using Newtonsoft.Json;
using Serilog;
using WinForm.Constants;
using WinForm.Controls.Extensions;
using WinForm.Models;

namespace WinForm.Forms;

public partial class MainForm : Form
{
    private const int AesKeySize = 256;
    private readonly ILogger _logger = Log.ForContext<MainForm>();
    private readonly IJsonEncryptor _jsonEncryptor;
    private readonly IAesEncryptionService _aesEncryptionService;

    public MainForm(
        IAesEncryptionService aesEncryptionService,
        IJsonEncryptor jsonEncryptor)
    {
        InitializeComponent();

        _jsonEncryptor = jsonEncryptor;
        _aesEncryptionService = aesEncryptionService;
    }

    #region Form Events
    private void MainForm_Shown(object sender, EventArgs e)
    {
        if (!File.Exists(Paths.UiStateFilePath))
        {
            return;
        }

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

            textBoxBefore.Text = File.ReadAllText(openFileDialog.FileName);
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
        var output = ProcessFile(textBoxFilePath.Text, textBoxJsonKeys.Text, textBoxAesKey.Text, textBoxAesIv.Text, UserAction.Encrypt, out string input);

        if (string.IsNullOrEmpty(output))
        {
            return;
        }

        textBoxBefore.Text = input;
        textBoxAfter.Text = output;
    }

    private void buttonDecrypt_Click(object sender, EventArgs e)
    {
        var output = ProcessFile(textBoxFilePath.Text, textBoxJsonKeys.Text, textBoxAesKey.Text, textBoxAesIv.Text, UserAction.Decrypt, out string input);

        if (string.IsNullOrEmpty(output))
        {
            return;
        }

        textBoxBefore.Text = input;
        textBoxAfter.Text = output;
    }
    #endregion

    #region ToolStripItems Click Events
    private void saveUiStateToolStripMenuItem_Click(object sender, EventArgs e) => SaveUiState();
    private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e) => textBoxLog.ClearLogs();
    private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();
    #endregion

    #region Private Methods
    private string ProcessFile(string filePath, string jsonKeys, string aesKey, string aesIv, UserAction action, out string input)
    {
        input = string.Empty;

        if (string.IsNullOrEmpty(filePath))
        {
            _logger.Warning("Please specify a path to a json file.");
            return string.Empty;
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

        input = File.ReadAllText(filePath);

        if (string.IsNullOrEmpty(input))
        {
            _logger.Warning("Unable to encrypt and/or decrypt a empty file.");
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

    #endregion
}
