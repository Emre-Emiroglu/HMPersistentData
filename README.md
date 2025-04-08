# HMPersistentData
HMPersistentData is a flexible and extendable data persistence system designed for Unity applications. It provides a simple API for saving, loading, and deleting data using various serialization strategies, including encrypted and unencrypted JSON.

## Features
HMPersistentData offers a modular and secure data persistence solution:
* Save / Load / Delete / DeleteAll: Save and retrieve any serializable object type, or delete them individually or all at once.
* Serializer Selection: Choose between default JSON and AES-encrypted JSON serializers.
* Custom Key & IV Support: For encrypted serialization, specify your own key and IV values to secure data.
* Editor Utility Tools: Easily manage saved data through an intuitive Editor window. Includes opening save file location, clearing all saved data, and changing the serializer settings at runtime.
* Static Utility Access: Use PersistentDataUtilities to perform all operations statically without manual instantiation.

## Getting Started
Install via UPM with git URL

`https://github.com/Emre-Emiroglu/HMPersistentData.git`

Clone the repository
```bash
  git clone https://github.com/Emre-Emiroglu/HMPersistentData.git
```
This project is developed using Unity version 6000.0.42f1.

## Usage
* Saving Data:
    ```csharp
    MyData data = new MyData();
    PersistentDataUtilities.Save("saveKey", data);
    ```
* Loading Data:
    ```csharp
    MyData loaded = PersistentDataUtilities.Load<MyData>("saveKey");
    ```
* Deleting Data:
    ```csharp
    PersistentDataUtilities.Delete("saveKey");
    ```
* Deleting All Data:
    ```csharp
    PersistentDataUtilities.DeleteAll();
    ```
* Changing Serializer Type (Editor Only): Use the Editor window: Window > HMPersistentData > Settings
  * Select between JsonSerializer and EncryptedJsonSerializer.
  * For EncryptedJsonSerializer, input your custom Key and IV.

* Error Handling:
  * If deserialization fails due to incompatible types or corrupted data, an exception is thrown.
  * If loading a nonexistent key, a default value is returned.
  * Encrypted serializer requires both Key and IV to be valid 16-character Base64 strings.

## Dependencies
* Newtonsoft Json

## Acknowledgments
Special thanks to the Unity community for their invaluable resources and tools.

For more information, visit the GitHub repository.