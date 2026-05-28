using System.Text;
using System.Xml.Linq;
using OpenMcdf;

var path = string.Join(" ", args);
using var root = RootStorage.OpenRead(path);
using var ms = new MemoryStream();
root.OpenStream("EncryptionInfo").CopyTo(ms);
var data = ms.ToArray();

ushort major = BitConverter.ToUInt16(data, 0), minor = BitConverter.ToUInt16(data, 2);
if (major != 4 || minor != 4) { Console.Error.WriteLine($"Unsupported version {major}.{minor} (Office 2007?)"); return; }

var doc = XDocument.Parse(Encoding.UTF8.GetString(data, 8, data.Length - 8));
XNamespace ns  = "http://schemas.microsoft.com/office/2006/encryption";
XNamespace nsP = "http://schemas.microsoft.com/office/2006/keyEncryptor/password";
Print("Key Data",           doc.Root?.Element(ns + "keyData"));
Print("Password Encryptor", doc.Descendants(nsP + "encryptedKey").FirstOrDefault());

void Print(string label, XElement? el)
{
    Console.WriteLine($"\n{label}:");
    foreach (var a in el?.Attributes() ?? Enumerable.Empty<XAttribute>())
    {
        var display = a.Name.LocalName == "saltValue" ? "saltValue (or IV)" : a.Name.LocalName;
        Console.WriteLine($"  {display,-28}: {a.Value}");
    }
}