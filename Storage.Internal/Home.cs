using System;
using Ivy;
using Ivy.Shared;
using Ivy.Views;

namespace Storage.Internal;

[App(icon: Icons.House, title: "Home")]
public class Home : ViewBase
{
    public override object? Build()
    {
        var navigator = UseNavigation();

        return new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Hello!").Large()
                    | Text.P("This is a demo app")
            ).Title("Home card");
    }
}

