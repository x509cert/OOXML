# OOXML

Small .NET console tool that reads encryption metadata from an encrypted Office Open XML document.

The tool opens the document as an OLE compound file, reads the `EncryptionInfo` stream, parses the XML encryption descriptor, and prints the `keyData` and password `encryptedKey` attributes. This is useful for inspecting the cryptographic parameters stored in encrypted `.docx`, `.xlsx`, and similar OOXML files without decrypting the document contents.

Format details are based on the Microsoft Office file format specification:

https://learn.microsoft.com/en-us/openspecs/office_file_formats/ms-offcrypto/3c34d72a-1a61-4b52-a893-196f9157f083?redirectedfrom=MSDN

## Usage

```powershell
dotnet run -- path\to\encrypted.docx [or pptx, xslx etc.]
```

The program currently supports Agile Encryption metadata with version `4.4`.

Sample output:

```text
Key Data:
  saltSize                    : 16
  blockSize                   : 16
  keyBits                     : 256
  hashSize                    : 64
  cipherAlgorithm             : AES
  cipherChaining              : ChainingModeCBC
  hashAlgorithm               : SHA512
  saltValue (or IV)           : ktwu3iMe57cJ0LTDJHwf7g==

Password Encryptor:
  spinCount                   : 100000
  saltSize                    : 16
  blockSize                   : 16
  keyBits                     : 256
  hashSize                    : 64
  cipherAlgorithm             : AES
  cipherChaining              : ChainingModeCBC
  hashAlgorithm               : SHA512
  saltValue (or IV)           : p8MRLOBj/a5MOYG1tvfA/Q==
  encryptedVerifierHashInput  : D8jQbWvjyPK8uWa1Z/0hiw==
  encryptedVerifierHashValue  : ikHKt4eXSu9Pemxqfen8zPDq/Z8WnJlfv056VKGe/ELkiIuv9EedAm54/QsfCIntPmakyTrTwfL+EoKVqcelUw==
  encryptedKeyValue           : p0lVCnKVdfJnf/s0HkVK/KrmggxSpYbeH69p8MBvahw=
```

## Dependency

This project uses [OpenMcdf](https://www.nuget.org/packages/OpenMcdf/) to read the OLE compound file structure used by encrypted OOXML packages.

To add the package to a .NET project:

```powershell
dotnet add package OpenMcdf
```

To match this project exactly:

```powershell
dotnet add package OpenMcdf --version 3.1.4
```
