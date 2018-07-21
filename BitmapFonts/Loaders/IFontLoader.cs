namespace BitmapFonts.Loaders
{
    public interface IFontLoader
    {
        FontFile ReadFile(string filename);
    }
}