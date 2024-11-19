namespace VozAmiga.Api.Utils.Database;

public record Page(uint Number, uint Size)
{
    public Page(int number, int size)
        : this((uint)number, (uint)size)
    { }

    public static Page Default => new(0u, 25u);
}
