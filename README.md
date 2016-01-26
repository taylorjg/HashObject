
## Description

I wanted a way to generate an MD5 hash string for an arbitrary C# object with usage like this:

```C#
var obj = ...
var hash = ObjectHasher.ComputeHash(obj);
```

My implementation serialises the object to a stream using [Json.NET](http://www.newtonsoft.com/json)
and uses [`CryptoStream`](https://msdn.microsoft.com/en-us/library/system.security.cryptography.cryptostream.aspx)
and [`MD5`](https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5.aspx)
to generate the hash. The streams are composed as follows:

* [`JsonTextWriter`](http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonTextWriter.htm)
* [`StreamWriter`](https://msdn.microsoft.com/en-us/library/system.io.streamwriter.aspx)
* [`CryptoStream`](https://msdn.microsoft.com/en-us/library/system.security.cryptography.cryptostream.aspx)
* [`Stream.Null`](https://msdn.microsoft.com/en-us/library/system.io.stream.null.aspx)

I am only interested in the hash value. I don't need the encrypted data. Hence, I use the null stream as the
bottommost stream in the stack of composed streams. The null stream is:

> A Stream with no backing store.
>
> Use Null to redirect output to a stream that will not consume any operating system resources. When the methods of Stream that provide writing are invoked on Null, the call simply returns, and no data is written. Null also implements a Read method that returns zero without reading data.

I must confess that I wrote my own `NullStream` class (deriving from the abstract [Stream](https://msdn.microsoft.com/en-us/library/system.io.stream.aspx) class) until I spotted that the BCL provides [`Stream.Null`](https://msdn.microsoft.com/en-us/library/system.io.stream.null.aspx).

## Links

* [Calculate hash when writing to stream](http://stackoverflow.com/questions/4725507/calculate-hash-when-writing-to-stream)
* [Can Json.NET serialize / deserialize to / from a stream?](http://stackoverflow.com/questions/8157636/can-json-net-serialize-deserialize-to-from-a-stream)
