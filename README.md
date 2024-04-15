# JsonEncryptor
An easy-to-use tool for encrypting specific elements within a JSON file using AES encryption. The inspiration for this project stemmed from various situations where storing sensitive information in raw form within app settings or environment variables wasn't ideal, and access to dedicated secrets stores wasn't available.

![JsonEncryptor](assets/JsonEncryptor.png)

## Features
- Load your JSON file
- Use your own AES Key and Iv (or generate a new pair)
- Select which value(s) to encrypt/decrypt by stating the key(s)
- Load your JSON file and compare before and after
- Save the UI state on a separate JSON file so that you can come back to where you left off

## License
This project is licensed under the [MIT License](LICENSE).
