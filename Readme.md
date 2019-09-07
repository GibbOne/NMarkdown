
# NMarkdown

It's a quite simple fluent library in **C#** to compose **programmatically** a mark down document.

## An example

This code builds this document without the example paragraph (otherwise it will produce an infinite recursion :-)).

``` c#
var doc = new Document("NMarkdown");
doc.Text("It's a quite simple fluent library in ").Bold("C#").Regular(" to compose ")
    .Bold("programmatically").Regular(" a mark down document.")
    .NewLine();

doc.AddHeader2("License");
doc.Text("NMarkdown is released under the ").Link("MIT license", "License.txt")
    .Regular(".").NewLine();
doc.Text("This license allow the use of NMarkdown in free and commercial applications and libraries without restrictions.")
    .NewLine();
```

## License

NMarkdown is released under the [MIT license](License.txt).
This license allow the use of NMarkdown in free and commercial applications and libraries without restrictions.
