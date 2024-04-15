using System.ComponentModel;

namespace JsonEncryptor.WinForm.Controls;

[ToolboxItem(true)]
public class PlaceHolderTextBox : TextBox
{
    private bool isPlaceHolder = true;
    private string placeHolderText = string.Empty;

    public string PlaceHolderText
    {
        get { return placeHolderText; }
        set
        {
            placeHolderText = value;
            SetPlaceHolder();
        }
    }

    public new string Text
    {
        get => isPlaceHolder ? string.Empty : base.Text;
        set => base.Text = value;
    }

    public PlaceHolderTextBox()
    {
        GotFocus += TextBox_GotFocus;
        LostFocus += TextBox_LostFocus;
    }

    private void TextBox_LostFocus(object? sender, EventArgs e)
    {
        SetPlaceHolder();
    }

    private void SetPlaceHolder()
    {
        if (string.IsNullOrEmpty(base.Text))
        {
            base.Text = PlaceHolderText;
            ForeColor = Color.Gray;
            Font = new Font(Font, FontStyle.Italic);
            isPlaceHolder = true;
        }
    }

    private void TextBox_GotFocus(object? sender, EventArgs e)
    {
        RemovePlaceHolder();
    }

    private void RemovePlaceHolder()
    {
        if (isPlaceHolder)
        {
            base.Text = string.Empty;
            ForeColor = SystemColors.WindowText;
            Font = new Font(Font, FontStyle.Regular);
            isPlaceHolder = false;
        }
    }
}