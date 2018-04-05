// A set of functions that build up state
// which eventually will be converted to an SVG file
// these functions will be invoked by the "ts"\"js" file that wishes to convert to svg
// in VS Code, the task runner will RUN the above file,
// which will in turn invoke conversion methods.

class SvgBuilder
{
    string body = "";

    function Save(string fileName) {
        saveToDisk(text, fileName);
    }

    function EnterText(string text) {
        body = text;
    }
}
