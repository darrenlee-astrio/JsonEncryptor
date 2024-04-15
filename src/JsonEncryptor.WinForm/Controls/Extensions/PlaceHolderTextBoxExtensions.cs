namespace JsonEncryptor.WinForm.Controls.Extensions;

public static class PlaceHolderTextBoxExtensions
{
    public static void UpdateText(this PlaceHolderTextBox textBox, string text)
    {
        textBox.Focus();
        textBox.Text = text;
    }

    public static void UpdateTextIfNotNullOrEmpty(this PlaceHolderTextBox textBox, string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        textBox.Focus();
        textBox.Text = text;
    }
}
